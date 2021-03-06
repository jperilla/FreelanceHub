﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Raven.Imports.Newtonsoft.Json;

namespace Web.Models
{
    public class Signup : IValidatableObject
    {
        [Required(ErrorMessage = "Please enter your email.")]
        [RegularExpression("^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Invalid Email Adresss" )]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your name.")]
        public string Name { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Please enter your password.")]
        public string Password { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Please confirm your password.")]
        public string ConfirmPassword { get; set; }

        public string Plan { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password != ConfirmPassword)
            {
                yield return new ValidationResult("Passwords must match.", new[] { "ConfirmPassword" });
            }
        }
    }
}