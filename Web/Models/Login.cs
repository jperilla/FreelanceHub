﻿using System.ComponentModel.DataAnnotations;
using Raven.Imports.Newtonsoft.Json;

namespace Web.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Please enter your email address.")]
        public string Email { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Please enter your password.")]
        public string Password { get; set; }
    }
}