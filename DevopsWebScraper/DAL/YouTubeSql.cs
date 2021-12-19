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
            if (!DatabaseExists())
            {
                CreateDatabase();
            }
        }

        public static void InsertYouTubeVideo(YouTubeVideo video)
        {
            string sqlString = "INSERT INTO YouTubeVideo (Title, Uploader, Views, Link) VALUES (@Title, @Uploader, @Views, @Link);";

            using (SqliteConnection connection = DbConnectionFactory())
            {
                connection.Open();
                connection.Execute(sqlString, video);
            }
        }
        public IEnumerable<YouTubeVideo> GetYouTubeVideos()
        {
            String youtubeSql = "DELETE FROM YouTubeVideo;";

            using (SqliteConnection connection = DbConnectionFactory())
            {
                return connection.Query<YouTubeVideo>(youtubeSql);
            }
        }

        public static void DeleteYouTubeVideo()
        {
            String youtubeSql = "DELETE FROM YouTubeVideo;";
            using (SqliteConnection connection = DbConnectionFactory())
            {
                connection.Query<YouTubeVideo>(youtubeSql);
            }
        }
    }
}
