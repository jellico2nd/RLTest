using MaturingPolicies.Model.ConcreteTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaturingPolicies.Helpers
{
    public class PolicyHelper
    {
        Dictionary<string, int> ValidTypesWithFees = new Dictionary<string, int>()
        {
            {"A", 3 },
            {"B", 5 },
            {"C", 7 }
        };

        public bool ValidPolicyNumber(string policyNumber)
        {
            if (ValidTypesWithFees.ContainsKey(policyNumber.Substring(0, 1).ToUpper()))
            {
                return true;
            }
            return false;
        }

        public void SetPolicyType(Policy policy)
        {
            var tempPolicyType = policy.PolicyNumber.Substring(0, 1).ToUpper();
            int fee;
            if (policy.ValidPolicy)
            {
                ValidTypesWithFees.TryGetValue(tempPolicyType, out fee);
                policy.ManagementFee = fee;
                switch (tempPolicyType)
                {
                    case "A":
                        policy.EligableForBonus = policy.PolicyStartDate < new DateTime(1990, 1, 1);
                        break;
                    case "B":
                        policy.EligableForBonus = policy.Membership;
                        break;
                    case "C":
                        policy.EligableForBonus = (policy.PolicyStartDate <= new DateTime(1990, 1, 1) && policy.Membership);
                        break;
                    default:
                        break;
                }
            }
        }

        public decimal MaturityValue(Policy policy)
        {
            decimal uplift = 1 + (policy.UpliftPercentage / 100);
            int bonus = 0;
            if (policy.EligableForBonus)
            {
                bonus = policy.DiscretionaryBonus;
            }
            decimal mgmtFeeValue = ((decimal)policy.ManagementFee / 100);
            var result = (policy.Premiums - (policy.Premiums * mgmtFeeValue) + bonus) * uplift;
            return result;
        }
    }
}
