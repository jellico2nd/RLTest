﻿using MaturingPolicies.Helpers;
using MaturingPolicies.Model.ConcreteTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

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

        public void CreateOutputFile(XmlDocument xml)
        {
            if(xml == null)
            {
                throw new ArgumentNullException("XmlDocument cannot be null");
            }
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
                XmlText valueText = xml.CreateTextNode(myPolicyHelper.MaturityValue(policy).PolicyValue);
                maturityValue.AppendChild(valueText);
                policyElement.AppendChild(maturityValue);
            }

            xml.Save(@"E:\MaturedPolices.xml");
        }

        public void CreateOutputFile(XmlSerializer xmlSerializer)
        {
            List<PolicyDTO> OutputList = new List<PolicyDTO>();
            Stream fs = new FileStream(@"E:\MaturedPolices.xml", FileMode.Create);
            XmlWriter writer = new XmlTextWriter(fs, Encoding.Unicode);
            myPolicies.ForEach(policy => OutputList.Add(myPolicyHelper.MaturityValue(policy)));
            xmlSerializer.Serialize(writer, OutputList);
            writer.Close();
        }
    }
}
