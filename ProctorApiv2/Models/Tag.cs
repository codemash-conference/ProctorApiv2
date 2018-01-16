using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProctorApiv2.Models
{
    public class Tag
    {
        public Tag()
        {
            Sessions = new List<Session>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Id")]
        public List<Session> Sessions { get; set; }
    }
}