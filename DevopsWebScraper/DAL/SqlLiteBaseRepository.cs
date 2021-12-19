using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DevopsWebScraper.DAL
{
    class SqliteBaseRepository
    {
        public SqliteBaseRepository()
        {
        }
        //maakt connectie met db
        public static SqliteConnection DbConnectionFactory()
        {
            return new SqliteConnection(@"DataSource=WebscraperDB.sqlite");
        }
        // kijkt of db bestaat
        protected static bool DatabaseExists()
        {
            return File.Exists(@"WebscraperDB.sqlite");
        }
        // database aanmaken
        protected static void CreateDatabase()
        {
            using (var connection = DbConnectionFactory())
            {
                connection.Open();
                connection.Execute(
                    @"CREATE TABLE YouTubeVideo
                    (
                    Id                      INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title                   VARCHAR(200),
                    Uploader                VARCHAR(50),
                    Views                   VARCHAR(50),
                    Link                    VARCHAR(300)
                    )"
                    );
            }
        }
    }
}
