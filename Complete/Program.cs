using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OO_programming
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }


    /// <summary>
    /// Class a capture details accociated with an employee's pay slip record
    /// </summary>
    public class PaySlip
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public double hourlyRate { get; set; }
        public char taxThreshold { get; set; }
    }

    /// <summary>
    /// Base class to hold all Pay calculation functions
    /// Default class behaviour is tax calculated with tax threshold applied
    /// </summary>
    public class TaxBracket
    {
        public double lowerLimit { get; set; }
        public double upperLimit { get; set; }
        public double taxRateA { get; set; }
        public double taxRateB { get; set; }
    }
    public class PayCalculator
    {
        public List<TaxBracket> TaxBrackets { get; set; }

        public double superRate = 0.11;

        public PayCalculator()
        {
            TaxBrackets = new List<TaxBracket>();
        }
        public double CalculateGrossPay(double hoursWorked, double hourlyRate)
        {
            double grossPay = hoursWorked * hourlyRate;
            return grossPay;
        }

        public double CalculateSuperannuation(double grossPay)
        {
            double superannuation = superRate * grossPay;
            return superannuation;
        }

        public double CalculateTax(double grossPay)
        {
            foreach (var bracket in TaxBrackets)
            {
                if (grossPay > bracket.lowerLimit && grossPay <= bracket.upperLimit)
                {
                    // Tax formula: y = ax - b
                    return (bracket.taxRateA * grossPay) - bracket.taxRateB + 0.99;
                }
            }
            return 0;
        }
        public double CalculateNetPay(double grossPay, double tax)
        {
            double netPay = grossPay - tax;
            return netPay;
        }
    }
}
