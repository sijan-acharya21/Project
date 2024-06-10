using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using System.Runtime.CompilerServices;


namespace OO_programming
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Add code below to complete the implementation to populate the listBox
            // by reading the employee.csv file into a List of PaySlip objects, then binding this to the ListBox.
            // CSV file format: <employee ID>, <first name>, <last name>, <hourly rate>,<taxthreshold>
            string employeeFile = @"\\Mac\Home\Desktop\Testing Out\Part 3 application files\OO programming\employee.csv";
            string taxWithThresholdFile = @"\\Mac\Home\Desktop\Testing Out\Part 3 application files\OO programming\taxrate-withthreshold.csv";
            string taxNoThresholdFile = @"\\Mac\Home\Desktop\Testing Out\Part 3 application files\OO programming\taxrate-nothreshold.csv";

            List<int> initialValue = new List<int>();
            List<int> finalValue = new List<int>();
            List<double> taxRatesA = new List<double>();
            List<double> taxRatesB = new List<double>();

            using (var reader = new StreamReader(taxWithThresholdFile))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null, // Disable header validation
                HasHeaderRecord = false, // There is no header in the employee.csv file
                MissingFieldFound = null,
            }))
            {
                var records = csv.GetRecords<PayCalculator>().ToList();

                for (int i = 0; i < records.Count; i++)
                {
                    var record = records[i];
                    initialValue.Add(record.InitialValue);
                    finalValue.Add(record.FinalValue);
                    taxRatesA.Add(record.TaxRatesA);
                    taxRatesB.Add(record.TaxRatesB);
                }
            }
                
                List<int> idList = new List<int>();
                List<string> firstNameList = new List<string>();
                List<string> lastNameList = new List<string>();
                List<double> hourlyRateList = new List<double>();
                List<char> taxThresholdList = new List<char>();

                // Read and parse the CSV file
                using (var reader = new StreamReader(employeeFile))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null, // Disable header validation
                    HasHeaderRecord = false, // There is no header in the employee.csv file
                    MissingFieldFound = null,
                }))
                {
                    var records = csv.GetRecords<PaySlip>().ToList();

                    for (int i = 0; i < records.Count; i++)
                    {
                        var record = records[i];
                        idList.Add(record.Id);
                        firstNameList.Add(record.FirstName);
                        lastNameList.Add(record.LastName);
                        hourlyRateList.Add(record.HourlyRate);
                        taxThresholdList.Add(record.TaxThreshold);

                        listBox1.Items.Add($"{idList[i]} {firstNameList[i]} {lastNameList[i]}");
                    }
                }
}

        private async void button1_Click(object sender, EventArgs e)
        {
            // Add code below to complete the implementation to populate the
            // payment summary (textBox2) using the PaySlip and PayCalculatorNoThreshold
            // and PayCalculatorWithThresholds classes object and methods.
            string employeeFile = @"\\Mac\Home\Desktop\Testing Out\Part 3 application files\OO programming\employee.csv";
            string taxWithThresholdFile = @"\\Mac\Home\Desktop\Testing Out\Part 3 application files\OO programming\taxrate-withthreshold.csv";
            string taxNoThresholdFile = @"\\Mac\Home\Desktop\Testing Out\Part 3 application files\OO programming\taxrate-nothreshold.csv";

            List<int> idList = new List<int>();
            List<string> firstNameList = new List<string>();
            List<string> lastNameList = new List<string>();
            List<double> hourlyRateList = new List<double>();
            List<char> taxThresholdList = new List<char>();

            // Read and parse the CSV file
            using (var reader = new StreamReader(employeeFile))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null, // Disable header validation
                HasHeaderRecord = false, // There is no header in the employee.csv file
                MissingFieldFound = null,
            }))
            {
                var records = csv.GetRecords<PaySlip>().ToList();

                for (int i = 0; i < records.Count; i++)
                {
                    var record = records[i];
                    idList.Add(record.Id);
                    firstNameList.Add(record.FirstName);
                    lastNameList.Add(record.LastName);
                    hourlyRateList.Add(record.HourlyRate);
                    taxThresholdList.Add(record.TaxThreshold);
                }
            }

            if (listBox1.SelectedItem != null)
            {
                int selectedIndex = listBox1.SelectedIndex;
                int employeeId = idList[selectedIndex];
                string firstname = firstNameList[selectedIndex];
                string lastName = lastNameList[selectedIndex];
                double hourlyRate = hourlyRateList[selectedIndex];
                char taxThreshold = taxThresholdList[selectedIndex];
                if (double.TryParse(textBox1.Text, out double hoursWorked))
                {

                    textBox2.TextAlign = HorizontalAlignment.Left;
                    textBox2.Text =
                        $"Employee Id: {employeeId}" + System.Environment.NewLine +
                        $"First name: {firstname}" + System.Environment.NewLine +
                        $"Last name: {lastName}" + System.Environment.NewLine +
                        $"Hours worked: {hoursWorked} " + System.Environment.NewLine +
                        $"Hourly rate: {hourlyRate}" + System.Environment.NewLine +
                        $"Tax threshold: {taxThreshold} " + System.Environment.NewLine +
                        $"Gross pay: {hourlyRate * hoursWorked}" + System.Environment.NewLine +
                        $"Tax: " + System.Environment.NewLine +
                        $"Net pay: " + System.Environment.NewLine +
                        $"Superannuation: " + System.Environment.NewLine;

                }
                else
                {
                    MessageBox.Show("No");
                }
            }    
        }
    

        private void button2_Click(object sender, EventArgs e)
        {
            // Add code below to complete the implementation for saving the
            // calculated payment data into a csv file.
            // File naming convention: Pay_<full name>_<datetimenow>.csv
            // Data fields expected - EmployeeId, Full Name, Hours Worked, Hourly Rate, Tax Threshold, Gross Pay, Tax, Net Pay, Superannuation

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
