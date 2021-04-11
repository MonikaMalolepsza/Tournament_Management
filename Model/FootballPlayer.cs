using System;
using MySql.Data.MySqlClient;

namespace Tournament_Management.Model
{
    public class FootballPlayer : Player
    {
        #region Attributes

        private int _goal;

        #endregion

        #region Properties

        public int Goal { get => _goal; set => _goal = value; }

        #endregion

        #region Constructors

        public FootballPlayer()
        {

        }

        #endregion


        #region Methods

        public override void Update()
        {
            MySqlConnection connection = new MySqlConnection("Server=127.0.0.1;Port=3307;Database=tournament;Uid=root;Pwd=example");

            connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {

                string updateFootballPlayer = $"UPDATE footballPlayer SET goals='{Goal}' WHERE player_id = (SELECT player_id FROM player WHERE participant_id = {Id})";
                string updatePlayer = $"UPDATE player SET surname='{Surname}', speed='{Speed}', active='{Active}' WHERE participant_id='{Id}'";
                string updateName = $"UPDATE participant SET name='{Name}' WHERE id='{Id}'";


                MySqlCommand cmd = new MySqlCommand()
                {
                    Connection = connection,
                    Transaction = transaction
                };

                cmd.CommandText = updateName;
                cmd.ExecuteNonQuery();
                cmd.CommandText = updatePlayer;
                cmd.ExecuteNonQuery();
                cmd.CommandText = updateFootballPlayer;
                cmd.ExecuteNonQuery();

                transaction.Commit();

            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
            finally
            {
                connection.Close();

            }

        }

        public override void Put()
        {
            MySqlConnection connection = new MySqlConnection("Server=127.0.0.1;Port=3307;Database=tournament;Uid=root;Pwd=example");

            connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {

                string insertParticipant = $"INSERT INTO participant (name) VALUES ('{Name}')";


                MySqlCommand cmd = new MySqlCommand()
                {
                    Connection = connection,
                    Transaction = transaction
                };

                cmd.CommandText = insertParticipant;
                cmd.ExecuteNonQuery();
                int participant_id = (int)cmd.LastInsertedId;
                string insertPlayer = $"INSERT INTO player (participant_id, surname, speed, active) VALUES('{participant_id}','{Surname}', '{Speed}', '{Active}')";
                cmd.CommandText = insertPlayer;
                cmd.ExecuteNonQuery();
                int player_id = (int)cmd.LastInsertedId;
                string insertFootballPlayer = $"INSERT INTO footballPlayer (player_id, goals) VALUES('{player_id}', '{Goal}'))";
                cmd.CommandText = insertFootballPlayer;
                cmd.ExecuteNonQuery();


                transaction.Commit();

            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
            finally
            {
                connection.Close();

            }


        }

        public override void Delete()
        {
            MySqlConnection connection = new MySqlConnection("Server=127.0.0.1;Port=3307;Database=tournament;Uid=root;Pwd=example");

            try
            {
                connection.Open();

                string query = $"DELETE FROM participant p WHERE p.id = {Id}";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
            finally
            {
                connection.Close();

            }

        }

        public override void Get(int id)
        {
            MySqlConnection connection = new MySqlConnection("Server=127.0.0.1;Port=3307;Database=tournament;Uid=root;Pwd=example");

            try
            {
                connection.Open();
                string query = $"SELECT * FROM participant p JOIN player pl ON p.id = pl.participant_id JOIN footballPlayer fp ON pl.id = fp.player_id WHERE p.id = {id}";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Id = reader.GetInt32("id");
                    Name = reader.GetString("name");
                    Surname = reader.GetString("surname");
                    Goal = reader.GetInt32("goals");
                    Speed = reader.GetDouble("speed");
                    Active = reader.GetBoolean("active");
                }

                reader.Close();

            }
            catch (Exception e)
            {

            }
            finally
            {
                connection.Close();

            }
        }

        #endregion


    }
}
