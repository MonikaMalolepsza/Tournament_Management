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
        private List<Player> _players;
        private List<Referee> _referees;
        private List<Trainer> _trainers;
        private List<Physio> _physios;

        #endregion

        #region Properties

        public List<Team> Teams { get => _teams; set => _teams = value; }
        public List<Player> Players { get => _players; set => _players = value; }
        public List<Referee> Referees { get => _referees; set => _referees = value; }
        public List<Trainer> Trainers { get => _trainers; set => _trainers = value; }
        public List<Physio> Physios { get => _physios; set => _physios = value; }

        #endregion

        #region Constructors

        public Controller()
        {
            Teams = new List<Team>();
            Players = new List<Player>();
            Physios = new List<Physio>();
            Trainers = new List<Trainer>();
            Referees = new List<Referee>();

            this.GetAllFootballPlayers();

        }

        public Controller(List<Team> teams, List<Trainer> trainers, List<Referee> referees, List<Player> players, List<Physio> physios)
        {
            Teams = teams;
            Players = players;
            Physios = physios;
            Trainers = trainers;
            Referees = referees;
        }
        #endregion

        #region Methods

        public void GetAllFootballPlayers()
        {
            string query = "SELECT p.id FROM participant p JOIN player pl ON p.id = pl.participant_id JOIN footballPlayer fp ON pl.id = fp.player_id";
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Port=3307;Database=tournament;Uid=root;Pwd=example");

            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    FootballPlayer fp = new FootballPlayer();
                    fp.Get((int)rdr.GetInt64("id"));
                    Players.Add(fp);

                }

                rdr.Close();
            }
            finally
            {
                con.Close();
            }
        }

        /*
        public void Test()
        {
            Player s = new Player("Jens", 70.5, true);
            Trainer t = new Trainer("Thomas", 50);
            Physio p = new Physio("Monika", 2);
            Team m = new Team("FC Koeln");
            m.NewMember(s);
            m.NewMember(t);
            m.NewMember(p);
        }
        */

        #endregion
    }
}
