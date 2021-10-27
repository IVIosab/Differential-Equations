using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DifferentialEquations_ComputationalPracticum
{
    // The main class that contains common fields and methods needed by all descendents
    public class Numerical_Method
    {
        public double[] x, y;
        public int N;
        protected double c, x0, y0, X, h;

        public Numerical_Method(int N, double x0, double y0, double X)
        {
            this.N = N + 1;
            this.X = X;
            this.x0 = x0;
            this.y0 = y0;
            if (x0 == 0)
            {
                MessageBox.Show("IVP has no solution");
                Application.Exit();
            }
            c = x0 * Math.Sqrt(y0);
            h = (X - x0) / N;
        }

        // Prepares the XY values for the exact graph with step 0.01
        public void Calculate()
        {
            int n = (int)((X - x0) / 0.01);
            x = new double[n + 1];
            y = new double[n + 1];
            int co = 0;
            for (double i = x0; i <= X; i += 0.01)
            {
                x[co] = i;
                y[co] = Exact(i);
                co++;
            }
        }

        // The function that the application operates on: f(x, y) = y'
        protected double F(double x, double y)
        {
            if (x == 0.0)
            {
                MessageBox.Show("Interval [x0, X] has a point of discontinuity");
                Application.Exit();
            }
            return (((2 * Math.Sqrt(y) * (Math.Cos(x))) / x) - ((2 * y) / (x)));
        }

        // Returns the exact solution calculated at some point x. 
        protected double Exact(double x)
        {
            if (x == 0.0)
            {
                MessageBox.Show("Interval [x0, X] has a point of discontinuity");
                Application.Exit();
            }
            return ((Math.Pow((Math.Sin(x) + c) ,2)) / Math.Pow(x, 2));
        }

        // Checks 3 values for Overflow
        protected void CheckOverflow(double a, double b, double c)
        {
            if (double.IsInfinity(a) || double.IsInfinity(b) || double.IsInfinity(c))
            {
                MessageBox.Show("Overflow: Calculations exceeded the range for Double data type");
                Application.Exit();
            }
        }
    }
}
