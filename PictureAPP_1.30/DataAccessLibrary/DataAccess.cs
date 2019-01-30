using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace DataAccessLibrary
{
    public class DataAccess
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=Picture.db"))
            {
                db.Open();
                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS picture (Primary_Key INTEGER PRIMARY KEY, " +
                    "Text_Entry NVARCHAR(2048) NULL)";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }
        public static void AddData(string picture)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=Picture.db"))
            {
                db.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO picture VALUES (NULL, @Entry);";
                insertCommand.Parameters.AddWithValue("@Entry", picture);
                insertCommand.ExecuteReader();
                db.Close();
            }
        }
        public static List<String> GetData()
        {
            List<String> entries = new List<string>();
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Text_Entry from picture", db);
                //Read 方法将向前浏览返回的数据的行
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                }
                db.Close();
            }
            return entries;
        }


    }


}
