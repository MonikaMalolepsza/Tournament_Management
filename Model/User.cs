using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tournament_Management.Helper;


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
            this.Name = "";
            this.Surname = "";
            this.Role = 1;
        }

        public User(string name, string surname, string un, string pass, int r)
        {
            this.Role = r;
            this.Email = un;
            this.Name = name;
            this.Surname = surname;
        }

        public User(string username)
        {
            this.Role = 1;
            this.Email = username;
            this.Password = "password";
            this.Name = "";
            this.Surname = "";
        }

        #endregion Constructors

        #region Methods

        public void Update()
        {
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);
            con.Open();
            try
            {
                string updateUsr = $"UPDATE auth_user SET name='{Name}', role_id='{Role}', surname='{Surname}', email='{Email}' WHERE ID='{Id}'";
                MySqlCommand cmd = new MySqlCommand(updateUsr, con);

                cmd.ExecuteNonQuery();
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
         public void UpdatePassword()
        {
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);
            con.Open();
            try
            {
                string updateUsrPass = $"UPDATE auth_user SET password='{Password}' WHERE ID='{Id}'";
                MySqlCommand cmd = new MySqlCommand(updateUsrPass, con);

                cmd.ExecuteNonQuery();
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

        public void Put()
        {
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);
            con.Open();
            try
            {
                string addNewUsr = $"INSERT INTO auth_user (name, surname, Email, role_id, password) VALUES ('{Name}', '{Surname}', '{Email}', '{Role}', '{Password}')";
                MySqlCommand cmd = new MySqlCommand(addNewUsr, con);

                cmd.ExecuteNonQuery();
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

        public void Delete()
        {
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

            try
            {
                con.Open();

                string query = $"DELETE FROM auth_user WHERE ID = '{Id}'";
                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.ExecuteNonQuery();
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

        public void Get(int id)
        {
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

            try
            {
                con.Open();
                string query = $"SELECT * FROM auth_user U WHERE U.ID = '{id}'";
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
                throw e;
            }
            finally
            {
                con.Close();
            }
        }

        #endregion Methods
    }
}