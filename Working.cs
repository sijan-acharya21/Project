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
            var records = csv.GetRecords<PaySlip>();
            foreach (var record in records)
            {
                idList.Add(record.Id);
                firstNameList.Add(record.FirstName);
                lastNameList.Add(record.LastName);
                hourlyRateList.Add(record.HourlyRate);
                taxThresholdList.Add(record.TaxThreshold);
            }
        }
        listBox1.Items.Add($"{string.Join(",", idList)} {string.Join(",", firstNameList)} {string.Join(",", lastNameList)}");
    }     
