using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Tournament_Management.Model
{
    public class Team : Participant
    {
        #region Attributes

        private List<Participant> _list;
        private string _foundingDate;
        private int _type;

        #endregion

        #region Properties

        public List<Participant> List { get => _list; set => _list = value; }
        public string FoundingDate { get => _foundingDate; set => _foundingDate = value; }
        public int Type { get => _type; set => _type = value; }

        #endregion

        #region Constructors

        public Team()
        {
            List = new List<Participant>();
        }

        public Team(string name) : base(name)
        {
            List = new List<Participant>();
        }

        public Team(string name, List<Participant> team) : base(name)
        {
            List = team;
        }



        #endregion

        #region Methods

        public void NewMember(Participant teilnehmer)
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
            throw new NotImplementedException();
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
                int team_id = (int)cmd.LastInsertedId;

                //if (List.Count > 0)
                //{
                //    forEach(Participant p in List)
                //    {
                //        string insertMembers = $"INSERT INTO TEAM_MEMBER (person_id, team_id) VALUES('{p.id}', '{team_id}', '1')";
                //        cmd.CommandText = insertMembers;
                //        cmd.ExecuteNonQuery();
                //    }

                //  }


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

        private void GetMembers(int id)
        {
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
                        List.Add(p);
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
                GetMembers(id);
            }
            catch (Exception e)
            {

            }
            finally
            {
                con.Close();
            }
        }
        #endregion
    }
}
