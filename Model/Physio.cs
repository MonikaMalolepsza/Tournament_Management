using MySql.Data.MySqlClient;
using System;
namespace Tournament_Management.Model
{
    public class Physio : Person
    {
        #region Attributes

        private int _experience;
        private int _type;

        #endregion

        #region Properties

        public int Experience { get => _experience; set => _experience = value; }

        #endregion

        #region Constructors

        public Physio()
        {
            this.Experience = 0;
        }

        public Physio(string name) : base(name)
        {
            this.Experience = 0;

        }

        public Physio(string name, int experience) : base(name)
        {
            this.Experience = experience;
        }

        #endregion

        #region Methods

        public int HowExperienced()
        {
            return this.Experience;
        }

        public override string GiveInfo()
        {
            return base.GiveInfo() + ", " + $"Experience in years: {Experience}";
        }

        public override void Update()
        {
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=tournament;Uid=user;Pwd=user;");

            con.Open();
            MySqlTransaction transaction = con.BeginTransaction();

            try
            {
                string updatePerson = $"UPDATE PERSON SET name='{Name}', age='{Age}' surname='{Surname}', active='{Active}' WHERE ID='{Id}'";
                string updatePhysio = $"UPDATE PHYSIO SET experience='{Experience}' WHERE PERSON_ID= {Id}";

                MySqlCommand cmd = new MySqlCommand()
                {
                    Connection = con,
                    Transaction = transaction
                };

                cmd.CommandText = updatePerson;
                cmd.ExecuteNonQuery();
                cmd.CommandText = updatePhysio;
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

                /*
                
                    `id` INT(11) NOT NULL AUTO_INCREMENT,
                    `experience` INT NULL,
                    `person_id` INT(11) NULL DEFAULT NULL,
                    PRIMARY KEY (`id`),
                    `team_id` INT(11) NULL DEFAULT NULL,

                 */

                cmd.CommandText = insertParticipant;
                cmd.ExecuteNonQuery();
                int person_id = (int)cmd.LastInsertedId;
                string insertPhysio = $"INSERT INTO PHYSIO (experience, person_id, team_id) VALUES('{Experience}', '{person_id}', '1')";
                cmd.CommandText = insertPhysio;
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
            catch (Exception e)`
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
                string query = $"SELECT * FROM PERSON P JOIN PHYSIO PHYS ON P.ID = PHYS.PERSON_ID WHERE P.ID = {id}";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Id = reader.GetInt32("id");
                    Experience = reader.GetInt32("experience");
                    Name = reader.GetString("name");
                    Surname = reader.GetString("surname");
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
