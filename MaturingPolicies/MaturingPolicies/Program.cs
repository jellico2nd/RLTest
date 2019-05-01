using MaturingPolicies.DataLoad;
using MaturingPolicies.Helpers;
using MaturingPolicies.Model.ConcreteTypes;
using MaturingPolicies.Output;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MaturingPolicies
{
    class Program
    {
        
        static void Main(string[] args)
        {
            PolicyHelper policyHelper = new PolicyHelper();
            List<Policy> validPolicies = new List<Policy>();

            Console.WriteLine("Hello World!");
            try
            {
                validPolicies = new DataLoadFromCSVFile(@"E:\MaturityData.csv").GetValidPolicies();
                Console.WriteLine("Load Complete...");
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("An error at load has occured: {0}", e.Message));
            }
            try
            {
                var result = new OutputXMLFile(validPolicies, policyHelper);
                result.CreateOutputFile(new XmlSerializer(typeof(PolicyDTO), new XmlRootAttribute("MaturedPolicies") { IsNullable = false }));
                Console.WriteLine("Creating XML File Complete...");
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("An error at load has occured: {0}", e.Message));
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
