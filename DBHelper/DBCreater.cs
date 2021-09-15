using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Tournament_Management.DBHelper
{
    public static class DatabaseCreator
    {
        private const string _connectionString = "Server=127.0.0.1;Uid=root;Pwd=;";
        private static string _structurePath;
        public static void GenerateDatabase()
        {
            _structurePath = "Tournament_Management.Resources";
            using (MySqlConnection con = new MySqlConnection(_connectionString))
            {
                con.Open();
                using (MySqlTransaction trans = con.BeginTransaction())
                using (MySqlCommand cmd = new MySqlCommand() { Connection = con, Transaction = trans })
                    try
                    {
                        string currentStatement = CreateDatabase();
                        if (currentStatement != "")
                        {
                            cmd.CommandText = currentStatement;
                            cmd.ExecuteNonQuery();
                        }
                        currentStatement = CreateTables();
                        if (currentStatement != "")
                        {
                            cmd.CommandText = currentStatement;
                            cmd.ExecuteNonQuery();
                        }
                        currentStatement = InsertExampleData();
                        if (currentStatement != "")
                        {
                            cmd.CommandText = currentStatement;
                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                    }
            }
        }
        private static string CreateDatabase()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream s = assembly.GetManifestResourceStream($"{_structurePath}.CreateDatabase.sql"))
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        private static string CreateTables()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream s = assembly.GetManifestResourceStream($"{_structurePath}.CreateTables.sql"))
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        private static string InsertExampleData()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream s = assembly.GetManifestResourceStream($"{_structurePath}.InsertData.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                return sr.ReadToEnd();
            }
        }
    }
}