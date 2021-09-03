using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tournament_Management.Model;

namespace Tournament_Management.ControllerNS
{
    public class UserController
    {
        #region Attributes

        private User _user;
        private List<User> _users;

        private Dictionary<int, string> _roles;

        #endregion Attributes

        #region Properties

        public User User { get => _user; set => _user = value; }
        public Dictionary<int, string> Roles { get => _roles; set => _roles = value; }
        public List<User> Users { get => _users; set => _users = value; }

        #endregion Properties

        #region Constructors

        public UserController()
        {
            Users = new List<User>();
            Roles = new Dictionary<int, string>();
            GetAllRoles();
        }

        #endregion Constructors

        #region Methods

        public void logout()
        {
            User = null;
        }

        public bool isloggedin()
        {
            return User != null;
        }

        /*
            1 = Guest (default)
            2 = User
            3 = Admin
        */

        public bool isGuest()
        {
            return User?.Role == 1;
        }

        public bool isAdmin()
        {
            return User?.Role == 3;
        }

        public List<User> getAllUsers()
        {
            throw new NotImplementedException();
        }

        public void GetAllRoles()
        {
            Roles.Clear();
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            string selectTypes = "SELECT * FROM Roles";
            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(selectTypes, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Roles.Add(reader.GetInt32("id"), reader.GetString("role_d"));
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

        public void Authenticate(string email, string pass)
        {
            User = new User();
            string query = $"SELECT ID FROM auth_user WHERE email = @email AND PASSWORD = @pass";
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.Parameters.Add(new MySqlParameter("@email", email));
                cmd.Parameters.Add(new MySqlParameter("@pass", pass));

                var result = cmd.ExecuteScalar();
                User.Get(Convert.ToInt32(result));
            }
            catch (Exception e)
            {
            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteUser(int id)
        {
            Users.First(u => u.Id == id).Delete();
        }

        public void AddUser(string username, string password)
        {
            User newU = new User(username, password);
            newU.Put();
        }

        public void UpdateRole(int id, int r)
        {
            User usrToUpdate = Users.First(u => u.Id == id);
            usrToUpdate.Role = r;
            usrToUpdate.Update();
        }

        public void UpdatePassword(int id, string password)
        {
            User usrToUpdate = Users.First(u => u.Id == id);
            usrToUpdate.Password = password;
            usrToUpdate.Update();
        }

        #endregion Methods
    }
}