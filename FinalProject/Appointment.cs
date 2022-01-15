using System;
using System.Xml;
using System.Xml.Serialization;

namespace FinalProject
{  
        public class Appointment :  IDisposable

        {
            private string time;
            private Mobile mobile;
            private string deviceIssue;
            private string warrenty;

            [XmlElement]
            public string Time { get => this.time; set => this.time = value; }
            [XmlElement]
            public Mobile Mobile { get => this.mobile; set => this.mobile = value; }
            [XmlElement]
            public string DeviceIssue { get => this.deviceIssue; set => this.deviceIssue = value; }
        public string Warrenty { get => warrenty; set => warrenty = value; }

        public Appointment()
            {

            }
            public Appointment(Mobile mobile)
            {
                this.mobile = mobile;
            }

            public Appointment(Mobile mobile, string time)
            {
                this.mobile = mobile;
                this.time = time;
            }

           
            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
         
        }
     
}