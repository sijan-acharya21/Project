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
        public double LowerLimit { get; set; }
        public double UpperLimit { get; set; }
        public double TaxRateA { get; set; }
        public double TaxRateB { get; set; }

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
                double grossPay = hoursWorked * hourlyRate + 0.99;
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
                    if (grossPay > bracket.LowerLimit && grossPay <= bracket.UpperLimit)
                    {
                        // Using the tax formula: Tax = a * grossPay + b
                        return bracket.TaxRateA * grossPay + bracket.TaxRateB;
                    }
                }
                return 0; // Default tax if no bracket is matched
            }

        }

        /// <summary>
        /// Extends PayCalculator class handling No tax threshold
        /// </summary>
        public class PayCalculatorNoThreshold : PayCalculator
        {
            string taxNoThresholdFile = @"\\Mac\Home\Desktop\Testing Out\Part 3 application files\OO programming\taxrate-nothreshold.csv";

        }

        /// <summary>
        /// Extends PayCalculator class handling With tax threshold
        /// </summary>
        public class PayCalculatorWithThreshold : PayCalculator
        {



        }
    }
}