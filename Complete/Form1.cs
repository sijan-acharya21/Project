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
using System.Linq.Expressions;
using static OO_programming.TaxBracket;

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
                    idList.Add(record.id);
                    firstNameList.Add(record.firstName);
                    lastNameList.Add(record.lastName);
                    hourlyRateList.Add(record.hourlyRate);
                    taxThresholdList.Add(record.taxThreshold);

                    listBox1.Items.Add($"{idList[i]} {firstNameList[i]} {lastNameList[i]}");
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string employeeFile = @"\\Mac\Home\Desktop\Testing Out\Part 3 application files\OO programming\employee.csv";
            string taxWithThresholdFile = @"\\Mac\Home\Desktop\Testing Out\Part 3 application files\OO programming\taxrate-withthreshold.csv";

            List<int> idList = new List<int>();
            List<string> firstNameList = new List<string>();
            List<string> lastNameList = new List<string>();
            List<double> hourlyRateList = new List<double>();
            List<char> taxThresholdList = new List<char>();

            // Read and parse the employee CSV file
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
                    idList.Add(record.id);
                    firstNameList.Add(record.firstName);
                    lastNameList.Add(record.lastName);
                    hourlyRateList.Add(record.hourlyRate);
                    taxThresholdList.Add(record.taxThreshold);
                }
            }

            // Read the tax brackets from the CSV file
            var taxBrackets = ReadTaxBrackets(taxWithThresholdFile);

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
                    PayCalculator employee = new PayCalculator
                    {
                        TaxBrackets = taxBrackets
                    };



                    double grossPay = employee.CalculateGrossPay(hoursWorked, hourlyRate);
                    double superannuation = employee.CalculateSuperannuation(grossPay);
                    double tax = employee.CalculateTax(grossPay);
                    //double netPay = employee.CalculateNetPay(grossPay, tax);

                    textBox2.TextAlign = HorizontalAlignment.Left;
                    textBox2.Text =
                        $"Employee Id: {employeeId}" + Environment.NewLine +
                        $"First name: {firstname}" + Environment.NewLine +
                        $"Last name: {lastName}" + Environment.NewLine +
                        $"Hours worked: {hoursWorked} " + Environment.NewLine +
                        $"Hourly rate: {hourlyRate}" + Environment.NewLine +
                        $"Tax threshold: {taxThreshold} " + Environment.NewLine +
                        $"Gross pay: {grossPay}" + Environment.NewLine +
                        $"Tax: {tax}" + Environment.NewLine +
                        $"Net pay: " + Environment.NewLine +
                        $"Superannuation: {superannuation}" + Environment.NewLine;
                }
                else
                {
                    MessageBox.Show("Invalid data entered");
                }
            }
        }

        private List<TaxBracket> ReadTaxBrackets(string taxWithThreshold)
        {
            List<TaxBracket> taxBrackets = new List<TaxBracket>();

            using (var reader = new StreamReader(taxWithThreshold))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null, // Disable header validation
                HasHeaderRecord = false, // There is no header in the taxrate-withthreshold.csv file
                MissingFieldFound = null,
            }))
            {
                var records = csv.GetRecords<TaxBracket>().ToList();
                taxBrackets = records;
            }

            return taxBrackets;
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

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
        }
    }
}
