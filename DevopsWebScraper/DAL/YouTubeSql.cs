using Dapper;
using Microsoft.Data.Sqlite;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using DevopsWebScraper.Models;

namespace DevopsWebScraper.DAL
{
    class YouTubeSql : SqliteBaseRepository
    {
        public YouTubeSql()
        {
            // kijken of db bestaat
            if (!DatabaseExists())
            {
                CreateDatabase();
            }
        }

        // dit is de insert voor dapper
        public static void InsertYouTubeVideo(YouTubeVideo video)
        {
            string sqlString = "INSERT INTO YouTubeVideo (Title, Uploader, Views, Link) VALUES (@Title, @Uploader, @Views, @Link);";

            using (SqliteConnection connection = DbConnectionFactory())
            {
                connection.Open();
                connection.Execute(sqlString, video);
            }
        }
        // get videos
        public IEnumerable<YouTubeVideo> GetYouTubeVideos()
        {
            string youtubeSql = "SELECT * FROM YouTubeVideo;";

            using (SqliteConnection connection = DbConnectionFactory())
            {
                return connection.Query<YouTubeVideo>(youtubeSql);
            }
        }
        // delete video
        public static void DeleteYouTubeVideo()
        {
            string youtubeSql = "DELETE FROM YouTubeVideo;";
            using (SqliteConnection connection = DbConnectionFactory())
            {
                connection.Query<YouTubeVideo>(youtubeSql);
            }
        }
    }
}
