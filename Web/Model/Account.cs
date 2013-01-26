using System.Security.Principal;
using System.ComponentModel.DataAnnotations;
using Raven.Client;
using Raven.Client.Linq;
using Raven.Imports.Newtonsoft.Json;

namespace Web.Model
{
    public partial class Account
    {
        #region Constants

        public const string BASIC_MONTHLY_PLAN_URL = "https://geekgirlsoftware.chargify.com/h/3283078/subscriptions/new";
        public const string BASIC_YEARLY_PLAN_URL = "https://geekgirlsoftware.chargify.com/h/3283079/subscriptions/new";

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

        //public AuthenicationUser User { get; set; }

        public string Plan { get; set; }

        #endregion

        #region Public Methods

        public static Account GetById(IDocumentSession session, string id)
        {
            return session.Load<Account>(id);
        }

        public bool IsAccountCurrent()
        {
            bool isCurrent = false;
            Chargify chargify = new Chargify();
            if(chargify.IsCustomerSubscriptionCurrent(this.Id))
                isCurrent = true;
            return isCurrent;
        }

        #endregion

        

    }
}