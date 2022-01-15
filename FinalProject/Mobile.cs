using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FinalProject
{
    [XmlInclude(typeof(BatteryService))]
    [XmlInclude(typeof(CamaraService))]
    [XmlInclude(typeof(ScreenService))]
    [XmlInclude(typeof(SoftwareService))]
    [Serializable]
    public abstract class Mobile : IMobile
    {
        private string fullName;
        private string modelNumber;
        private string itemDescription;
        private string phoneNumber;

        public Mobile()
        {

        }
        protected Mobile(string fullName, string modelNumber, string itemDescription , string phoneNumber)
        {
            this.fullName = fullName;
            this.modelNumber = modelNumber;
            this.itemDescription = itemDescription;
            this.PhoneNumber = phoneNumber;
        }

        public string FullName { get => fullName; set => fullName = value; }
        public string ModelNumber { get => modelNumber; set => modelNumber = value; }
        public string ItemDescription { get => itemDescription; set => itemDescription = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        public abstract string GetItemDescription();
        public abstract string GetModelNumber();

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
