using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tournament_Management.Model
{
    public class Tournament
    {
        #region Attributes

        private List<Game> _games;
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

        #endregion Properties

        #region Constructors

        public Tournament()
        {
            this.Id = 0;
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
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            con.Open();
            MySqlTransaction transaction = con.BeginTransaction();

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
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            con.Open();
            MySqlTransaction transaction = con.BeginTransaction();

            try
            {
                con.Open();

                string query = $"INSERT INTO Tournament (type_id, name, active) VALUES ('{Type}', '{Name}', '{Active}')";
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

        public void Delete()
        {
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

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
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            try
            {
                con.Open();
                string query = $"SELECT * FROM Tournament WHERE G.ID = '{id}'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                Score scoreSet = new Score();
                while (reader.Read())
                {
                    Type = reader.GetInt32("type_id");
                    Name = reader.GetString("name");
                    Active = reader.GetBoolean("active");
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

        public List<Game> GetAllGames(int id)
        {
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            List<Game> result = new List<Game>();
            Game g = new Game();
            try
            {
                /*
                 TODO: TEST THIS!!
                 */

                con.Open();
                string query = $"SELECT ID FROM GAME G WHERE G.TOURNAMENT_ID = '{Id}'";

                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                Score scoreSet = new Score();
                while (reader.Read())
                {
                    g.Get((int)reader.GetInt64("ID"));
                    result.Add(g);
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

        #endregion Methods
    }
}