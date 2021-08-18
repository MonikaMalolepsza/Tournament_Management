using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tournament_Management.Model
{
    public class Game
    {
        #region Attributes

        private List<Score> _scores;
        private int _tournamentId;
        private int _id;

        #endregion Attributes

        #region Properties

        public int Id { get => _id; set => _id = value; }
        public int TournamentId { get => _tournamentId; set => _tournamentId = value; }
        public List<Score> Scores { get => _scores; set => _scores = value; }

        #endregion Properties

        #region Constructors

        public Game()
        {
            this.Id = 0;
            this.Scores = new List<Score>();
        }

        public Game(int t1, int score1, int t2, int score2, int id)
        {
            this.Id = 0;
            this.Scores = new List<Score>();
            Scores.Add(new Score(t1, score1));
            Scores.Add(new Score(t2, score2));
            this.Id = id;
        }

        #endregion Constructors

        #region Methods

        public void Update()
        {
            // TODO smth still not working...

            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            con.Open();
            MySqlTransaction transaction = con.BeginTransaction();

            try
            {
                string updatePerson = $"UPDATE Game SET tournament_id='{TournamentId}' WHERE ID='{Id}'";

                MySqlCommand cmd = new MySqlCommand()
                {
                    Connection = con,
                    Transaction = transaction
                };

                cmd.CommandText = updatePerson;
                cmd.ExecuteNonQuery();
                if (Scores != null && Scores.Count > 0)
                {
                    foreach (Score s in Scores)
                    {
                        string insertScores = $"UPDATE Score SET team_id='{s.Team}', score='{s.Points}' WHERE GAME_ID='{Id}'";
                        cmd.CommandText = insertScores;
                        cmd.ExecuteNonQuery();
                    }
                }
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

        public void Put()
        {
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            con.Open();
            MySqlTransaction transaction = con.BeginTransaction();

            try
            {
                string insertQuery = $"INSERT INTO GAME (tournament_id) VALUES ('{TournamentId}')";

                MySqlCommand cmd = new MySqlCommand()
                {
                    Connection = con,
                    Transaction = transaction
                };

                /*

                      `team_id` INT(11) NULL DEFAULT NULL,
                      `game_id` INT(11) NULL DEFAULT NULL,
                      `score` INT(3) NULL DEFAULT NULL,

                 */

                cmd.CommandText = insertQuery;
                cmd.ExecuteNonQuery();
                int game_id = (int)cmd.LastInsertedId;

                if (Scores != null && Scores.Count > 0)
                {
                    foreach (Score s in Scores)
                    {
                        string insertScores = $"INSERT INTO SCORE (team_id, game_id, score) VALUES('{s.Team}', '{Id}', '{s.Points}')";
                        cmd.CommandText = insertScores;
                        cmd.ExecuteNonQuery();
                    }
                }
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

        public void Delete()
        {
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            try
            {
                con.Open();

                string query = $"DELETE FROM GAME WHERE ID = '{Id}'";
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
                string query = $"SELECT * FROM GAME G JOIN SCORE S ON G.ID = S.GAME_ID  WHERE G.ID = '{id}'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    /*
                     * TODO:
                     no idea if this works :D
                     */
                    Score scoreSet = new Score();
                    Id = reader.GetInt32("id");
                    TournamentId = reader.GetInt32("tournament_id");
                    scoreSet.Points = reader.GetInt32("score");
                    scoreSet.Team = reader.GetInt32("team_id");
                    Scores.Add(scoreSet);
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