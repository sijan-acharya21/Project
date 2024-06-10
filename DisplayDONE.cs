// Ensure CsvHelper is installed into the assembly before running the code
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
            string filePath = @"\\Mac\Home\Desktop\Testing Out\Part 3 application files\OO programming\employee.csv";

            List<int> idList = new List<int>();
            List<string> firstNameList = new List<string>();
            List<string> lastNameList = new List<string>();
            List<double> hourlyRateList = new List<double>();
            List<char> taxThresholdList = new List<char>();


            // Read and parse the CSV file
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null, // Disable header validation
                HasHeaderRecord = false, // There is no header in the employee.csv file
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


// Payslip class
public class PaySlip
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double HourlyRate { get; set; }
    public char TaxThreshold { get; set; }
    
}
