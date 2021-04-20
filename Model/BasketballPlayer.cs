using System;
using MySql.Data.MySqlClient;

namespace Tournament_Management.Model
{
    public class BasketballPlayer : Person
    {
        #region Attributes

        private int _fieldGoals;
        private int _height;
        private int _type;
        private double _speed;

        #endregion

        #region Properties

        public int Goals { get => _fieldGoals; set => _fieldGoals = value; }
        public int Height { get => _height; set => _height = value; }
        public double Speed { get => _speed; set => _speed = value; }
        public int Type { get => _type; set => _type = value; }

        #endregion

        #region Constructors

        public BasketballPlayer()
        {
            this.Goals = 0;
            this.Speed = 0;
            this.Type = 0;
            this.Height = 0;
        }

        #endregion


        #region Methods

        public override void Update()
        {
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            con.Open();
            MySqlTransaction transaction = con.BeginTransaction();

            try
            {
                /*
                INSERT INTO BASKETBALLPLAYER 
                (field_goal, speed, type_id, person_id, team_id, height) VALUES('{Goals}','{Speed}','1','{person_id}','1','{Height}')
                 
                INSERT INTO PERSON 
                (name, surname, age, active) VALUES ('{Name}', '{Surname}', '{Age}', '{Active}')

                `id` INT(11) NOT NULL AUTO_INCREMENT,
                `name` VARCHAR(50) NULL DEFAULT NULL,
                `surname` VARCHAR(50) NULL DEFAULT NULL,
                `age` INT NOT NULL,
                `active` TINYINT(1) NULL

                 */

                string updateBasketballplayer = $"UPDATE BASKETBALLPLAYER SET field_goal='{Goals}', speed='{Speed}', height='{Height}', type={Type}  WHERE  PERSON_ID = '{Id}'";
                string updatePlayer = $"UPDATE PERSON SET name='{Name}', age='{Age}' surname='{Surname}', active='{Active}' WHERE ID ='{Id}'";


                MySqlCommand cmd = new MySqlCommand()
                {
                    Connection = con,
                    Transaction = transaction
                };

                cmd.CommandText = updatePlayer;
                cmd.ExecuteNonQuery();
                cmd.CommandText = updateBasketballplayer;
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
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

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

                `id` INT(11) NOT NULL AUTO_INCREMENT,
                `height` INT(3) NULL DEFAULT NULL,
                `field_goal` INT NULL,
                `speed` INT NULL,
                `type_id` INT(11) NULL DEFAULT NULL,
                `person_id` INT(11) NULL DEFAULT NULL,
                `team_id` INT(11) NULL DEFAULT NULL,

                 */

                cmd.CommandText = insertParticipant;
                cmd.ExecuteNonQuery();
                int person_id = (int)cmd.LastInsertedId;
                string insertPlayer = $"INSERT INTO BASKETBALLPLAYER (field_goal, speed, type_id, person_id, team_id, height) VALUES('{Goals}','{Speed}', '{Type}', '{person_id}', '1', '{Height}')";
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
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

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
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            try
            {
                con.Open();
                string query = $"SELECT * FROM PERSON P JOIN BASKETBALLPLAYER BP ON P.ID = BP.PERSON_ID WHERE P.ID = '{id}'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    Id = reader.GetInt32("id");
                    Name = reader.GetString("name");
                    Surname = reader.GetString("surname");
                    Goals = reader.GetInt32("field_goal");
                    Type = reader.GetInt32("type_id");
                    Speed = reader.GetDouble("speed");
                    Height = reader.GetInt32("height");
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
