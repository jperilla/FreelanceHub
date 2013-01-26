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
        public string EmailAddress { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Please enter your password")]
        public string PasswordText { get; set; }

        public bool RememberMe { get; set; }
    }
}