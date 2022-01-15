using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;
using FinalProject;

namespace FinalWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    enum MobileServices{
        BatteryService,
        CamaraService,
        ScreenService,
        SoftwareService
    }
    public partial class MainWindow : Window
    {
      
        private AppoinmentList appList = null;
        public string fileName = "AppoinmentList.xml";
       
        public MainWindow()
        {
            InitializeComponent();
            SetTimeSlots();
            AddingServices();
            appList = new AppoinmentList();
            DataContext = this;
        }

      
        public void SetTimeSlots()
        {

            timeSlotBookingComboBox.Items.Clear();
            for (int i = 10; i < 20; i++)
            {
                timeSlotBookingComboBox.Items.Add(TimeInHourFormat(i));
            }
            timeSlotBookingComboBox.SelectedIndex = 0;

        }

        public string TimeInHourFormat(int hours)
        {
            var tFormat = "AM";
            if (hours == 0)
            {
                hours = 12;
            }

            else if (hours > 12)
            {
                hours -= 12;
                tFormat = "PM";
            }
            else if (hours == 12)
            {
                tFormat = "PM";
            }
            var timeIn24 = string.Format("{0}:{1:00} {2}", hours, 00, tFormat);
            return timeIn24;
        }

        void AddingServices()
        {
            var vehicles = Enum.GetValues(typeof(MobileServices));
            foreach (var val in vehicles)
            {
                selectService.Items.Add(val);
            }
        }

        public bool CheckValidation()
        {
            bool check = true;
            // Time Booking Validation
            if (timeSlotBookingComboBox.Text != "")
            {
                TimeSlotError.Visibility = Visibility.Hidden;
                TimeSlotError.Foreground = Brushes.White;
            }
            else
            {
                check = false;
                TimeSlotError.Visibility = Visibility.Visible;
                TimeSlotError.Foreground = Brushes.Red;
            }
            // Issue Selection Valiation
            if (selectService.Text != "")
            {
                selectReason.Visibility = Visibility.Hidden;
                selectReason.Foreground = Brushes.White;
            }
            else
            {
                check = false;
                selectReason.Visibility = Visibility.Visible;
                selectReason.Foreground = Brushes.Red;
            }

            //Customer Name Validation
            if (nameTextBox.Text == "" || !ValidString(nameTextBox.Text))
            {
                check = false;
                enterName.Foreground = Brushes.Red;
                enterName.Visibility = Visibility.Visible;

            }
            else
            {

                enterName.Visibility = Visibility.Hidden;
                enterName.Foreground = Brushes.White;
            }

            //mobile number Validation

            if (phoneTextBox.Text == "" || phoneTextBox.Text.Length != 10)
            {

                check = false;
                phoneNumber.Visibility = Visibility.Visible;
                phoneNumber.Foreground = Brushes.Red;
            }
            else
            {
                phoneNumber.Visibility = Visibility.Hidden;
                phoneNumber.Foreground = Brushes.White;
            }

            //model number validation


            if (modelNumberTextBox.Text == "" || modelNumberTextBox.Text.Length < 3)
            {
                check = false;
                mobileModel.Visibility = Visibility.Visible;
                mobileModel.Foreground = Brushes.Red;
            }
            else
            {
                mobileModel.Visibility = Visibility.Hidden;
                mobileModel.Foreground = Brushes.White;
            }

            // Message Box Validation
            if (briefTextBlock.Text == "" || !ValidDescrpition(briefTextBlock.Text))
            {
                check = false;
                message.Visibility = Visibility.Visible;
                message.Foreground = Brushes.Red;

            }
            else
            {
                message.Visibility = Visibility.Hidden;
                message.Foreground = Brushes.White;
            }

            return check;
        }

        private void bookAppoinmentOnClick(object sender, RoutedEventArgs e)
        {
            Mobile mobile = null;
            if (CheckValidation())
            {
                string warranty = null;
                string selectedTimeSlot = timeSlotBookingComboBox.SelectedItem.ToString();
                string selectedIssue = selectService.SelectedItem.ToString();
                string fullName = nameTextBox.Text;
                string modelNumber = modelNumberTextBox.Text;
                string userAddress = briefTextBlock.Text;
                string phoneNumber = phoneTextBox.Text;
                if (rbYes.IsChecked == true)
                {
                    warranty = rbYes.Content.ToString();
                }
                else if(rbNo.IsChecked == true)
                {
                    warranty = rbNo.Content.ToString();
                }
                     
                Appointment appoinment = new Appointment();
                appoinment.Time = selectedTimeSlot;
                appoinment.Warrenty = warranty;
                switch (selectService.SelectedValue)
                {
                    case MobileServices.BatteryService:
                        mobile = new BatteryService(fullName, modelNumber, userAddress, phoneNumber);
                        appoinment.DeviceIssue = "Battery Service";
                        break;

                    case MobileServices.CamaraService:
                        mobile = new CamaraService(fullName, modelNumber, userAddress, phoneNumber);
                        appoinment.DeviceIssue = "Camara Service";
                        break;

                    case MobileServices.ScreenService:
                        mobile = new ScreenService(fullName, modelNumber, userAddress, phoneNumber);
                        appoinment.DeviceIssue = "Screen Service";
                        break;

                    case MobileServices.SoftwareService:
                        mobile = new SoftwareService(fullName, modelNumber, userAddress, phoneNumber);
                        appoinment.DeviceIssue = "Software Service";
                        break;

                }
                appoinment.Mobile = mobile;
                appList.Add(appoinment);


                timeSlotBookingComboBox.Items.Remove(timeSlotBookingComboBox.SelectedItem);
                if (timeSlotBookingComboBox.Items.Count == 0)
                {

                    MessageBox.Show("There are no slots available!");
                }
                else
                {
                    timeSlotBookingComboBox.SelectedIndex = 0;
                }
                StoringDataInGridView();
                ClearingForm();
            }
        
        }

        private void StoringDataInGridView()
        {
            XmlSerializer serializer = null;
            TextWriter writer = null;
            try
            {
                
                serializer = new XmlSerializer(typeof(AppoinmentList));
                writer = new StreamWriter(fileName);
                serializer.Serialize(writer, appList);
                writer.Close();
                 customerGrid.ItemsSource = appList.List;
                MessageBox.Show("Data saved successfully");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                if (writer != null)
                {
                    writer.Close();
                }
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        private void ClearingForm()
        {
            nameTextBox.Text = string.Empty;
            modelNumberTextBox.Text = string.Empty;
            briefTextBlock.Text = string.Empty;
            selectService.SelectedIndex = -1;  
            phoneTextBox.Text = string.Empty;
            MessageShow.Content = string.Empty;
        }

        
        private void showAppointmentOnClick(object sender, RoutedEventArgs e)
        {
            XmlSerializer serializer = null;
            StreamReader reader = null;
          
            try
            {
                if (File.Exists(fileName))
                {

                    serializer = new XmlSerializer(typeof(AppoinmentList));
                    reader = new StreamReader(fileName);
                    appList = (AppoinmentList)serializer.Deserialize(reader);
                    customerGrid.ItemsSource = appList.List;

                    if (appList.Count <= 0)
                    {
                        MessageBox.Show("No data available");
                    }
                    for (int i = 0; i < appList.Count; i++)
                    {
                        timeSlotBookingComboBox.Items.Remove(appList[i].Time);
                        if (timeSlotBookingComboBox.Items.Count == 0)
                        {
                            appointmentButton.IsEnabled = false;
                        }
                        else
                        {
                            timeSlotBookingComboBox.SelectedIndex = 0;
                        }
                    }
                    MessageShow.Content = string.Empty;
                }
                else {
                    MessageBox.Show("No data available");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

            }

        }


        bool ValidString(String userString)
        {
            StringBuilder uString = new StringBuilder(userString);


            bool isValid = true;
            
            for (int i = 0; i < uString.Length; i++)
            {
                if (!char.IsLetter(uString[i]))
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {           

                string modelText = searchBox.Text;
                if (modelText.Length > 0)
                {

                   var searchQuery = from appoinment in appList.List
                                     where appoinment.Mobile.FullName.ToString().ToLower().Contains(modelText.ToLower())
                                     select appoinment;
                    customerGrid.ItemsSource = searchQuery;
                }
                else
                {
                    customerGrid.ItemsSource = appList.List;
                }          
           
        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (nameTextBox.Text == "" ||!ValidString(nameTextBox.Text) )
            {
                MessageShow.Content = string.Format(" Please Enter First Name without space");
                    
                nameTextBox.Foreground = Brushes.Red;
            }
            else
            {
                nameTextBox.Foreground = Brushes.Black;
                MessageShow.Content = string.Empty;
            }

        }
       

        private void modelNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (modelNumberTextBox.Text == "" || modelNumberTextBox.Text.Length <3)
            {
                MessageShow.Content = string.Format("Enter Model Number more than 3 characters");
                modelNumberTextBox.Foreground = Brushes.Red;
            }
            else
            {
                modelNumberTextBox.Foreground = Brushes.Black;
                MessageShow.Content = string.Empty;

            }

        }

        private void briefTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (briefTextBlock.Text == "" || !ValidDescrpition(briefTextBlock.Text) )
            {
                MessageShow.Content = string.Format("Enter Address within more than 10 words");
                briefTextBlock.Foreground = Brushes.Red;
            }
            else
            {
                briefTextBlock.Foreground = Brushes.Black;
                MessageShow.Content = string.Empty;

            }
        }

        public static bool IsVaidNumber(string number)
        {
            // return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
            StringBuilder MobileNumber = new StringBuilder(number);
            if (number.Length != 10)
            {
                return false;
            }

            bool isValid = true;

            for (int i = 0; i < MobileNumber.Length ; i++)
            {
                if (!char.IsDigit(MobileNumber[i]))
                {
                    isValid = false;
                }
            }
            return isValid;
        }

       
        bool ValidDescrpition(string PCString)
        {
            StringBuilder PCModelNumber = new StringBuilder(PCString);
            if (PCString.Length < 10 ||  PCString.Length > 50)
            {
                return false;
            }

            bool isValid = true;
            

            for (int i = 0; i < PCModelNumber.Length ; i++)
            {
                if (!(char.IsLetter(PCModelNumber[i]) ||(char.IsWhiteSpace(PCModelNumber[i]))))
                {
                    isValid = false;
                }
            }
                                    
            return isValid;
        }

        private void phoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (phoneTextBox.Text == "" || !IsVaidNumber(phoneTextBox.Text))
            {
                MessageShow.Content = string.Format(" Enter 10 digit number");
                phoneTextBox.Foreground = Brushes.Red;
            }
            else
            {
                phoneTextBox.Foreground = Brushes.Black;
                MessageShow.Content = string.Empty;

            }
        }
    }
   
}
