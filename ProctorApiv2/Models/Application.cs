using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProctorApiv2.Models
{
    public class Application
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string School { get; set; }
        public string Major { get; set; }
        public string Topics { get; set; }
        public string Essay { get; set; }
        public string SubmitDate { get; set; }
        public bool FirstTimer { get; set; }
        public int HowManyYears { get; set; }
        public bool AcceptedByCodemash { get; set; }
        public bool AcceptedByApplicant { get; set; }
        public bool Registered { get; set; }
    }
}