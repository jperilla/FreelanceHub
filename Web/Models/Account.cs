using System.Security.Principal;
using System.ComponentModel.DataAnnotations;
using Raven.Client;
using Raven.Client.Linq;
using ChargifyNET;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using Griffin.MvcContrib.Providers.Membership;
using Raven.Imports.Newtonsoft.Json;
using System;

namespace Web.Models
{
    public partial class Account
    {
        #region Constants
        
        public static readonly string BUDGET_MONTHLY_PLAN_URL = ConfigurationManager.AppSettings["ChargifyBudgetMonthlySubscriptionUrl"];
        public static readonly string BUDGET_YEARLY_PLAN_URL = ConfigurationManager.AppSettings["ChargifyBudgetYearlySubscriptionUrl"]; 
        public static readonly string FREELANCER_MONTHLY_PLAN_URL = ConfigurationManager.AppSettings["ChargifyFreelancerMonthlySubscriptionUrl"];
        public static readonly string FREELANCER_YEARLY_PLAN_URL = ConfigurationManager.AppSettings["ChargifyFreelancerYearlySubscriptionUrl"]; 
        public static readonly string AGENCY_MONTHLY_PLAN_URL = ConfigurationManager.AppSettings["ChargifyAgencyMonthlySubscriptionUrl"];
        public static readonly string AGENCY_YEARLY_PLAN_URL = ConfigurationManager.AppSettings["ChargifyAgencyYearlySubscriptionUrl"];


        #endregion

        #region Properties
        public int Id { get; set; }
        public string Email { get; set; }
       
        public IList<Job> Jobs { get; set; }
        public IList<Search> Searches { get; set; }
        // Custom Search

        [JsonIgnore]
        public ICustomer ChargifyCustomer { get; private set; }
        [JsonIgnore]
        public IDictionary<int, ISubscription> CustomerSubscriptions { get; private set; }


        [JsonIgnore]
        public string CustomerFullName
        {
            get
            {
                string fullName = null;
                if (ChargifyCustomer == null)
                    LoadChargifyInfo();
                fullName = ChargifyCustomer.FullName;
                return fullName;
            }
        }


        [JsonIgnore]
        public string CustomerFirstName
        {
            get
            {
                string name = null;
                if (ChargifyCustomer == null)
                    LoadChargifyInfo();
                name = ChargifyCustomer.FirstName;
                return name;
            }
        }


        [JsonIgnore]
        public string CustomerSubscriptionProductName
        {
            get
            {
                string subName = null;
                if (CustomerSubscriptions == null)
                    LoadChargifyInfo();
                
                subName = CustomerSubscriptions.Values.First().Product.Name;

                return subName;
            }
        }


        [JsonIgnore]
        public string CustomerSubscriptionStatus
        {
            get
            {
                string status = null;
                if (CustomerSubscriptions == null)
                    LoadChargifyInfo();

                status = CustomerSubscriptions.Values.First().State.ToString();

                return status;
            }
        }


        [JsonIgnore]
        public string Statistics
        {
            get
            {  
                return ("You currently have " + Jobs.Count + " Jobs saved and " + Searches.Count + " Searches saved.");
            }
        }


        [JsonIgnore]
        public string CreditCard
        {
            get
            {
                string creditCard = null;
                if (CustomerSubscriptions == null)
                    LoadChargifyInfo();

                creditCard = CustomerSubscriptions.Values.First().CreditCard.FullNumber;

                return creditCard;
            }
        }


        [JsonIgnore]
        public string BalanceSummary
        {
            get
            {
                decimal balance;
                decimal nextCharge;
                DateTime nextChargeDate;
                if (CustomerSubscriptions == null)
                    LoadChargifyInfo();

                balance = CustomerSubscriptions.Values.First().Balance;
                nextCharge = CustomerSubscriptions.Values.First().Product.Price;
                nextChargeDate = CustomerSubscriptions.Values.First().CurrentPeriodEndsAt;

                return ("You're balance is $" + balance + 
                    ". You're next charge for $" + nextCharge + 
                    " will be made on " + nextChargeDate.ToShortDateString() + ".");
            }
        }

        private static ChargifyConnect Chargify;
        #endregion

        #region Public Methods

        public Account(string email)
        {
            Email = email;
            Jobs = new List<Job>();
            Searches = new List<Search>();

            // Create Chargify object
            Chargify = new ChargifyConnect();
            Chargify.apiKey = ConfigurationManager.AppSettings["ChargifyApiKey"];
            Chargify.Password = ConfigurationManager.AppSettings["ChargifyApiPassword"];
            Chargify.URL = ConfigurationManager.AppSettings["ChargifyUrl"];
            Chargify.SharedKey = ConfigurationManager.AppSettings["ChargifySharedKey"];
        }

        public void LoadChargifyInfo()
        {
            ChargifyCustomer = Chargify.LoadCustomer(Email);
            if(ChargifyCustomer != null)
                CustomerSubscriptions = Chargify.GetSubscriptionListForCustomer(ChargifyCustomer.ChargifyID);
        }

        public bool IsAccountCurrent()
        {
            bool isCurrent = false;
            ICustomer customer = Chargify.LoadCustomer(Email);
            if (customer == null)
                return false;

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

       

        public static bool IsAccountAtLimit(string email)
        {
            // TODO: check if account is at based on current subscription, number of saved searches,
            // jobs, etc, if so redirect to Account Page to Upgrade
            return false;
        }

        public static Account GetAccount(string email, IDocumentSession session)
        {
            // Load Account
            IEnumerable<Account> accounts = from account in session.Query<Account>()
                                            where account.Email == email
                                            select account;

            // Check that the account is current in Chargify
            if (accounts != null &&
                accounts.Count() == 1)
                return accounts.FirstOrDefault();

            return null;
        }
 
        #endregion

        

    }
}