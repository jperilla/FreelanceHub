using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Griffin.MvcContrib.Providers.Membership;

namespace Web.Factories
{
    public static class PasswordPolicyFactory
    {
        public static IPasswordPolicy CreatePasswordPolicy()
        {
            return new PasswordPolicy
            {
                IsPasswordQuestionRequired = false,
                IsPasswordResetEnabled = true,
                IsPasswordRetrievalEnabled = false,
                MaxInvalidPasswordAttempts = 10,
                MinRequiredNonAlphanumericCharacters = 0,
                PasswordAttemptWindow = 10,
                PasswordMinimumLength = 6,
                PasswordStrengthRegularExpression = null
            };
        }
    }
}