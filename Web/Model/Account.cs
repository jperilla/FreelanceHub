using System.Security.Principal;
using System.ComponentModel.DataAnnotations;
using Raven.Client;
using Raven.Client.Linq;
using Raven.Imports.Newtonsoft.Json;
using ChargifyNET;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Web.Model
{
    public partial class Account
    {
        #region Constants
        
        public static readonly string BASIC_MONTHLY_PLAN_URL = ConfigurationManager.AppSettings["ChargifyFreelancerMonthlySubscriptionUrl"];
        public static readonly string BASIC_YEARLY_PLAN_URL = ConfigurationManager.AppSettings["ChargifyFreelancerYearlySubscriptionUrl"];

        #endregion

        #region Properties
        [Required(ErrorMessage = "* Email Required")]
        public string Id { get; set; }

        [Required(ErrorMessage = "* Name Required")]
        public string Name { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "* Password Required")]
        public string Password { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "* Confirm Password")]
        public string ConfirmPassword { get; set; }

        public bool RememberMe { get; set; }

        public string Plan { get; set; }

        [JsonIgnore]
        private static ChargifyConnect Chargify { get; set; }

        #endregion

        #region Public Methods

        public Account()
        {
            // Create Chargify object
            Chargify = new ChargifyConnect();
            Chargify.apiKey = ConfigurationManager.AppSettings["ChargifyApiKey"];
            Chargify.Password = ConfigurationManager.AppSettings["ChargifyApiPassword"];
            Chargify.URL = ConfigurationManager.AppSettings["ChargifyUrl"];
            Chargify.SharedKey = ConfigurationManager.AppSettings["ChargifySharedKey"];
        }

        public static Account GetById(IDocumentSession session, string id)
        {
            return session.Load<Account>(id);
        }

        public bool IsAccountCurrent()
        {
            bool isCurrent = false;
            ICustomer customer = Chargify.LoadCustomer(Id);
            IDictionary<int, ISubscription> customerSubscriptions = Chargify.GetSubscriptionListForCustomer(customer.ChargifyID);
            // Does this customer have any current subscriptions? TODO: needs more work, add other states, can a customer have more than one subscription (loop), what plan, set role
            foreach (KeyValuePair<int,ISubscription> subscription in customerSubscriptions)
            {
                // Is the customer active or trialing?
                 if (subscription.Value.State == SubscriptionState.Active
                    || subscription.Value.State == SubscriptionState.Trialing)
                 {
                    isCurrent = true;
                    break;
                }
            }           
           
            return isCurrent;
        }

        #endregion

        

    }
}