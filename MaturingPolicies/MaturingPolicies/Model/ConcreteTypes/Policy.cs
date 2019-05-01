using MaturingPolicies.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaturingPolicies.Model.ConcreteTypes
{
    public class Policy
    {
        public Policy()
        {

        }
        public Policy(string policyDetails)
        {
            DataLoadHelper dataLoadHelper = new DataLoadHelper();
            PolicyHelper policyHelper = new PolicyHelper();

            var detail = policyDetails.Split(",");

            ValidPolicy = policyHelper.ValidPolicyNumber(detail[0]);
            if (ValidPolicy)
            {
                PolicyNumber = detail[0];
                PolicyStartDate = dataLoadHelper.GetDateStarted(detail[1]);
                Premiums = dataLoadHelper.GetIntValueFromString(detail[2]);
                Membership = dataLoadHelper.GetMembership(detail[3]);
                DiscretionaryBonus = dataLoadHelper.GetIntValueFromString(detail[4]);
                UpliftPercentage = dataLoadHelper.GetDecimalFromString(detail[5]);
            }
        }

        public bool ValidPolicy { get; set; }
        public int ManagementFee { get; set; }
        public bool EligableForBonus { get; set; }

        public string PolicyNumber { get; set; }
        public DateTime PolicyStartDate { get; set; }
        public int Premiums { get; set; }
        public bool Membership { get; set; }
        public int DiscretionaryBonus { get; set; }
        public decimal UpliftPercentage { get; set; }
    }
}
