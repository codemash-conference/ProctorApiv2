using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProctorApiv2.ViewModels.SessionizeSpeaker
{
    public class Session
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Speaker
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string bio { get; set; }
        public string tagLine { get; set; }
        public string profilePicture { get; set; }
        public List<Session> sessions { get; set; }
        public bool isTopSpeaker { get; set; }
        public List<Link> links { get; set; }
    }

    public class Link
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string LinkType { get; set; }
    }
}