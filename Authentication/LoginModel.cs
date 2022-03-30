﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreAPI.Authentication
{
    public class LoginModel
    {
        /* this is a login class */
        [Required(ErrorMessage = "Username is required")]
        public string Username {get;set;}

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
