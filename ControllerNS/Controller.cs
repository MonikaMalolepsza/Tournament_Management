using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using System.IO;
using Tournament_Management.Model;
using Tournament_Management.Helper;
using Newtonsoft.Json;

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
        private List<Tournament> _tournaments;
        private List<Game> _games;
        private Participant _newParticipant;
        private int _activeParticipant;

        private Dictionary<int, string> _typeList;

        #endregion Attributes

        #region Properties

        public List<Team> Teams { get => _teams; set => _teams = value; }
        public List<Person> Players { get => _players; set => _players = value; }
        public List<Referee> Referees { get => _referees; set => _referees = value; }
        public List<Trainer> Trainers { get => _trainers; set => _trainers = value; }
        public List<Physio> Physios { get => _physios; set => _physios = value; }
        public List<Participant> Participants { get => _participants; set => _participants = value; }
        public Participant NewParticipant { get => _newParticipant; set => _newParticipant = value; }
        public Dictionary<int, string> TypeList { get => _typeList; set => _typeList = value; }
        public int ActiveParticipant { get => _activeParticipant; set => _activeParticipant = value; }
        public List<Tournament> Tournaments { get => _tournaments; set => _tournaments = value; }
        public List<Game> Games { get => _games; set => _games = value; }

        #endregion Properties

        #region Constructors

        public Controller()
        {
            Teams = new List<Team>();
            Players = new List<Person>();
            Physios = new List<Physio>();
            Trainers = new List<Trainer>();
            Referees = new List<Referee>();
            Tournaments = new List<Tournament>();
            TypeList = new Dictionary<int, string>();
            Participants = new List<Participant>();
            Games = new List<Game>();
            ActiveParticipant = -1;

            GetAllTypes();
        }

        public Controller(List<Team> teams, List<Trainer> trainers, List<Referee> referees, List<Person> players, List<Physio> physios, List<Tournament> tournaments, List<Game> games)
        {
            Teams = teams;
            Players = players;
            Physios = physios;
            Trainers = trainers;
            Referees = referees;
            Games = games;
            Tournaments = tournaments;
        }

        #endregion Constructors

        #region Methods

        public void GetAllTypes()
        {
            TypeList.Clear();
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

            string selectTypes = "SELECT * FROM TYPE";
            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(selectTypes, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TypeList.Add(reader.GetInt32("id"), reader.GetString("type"));
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

        public List<Person> GetAllCandidates(int teamId)
        {
            // TODO: FIX THE QUERY!?
            List<Person> result = new List<Person>();
            Person p = null;
            string sql = "SELECT P.ID, " +
            "CASE " +
            "WHEN((SELECT 1 FROM TRAINER T WHERE T.PERSON_ID = P.iD) IS NOT NULL) THEN 'Trainer' " +
            "WHEN((SELECT 1 FROM PHYSIO PH WHERE PH.PERSON_ID = P.iD) IS NOT NULL) THEN 'Physio' " +
            "WHEN((SELECT 1 FROM FOOTBALLPLAYER FP WHERE FP.PERSON_ID = P.iD) IS NOT NULL) THEN 'Footballplayer' " +
            "WHEN((SELECT 1 FROM BASKETBALLPLAYER BS WHERE BS.PERSON_ID = P.iD) IS NOT NULL) THEN 'Basketballplayer' " +
            "WHEN((SELECT 1 FROM HANDBALLPLAYER HB WHERE HB.PERSON_ID = P.iD) IS NOT NULL) THEN 'Handballplayer' " +
            "ELSE 'No Candidates found!' " +
            "END AS Profession " +
            "FROM PERSON P " +
            "LEFT OUTER JOIN team_member pm " +
            "ON P.ID = pm.PERSON_ID " +
            $"WHERE pm.TEAM_ID <> {teamId} " +
            "OR pm.PERSON_ID IS NULL";

            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);
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

                        default: break;
                    }
                    if (p != null)
                    {
                        p.Get((int)reader.GetInt64("ID"));
                        result.Add(p);
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

            return result;
        }

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
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);
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
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

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

        public void GetAllTeams()
        {
            Teams.Clear();
            string query = "SELECT * FROM TEAM";
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Team team = new Team();
                    team.Get((int)rdr.GetInt64("id"));
                    Teams.Add(team);
                }

                rdr.Close();
            }
            finally
            {
                con.Close();
            }
        }

        public void GetAllTournaments()
        {
            Tournaments.Clear();
            string query = "SELECT * FROM TOURNAMENT";
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Tournament t = new Tournament();
                    t.Get((int)rdr.GetInt64("id"));
                    Tournaments.Add(t);
                }

                rdr.Close();
            }
            finally
            {
                con.Close();
            }
        }

        public List<Team> GetAllTournamentCandidates(int Id)
        {
            List<Team> result = new List<Team>();

            string query = $"SELECT T.ID FROM TEAM T WHERE NOT EXISTS(SELECT NULL FROM TOURNAMENT_PARTICIPANTS TP WHERE TP.TOURNAMENT_ID={Id} AND TP.TEAM_ID=T.ID)";
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Team t = new Team();
                    t.Get((int)rdr.GetInt64("id"));
                    result.Add(t);
                }

                rdr.Close();
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public void GetAllTournamentsForType(int type)
        {
            Tournaments.Clear();
            string query = $"SELECT * FROM TOURNAMENT WHERE type_id = {type}";
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Tournament t = new Tournament();
                    t.Get((int)rdr.GetInt64("id"));
                    Tournaments.Add(t);
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
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

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
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

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
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

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
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

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
            MySqlConnection con = new MySqlConnection(GlobalConst.connectionString);

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

        public DataTable GetRanking(int tournId)
        {
            DataTable dt = new DataTable();
            var sql = "SELECT @`curRow` := @`curRow` +1 AS \"Position\", Total.*"
                        + " FROM"
                        + " (SELECT SUBVALS.NAME"
                        + " , SUM(SUBVALS.Wins * 3) + SUM(SUBVALS.Draws * 1) / 2 AS \"Points\""
                        + " , SUBVALS.Wins"
                        + " , SUBVALS.Loses"
                        + " , SUBVALS.Draws"
                        + " , SUBVALS.Difference"
                        + " FROM"
                        + " (" +
                        " SELECT SUBSTATS.NAME"
                        + " , SUM(if (SUBSTATS.STATUS = 'Win',1,0)) as 'Wins'"
                        + " , SUM(if (SUBSTATS.STATUS = 'Lose',1,0)) as 'Loses'"
                        + " , SUM(if (SUBSTATS.STATUS = 'Draw',1,0)) as 'Draws'"
                        + " , SUM(SUBSTATS.DIFFERENCE) as \"Difference\""
                        + " FROM"
                        + " (" +
                        " SELECT A.NAME,"
                        + " A.SCORE - B.SCORE as \"Difference\","
                        + " CASE"
                        + " WHEN A.SCORE > B.SCORE THEN 'Win'"
                        + " WHEN A.SCORE < B.SCORE THEN 'Lose'"
                        + " else 'Draw' end as \"status\""
                        + " FROM"
                        + " (SELECT T.NAME, S.*"
                        + " FROM TEAM T"
                        + " JOIN SCORE S"
                        + " ON S.TEAM_ID = T.ID"
                        + " JOIN GAME G"
                        + " ON S.GAME_ID = G.ID"
                        + $" WHERE G.TOURNAMENT_ID = {tournId}) AS A"
                        + " JOIN"
                        + " (SELECT T.NAME, S.*"
                        + " FROM TEAM T"
                        + " JOIN SCORE S"
                        + " ON S.TEAM_ID = T.ID"
                        + " JOIN GAME G"
                        + " ON S.GAME_ID = G.ID"
                        + $" WHERE G.TOURNAMENT_ID = {tournId}) AS B"
                        + " ON A.GAME_ID = B.GAME_ID AND A.NAME <> B.NAME) AS SUBSTATS"
                        + " GROUP BY SUBSTATS.NAME) AS SUBVALS"
                        + " GROUP BY SUBVALS.NAME"
                        + " ORDER BY Points desc, Difference DESC"
                        + " ) As Total, (SELECT @`curRow` := 0) r";

            using (MySqlConnection con = new MySqlConnection(GlobalConst.connectionString))
            {
                try
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
                    {
                        //cmd.Parameters.AddWithValue("@curRow", 0);
                        dt.Load(cmd.ExecuteReader());
                        return dt;
                    }
                }
                catch (MySqlException e)
                {
                    throw e;
                }
            }
        }

        public string SerializeFromGrid<T>(List<T> toSerialize, int type)
        {
            string result = string.Empty;

            if (type == 1)
            {
                //XML
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
                StringWriter writer = new StringWriter();
                xmlSerializer.Serialize(writer, toSerialize);
                result = writer.ToString();
            }
            else if (type == 2)
            {
                //JSON
                result = JsonConvert.SerializeObject(toSerialize);
            }

            return result;
        }

        public string SerializeToObject<T>(List<T> toSerialize, int type)
        {
            string result = string.Empty;

            if (type == 1)
            {
                //XML
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                XmlSerializer ser = new XmlSerializer(typeof(Team));
                StringWriter writer = new StringWriter();
                ser.Serialize(writer, toSerialize);
                result = writer.ToString();
            }
            else if (type == 2)
            {
                //JSON
                result = JsonConvert.SerializeObject(toSerialize);
            }

            return result;
        }

        #endregion Methods
    }
}