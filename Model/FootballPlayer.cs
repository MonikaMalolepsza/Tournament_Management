using System;
using MySql.Data.MySqlClient;

namespace Tournament_Management.Model
{
    public class FootballPlayer : Person
    {
        #region Attributes

        private int _goals;

        #endregion

        #region Properties

        public int Goals { get => _goals; set => _goals = value; }

        #endregion

        #region Constructors

        public FootballPlayer()
        {

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

                string updateFootballPlayer = $"UPDATE FOOTBALLPLAYER SET goals='{Goals}' WHERE player_id = (SELECT player_id FROM player WHERE participant_id = {Id})";
                string updatePlayer = $"UPDATE player SET surname='{Surname}', speed='{Speed}', active='{Active}' WHERE participant_id='{Id}'";
                string updateName = $"UPDATE participant SET name='{Name}' WHERE id='{Id}'";

                MySqlCommand cmd = new MySqlCommand()
                {
                    Connection = con,
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

                cmd.CommandText = insertParticipant;
                cmd.ExecuteNonQuery();
                int person_id = (int)cmd.LastInsertedId;
                string insertPlayer = $"INSERT INTO FOOTBALLPLAYER (goals, speed, type_id, person_id, team_id) VALUES('{Goals}','{Speed}', '1', '{person_id}', '1')";
                cmd.CommandText = insertPlayer;
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


        public override void Delete()
        {
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            try
            {
                con.Open();

                string query = $"DELETE FROM PERSON P WHERE P.ID = {Id}";
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
                string query = $"SELECT * FROM PERSON P JOIN FOOTBALLPLAYER FP ON P.ID = FP.PERSON_ID WHERE P.ID = {id}";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    Id = reader.GetInt32("id");
                    Name = reader.GetString("name");
                    Surname = reader.GetString("surname");
                    Goals = reader.GetInt32("goals");
                    Speed = reader.GetDouble("speed");
                    Active = reader.GetBoolean("active");
                    Age = reader.GetInt32("age");
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
