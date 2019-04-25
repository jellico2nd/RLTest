using MaturingPolicies.Helpers;
using MaturingPolicies.Model.ConcreteTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace MaturingPolicies.Output
{
    public class OutputXMLFile
    {
        public OutputXMLFile(List<Policy> policies, PolicyHelper policyHelper)
        {
            myPolicies = policies;
            myPolicyHelper = policyHelper;
        }

        private readonly List<Policy> myPolicies;
        private PolicyHelper myPolicyHelper;
        private XmlDocument xml = new XmlDocument();

        public void CreateOutputFile()
        {
            XmlDeclaration xmlDeclaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = xml.DocumentElement;
            xml.InsertBefore(xmlDeclaration, root);
            XmlElement policyElement = xml.CreateElement(string.Empty, "policy", string.Empty);

            foreach (var policy in myPolicies)
            {
                xml.AppendChild(policyElement);

                XmlElement policyNumberElement = xml.CreateElement(string.Empty, "policynumber", string.Empty);
                XmlText policyNumberText = xml.CreateTextNode(policy.PolicyNumber);
                policyNumberElement.AppendChild(policyNumberText);
                policyElement.AppendChild(policyNumberElement);

                XmlElement maturityValue = xml.CreateElement(string.Empty, "maturityvalue", string.Empty);
                XmlText valueText = xml.CreateTextNode(myPolicyHelper.MaturityValue(policy).ToString());
                maturityValue.AppendChild(valueText);
                policyElement.AppendChild(maturityValue);
            }

            xml.Save(@"E:\MaturedPolices.xml");
        }
    }
}
