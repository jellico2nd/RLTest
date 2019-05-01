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
                PrintExceptionMessage(e);
            }
            try
            {
                var result = new OutputXMLFile(validPolicies, policyHelper);
                result.CreateOutputFile(new XmlSerializer(typeof(List<PolicyDTO>), new XmlRootAttribute("MaturedPolicies") { IsNullable = false }));
                Console.WriteLine("Creating XML File Complete...");
            }
            catch (Exception e)
            {
                PrintExceptionMessage(e);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void PrintExceptionMessage(Exception e)
        {
            Console.WriteLine(string.Format("An error at load has occured: {0}", e.Message));
            if (e.InnerException != null)
            {
                Console.WriteLine(string.Format("InnerException: {0}", e.InnerException.Message));
            }
        }
    }
}
