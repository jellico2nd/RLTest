using MaturingPolicies.Model.ConcreteTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaturingPolicies.Helpers
{
    public class DataLoadHelper
    {
        internal bool GetMembership(string membership)
        {
            if(membership.ToLower() == "y")
            {
                return true;
            }
            return false;
        }

        internal decimal GetDecimalFromString(string value)
        {
            decimal result;
            decimal.TryParse(value, out result);

            return result;
        }

        internal int GetIntValueFromString(string value)
        {
            int result;
            int.TryParse(value, out result);

            return result;
        }

        internal DateTime GetDateStarted(string dateString)
        {
            DateTime result = new DateTime();
            DateTime.TryParse(dateString, out result);

            return result;
        }
    }
}
