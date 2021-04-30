using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Tournament_Management.Model
{
    public class Team : Participant
    {
        #region Attributes

        private List<Participant> _list;
        private string _foundingDate;

        #endregion

        #region Properties

        public List<Participant> List { get => _list; set => _list = value; }
        public string FoundingDate { get => _foundingDate; set => _foundingDate = value; }

        #endregion

        #region Constructors

        public Team()
        {
            List = new List<Participant>();
        }

        public Team(string name) : base(name)
        {
            List = new List<Participant>();
        }

        public Team(string name, List<Participant> team) : base(name)
        {
            List = team;
        }



        #endregion

        #region Methods

        public void NewMember(Participant teilnehmer)
        {
            List.Add(teilnehmer);
        }

        public string OutputTeamInformation()
        {
            string res = $"Team: {Name}\r\n";
            foreach (Participant t in List)
            {
                res += t.GiveInfo();
            }
            return res;
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Put()
        {
            throw new NotImplementedException();
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override void Get(int id)
        {
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            try
            {
                con.Open();
                string query = $"SELECT * FROM Team where ID = {id}";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Id = reader.GetInt32("id");
                    Name = reader.GetString("name");
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
