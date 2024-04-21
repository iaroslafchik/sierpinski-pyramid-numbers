using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sierpinski_pyramid_numbers
{
    public partial class Form1 : Form
    {
        int iteration;

        BigInteger vertices;
        BigInteger planes;
        BigInteger pyramids;
        BigInteger edges;

        BigInteger verticesSum;
        BigInteger planesSum;
        BigInteger pyramidsSum;
        BigInteger edgesSum;

        DateTime startTime;
        DateTime endTime;

        public Form1()
        {
            InitializeComponent();

            iteration = 0;

            toolStripTextBox1.Text = iteration.ToString();
            Calculate(this, EventArgs.Empty);
        }

        BigInteger Pow(BigInteger value, BigInteger power)
        {
            if (power == 0)
                return 1;

            BigInteger answer = 1;

            for (; power > 0;)
            {
                if (power % 2 != 0)
                {
                    answer *= value;
                    power = power - 1;
                }
                power /= 2;
                value *= value;
            }
            
            return answer;
        }

        private void Calculate(object sender, EventArgs e)
        {
            iteration = int.Parse(toolStripTextBox1.Text);

            vertices = 5;
            planes = 5;
            pyramids = 1;
            edges = 8;

            verticesSum = vertices;
            planesSum = planes;
            pyramidsSum = pyramids;
            edgesSum = edges;

            startTime = DateTime.Now;

            for (int i = 0; i < iteration; i++)
            {
                vertices = (Pow(2, i + 1 + 4) + 23 * Pow(5, i + 1) + 21) / 12;
                verticesSum += vertices;

                planes = planes * 5 - 3;
                planesSum += planes;

                pyramids = pyramids * 5;
                pyramidsSum += pyramids;

                edges = edges * 5 - Pow(2, i + 1 + 1);
                edgesSum += edges;
            }

            endTime = DateTime.Now;

            dataGridView1.Rows.Clear();

            dataGridView1.Columns[1].HeaderText = string.Format("Количество на {0} итерации", iteration);
            dataGridView1.Columns[2].HeaderText = string.Format("Сумма итераций 0..{0}", iteration);

            dataGridView1.Rows.Add("Вершин", vertices, verticesSum);
            dataGridView1.Rows.Add("Сторон", planes, planesSum);
            dataGridView1.Rows.Add("Пирамид", pyramids, pyramidsSum);
            dataGridView1.Rows.Add("Ребер", edges, edgesSum);

            toolStripLabel2.Text = (endTime - startTime).ToString();
        }
    }
}
