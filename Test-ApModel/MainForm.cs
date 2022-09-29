using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Test_ApModel
{
    public partial class MainForm : Form
    {
        private readonly double dt = 1;
        private readonly double rightBound = 600;

        public MainForm()
        {
            InitializeComponent();
            this.mainChart.Series.Clear();
            GraphAperiodic(0.297, 140, "Апроксимований результат");
            GraphPoints(
                new double[] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120,
                               140, 150, 160, 170, 180, 190, 200, 210, 220, 230, 240, 250,
                               260, 270, 280, 290, 300, 310, 320, 330, 340, 350, 360, 370,
                               380, 390, 400, 410, 420, 430, 440, 450, 460, 470, 480, 490,
                               500, 510, 520, 530, 540, 550, 560, 570, 580, 590, 600 },
                new double[] { 0, 0.025926, 0.046296, 0.064815, 0.07963, 0.096296, 0.109259,
                               0.122222, 0.131481, 0.140741, 0.151852, 0.166667, 0.183333, 0.189815,
                               0.196296, 0.201852, 0.207407, 0.212963, 0.218519, 0.223148, 0.227778,
                               0.232407, 0.237037, 0.239815, 0.242593, 0.247222, 0.251852, 0.255556,
                               0.259259, 0.262963, 0.266667, 0.267593, 0.268519, 0.269444, 0.27037,
                               0.272222, 0.274074, 0.275926, 0.277778, 0.279167, 0.280556, 0.281944,
                               0.283333, 0.284568, 0.285802, 0.287037, 0.288272, 0.289506, 0.290741,
                               0.291049, 0.291358, 0.291667, 0.291975, 0.292284, 0.292593, 0.292901,
                               0.29321, 0.293519, 0.293827, 0.294136, 0.294444 },
                "Експерементальні дані");
        }

        private void GraphAperiodic(double K, double T, string name)
        {
            var series = this.mainChart.Series.Add(name);
            series.ChartType = SeriesChartType.Spline;
            series.BorderWidth = 2;

            var ap = new Aperiodic(this.dt, K, T);
            for(double x = 0; x <= this.rightBound; x += dt)
            {
                var newVal = ap.Calc(1);
                series.Points.Add(new DataPoint(x, newVal));
            }
        }

        private void GraphPoints(double[] xPoints, double[] yPoints, string name)
        {
            var series = this.mainChart.Series.Add(name);
            series.ChartType = SeriesChartType.Spline;
            series.BorderWidth = 2;

            for (int i = 0; i < xPoints.Length; i++)
            {
                series.Points.Add(new DataPoint(xPoints[i], yPoints[i]));
            }
        }
    }
}
