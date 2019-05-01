using MaturingPolicies.Model.ConcreteTypes;

namespace MaturingPolicies
{
    public class PolicyDTO
    {
        public PolicyDTO()
        {

        }
        public PolicyDTO(Policy policy)
        {
            PolicyNumber = policy.PolicyNumber;
            PolicyValue = new Helpers.PolicyHelper().MaturityValue(policy).ToString();
        }
        public string PolicyNumber { get; set; }
        public string PolicyValue { get; set; }
    }
}