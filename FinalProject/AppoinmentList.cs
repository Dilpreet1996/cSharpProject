using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FinalProject
{
    [XmlRoot("AppointmentList")]
    [XmlInclude(typeof(BatteryService))]
    [XmlInclude(typeof(CamaraService))]
    [XmlInclude(typeof(ScreenService))]
    [XmlInclude(typeof(SoftwareService))]
    
       
        public class AppoinmentList : IEnumerable<Appointment>
        {
            [XmlArray("Appoinments")]
            [XmlArrayItem("Appoinment")]
            private ObservableCollection<Appointment> appointments;

            public AppoinmentList()
            {
            appointments = new ObservableCollection<Appointment>();
            }

            public AppoinmentList(ObservableCollection<Appointment> list)
            {
                this.appointments = list;
            }

            public Appointment this[int i]
            {
                get { return List[i]; }
                set { List[i] = value; }
            }

            public int Count
            {
                get { return List.Count; }

            }

            public ObservableCollection<Appointment> List { get => appointments; set => appointments = value; }

            public void Add(Appointment appointment)
            {
                this.List.Add(appointment);
            }

            public void Remove(Appointment appointment)
            {
                this.List.Remove(appointment);
            }

            public void RemoveAt(int i)
            {
                this.List.RemoveAt(i);
            }

            public IEnumerator<Appointment> GetEnumerator()
            {
                return List.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return List.GetEnumerator();
            }
        }
     
}
