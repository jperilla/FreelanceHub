using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChargifyNET;

namespace Web.Model
{
    public class Chargify
    {
        public bool IsCustomerSubscriptionCurrent(string accountId)
        {
            bool isCurrent = false;
            ICustomer customer = this.LoadCustomer(accountId);
            IDictionary<int, ISubscription> customerSubscriptions = this.GetSubscriptionListForCustomer(customer.ChargifyID);
            return isCurrent;
        }

        public string GetSubscriptionUpdateLink(string firstName, string lastName, int subscriptionId)
        {
            return this.GetPrettySubscriptionUpdateURL(firstName, lastName, subscriptionId);
        }
 
    }
}