﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProctorApiv2.ViewModels
{
    public class PasswordReset
    {
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        public string UserId { get; set; }
    }
}