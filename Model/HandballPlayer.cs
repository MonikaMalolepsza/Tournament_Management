﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tournament_Management.Model
{
    public class HandballPlayer : Person
    {
        #region Attributes
        private int _goals;
        private string _position;
        #endregion
        #region Properties
        public int Goals { get => _goals; set => _goals = value; }
        public string Position { get => _position; set => _position = value; }
        #endregion
        #region Constructors
        #endregion
        #region Methods
        public override void Update()
        {
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            con.Open();
            MySqlTransaction transaction = con.BeginTransaction();

            try
            {
                string updatePerson = $"UPDATE PERSON SET NAME='{Name}', SURNAME='{Surname}', ACTIVE='{Active}', AGE='{Age}' WHERE ID='{Id}'";
                string updateHandballplayer = $"UPDATE HANDBALLPLAYER SET position='{Position}', goals='{Goals}' WHERE PERSON_ID='{Id}'";

                MySqlCommand cmd = new MySqlCommand()
                {
                    Connection = con,
                    Transaction = transaction
                };

                cmd.CommandText = updatePerson;
                cmd.ExecuteNonQuery();
                cmd.CommandText = updateHandballplayer;
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

                string insertParticipant = $"INSERT INTO PERSON P (name, surname, age, active) VALUES ('{Name}', '{Surname}', '{Age}', '{Active}')";


                MySqlCommand cmd = new MySqlCommand()
                {
                    Connection = con,
                    Transaction = transaction
                };

                cmd.CommandText = insertParticipant;
                cmd.ExecuteNonQuery();
                int person_id = (int)cmd.LastInsertedId;
                string insertPlayer = $"INSERT INTO HANDBALLPLAYER (goals, speed, type_id, person_id, team_id, position) VALUES('{Goals}','{Speed}', '1', '{person_id}', '{Position}')";
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
                string query = $"SELECT * FROM PERSON P JOIN HANDBALLPLAYER HP ON P.ID = HP.PERSON_ID WHERE P.ID = {id}";
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
                    Position = reader.GetString("position");
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