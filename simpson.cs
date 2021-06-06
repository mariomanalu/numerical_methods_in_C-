using System;
using MathNet.Numerics.Integration;

public class NumericalMethods
{
 
    // Define the function to be integrated
    static double func(double x)
    {
        return (double)Math.Log(x);
    }

    // Simpsons method
    static double simpsons_(double lower_bound, double upper_bound, int n)
    {
        
        double height = (upper_bound - lower_bound) / n;
 
        double[] x = new double[n + 1];
        double[] fx= new double[n + 1];
 
        
        for (int i = 0; i <= n; i++) {
            x[i] = lower_bound + i * height;
            fx[i] = func(x[i]);
        }
 
        // Doing the f(a) + 4f(x_1) + 2f(x_2) + 4f(x_3) + ... + f(b) part
        double result = 0;
        for (int i = 0; i <= n; i++) {
            if (i == 0 || i == n)
                result += fx[i];
            else if (i % 2 != 0)
                result += 4 * fx[i];
            else
                result += 2 * fx[i];
        }
        
        // Times height / three
        result = result * (height / 3);
        return result;
    }
 
    // Gaussian quadrature method
    static double gaussian_quadrature_(double lower_bound, double upper_bound, int n){
        
        int nonstandardinterval = 0;

        if (lower_bound != -1 && upper_bound != 1){
            nonstandardinterval = 1;
        }

        // Compute the corresponding Legendre polynomial
        GaussLegendreRule rule = new GaussLegendreRule(-1, 1, n);

        double[] nodes = rule.Abscissas;
        double[] weights = rule.Weights;
		
		double half_length = 0;
        
        if (nonstandardinterval == 1){
            double mdpt = (lower_bound + upper_bound)/ 2.0;
            half_length = (upper_bound - lower_bound)/2.0;
            for (int i = 0; i < nodes.Length; i++){
                nodes[i] = nodes[i]*half_length+mdpt;
            } 
        }

        double[] values = new double[n];
        
        for(int i = 0; i < values.Length; i++){
            values[i] = func(nodes[i]);
        }

		double result = 0;
        for (int i = 0; i < weights.Length; i++){
			result += (weights[i] * values[i]);
		}

        if (nonstandardinterval == 1){
            result = half_length * result;
        }
        return result;
    }

    // Driver Code
    public static void Main()
    {
        double bottom = (double) 4;

        double top = (double) 5.2;
         
        // Number of interval
        int n = 6;
         
        Console.WriteLine(gaussian_quadrature_(bottom, top, n));
		Console.WriteLine(simpsons_(bottom, top, n));
    }
}