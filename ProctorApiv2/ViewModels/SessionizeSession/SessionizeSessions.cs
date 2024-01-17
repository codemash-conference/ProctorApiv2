using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProctorApiv2.ViewModels.SessionizeSession
{
    public class QuestionAnswer
    {
        public int id { get; set; }
        public string question { get; set; }
        public string questionType { get; set; }
        public string answer { get; set; }
        public int sort { get; set; }
        public object answerExtra { get; set; }
    }

    public class Speaker
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<CategoryItem> categoryItems { get; set; }
        public int sort { get; set; }
    }

    public class CategoryItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class Session
    {
        public List<QuestionAnswer> questionAnswers { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime startsAt { get; set; }
        public DateTime endsAt { get; set; }
        public bool isServiceSession { get; set; }
        public bool isPlenumSession { get; set; }
        public List<Speaker> speakers { get; set; }
        public List<Category> categories { get; set; }
        public int roomId { get; set; }
        public string room { get; set; }
    }

    public class RootObject
    {
        public object groupId { get; set; }
        public string groupName { get; set; }
        public List<Session> sessions { get; set; }
    }
}