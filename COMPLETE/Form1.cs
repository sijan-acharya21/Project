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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            string employeeFile = @"\\Mac\Home\Desktop\Project\Part 3\Cl_OOProgramming_AE_Pro_Appx\Part 3 application files\OO programming\employee.csv";

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

        private void button1_Click(object sender, EventArgs e)
        {
            string employeeFile = @"\\Mac\Home\Desktop\Project\Part 3\Cl_OOProgramming_AE_Pro_Appx\Part 3 application files\OO programming\employee.csv";
            string taxWithThresholdFile = @"\\Mac\Home\Desktop\Project\Part 3\Cl_OOProgramming_AE_Pro_Appx\Part 3 application files\OO programming\taxrate-withthreshold.csv";
            string taxNoThresholdFile = @"\\Mac\Home\Desktop\Project\Part 3\Cl_OOProgramming_AE_Pro_Appx\Part 3 application files\OO programming\taxrate-nothreshold.csv";

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

                foreach (var record in records)
                {
                    idList.Add(record.id);
                    firstNameList.Add(record.firstName);
                    lastNameList.Add(record.lastName);
                    hourlyRateList.Add(record.hourlyRate);
                    taxThresholdList.Add(record.taxThreshold);
                }
            }

            if (listBox1.SelectedItem != null)
            {
                int selectedIndex = listBox1.SelectedIndex;
                char taxThreshold = taxThresholdList[selectedIndex];
                double hourlyRate = hourlyRateList[selectedIndex];

                // Determine which tax rate file to use
                string taxFile = taxThreshold == 'Y' ? taxWithThresholdFile : taxNoThresholdFile;
                var taxBrackets = ReadTaxBrackets(taxFile);
                
                if (double.TryParse(textBox1.Text, out double hoursWorked))
                    
                {
                    if (hourlyRate <= 20.33 || hourlyRate >= 36.88)
                    {
                        MessageBox.Show("Hourly rate does not fall between $20.33 and $36.88");
                    } 
                    else if (hoursWorked >= 0 && hoursWorked <= 40)
                    {

                        PayCalculator employee = new PayCalculator
                        {
                            TaxBrackets = taxBrackets
                        };
                        
                        double grossPay = employee.CalculateGrossPay(hoursWorked, hourlyRate);
                        double superannuation = employee.CalculateSuperannuation(grossPay);
                        double tax = employee.CalculateTax(grossPay);
                        double netPay = employee.CalculateNetPay(grossPay, tax);

                        textBox2.TextAlign = HorizontalAlignment.Left;
                        textBox2.Text =
                            $"Employee Id: {idList[selectedIndex]}" + Environment.NewLine +
                            $"First name: {firstNameList[selectedIndex]}" + Environment.NewLine +
                            $"Last name: {lastNameList[selectedIndex]}" + Environment.NewLine +
                            $"Hours worked: {hoursWorked}" + Environment.NewLine +
                            $"Hourly rate: {hourlyRate}" + Environment.NewLine +
                            $"Tax threshold: {taxThreshold}" + Environment.NewLine +
                            $"Gross pay: {grossPay}" + Environment.NewLine +
                            $"Tax: {tax}" + Environment.NewLine +
                            $"Net pay: {netPay}" + Environment.NewLine +
                            $"Superannuation: {superannuation}" + Environment.NewLine;
                    }
                     else
                    {
                        MessageBox.Show("Enter a number between 0 and 40");
                    }
                }
            }
        }

        private List<TaxBracket> ReadTaxBrackets(string filePath)
        {
            List<TaxBracket> taxBrackets = new List<TaxBracket>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null, // Disable header validation
                HasHeaderRecord = false, // There is no header in the tax rate files
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
            if (listBox1.SelectedItem != null)
            {
                string employeeFile = @"\\Mac\Home\Desktop\Project\Part 3\Cl_OOProgramming_AE_Pro_Appx\Part 3 application files\OO programming\employee.csv";
                string taxWithThresholdFile = @"\\Mac\Home\Desktop\Project\Part 3\Cl_OOProgramming_AE_Pro_Appx\Part 3 application files\OO programming\taxrate-withthreshold.csv";
                string taxNoThresholdFile = @"\\Mac\Home\Desktop\Project\Part 3\Cl_OOProgramming_AE_Pro_Appx\Part 3 application files\OO programming\taxrate-nothreshold.csv";

                List<int> idList = new List<int>();
                List<string> firstNameList = new List<string>();
                List<string> lastNameList = new List<string>();
                List<double> hourlyRateList = new List<double>();
                List<char> taxThresholdList = new List<char>();

                // Read and parse the employee CSV file (similar code as button1_Click)
                using (var reader = new StreamReader(employeeFile))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null, // Disable header validation
                    HasHeaderRecord = false, // There is no header in the employee.csv file
                    MissingFieldFound = null,
                }))
                {
                    var records = csv.GetRecords<PaySlip>().ToList();

                    foreach (var record in records)
                    {
                        idList.Add(record.id);
                        firstNameList.Add(record.firstName);
                        lastNameList.Add(record.lastName);
                        hourlyRateList.Add(record.hourlyRate);
                        taxThresholdList.Add(record.taxThreshold);
                    }
                }

                int selectedIndex = listBox1.SelectedIndex;
                char taxThreshold = taxThresholdList[selectedIndex];

                // Determine which tax rate file to use
                string taxFile = taxThreshold == 'Y' ? taxWithThresholdFile : taxNoThresholdFile;
                var taxBrackets = ReadTaxBrackets(taxFile);

                if (double.TryParse(textBox1.Text, out double hoursWorked))
                {
                    var calculator = new PayCalculator
                    {
                        TaxBrackets = taxBrackets
                    };

                    double grossPay = calculator.CalculateGrossPay(hoursWorked, hourlyRateList[selectedIndex]);
                    double superannuation = calculator.CalculateSuperannuation(grossPay);
                    double tax = calculator.CalculateTax(grossPay);
                    double netPay = calculator.CalculateNetPay(grossPay, tax);

                    string fullName = $"{firstNameList[selectedIndex]} {lastNameList[selectedIndex]}";
                    string fileName = $"Pay_{fullName}_{DateTime.Now:yyyyMMddHHmmss}.csv";
                    string filePath = Path.Combine(@"\\Mac\Home\Desktop\Project\Part 3\Cl_OOProgramming_AE_Pro_Appx\Part 3 application files\", fileName);

                    var paySlipRecord = new
                    {
                        EmployeeId = idList[selectedIndex],
                        FullName = fullName,
                        HoursWorked = hoursWorked,
                        HourlyRate = hourlyRateList[selectedIndex],
                        TaxThreshold = taxThreshold,
                        GrossPay = grossPay,
                        Tax = tax,
                        NetPay = netPay,
                        Superannuation = superannuation
                    };

                    try
                    {
                        using (var writer = new StreamWriter(filePath))
                        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                        {
                            csv.WriteRecords(new[] { paySlipRecord });
                        }

                        MessageBox.Show($"Pay slip saved to {filePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid number for hours worked.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select an employee from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
