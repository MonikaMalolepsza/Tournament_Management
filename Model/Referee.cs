using MySql.Data.MySqlClient;
using System;
namespace Tournament_Management.Model
{
    public class Referee : Person

    {
        #region Attributes

        /**
            Assistant Referee. (minimum age 12) ...
            Regional Referee. (minimum age 12) ...
            Intermediate Referee. (minimum age 14) ...
            Advanced Referee. (minimum age 16) ...
            National Referee. (minimum age 18)
         */
        private string _certificate;
        private int _type;

        #endregion

        #region Properties


        public string Certificate { get => _certificate; set => _certificate = value; }
        public int Type { get => _type; set => _type = value; }

        #endregion

        #region Constructors

        public Referee()
        {
            this.Certificate = "";
            this.Type = 0;
        }

        public Referee(string name) : base(name)
        {
            this.Certificate = "";
            this.Type = 0;
        }

        public Referee(string name, string certificate, int type) : base(name)
        {
            this.Certificate = certificate;
            this.Type = type;
        }

        #endregion

        #region Methods

        public string GiveCert()
        {
            return this.Certificate;
        }

        public override string GiveInfo()
        {
            return base.GiveInfo() + ", " + $"Certificate: {Certificate}";
        }

        public override void Update()
        {
        MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

        con.Open();
        MySqlTransaction transaction = con.BeginTransaction();

        try
        {
            string updatePerson = $"UPDATE PERSON SET name='{Name}', age='{Age}' surname='{Surname}', active='{Active}' WHERE  ID='{Id}'";
            string updateReferee = $"UPDATE REFEREE SET certificate='{Certificate}', type_id='{Type}' WHERE PERSON_ID= '{Id}'";

            MySqlCommand cmd = new MySqlCommand()
            {
                Connection = con,
                Transaction = transaction
            };

            cmd.CommandText = updatePerson;
            cmd.ExecuteNonQuery();
            cmd.CommandText = updateReferee;
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
                    `experience` INT NULL,
                    `person_id` INT(11) NULL DEFAULT NULL,
                    PRIMARY KEY (`id`),
                    `team_id` INT(11) NULL DEFAULT NULL,

                 */

                cmd.CommandText = insertParticipant;
                cmd.ExecuteNonQuery();
                int person_id = (int)cmd.LastInsertedId;
                string insertReferee = $"INSERT INTO REFEREE (certificate, person_id, type_id) VALUES('{Certificate}', '{person_id}', '{Type}')";
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
                string query = $"SELECT * FROM PERSON P JOIN REFEREE REF ON P.ID = REF.PERSON_ID WHERE P.ID = '{id}'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Id = reader.GetInt32("id");
                    Name = reader.GetString("name");
                    Certificate = reader.GetString("certificate");
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
