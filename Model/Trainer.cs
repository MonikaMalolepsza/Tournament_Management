using MySql.Data.MySqlClient;
using System;
namespace Tournament_Management.Model
{
    public class Trainer : Person
    {

        #region Attributes

        private int _type;

        #endregion

        #region Properties

        public int Type { get => _type; set => _type = value; }

        #endregion

        #region Constructors

        public Trainer()
        {
            this.Type = 0;
        }

        public Trainer(string name) : base(name)
        {
            this.Type = 0;

        }

        public Trainer(string name, int type) : base(name)
        {
            this.Type = type;
        }

        #endregion

        #region Methods

        public int HowOld()
        {
            return this.Age;
        }

        public override string GiveInfo()
        {
            return base.GiveInfo() + ", " + $"Age: {Age}";
        }

        public override void Update()
        {
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            con.Open();
            MySqlTransaction transaction = con.BeginTransaction();

            try
            {
                string updatePerson = $"UPDATE PERSON SET name='{Name}', age='{Age}' surname='{Surname}', active='{Active}' WHERE ID='{Id}'";
                string updateTrainer = $"UPDATE TRAINER SET type_id='{Type}' WHERE PERSON_ID= {Id}";

                MySqlCommand cmd = new MySqlCommand()
                {
                    Connection = con,
                    Transaction = transaction
                };

                cmd.CommandText = updatePerson;
                cmd.ExecuteNonQuery();
                cmd.CommandText = updateTrainer;
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
                `person_id` INT(11) NULL DEFAULT NULL,
                `type_id` INT(11) NULL DEFAULT NULL,
                `team_id` INT(11) NULL DEFAULT NULL,

                 */

                cmd.CommandText = insertParticipant;
                cmd.ExecuteNonQuery();
                int person_id = (int)cmd.LastInsertedId;
                string insertReferee = $"INSERT INTO REFEREE (team_id, person_id, type_id) VALUES('1', '{person_id}', '{Type}')";
                cmd.CommandText = insertReferee;
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

                string query = $"DELETE FROM PERSON P WHERE P.ID = {Id}";
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
                string query = $"SELECT * FROM PERSON P JOIN TRAINER TR ON P.ID = TR.PERSON_ID WHERE P.ID = {id}";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Id = reader.GetInt32("id");
                    Name = reader.GetString("name");
                    Type = reader.GetInt32("type_id");
                    Surname = reader.GetString("surname");
                    Type = reader.GetInt32("type_id");
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
