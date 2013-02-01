using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using ChargifyNET;
using System.Configuration;

namespace Web.Model
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Email Address")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class SignupModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Plan { get; set; }
    }

    public partial class Account
    {
        #region Constants

        public static readonly string BASIC_MONTHLY_PLAN_URL = ConfigurationManager.AppSettings["ChargifyFreelancerMonthlySubscriptionUrl"];
        public static readonly string BASIC_YEARLY_PLAN_URL = ConfigurationManager.AppSettings["ChargifyFreelancerYearlySubscriptionUrl"];

        #endregion

        #region Properties

        public string Email { get; set; }
        
        private static ChargifyConnect Chargify { get; set; }

        #endregion

        #region Public Methods

        public Account(string email)
        {
            Email = email;

            // Create Chargify object
            Chargify = new ChargifyConnect();
            Chargify.apiKey = ConfigurationManager.AppSettings["ChargifyApiKey"];
            Chargify.Password = ConfigurationManager.AppSettings["ChargifyApiPassword"];
            Chargify.URL = ConfigurationManager.AppSettings["ChargifyUrl"];
            Chargify.SharedKey = ConfigurationManager.AppSettings["ChargifySharedKey"];
        }

        public bool IsSubscriptionActive()
        {
            bool IsActive = false;
            ICustomer customer = Chargify.LoadCustomer(Email);
            if (null != customer)
            {
                IDictionary<int, ISubscription> customerSubscriptions = Chargify.GetSubscriptionListForCustomer(customer.ChargifyID);
                // Does this customer have any current subscriptions? TODO: needs more work, add other states, can a customer have more than one subscription (loop), what plan, set role
                foreach (KeyValuePair<int, ISubscription> subscription in customerSubscriptions)
                {
                    // Is the customer active or trialing?
                    if (subscription.Value.State == SubscriptionState.Active
                       || subscription.Value.State == SubscriptionState.Trialing)
                    {
                        IsActive = true;
                        break;
                    }
                }
            }

            return IsActive;
        }

        #endregion
    }
}
