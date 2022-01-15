using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class CamaraService : Mobile
    {
        public CamaraService()
        {

        }

        public CamaraService(string fullName, string modelNumber, string itemDescription, string phoneNumber) : base(fullName, modelNumber, itemDescription, phoneNumber)
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
