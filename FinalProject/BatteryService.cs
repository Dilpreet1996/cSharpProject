using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class BatteryService : Mobile
    {
        public BatteryService()
        {

        }

        public BatteryService(string fullName, string modelNumber, string itemDescription, string phoneNumber) : base(fullName, modelNumber, itemDescription, phoneNumber)
        {
        }

        public override string GetItemDescription()
        {
            throw new NotImplementedException();
        }

        public override string GetModelNumber()
        {
            throw new NotImplementedException();
        }
    }
}
