using Microsoft.VisualStudio.TestTools.UnitTesting;
using OO_programming;
using System;
using System.Collections.Generic;

namespace UnitTesting_PayCalculator
{
    [TestClass]
    public class PayCalculator_Tests
    {
        [TestMethod]
        public void CalculateGrossPay_ReturnGrossPay()
        {
            // Arrange
            PayCalculator payCalculator = new PayCalculator();

            // Act
            double netIncome = payCalculator.CalculateGrossPay(40, 25);

            // Assert
            Assert.AreEqual(1000, netIncome);
        }
        [TestMethod]
        public void CalculateSuperannuation_ReturnSuperannuation()
        {
            // Arrange
            PayCalculator payCalculator = new PayCalculator();

            // Act
            double superannuation = payCalculator.CalculateSuperannuation(1000);

            // Assert
            Assert.AreEqual(110, superannuation);
        }

        [TestMethod]
        public void CalculateNetPay_ReturnNetPay()
        {
            // Arrange
            PayCalculator payCalculator = new PayCalculator();

            // Act
            double netPay = payCalculator.CalculateNetPay(1000, 162.4781);

            // Assert
            Assert.AreEqual(837.5219, netPay);
        }
        [TestMethod]
        public void CalculateTaxWithThreshold_ReturnTax()
        {
            // Arrange
            PayCalculator paycalculator = new PayCalculator();
            PayCalculator paycalculator2 = new PayCalculator();
            PayCalculator paycalculator3 = new PayCalculator();
            PayCalculator paycalculator4 = new PayCalculator();
            PayCalculator paycalculator5 = new PayCalculator();
            PayCalculator paycalculator6 = new PayCalculator();   

            paycalculator.TaxBrackets = new List<TaxBracket> { new TaxBracket { lowerLimit = 0, upperLimit = 359, taxRateA = 0, taxRateB = 0 } };
            paycalculator2.TaxBrackets = new List<TaxBracket> { new TaxBracket { lowerLimit = 0, upperLimit = 359, taxRateA = 0, taxRateB = 0 } };
            paycalculator3.TaxBrackets = new List<TaxBracket> { new TaxBracket { lowerLimit = 359, upperLimit = 438, taxRateA = 0.1900, taxRateB = 68.3462 } };
            paycalculator4.TaxBrackets = new List<TaxBracket> { new TaxBracket { lowerLimit = 548, upperLimit = 721, taxRateA = 0.2100, taxRateB = 68.3465 } };
            paycalculator5.TaxBrackets = new List<TaxBracket> { new TaxBracket { lowerLimit = 548, upperLimit = 721, taxRateA = 0.2100, taxRateB = 68.3465 } };
            paycalculator6.TaxBrackets = new List<TaxBracket> { new TaxBracket { lowerLimit = 865, upperLimit = 1282, taxRateA = 0.3477, taxRateB = 186.2119 } };
            

            double grossPay = 50;
            double grossPay2 = 250;
            double grossPay3 = 400;
            double grossPay4 = 550;
            double grossPay5 = 700;
            double grossPay6 = 1000;
            

            // Act
            double taxWithThreshold = paycalculator.CalculateTax(grossPay);
            double taxWithThreshold2 = paycalculator2.CalculateTax(grossPay2);
            double taxWithThreshold3 = paycalculator3.CalculateTax(grossPay3);
            double taxWithThreshold4 = paycalculator4.CalculateTax(grossPay4);
            double taxWithThreshold5 = paycalculator5.CalculateTax(grossPay5);
            double taxWithThreshold6 = paycalculator6.CalculateTax(grossPay6);
            

            // Assert
            Assert.AreEqual(0.99, taxWithThreshold, 0.01);
            Assert.AreEqual(0.99, taxWithThreshold2, 0.01);
            Assert.AreEqual(8.64, taxWithThreshold3, 0.01);
            Assert.AreEqual(48.14, taxWithThreshold4, 0.01);
            Assert.AreEqual(79.64, taxWithThreshold5, 0.01);
            Assert.AreEqual(162.48, taxWithThreshold6, 0.01);
            
        }

        [TestMethod]
        public void CalculateTaxWithoutThreshold_ReturnTax()
        {
            // Arrange
            PayCalculator paycalculator = new PayCalculator();
            PayCalculator paycalculator2 = new PayCalculator();
            PayCalculator paycalculator3 = new PayCalculator();
            PayCalculator paycalculator4 = new PayCalculator();
            PayCalculator paycalculator5 = new PayCalculator();
            PayCalculator paycalculator6 = new PayCalculator();

            
            paycalculator.TaxBrackets = new List<TaxBracket> { new TaxBracket { lowerLimit = 0, upperLimit = 88, taxRateA = 0.1900, taxRateB = 0.1900 } };
            paycalculator2.TaxBrackets = new List<TaxBracket> { new TaxBracket { lowerLimit = 88, upperLimit = 371, taxRateA = 0.2348, taxRateB = 3.9639 } };
            paycalculator3.TaxBrackets = new List<TaxBracket> { new TaxBracket { lowerLimit = 371, upperLimit = 515, taxRateA = 0.2190, taxRateB = -1.9003 } };
            paycalculator4.TaxBrackets = new List<TaxBracket> { new TaxBracket { lowerLimit = 515, upperLimit = 932, taxRateA = 0.3477, taxRateB = 64.4297 } };
            paycalculator5.TaxBrackets = new List<TaxBracket> { new TaxBracket { lowerLimit = 515, upperLimit = 932, taxRateA = 0.3477, taxRateB = 64.4297 } };
            paycalculator6.TaxBrackets = new List<TaxBracket> { new TaxBracket { lowerLimit = 932, upperLimit = 1957, taxRateA = 0.3450, taxRateB = 61.9132 } };


            double grossPay = 50;
            double grossPay2 = 250;
            double grossPay3 = 400;
            double grossPay4 = 550;
            double grossPay5 = 700;
            double grossPay6 = 1000;

            // Act
            double taxWithoutThreshold = paycalculator.CalculateTax(grossPay);
            double taxWithoutThreshold2 = paycalculator2.CalculateTax(grossPay2);
            double taxWithoutThreshold3 = paycalculator3.CalculateTax(grossPay3);
            double taxWithoutThreshold4 = paycalculator4.CalculateTax(grossPay4);
            double taxWithoutThreshold5 = paycalculator5.CalculateTax(grossPay5);
            double taxWithoutThreshold6 = paycalculator6.CalculateTax(grossPay6);

            // Assert
            Assert.AreEqual(10.30, taxWithoutThreshold , 0.01);
            Assert.AreEqual(55.73, taxWithoutThreshold2, 0.01);
            Assert.AreEqual(90.49, taxWithoutThreshold3, 0.01);
            Assert.AreEqual(127.80, taxWithoutThreshold4, 0.01);
            Assert.AreEqual(179.95, taxWithoutThreshold5, 0.01);
            Assert.AreEqual(284.08, taxWithoutThreshold6, 0.01);

        }

    }
}
