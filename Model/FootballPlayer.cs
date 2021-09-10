using System;
using MySql.Data.MySqlClient;
using Tournament_Management.Helper;

namespace Tournament_Management.Model
{
    [Serializable]
    public class FootballPlayer : Person
    {
        #region Attributes

        private int _goals;
        private int _type;
        private double _speed;

        #endregion

        #region Properties

        public int Goals { get => _goals; set => _goals = value; }
        public double Speed { get => _speed; set => _speed = value; }
        public int Type { get => _type; set => _type = value; }

        #endregion

        #region Constructors

        public FootballPlayer()
        {
            this.Goals = 0;
            this.Speed = 0;
            this.Type = 0;
        }

        #endregion

        #region Methods

        public override void Update()
        {
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

            con.Open();
            MySqlTransaction transaction = con.BeginTransaction();

            try
            {
                string updatePlayer = $"UPDATE PERSON SET name='{Name}', age='{Age}', surname='{Surname}', active='{Active}' WHERE  ID='{Id}'";
                string updateFootballPlayer = $"UPDATE FOOTBALLPLAYER SET goals='{Goals}', speed='{Speed}', type_id='1' WHERE PERSON_ID = '{Id}'";

                MySqlCommand cmd = new MySqlCommand()
                {
                    Connection = con,
                    Transaction = transaction
                };

                cmd.CommandText = updatePlayer;
                cmd.ExecuteNonQuery();
                cmd.CommandText = updateFootballPlayer;
                cmd.ExecuteNonQuery();

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

        public override void Put()
        {
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

            con.Open();
            MySqlTransaction transaction = con.BeginTransaction();

            try
            {

                string insertParticipant = $"INSERT INTO PERSON (name, surname, age, active) VALUES ('{Name}', '{Surname}', '{Age}', '{Active}')";


                MySqlCommand cmd = new MySqlCommand()
                {
                    Connection = con,
                    Transaction = transaction
                };

                /*
                 * 
                `id` INT(11) NOT NULL AUTO_INCREMENT,
                `goals` INT NULL,
                `speed` INT NULL,
                `type_id` INT(11) NULL DEFAULT NULL,
                `person_id` INT(11) NULL DEFAULT NULL,
                `team_id` INT(11) NULL DEFAULT NULL,

                 */

                cmd.CommandText = insertParticipant;
                cmd.ExecuteNonQuery();
                int person_id = (int)cmd.LastInsertedId;
                string insertPlayer = $"INSERT INTO FOOTBALLPLAYER (goals, speed, type_id, person_id, team_id) VALUES('{Goals}','{Speed}', '1', '{person_id}', '1')";
                cmd.CommandText = insertPlayer;
                cmd.ExecuteNonQuery();


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
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

            try
            {
                con.Open();

                string query = $"DELETE FROM PERSON WHERE ID = '{Id}'";
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

        public override void Get(int id)
        {
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

            try
            {
                con.Open();
                string query = $"SELECT * FROM PERSON P JOIN FOOTBALLPLAYER FP ON P.ID = FP.PERSON_ID WHERE P.ID = '{id}'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    Id = reader.GetInt32("id");
                    Name = reader.GetString("name");
                    Surname = reader.GetString("surname");
                    Goals = reader.GetInt32("goals");
                    Type = reader.GetInt32("type_id");
                    Speed = reader.GetDouble("speed");
                    Active = reader.GetBoolean("active");
                    Age = reader.GetInt32("age");
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

        #endregion


    }
}
