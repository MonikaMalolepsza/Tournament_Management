using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tournament_Management.Model
{
    public class User
    {
        #region Attributes

        private string _email;
        private string _name;
        private string _surname;
        private string _password;
        private int _role;
        private int _id;

        #endregion Attributes

        #region Properties

        public string Password { get => _password; set => _password = value; }
        public int Role { get => _role; set => _role = value; }
        public string Email { get => _email; set => _email = value; }
        public string Name { get => _name; set => _name = value; }
        public string Surname { get => _surname; set => _surname = value; }
        public int Id { get => _id; set => _id = value; }

        #endregion Properties

        #region Constructors

        public User()
        {
            this.Email = "";
            this.Password = "";
            this.Name = "";
            this.Surname = "";
            this.Role = 1;
        }

        public User(string name, string surname, string un, string pass, int r)
        {
            this.Password = pass;
            this.Role = r;
            this.Email = un;
            this.Name = name;
            this.Surname = surname;
        }

        public User(string username, string passwort)
        {
            this.Password = passwort;
            this.Role = 1;
            this.Email = username;
            this.Name = "";
            this.Surname = "";
        }

        #endregion Constructors

        #region Methods

        public void Update()
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

        public void Put()
        {
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            con.Open();
            MySqlTransaction transaction = con.BeginTransaction();

            try
            {
                // string insertParticipant = $"INSERT INTO PERSON (name, surname, age, active) VALUES ('{Name}', '{Surname}', )";

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

        public void Delete()
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

        public void Get(int id)
        {
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            try
            {
                con.Open();
                string query = $"SELECT * FROM AUTH_USER U WHERE U.ID = '{id}'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Id = reader.GetInt32("id");
                    Name = reader.GetString("name");
                    Email = reader.GetString("email");
                    Surname = reader.GetString("surname");
                    Role = reader.GetInt32("role_id");
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