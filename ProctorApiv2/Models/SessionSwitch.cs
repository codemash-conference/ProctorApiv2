using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProctorApiv2.Models
{
    public class SessionSwitch
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public int SessionId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? StatusChangeTime { get; set; }
        public string Status { get; set; }
        public int? RelatedSessionSwitchId { get; set; }
    }
}