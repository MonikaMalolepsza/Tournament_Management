using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tournament_Management.Model
{
    [Serializable]
    public class Team : Participant
    {
        #region Attributes

        private List<Person> _list;
        private int _type;

        #endregion Attributes

        #region Properties

        public List<Person> List { get => _list; set => _list = value; }
        public int Type { get => _type; set => _type = value; }

        #endregion Properties

        #region Constructors

        public Team()
        {
            List = new List<Person>();
        }

        public Team(string name) : base(name)
        {
            List = new List<Person>();
        }

        public Team(string name, List<Person> team) : base(name)
        {
            List = team;
        }

        #endregion Constructors

        #region Methods

        public void NewMember(Person teilnehmer)
        {
            List.Add(teilnehmer);
        }

        public string OutputTeamInformation()
        {
            string res = $"Team: {Name}\r\n";
            foreach (Participant t in List)
            {
                res += t.GiveInfo();
            }
            return res;
        }

        public override void Update()
        {
            string updateTeam = $"UPDATE TEAM SET NAME = '{Name}', TYPE_ID = '{Type}' WHERE ID = '{Id}'";

            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand { Connection = con };
                cmd.CommandText = updateTeam;
                cmd.ExecuteNonQuery();
                SaveMembers();
            }
            catch (Exception e)
            {
            }
            finally
            {
                con.Close();
            }
        }

        public override void Put()
        {
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            con.Open();
            MySqlTransaction transaction = con.BeginTransaction();

            try
            {
                string insertParticipant = $"INSERT INTO TEAM (name, type_id) VALUES ('{Name}', '{Type}')";

                MySqlCommand cmd = new MySqlCommand()
                {
                    Connection = con,
                    Transaction = transaction
                };

                cmd.CommandText = insertParticipant;
                cmd.ExecuteNonQuery();
                Id = (int)cmd.LastInsertedId;

                if (List != null && List.Count > 0)
                {
                    foreach (Person p in List)
                    {
                        string insertMember = $"INSERT INTO TEAM_MEMBER (PERSON_ID, TEAM_ID) VALUES ('{p.Id}','{ Id}')";
                        cmd.CommandText = insertMember;
                        cmd.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
            finally
            {
                con.Close();
            }
        }

        public override void Delete()
        {
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            try
            {
                con.Open();

                string query = $"DELETE FROM TEAM WHERE ID = '{Id}'";
                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
            }
            finally
            {
                con.Close();
            }
        }

        private List<Person> GetMembers(int id)
        {
            List<Person> result = new List<Person>();
            Person p = null;
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            try
            {
                con.Open();
                string sql = "SELECT P.ID," +
                                " case " +
                                "when((SELECT 1 from TRAINER T where T.PERSON_ID = P.id) is not null) then 'Trainer' " +
                                "when((SELECT 1 from FOOTBALLPLAYER FP where FP.PERSON_ID = P.id) is not null) then 'Footballplayer' " +
                                "when((SELECT 1 from HANDBALLPLAYER HP where HP.PERSON_ID = P.id) is not null) then 'Handballplayer' " +
                                "when((SELECT 1 from BASKETBALLPLAYER BP where BP.PERSON_ID = P.id) is not null) then 'Basketballplayer' " +
                                "when((SELECT 1 from PHYSIO PH where PH.PERSON_ID = P.id) is not null) then 'Physio' " +
                                "when((SELECT 1 from REFEREE R where R.PERSON_ID = P.id) is not null) then 'Referee' " +
                                "end as Profession " +
                                "FROM PERSON P " +
                                "JOIN TEAM_MEMBER tm ON p.id = tm.person_id " +
                                "JOIN TEAM t ON tm.team_id = t.id " +
                                $"WHERE t.id = {id}";

                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    switch (reader.GetString("Profession"))
                    {
                        case "Footballplayer":
                            p = new FootballPlayer();
                            break;

                        case "Trainer":
                            p = new Trainer();
                            break;

                        case "Handballplayer":
                            p = new HandballPlayer();
                            break;

                        case "Basketballplayer":
                            p = new BasketballPlayer();
                            break;

                        case "Physio":
                            p = new Physio();
                            break;

                        case "Referee":
                            p = new Referee();
                            break;

                        default: break;
                    }
                    if (p != null)
                    {
                        p.Get((int)reader.GetInt64("ID"));
                        result.Add(p);
                    }
                    p = null;
                }
                reader.Close();
            }
            catch (Exception e)
            {
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public override void Get(int id)
        {
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            try
            {
                con.Open();
                string query = $"SELECT * FROM Team where ID = {id}";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Id = reader.GetInt32("id");
                    Name = reader.GetString("name");
                    Type = reader.GetInt32("type_id");
                }

                reader.Close();
                List = GetMembers(id);
            }
            catch (Exception e)
            {
            }
            finally
            {
                con.Close();
            }
        }

        private void SaveMembers()
        {
            List<Person> oldMembers = GetMembers(Id);

            //TODO:Hier funktioniert etwas mit dem Vergleichen noch nicht so ganz
            List<Person> membersToRemove = oldMembers.Except(List).ToList();
            List<Person> membersToAdd = List.Except(oldMembers).ToList();

            string deleteSql = $"DELETE FROM TEAM_MEMBER WHERE TEAM_ID = '{Id}' AND PERSON_ID IN ('{string.Join("', '", membersToRemove.Select(x => x.Id))}')";

            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand { Connection = con };
                cmd.CommandText = deleteSql;
                cmd.ExecuteNonQuery();

                foreach (Person p in membersToAdd)
                {
                    string insertSql = $"INSERT INTO TEAM_MEMBER (TEAM_ID, PERSON_ID) VALUES ('{Id}', '{p.Id}')";
                    cmd.CommandText = insertSql;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                con.Close();
            }
        }

        #endregion Methods
    }
}