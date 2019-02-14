using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using PictureByBindingAPP_2._11_Model;
using System.Collections.ObjectModel;

namespace DataAccessLibrary
{
    public class DataAccess
    {
        public static void InitialzeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection("Filename=images1.db")) 
            {
                db.Open();
                String tableCommand = "CREATE TABLE IF NOT EXISTS Image (ID INTEGER PRIMARY KEY,imageurl TEXT NULL,Title TEXT NULL,Author TEXT NULL)";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }
        public static void AddData(int id,string imageurl,string title,string author)
        {
            using (SqliteConnection  db=new SqliteConnection("Filename=images1.db"))
            {
                db.Open();
                SqliteCommand insertcommand = new SqliteCommand();
                insertcommand.Connection = db;
                insertcommand.CommandText = "INSERT OR IGNORE INTO Image VALUES(@id,@Entry,@Entry1,@Entry2)";
                insertcommand.Parameters.AddWithValue("@Entry", imageurl);
                insertcommand.Parameters.AddWithValue("@Entry1", title);
                insertcommand.Parameters.AddWithValue("@Entry2", author);
                insertcommand.Parameters.AddWithValue("@id", id);
                insertcommand.ExecuteReader();
                db.Close();
            }
        }
        public static string selectData(int id)
        {
            string imageurl=null;
            using (SqliteConnection db=new SqliteConnection("Filename=images1.db"))
            {
                db.Open();
                SqliteCommand selectcommand = new SqliteCommand();
                selectcommand.Connection = db;
                selectcommand.CommandText = "SELECT imageurl from Image where ID=" + id;
                SqliteDataReader sqliteDataReader = selectcommand.ExecuteReader();
                while (sqliteDataReader.Read())
                {
                    imageurl = sqliteDataReader.GetString(0);
                }
                db.Close();
            }
            return imageurl;
        }
        public static Imagemodel RandomShowOne()
        {
            using (SqliteConnection db=new SqliteConnection("Filename=images1.db"))
            {
                Imagemodel imageurl = new Imagemodel(); ;
                db.Open();
                SqliteCommand sqliteCommand = new SqliteCommand();
                sqliteCommand.Connection = db;
                sqliteCommand.CommandText = "Select * from Image order by random() limit 1";
                SqliteDataReader reader = sqliteCommand.ExecuteReader();
                while (reader.Read())
                {
                    imageurl.imageId = reader.GetInt16(0);
                    imageurl.CoverImage = reader.GetString(1);
                    imageurl.Title = reader.GetString(2);
                    imageurl.Author = reader.GetString(3);
                }
                return imageurl;
            }
        }
        public static List<Imagemodel> ShowAll()
        {
            Imagemodel imagemodel = new Imagemodel();
            List<Imagemodel> imagemodels  = new List<Imagemodel>();
            using (SqliteConnection db=new SqliteConnection("Filename=images1.db"))
            {
                db.Open();
                SqliteCommand sqliteCommand = new SqliteCommand();
                sqliteCommand.Connection = db;
                sqliteCommand.CommandText = "select * from Image";
                SqliteDataReader reader = sqliteCommand.ExecuteReader();
                while (reader.Read())
                {
                    imagemodel.imageId = reader.GetInt16(0);
                    imagemodel.CoverImage = reader.GetString(1);
                    imagemodel.Title = reader.GetString(2);
                    imagemodel.Author = reader.GetString(3);
                    imagemodels.Add(imagemodel);
                }
                db.Close();
            }
            return imagemodels;
        }
    }
}
