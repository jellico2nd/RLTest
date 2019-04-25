using MaturingPolicies.Model.ConcreteTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaturingPolicies.Helpers
{
    public class PolicyHelper
    {
        List<string> ValidTypes = new List<string> { "A", "B", "C" };
        int[] mgmtFees = { 3, 5, 7 };

        public bool ValidPolicyNumber(string policyNumber)
        {
            if (ValidTypes.Contains(policyNumber.Substring(0, 1).ToUpper()))
            {
                return true;
            }
            return false;
        }

        public void SetPolicyType(Policy policy)
        {
            var tempPolicyType = policy.PolicyNumber.Substring(0, 1).ToUpper();
            if (policy.ValidPolicy)
            {
                switch (tempPolicyType)
                {
                    case "A":
                        policy.ManagementFee = mgmtFees[0];
                        if(policy.PolicyStartDate < new DateTime(1990, 1, 1))
                        {
                            policy.EligableForBonus = true;
                        }
                        else
                        {
                            policy.EligableForBonus = false;
                        }
                        break;
                    case "B":
                        policy.ManagementFee = mgmtFees[1];
                        if (policy.Membership)
                        {
                            policy.EligableForBonus = true;
                        }
                        else
                        {
                            policy.EligableForBonus = false;
                        }
                        break;
                    case "C":
                        policy.ManagementFee = mgmtFees[2];
                        if (policy.PolicyStartDate <= new DateTime(1990, 1, 1) && policy.Membership)
                        {
                            policy.EligableForBonus = true;
                        }
                        else
                        {
                            policy.EligableForBonus = false;
                        }
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
