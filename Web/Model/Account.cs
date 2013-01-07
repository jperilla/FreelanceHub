using System.Security.Principal;
using Newtonsoft.Json;
using Raven.Bundles.Authentication;
using System.ComponentModel.DataAnnotations;
using Raven.Client;
using Raven.Client.Linq;

namespace Web.Model
{
    public partial class Account
    {
        #region Properties

        [Required(ErrorMessage = "Required")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Required")]
        public string ConfirmPassword { get; set; }


        public AuthenticationUser User { get; set; }

        public bool RemeberMe { get; set; }

        #endregion

        #region Public Methods

        public static Account GetById(IDocumentSession session, string id)
        {
            return session.Load<Account>(id);
        }

        #endregion

        

    }
}