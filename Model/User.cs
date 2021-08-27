using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tournament_Management.Model
{
    public class User : Person
    {
        
        #region Attributes

        private string _username;
        private string _passwort;
        private int _role;

        #endregion

        #region Properties
        public string Passwort { get => _passwort; set => _passwort = value; }
        public int Role { get => _role; set => _role = value; }
        public string Username { get => _username; set => _username = value; }

        #endregion

        #region Constructors

        public User()
        {
            this.Username = "";
            this.Passwort = "";
            this.Role = 1;
        }

        public User(string name, bool active, string surname, string un, string pass, int r) : base(name, surname, active)
        {
            this.Passwort = pass;
            this.Role = r;
            this.Username = un;

        }

        public User(string username, string passwort)
        {
            this.Passwort = passwort;
            this.Role = 1;
            this.Username = username;
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

            /*    string updatePerson = $"UPDATE PERSON SET name='{Name}', age='{Age}', surname='{Surname}', active='{Active}' WHERE ID='{Id}'";
                string updateTrainer = $"UPDATE TRAINER SET type_id='{Type}' WHERE PERSON_ID= '{Id}'";

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
                */

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
                cmd.CommandText = insertParticipant;
                cmd.ExecuteNonQuery();
                int person_id = (int)cmd.LastInsertedId;
                string insertTrainer = $"INSERT INTO TRAINER (team_id, person_id, type_id) VALUES('1', '{person_id}', '{Type}')";
                cmd.CommandText = insertTrainer;
                cmd.ExecuteNonQuery();


                transaction.Commit();
                */

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
                string query = $"SELECT * FROM PERSON P JOIN USER U ON P.ID = U.PERSON_ID  WHERE P.ID = '{id}'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Id = reader.GetInt32("id");
                    Name = reader.GetString("name");
                    Username = reader.GetString("username");
                    Surname = reader.GetString("surname");
                    Active = reader.GetBoolean("active");
                    Age = reader.GetInt32("age");
                    Role = reader.GetInt32("role");
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