using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Tournament_Management.Model;


namespace Tournament_Management.ControllerNS
{
    public class Controller
    {

        #region Attributes

        private List<Team> _teams;
        private List<Person> _players;
        private List<Referee> _referees;
        private List<Trainer> _trainers;
        private List<Physio> _physios;
        private List<Participant> _participants;
        private Participant _newParticipant;

        #endregion

        #region Properties

        public List<Team> Teams { get => _teams; set => _teams = value; }
        public List<Person> Players { get => _players; set => _players = value; }
        public List<Referee> Referees { get => _referees; set => _referees = value; }
        public List<Trainer> Trainers { get => _trainers; set => _trainers = value; }
        public List<Physio> Physios { get => _physios; set => _physios = value; }
        public List<Participant> Participants { get => _participants; set => _participants = value; }
        public Participant NewParticipant { get => _newParticipant; set => _newParticipant = value; }

        #endregion

        #region Constructors

        public Controller()
        {
            Teams = new List<Team>();
            Players = new List<Person>();
            Physios = new List<Physio>();
            Trainers = new List<Trainer>();
            Referees = new List<Referee>();

            Participants = new List<Participant>();

            //GetAllPeople();

        }

        public Controller(List<Team> teams, List<Trainer> trainers, List<Referee> referees, List<Person> players, List<Physio> physios)
        {
            Teams = teams;
            Players = players;
            Physios = physios;
            Trainers = trainers;
            Referees = referees;
        }
        #endregion

        #region Methods

        public void GetAllPeople()
        {
            Participants.Clear();

            Participant p = null;
            string sql = "SELECT P.ID," +
                " case " +
                "when((SELECT 1 from TRAINER T where T.PERSON_ID = P.id) is not null) then 'Trainer' " +
                "when((SELECT 1 from FOOTBALLPLAYER FP where FP.PERSON_ID = P.id) is not null) then 'Footballplayer' " +
                "when((SELECT 1 from HANDBALLPLAYER HP where HP.PERSON_ID = P.id) is not null) then 'Handballplayer' " +
                "when((SELECT 1 from BASKETBALLPLAYER BP where BP.PERSON_ID = P.id) is not null) then 'Basketballplayer' " +
                "when((SELECT 1 from PHYSIO PH where PH.PERSON_ID = P.id) is not null) then 'Physio' " +
                "when((SELECT 1 from REFEREE R where R.PERSON_ID = P.id) is not null) then 'Referee' " +
                "end as Profession " +
                "FROM PERSON P";
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");
            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    switch (reader.GetString("Profession"))
                    {
                        case "Footballplayer":
                            p = new FootballPlayer();
                            break;
                        case "Trainer":
                            p = new Trainer();
                            break;
                        case "Handballplayer":
                            p = new HandballPlayer();
                            break;
                        case "Basketballplayer":
                            p = new BasketballPlayer();
                            break;
                        case "Physio":
                            p = new Physio();
                            break;
                        case "Referee":
                            p = new Referee();
                            break;
                        default: break;
                    }
                    if (p != null)
                    {
                        p.Get((int)reader.GetInt64("ID"));
                        Participants.Add(p);
                    }
                    p = null;
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

        public void GetAllFootballPlayers()
        {
            Participants.Clear();
            string query = "SELECT P.ID FROM PERSON P JOIN FOOTBALLPLAYER FP ON P.ID = FP.PERSON_ID";
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    FootballPlayer fp = new FootballPlayer();
                    fp.Get((int)rdr.GetInt64("id"));
                    Participants.Add(fp);
                }

                rdr.Close();
            }
            finally
            {
                con.Close();
            }
        }
        public void GetAllBasketballPlayers()
        {
            Participants.Clear();
            string query = "SELECT P.ID FROM PERSON P JOIN BASKETBALLPLAYER BP ON P.ID = BP.PERSON_ID";
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    BasketballPlayer bbp = new BasketballPlayer();
                    bbp.Get((int)rdr.GetInt64("id"));
                    Participants.Add(bbp);
                }

                rdr.Close();
            }
            finally
            {
                con.Close();
            }
        }
        public void GetAllHndballPlayers()
        {
            Participants.Clear();
            string query = "SELECT P.ID FROM PERSON P JOIN HANDBALLPLAYER HB ON P.ID = HB.PERSON_ID";
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    HandballPlayer hbp = new HandballPlayer();
                    hbp.Get((int)rdr.GetInt64("id"));
                    Participants.Add(hbp);
                }

                rdr.Close();
            }
            finally
            {
                con.Close();
            }
        }
        public void GetAllPhysio()
        {
            Participants.Clear();
            string query = "SELECT P.ID FROM PERSON P JOIN PHYSIO HB ON P.ID = HB.PERSON_ID";
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Physio p = new Physio();
                    p.Get((int)rdr.GetInt64("id"));
                    Participants.Add(p);
                }

                rdr.Close();
            }
            finally
            {
                con.Close();
            }
        }
        
        public void GetAllTrainers()
        {
            Participants.Clear();
            string query = "SELECT P.ID FROM PERSON P JOIN TRAINER HB ON P.ID = HB.PERSON_ID";
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Trainer tr = new Trainer();
                    tr.Get((int)rdr.GetInt64("id"));
                    Participants.Add(tr);
                }

                rdr.Close();
            }
            finally
            {
                con.Close();
            }
        } 
        
        public void GetAllReferees()
        {
            Participants.Clear();
            string query = "SELECT P.ID FROM PERSON P JOIN REFEREE HB ON P.ID = HB.PERSON_ID";
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Referee re = new Referee();
                    re.Get((int)rdr.GetInt64("id"));
                    Participants.Add(re);
                }

                rdr.Close();
            }
            finally
            {
                con.Close();
            }
        }

        #endregion
    }
}
