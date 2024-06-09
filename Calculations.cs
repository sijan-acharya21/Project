// Calculate gross pay
double hourlyRate;
double hoursWorked;
double grossPay = hourlyRate * hoursWorked;

// Calculate net pay
double netPay;
double tax;
double grossPay;
netPay = grossPay - tax;

// Calculate tax
// Need to select a, b and x from the tax file sheet
// Need to check if the employee has claimed tax free threshold or not from tax file sheet
double coefficientA;
double coefficientB;
double x;
double tax = ax - b;

if (TaxThresholds.ToUpper == 'Y') {
  PayCalculatorWithThreshold();
} else {
  PayCalculatorNoThreshold();
}

// Calculate superannuation
double superAnnuation;
double superRate = 11;
double grossPay;
superAnnuation = superRate * grossPay;
