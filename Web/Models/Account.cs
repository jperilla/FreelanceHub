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
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Xml;
using System.Web;

namespace Web.Models
{
    public partial class Account
    {
        #region Constants

        public static readonly string BASE_URL = ConfigurationManager.AppSettings["ChargifyUrl"];

        public static readonly string FREE_PLAN_URL = ConfigurationManager.AppSettings["ChargifyFreeSubscriptionUrl"];
        public static readonly string BUDGET_MONTHLY_PLAN_URL = ConfigurationManager.AppSettings["ChargifyBudgetMonthlySubscriptionUrl"];
        public static readonly string BUDGET_YEARLY_PLAN_URL = ConfigurationManager.AppSettings["ChargifyBudgetYearlySubscriptionUrl"];
        public static readonly string FREELANCER_MONTHLY_PLAN_URL = ConfigurationManager.AppSettings["ChargifyFreelancerMonthlySubscriptionUrl"];
        public static readonly string FREELANCER_YEARLY_PLAN_URL = ConfigurationManager.AppSettings["ChargifyFreelancerYearlySubscriptionUrl"]; 
        public static readonly string AGENCY_MONTHLY_PLAN_URL = ConfigurationManager.AppSettings["ChargifyAgencyMonthlySubscriptionUrl"];
        public static readonly string AGENCY_YEARLY_PLAN_URL = ConfigurationManager.AppSettings["ChargifyAgencyYearlySubscriptionUrl"];
        public static readonly string UPDATE_PAYMENT_SHORTNAME = "update_payment";
        public static readonly string FREE_PLAN_HANDLE = ConfigurationManager.AppSettings["ChargifyFreePlanHandle"];
        public static readonly string BUDGET_MONTHLY_PLAN_HANDLE = ConfigurationManager.AppSettings["ChargifyBudgetMonthlyPlanHandle"];
        public static readonly string FREELANCER_MONTHLY_PLAN_HANDLE = ConfigurationManager.AppSettings["ChargifyFreelancerMonthlyPlanHandle"];
        public static readonly string AGENCY_MONTHLY_PLAN_HANDLE = ConfigurationManager.AppSettings["ChargifyAgencyMonthlyPlanHandle"];
        public static readonly string APP_BASE_URL = ConfigurationManager.AppSettings["applicationBasePath"];

        #endregion

        #region Properties

        public int Id { get; set; }
        public string Email { get; set; }
        public string PathToGoogleCseFile { get; set; }
        public IList<Job> Jobs { get; set; }
        public IList<string> Searches { get; set; }
        public IList<string> SitesToSearch { get; set; }

        [JsonIgnore]
        public string SitesToSearchString { get; set; }

        [JsonIgnore]
        public ICustomer ChargifyCustomer { get; private set; }

        private IDictionary<int, ISubscription> _customerSubscriptions;

        [JsonIgnore]
        public IDictionary<int, ISubscription> CustomerSubscriptions
        {
            get
            {
                if (_customerSubscriptions == null)
                    LoadChargifyInfo();
                
                return _customerSubscriptions;
            }
        }


        [JsonIgnore]
        public string CustomerFullName
        {
            get
            {
                string fullName = null;
                if (ChargifyCustomer == null)
                    LoadChargifyInfo();

                if(ChargifyCustomer != null)
                    fullName = ChargifyCustomer.FirstName + " " + ChargifyCustomer.LastName;

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

                if (ChargifyCustomer != null)
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
                if (_customerSubscriptions == null)
                    LoadChargifyInfo();

                if ((_customerSubscriptions != null) && (_customerSubscriptions.Count > 0))
                    subName = _customerSubscriptions.Values.First().Product.Name;
                else
                    subName = "You have no current subscriptions.";

                return subName;
            }
        }


        [JsonIgnore]
        public string CustomerSubscriptionStatus
        {
            get
            {
                string status = null;
                if (_customerSubscriptions == null)
                    LoadChargifyInfo();

                if ((_customerSubscriptions != null) && (_customerSubscriptions.Count > 0))
                    status = _customerSubscriptions.Values.First().State.ToString();
                else
                {
                    status = "Inactive";
                }

                return status;
            }
        }

        [JsonIgnore]
        public string UpdatePaymentLink
        {
            get
            {
                string link = null;

                if (_customerSubscriptions == null)
                    LoadChargifyInfo();

                if ((_customerSubscriptions != null) && (_customerSubscriptions.Count > 0))
                    link = BASE_URL + UPDATE_PAYMENT_SHORTNAME + "/"
                        + _customerSubscriptions.Values.First().SubscriptionID + "/"
                        + GenerateToken(UPDATE_PAYMENT_SHORTNAME, _customerSubscriptions.Values.First().SubscriptionID).Substring(0, 10);

                return link;
            }
        }

        private string GenerateToken(string shortName, int resourceId)
        {
            string value = shortName + "--" + resourceId + "--" + Chargify.SharedKey;
            var data = System.Text.Encoding.ASCII.GetBytes(value);
            data = System.Security.Cryptography.SHA1.Create().ComputeHash(data);
            return ByteArrayToString(data);
        }

        private static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba).ToLower();
            return hex.Replace("-", "");
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
                if (_customerSubscriptions == null)
                    LoadChargifyInfo();

                if ((_customerSubscriptions != null) 
                    && (_customerSubscriptions.Count > 0)
                    && _customerSubscriptions.Values.First().CreditCard != null)
                    creditCard = _customerSubscriptions.Values.First().CreditCard.FullNumber;
                else
                    creditCard = "None Saved";

                return creditCard;
            }
        }


        [JsonIgnore]
        public string BalanceSummary
        {
            get
            {
                string returnString = string.Empty;
                decimal balance = 0.0M;
                decimal nextCharge = 0.0M;
                DateTime nextChargeDate = DateTime.Now;
                if (_customerSubscriptions == null)
                    LoadChargifyInfo();

                if ((_customerSubscriptions != null) && (_customerSubscriptions.Count > 0))
                {
                    ISubscription subscription = _customerSubscriptions.Values.First();

                    if (subscription.Product.Handle == Account.FREE_PLAN_HANDLE)
                    {
                        returnString = "You're on the FREE plan. You're balance is $0.";
                    }
                    else
                    {
                        balance = subscription.Balance;
                        nextCharge = subscription.Product.Price;
                        nextChargeDate = subscription.CurrentPeriodEndsAt;


                        returnString = ("You're balance is $" + balance + "." + ((_customerSubscriptions == null ||
                                                                _customerSubscriptions.Count == 0 ||
                                                                _customerSubscriptions.Values.First().CurrentPeriodEndsAt == null
                                                                || PendingCancellationOn != null) ? "" :
                        ". You're next charge for $" + nextCharge +
                        " will be made on " + nextChargeDate.ToShortDateString() + "."));
                    }
                }

                return returnString;
            }
        }
        
        [JsonIgnore]
        public string PendingCancellationOn
        {
            get
            {
                string delayedCancelDate = null;
                if (_customerSubscriptions == null)
                    LoadChargifyInfo();

                if ((_customerSubscriptions != null) && (_customerSubscriptions.Count > 0))
                {
                    if (_customerSubscriptions.Values.First().DelayedCancelAt.Year != 0001)
                        delayedCancelDate = _customerSubscriptions.Values.First().DelayedCancelAt.ToShortDateString();
                }

                return delayedCancelDate;
            }
        }

        [JsonIgnore]
        public static ChargifyConnect Chargify;

        #endregion

        #region Public Methods

        public Account()
        {
            Jobs = new List<Job>();
            Searches = new List<string>();
            SitesToSearch = new List<string>();

            // Initially all customers use the same cse until they customize
            PathToGoogleCseFile = Account.APP_BASE_URL + "/Content/xml/cse.xml";

            // Create Chargify object
            Chargify = new ChargifyConnect();
            Chargify.apiKey = ConfigurationManager.AppSettings["ChargifyApiKey"];
            Chargify.Password = ConfigurationManager.AppSettings["ChargifyApiPassword"];
            Chargify.URL = ConfigurationManager.AppSettings["ChargifyUrl"];
            Chargify.SharedKey = ConfigurationManager.AppSettings["ChargifySharedKey"];

        }

        public Account(string email)
        {
            Email = email;
            Jobs = new List<Job>();
            Searches = new List<string>();
            SitesToSearch = new List<string>();

            // Initially all customers use the same cse until they customize
            PathToGoogleCseFile = Account.APP_BASE_URL + "/Content/xml/cse.xml";

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
                _customerSubscriptions = Chargify.GetSubscriptionListForCustomer(ChargifyCustomer.ChargifyID);
        }

        public bool HasJob(string url)
        {
            bool hasJob = false;

            var jobs = from j in Jobs 
                       where j.URL == url 
                       select j;
            if (jobs != null && jobs.Count() > 0)
                hasJob = true;

            return hasJob;
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
                // Is the customer in a live state?
                 if (subscription.Value.State == SubscriptionState.Active
                    || subscription.Value.State == SubscriptionState.Trialing
                    || subscription.Value.State == SubscriptionState.Assessing)
                 {
                    isCurrent = true;
                    break;
                }
            }           
           
            return isCurrent;
        }

        public void GenerateCseFile(IDocumentSession session)
        {
            // Generate user's file path
            PathToGoogleCseFile = Account.APP_BASE_URL + "/Content/xml/" + this.Email.Replace('.', '_').Replace('@', '_') + "_cse.xml";

            var googleCse = new GoogleCustomizations();
            googleCse.CustomSearchEngine = new GoogleCustomizationsCustomSearchEngine();
            googleCse.CustomSearchEngine.language = "en";
            googleCse.CustomSearchEngine.id = "dl-lwlywdoi";
            googleCse.CustomSearchEngine.creator = 013914245334065375847;
            googleCse.CustomSearchEngine.encoding = "UTF-8";
            googleCse.CustomSearchEngine.enable_suggest = true;
            googleCse.CustomSearchEngine.Title = "Freelancer";
            googleCse.CustomSearchEngine.Context = new GoogleCustomizationsCustomSearchEngineContext();
            googleCse.CustomSearchEngine.Context.BackgroundLabels = new GoogleCustomizationsCustomSearchEngineContextLabel[2];
            googleCse.CustomSearchEngine.Context.BackgroundLabels[0] = new GoogleCustomizationsCustomSearchEngineContextLabel();
            googleCse.CustomSearchEngine.Context.BackgroundLabels[0].mode = "FILTER";
            googleCse.CustomSearchEngine.Context.BackgroundLabels[0].name = "_cse_dl-lwlywdoi";
            googleCse.CustomSearchEngine.Context.BackgroundLabels[1] = new GoogleCustomizationsCustomSearchEngineContextLabel();
            googleCse.CustomSearchEngine.Context.BackgroundLabels[1].mode = "ELIMINATE";
            googleCse.CustomSearchEngine.Context.BackgroundLabels[1].name = "_cse_exclude_dl-lwlywdoi";
            googleCse.CustomSearchEngine.LookAndFeel = new GoogleCustomizationsCustomSearchEngineLookAndFeel();
            googleCse.CustomSearchEngine.LookAndFeel.ads_layout = 1;
            googleCse.CustomSearchEngine.LookAndFeel.promotion_url_length = "full";
            googleCse.CustomSearchEngine.LookAndFeel.enable_cse_thumbnail = true;
            googleCse.CustomSearchEngine.LookAndFeel.element_branding = "show";
            googleCse.CustomSearchEngine.LookAndFeel.url_length = "full";
            googleCse.CustomSearchEngine.LookAndFeel.text_font = "Arvo, serif";
            googleCse.CustomSearchEngine.LookAndFeel.custom_theme = true;
            googleCse.CustomSearchEngine.LookAndFeel.theme = 7;
            googleCse.CustomSearchEngine.LookAndFeel.element_layout = 8;
            googleCse.CustomSearchEngine.LookAndFeel.nonprofit = false;
            googleCse.CustomSearchEngine.LookAndFeel.Logo = new object();
            googleCse.CustomSearchEngine.LookAndFeel.Colors = new GoogleCustomizationsCustomSearchEngineLookAndFeelColors();
            googleCse.CustomSearchEngine.LookAndFeel.Colors.title = "#0000CC";
            googleCse.CustomSearchEngine.LookAndFeel.Colors.title_active="#0000CC";
            googleCse.CustomSearchEngine.LookAndFeel.Colors.title_hover="#0000CC";
            googleCse.CustomSearchEngine.LookAndFeel.Colors.visited="#0000CC";
            googleCse.CustomSearchEngine.LookAndFeel.Colors.text="#000000";
            googleCse.CustomSearchEngine.LookAndFeel.Colors.border="#FFFFFF";
            googleCse.CustomSearchEngine.LookAndFeel.Colors.background="#FFFFFF";
            googleCse.CustomSearchEngine.LookAndFeel.Colors.url = "#008000";
            googleCse.CustomSearchEngine.LookAndFeel.Promotions = new GoogleCustomizationsCustomSearchEngineLookAndFeelPromotions();
            googleCse.CustomSearchEngine.LookAndFeel.Promotions.title_active_color="#0000CC";
            googleCse.CustomSearchEngine.LookAndFeel.Promotions.title_hover_color="#0000CC";
            googleCse.CustomSearchEngine.LookAndFeel.Promotions.snippet_color="#000000";
            googleCse.CustomSearchEngine.LookAndFeel.Promotions.border_color="#336699";
            googleCse.CustomSearchEngine.LookAndFeel.Promotions.background_color="#FFFFFF";
            googleCse.CustomSearchEngine.LookAndFeel.Promotions.url_color="#008000";
            googleCse.CustomSearchEngine.LookAndFeel.Promotions.title_visited_color="#0000CC";
            googleCse.CustomSearchEngine.LookAndFeel.Promotions.title_color = "#0000CC";
            googleCse.CustomSearchEngine.LookAndFeel.SearchControls = new GoogleCustomizationsCustomSearchEngineLookAndFeelSearchControls();
            googleCse.CustomSearchEngine.LookAndFeel.SearchControls.tab_selected_background_color="#FFFFFF";
            googleCse.CustomSearchEngine.LookAndFeel.SearchControls.tab_selected_border_color="#FF9900";
            googleCse.CustomSearchEngine.LookAndFeel.SearchControls.tab_background_color="#E9E9E9";
            googleCse.CustomSearchEngine.LookAndFeel.SearchControls.tab_border_color="#E9E9E9";
            googleCse.CustomSearchEngine.LookAndFeel.SearchControls.button_background_color="#CECECE";
            googleCse.CustomSearchEngine.LookAndFeel.SearchControls.button_border_color="#666666";
            googleCse.CustomSearchEngine.LookAndFeel.SearchControls.input_border_color = "#D9D9D9";
            googleCse.CustomSearchEngine.LookAndFeel.Results = new GoogleCustomizationsCustomSearchEngineLookAndFeelResults();
            googleCse.CustomSearchEngine.LookAndFeel.Results.border_color = "#FFFFFF";
            googleCse.CustomSearchEngine.LookAndFeel.Results.background_color = "#FFFFFF";
            googleCse.CustomSearchEngine.LookAndFeel.Results.ads_border_color = "#FDF6E5";
            googleCse.CustomSearchEngine.LookAndFeel.Results.ads_background_color = "#FDF6E5";
            googleCse.CustomSearchEngine.LookAndFeel.Results.background_hover_color = "#FFFFFF";
            googleCse.CustomSearchEngine.LookAndFeel.Results.border_hover_color = "#FFFFFF";
            googleCse.CustomSearchEngine.AdSense = new object();
            googleCse.CustomSearchEngine.EnterpriseAccount = new GoogleCustomizationsCustomSearchEngineEnterpriseAccount();
            googleCse.CustomSearchEngine.EnterpriseAccount.AccountAdmin = new GoogleCustomizationsCustomSearchEngineEnterpriseAccountAccountAdmin();
            googleCse.CustomSearchEngine.EnterpriseAccount.AccountAdmin.job_title = "Owner";
            googleCse.CustomSearchEngine.EnterpriseAccount.AccountAdmin.country="US";
            googleCse.CustomSearchEngine.EnterpriseAccount.AccountAdmin.phone=9077482554;
            googleCse.CustomSearchEngine.EnterpriseAccount.AccountAdmin.email="julie@geekgirlsoftware.com";
            googleCse.CustomSearchEngine.EnterpriseAccount.AccountAdmin.last_name="Garcia";
            googleCse.CustomSearchEngine.EnterpriseAccount.AccountAdmin.first_name = "Julie";
            googleCse.CustomSearchEngine.EnterpriseAccount.Organization = new GoogleCustomizationsCustomSearchEngineEnterpriseAccountOrganization();
            googleCse.CustomSearchEngine.EnterpriseAccount.Organization.name="Geek Girl Software";
            googleCse.CustomSearchEngine.EnterpriseAccount.Organization.size=20;
            googleCse.CustomSearchEngine.EnterpriseAccount.Organization.type = "business";
            googleCse.CustomSearchEngine.ImageSearchSettings = new GoogleCustomizationsCustomSearchEngineImageSearchSettings();
            googleCse.CustomSearchEngine.ImageSearchSettings.enable = false;
            googleCse.CustomSearchEngine.autocomplete_settings = new object();
            googleCse.CustomSearchEngine.sort_by_keys = new GoogleCustomizationsCustomSearchEngineSort_by_keys[1];
            googleCse.CustomSearchEngine.sort_by_keys[0] = new GoogleCustomizationsCustomSearchEngineSort_by_keys();
            googleCse.CustomSearchEngine.sort_by_keys[0].key = "date";
            googleCse.CustomSearchEngine.sort_by_keys[0].label = "Date";
            googleCse.CustomSearchEngine.Annotations = new GoogleCustomizationsCustomSearchEngineAnnotations();
            googleCse.CustomSearchEngine.Annotations.Annotation = new GoogleCustomizationsCustomSearchEngineAnnotationsAnnotation[this.SitesToSearch.Count()];
            googleCse.CustomSearchEngine.Annotations.total = Convert.ToByte(this.SitesToSearch.Count());
            googleCse.CustomSearchEngine.Annotations.num = Convert.ToByte(this.SitesToSearch.Count());
            googleCse.CustomSearchEngine.Annotations.start = 0;

            int i = 0;
            foreach (var siteString in this.SitesToSearch)
            {
                // Load the CustomSearchSite element from database
                CustomSearchSite site = session.Load<CustomSearchSite>(siteString);

                // Add an annotation for each site
                var annotation = new GoogleCustomizationsCustomSearchEngineAnnotationsAnnotation();
                annotation.about = site.Url.Replace("https://", "").Replace("http://", "");
                annotation.Label = new GoogleCustomizationsCustomSearchEngineAnnotationsAnnotationLabel[1];
                annotation.Label[0] = new GoogleCustomizationsCustomSearchEngineAnnotationsAnnotationLabel();
                annotation.Label[0].name = googleCse.CustomSearchEngine.Context.BackgroundLabels[0].name; //include
                //annotation.AdditionalData = new GoogleCustomizationsCustomSearchEngineAnnotationsAnnotationAdditionalData();
                //annotation.AdditionalData.attribute = "original_url";
                //annotation.AdditionalData.value = site.Url;
                googleCse.CustomSearchEngine.Annotations.Annotation[i++] = annotation;
            }
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(googleCse.GetType());
            using (FileStream stream = new FileStream(HttpContext.Current.Server.MapPath("~/Content/xml/" + this.Email.Replace('.', '_').Replace('@', '_') + "_cse.xml"),
                                                FileMode.Create, FileAccess.ReadWrite))
            {
                x.Serialize(stream, googleCse);
            }
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
                accounts.Count() > 0)
            {
                var account = accounts.FirstOrDefault();
                account.LoadSitesToSearchString(session);
                return account;
            }

            return null;
        }
 
        #endregion

        #region Private Methods

        private void LoadSitesToSearchString(IDocumentSession session)
        {
            StringBuilder sitesToSearch = new StringBuilder();
            foreach (var site in this.SitesToSearch)
            {
                CustomSearchSite customSite = session.Load<CustomSearchSite>(site);
                string shortUrl;
                if (customSite != null && !string.IsNullOrEmpty(customSite.Url) && customSite.Url.IndexOf('/') != -1)
                {
                    shortUrl = customSite.Url.Substring(0, customSite.Url.IndexOf('/'));
                }
                else
                {
                    shortUrl = customSite.Url;
                }

                if (!string.IsNullOrEmpty(shortUrl) && shortUrl.Contains("*."))
                {
                    shortUrl = shortUrl.Remove(shortUrl.IndexOf("*"), 2);
                }

                sitesToSearch.Append("site:" + shortUrl + " OR ");
            }

            if (sitesToSearch.Length > 0)
            {
                sitesToSearch.Remove(sitesToSearch.Length - 3, 3);
                SitesToSearchString = sitesToSearch.ToString();
            }
            else
            {
                SitesToSearchString = string.Empty;
            }
        }


        #endregion

    }
}