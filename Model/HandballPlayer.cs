using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tournament_Management.Helper;

namespace Tournament_Management.Model
{
    [Serializable]
    public class HandballPlayer : Person
    {
        #region Attributes

        private int _goals;
        private double _speed;
        private int _type;
        private string _position;

        #endregion Attributes

        #region Properties

        public int Goals { get => _goals; set => _goals = value; }
        public string Position { get => _position; set => _position = value; }
        public double Speed { get => _speed; set => _speed = value; }
        public int Type { get => _type; set => _type = value; }

        #endregion Properties

        #region Constructors

        public HandballPlayer()
        {
            this.Goals = 0;
            this.Speed = 0;
            this.Type = 0;
            this.Position = "";
        }

        #endregion Constructors

        #region Methods

        public override void Update()
        {
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

            con.Open();
            MySqlTransaction transaction = con.BeginTransaction();

            try
            {
                string updatePerson = $"UPDATE PERSON SET NAME='{Name}', SURNAME='{Surname}', ACTIVE='{Active}', AGE='{Age}' WHERE ID='{Id}'";
                string updateHandballplayer = $"UPDATE HANDBALLPLAYER SET position='{Position}', goals='{Goals}', speed='{Speed}', type_id='2' WHERE PERSON_ID='{Id}'";

                MySqlCommand cmd = new MySqlCommand()
                {
                    Connection = con,
                    Transaction = transaction
                };

                cmd.CommandText = updatePerson;
                cmd.ExecuteNonQuery();
                cmd.CommandText = updateHandballplayer;
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
                string insertParticipant = $"INSERT INTO PERSON P (name, surname, age, active) VALUES ('{Name}', '{Surname}', '{Age}', '{Active}')";

                MySqlCommand cmd = new MySqlCommand()
                {
                    Connection = con,
                    Transaction = transaction
                };

                /*

                `id` INT(11) NOT NULL AUTO_INCREMENT,
                `position` VARCHAR(100) NULL DEFAULT NULL,
                `speed` INT NULL,
                `goals` INT NULL,
                `type_id` INT(11) NULL DEFAULT NULL,
                `person_id` INT(11) NULL DEFAULT NULL,
                `team_id` INT(11) NULL DEFAULT NULL,

                 */
                cmd.CommandText = insertParticipant;
                cmd.ExecuteNonQuery();
                int person_id = (int)cmd.LastInsertedId;
                string insertPlayer = $"INSERT INTO HANDBALLPLAYER (goals, speed, type_id, person_id, team_id, position) VALUES('{Goals}','{Speed}', '2', '{person_id}', '1', '{Position}')";
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
                string query = $"SELECT * FROM PERSON P JOIN HANDBALLPLAYER HP ON P.ID = HP.PERSON_ID WHERE P.ID = '{id}'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Id = reader.GetInt32("id");
                    Name = reader.GetString("name");
                    Type = reader.GetInt32("type_id");
                    Surname = reader.GetString("surname");
                    Goals = reader.GetInt32("goals");
                    Speed = reader.GetDouble("speed");
                    Active = reader.GetBoolean("active");
                    Age = reader.GetInt32("age");
                    Position = reader.GetString("position");
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

        #endregion Methods

    }
}