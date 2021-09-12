using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tournament_Management.Helper;

namespace Tournament_Management.Model
{
    [Serializable]
    public class Tournament
    {
        #region Attributes

        private List<Game> _games;
        private List<Team> _teams;
        private int _type;
        private int _id;
        private string _name;
        private bool _active;

        #endregion Attributes

        #region Properties

        public int Id { get => _id; set => _id = value; }
        public int Type { get => _type; set => _type = value; }
        public string Name { get => _name; set => _name = value; }
        public bool Active { get => _active; set => _active = value; }
        public List<Game> Games { get => _games; set => _games = value; }
        public List<Team> Teams { get => _teams; set => _teams = value; }

        #endregion Properties

        #region Constructors

        public Tournament()
        {
            this.Id = -1;
            this.Name = "";
            this.Active = false;
            this.Type = 0;
            this.Games = new List<Game>();
        }

        public Tournament(int id, int type, string name, bool active, List<Game> games)
        {
            this.Id = id;
            this.Name = name;
            this.Active = active;
            this.Type = type;
            this.Games = games;
        }

        #endregion Constructors

        #region Methods

        public void Update()
        {
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

            /*
                          `id` INT(11) NOT NULL AUTO_INCREMENT,
                          `type_id` INT(11) NULL DEFAULT NULL,
                          `name` VARCHAR(50) NULL DEFAULT NULL,
                          `start_date` DATE NULL DEFAULT NULL,
                          `end_date` DATE NULL DEFAULT NULL,
                          `active` TINYINT(1) NULL,
             */

            try
            {
                con.Open();

                string query = $"UPDATE Tournament SET type_id='{Type}', name='{Name}', active='{Active}' WHERE ID='{Id}'";
                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.ExecuteNonQuery();
                SaveMembers();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
        }

        private void SaveMembers()
        {
            List<Team> oldMembers = GetAllTeams(Id);

            List<Team> membersToRemove = oldMembers.Except(Teams).ToList();
            List<Team> membersToAdd = Teams.Except(oldMembers).ToList();

            string deleteSql = $"DELETE FROM TOURNAMENT_PARTICIPANTS WHERE TEAM_ID = '{Id}' AND TOURNAMENT_ID IN ('{string.Join("', '", membersToRemove.Select(x => x.Id))}')";

            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand { Connection = con };
                cmd.CommandText = deleteSql;
                cmd.ExecuteNonQuery();

                foreach (Team t in membersToAdd)
                {
                    string insertSql = $"INSERT INTO TOURNAMENT_PARTICIPANTS (TEAM_ID, TOURNAMENT_ID) VALUES ('{Id}', '{t.Id}')";
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

        public void Put()
        {
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

            try
            {
                con.Open();

                string query = $"INSERT INTO Tournament (type_id, name, active) VALUES ('{Type}', '{Name}', '{Active}')";
                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.ExecuteNonQuery();
                Id = (int)cmd.LastInsertedId;
            }
            catch (Exception e)
            {
            }
            finally
            {
                con.Close();
            }
        }

        public void Delete()
        {
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

            try
            {
                con.Open();

                string query = $"DELETE FROM TOURNAMENT WHERE ID = '{Id}'";
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

        public void Get(int id)
        {
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

            try
            {
                con.Open();
                string query = $"SELECT * FROM Tournament WHERE ID = '{id}'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Id = reader.GetInt32("id");
                    Type = reader.GetInt32("type_id");
                    Name = reader.GetString("name");
                    Active = reader.GetBoolean("active");
                }
                Games = GetAllGames(id);
                Teams = GetAllTeams(id);
                reader.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Game> GetAllGames(int id)
        {
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

            List<Game> result = new List<Game>();

            try
            {
                /*
                 TODO: TEST THIS!!
                 */

                con.Open();
                string query = $"SELECT ID FROM GAME G WHERE G.TOURNAMENT_ID = '{id}'";

                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                Score scoreSet = new Score();
                while (reader.Read())
                {
                    Game g = new Game();
                    g.Get((int)reader.GetInt64("ID"));
                    result.Add(g);
                }

                reader.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public List<Team> GetAllTeams(int id)
        {
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

            List<Team> result = new List<Team>();
            try
            {
                /*
                 TODO: TEST THIS!!
                 */

                con.Open();
                string query = $"SELECT team_id FROM TOURNAMENT_PARTICIPANTS TP WHERE TP.tournament_id = '{id}'";

                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Team t = new Team();
                    t.Get((int)reader.GetInt64("team_id"));
                    result.Add(t);
                }

                reader.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        #endregion Methods
    }
}