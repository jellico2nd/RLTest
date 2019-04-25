using MaturingPolicies.Helpers;
using MaturingPolicies.Model.ConcreteTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MaturingPolicies.DataLoad
{
    class DataLoadFromCSVFile
    {
        private List<Policy> myData = new List<Policy>();

        public DataLoadFromCSVFile(string path)
        {
            if (ValidateFile(path))
            {
                ProcessFile(path);
            }
        }

        private void ProcessFile(string path)
        {
            PolicyHelper policyHelper = new PolicyHelper();

            using (var reader = new StreamReader(path))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    Policy policy = new Policy(line);
                    policyHelper.SetPolicyType(policy);
                    myData.Add(policy);
                }
            }
        }

        public bool ValidateFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileLoadException(string.Format("File is missing: \r{0}", path));
            }
            return true;
        }

        public List<Policy> GetValidPolicies()
        {
            return myData.FindAll(x => x.ValidPolicy == true);
        }
    }
}
