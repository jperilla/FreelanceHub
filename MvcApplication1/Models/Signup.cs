using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Raven.Imports.Newtonsoft.Json;

namespace Web.Model
{
    public class Signup
    {
        [Required(ErrorMessage = "* Email Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Name Required")]
        public string Name { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "* Password Required")]
        public string Password { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "* Confirm Password")]
        public string ConfirmPassword { get; set; }

        public string Plan { get; set; }
    }
}