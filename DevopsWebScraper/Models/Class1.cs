using System;
using System.Collections.Generic;
using System.Text;

namespace DevopsWebScraper.Models
{
    internal class YouTubeVideo : IEquatable<YouTubeVideo>, IComparable<YouTubeVideo>
    {
        

        public string Title { get; set; }
        public string Uploader{ get; set; }
        public string Views { get; set; }
        public string Link { get; set; }

        public YouTubeVideo()
        {
        }

        public YouTubeVideo(string title, string uploader, string views, string link)
        {
            Title = title;
            Uploader = uploader;
            Views = views;
            Link = link;
        }

        public bool Equals(YouTubeVideo other)
        {
            return Link == other.Link;
        }

        public int CompareTo(YouTubeVideo other)
        {
            // compares link in db to current link
            return Link.CompareTo(other.Link);
        }

    }
    
    
}
