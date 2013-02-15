using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Raven.Imports.Newtonsoft.Json;

namespace Web.Model
{
    public class Login
    {
        [Required(ErrorMessage = "Please enter your email address")]
        public string Email { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
    }
}