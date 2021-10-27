using DifferentialEquations_ComputationalPracticum.Numerical_Methods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DifferentialEquations_ComputationalPracticum
{
    public partial class DE : Form
    {
        public DE()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;

            // Initial textboxes data.
            textBox_x0.Text = "3.14159265359";
            textBox_y0.Text = "2";
            textBox_X.Text = "15.7079632";
            textBox_n0.Text = "50";
            textBox_N.Text = "60";
            button_Click(sender, e);
        }

        private void button_Click(object sender, EventArgs e)
        {
            double x0 = 0, y0 = 0, X = 0;
            int n0 = 0, N = 0;
            try
            {
                x0 = double.Parse(textBox_x0.Text);
                y0 = double.Parse(textBox_y0.Text);
                X = double.Parse(textBox_X.Text);
                n0 = int.Parse(textBox_n0.Text);
                N = int.Parse(textBox_N.Text);
                if (n0 <= 0 || N <= 0 || n0 >= N || x0 >= X) throw new Exception();
            }
            catch
            {
                MessageBox.Show("Invalid input\n");
                Application.Exit();
            }


            int[] cnt = new int[N - n0 + 1];
            double[] EE = new double[N - n0 + 1], IEE = new double[N - n0 + 1], RKE = new double[N - n0 + 1];
            for (int i = n0; i <= N; i++)
            {
                cnt[i - n0] = i;
                Euler tmp1 = new Euler(i, x0, y0, X);
                ImprovedEuler tmp2 = new ImprovedEuler(i, x0, y0, X);
                RungeKutta tmp3 = new RungeKutta(i, x0, y0, X);

                EE[i - n0] = tmp1.Calculate();
                IEE[i - n0] = tmp2.Calculate();
                RKE[i - n0] = tmp3.Calculate();
            }


            Numerical_Method exact = new Numerical_Method(N, x0, y0, X);
            exact.Calculate();
            chartM.Series[3].Points.DataBindXY(exact.x, exact.y);

            if (checkBox_E.Checked)
            {
                Euler m1 = new Euler(N, x0, y0, X);
                m1.Calculate();
                chartM.Series[0].Points.DataBindXY(m1.x, m1.y);
                chartLE.Series[0].Points.DataBindXY(m1.x, m1.e);
                chartGE.Series[0].Points.DataBindXY(cnt, EE);
            }
            if (checkBox_IE.Checked)
            {
                ImprovedEuler m2 = new ImprovedEuler(N, x0, y0, X);
                m2.Calculate();
                chartM.Series[1].Points.DataBindXY(m2.x, m2.y);
                chartLE.Series[1].Points.DataBindXY(m2.x, m2.e);
                chartGE.Series[1].Points.DataBindXY(cnt, IEE);
            }
            if (checkBox_RK.Checked)
            {
                RungeKutta m3 = new RungeKutta(N, x0, y0, X);
                m3.Calculate();
                chartM.Series[2].Points.DataBindXY(m3.x, m3.y);
                chartLE.Series[2].Points.DataBindXY(m3.x, m3.e);
                chartGE.Series[2].Points.DataBindXY(cnt, RKE);
            }

            chartM.ChartAreas[0].AxisX.Minimum = x0;
            chartM.ChartAreas[0].AxisX.Maximum = X;
            chartLE.ChartAreas[0].AxisX.Minimum = x0;
            chartLE.ChartAreas[0].AxisX.Maximum = X;
            chartGE.ChartAreas[0].AxisX.Minimum = n0;
            chartGE.ChartAreas[0].AxisX.Maximum = N;
        }

        private void checkBox_E_CheckedChanged(object sender, EventArgs e)
        {
            chartM.Series[0].Enabled = checkBox_E.Checked;
            chartLE.Series[0].Enabled = checkBox_E.Checked;
            chartGE.Series[0].Enabled = checkBox_E.Checked;
            button_Click(sender, e);
        }

        private void checkBox_IE_CheckedChanged(object sender, EventArgs e)
        {
            chartM.Series[1].Enabled = checkBox_IE.Checked;
            chartLE.Series[1].Enabled = checkBox_IE.Checked;
            chartGE.Series[1].Enabled = checkBox_IE.Checked;
            button_Click(sender, e);
        }

        private void checkBox_RK_CheckedChanged(object sender, EventArgs e)
        {
            chartM.Series[2].Enabled = checkBox_RK.Checked;
            chartLE.Series[2].Enabled = checkBox_RK.Checked;
            chartGE.Series[2].Enabled = checkBox_RK.Checked;
            button_Click(sender, e);
        }
    }
}
