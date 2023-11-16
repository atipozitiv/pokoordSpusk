using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pokoordSpusk {
  public partial class Form1 : Form {
    public Form1() {
      InitializeComponent();
      MaximizeBox = false;
      FormBorderStyle = FormBorderStyle.Fixed3D;
      this.Text = "метод покоординатного спуска";
    }

    private void Form1_Load(object sender, EventArgs e) {
      label5.Visible = false;
      label6.Visible = false;
      chart1.Visible = false;
      this.chart1.Series[0].Points.Clear();
      this.chart1.Series[1].Points.Clear();
    }

    void drawChart(double start, double end) {
      double x = start;
      while (x < end) {
        double y = Func(x);
        this.chart1.Series[1].Points.AddXY(x, y);
        x += 0.1;
      }
    }

    public void Mistake() {
      label5.Visible = true;
      label6.Visible = false;
      label1.Visible = false;
      label2.Visible = false;
      label3.Visible = false;
      label4.Visible = false;
      textBox1.Visible = false;
      textBox2.Visible = false;
      textBox3.Visible = false;
      chart1.Visible = false;
    }

    double Func(double x) {
      double y = (27 - 18 * x + 2 * x * x) * Math.Exp(-x / 3);
      return y;
    }

    void Logic(double start, double end, double precision) {
      double x = (start + end) / 2;
      double right = x + (end - start) / 4;
      double left = x - (end - start) / 4;
      bool decreace = true;
      if (Func(right) < Func(left)) {
        while(decreace && x < end) {
          double xPast = x;
          x += precision;
          if(Func(x) > Func(xPast)) {
            decreace = false;
          }
        }
      } else {
        while(decreace && x > start) {
          double xPast = x;
          x -= precision;
          if(Func(x) > Func(xPast)) {
            decreace = false;
          }
        }
      }
      label6.Text = $"x = {Math.Round(x, 7)}    y = {Math.Round(Func(x), 7)}";
      label6.Visible = true;
    }

    private void рассчитатьToolStripMenuItem_Click(object sender, EventArgs e) {
      this.chart1.Series[0].Points.Clear();
      this.chart1.Series[1].Points.Clear();
      try {
        double startLine = Convert.ToDouble(textBox1.Text);
        double endLine = Convert.ToDouble(textBox2.Text);
        double precision = Convert.ToDouble(textBox3.Text);
        drawChart(startLine, endLine);
        chart1.Visible = true;
        if ((startLine >= endLine) || (precision <= 0) || (precision >= endLine - startLine)) {
          Mistake();
        } else {
          Logic(startLine, endLine, precision);
        }
      } catch {
        Mistake();
      }
    }

    private void очиститьToolStripMenuItem_Click(object sender, EventArgs e) {
      this.chart1.Series[0].Points.Clear();
      this.chart1.Series[1].Points.Clear();
      textBox1.Text = "";
      textBox2.Text = "";
      textBox3.Text = "";
      label5.Visible = false;
      label6.Visible = false;
      label1.Visible = true;
      label2.Visible = true;
      label3.Visible = true;
      label4.Visible = true;
      textBox1.Visible = true;
      textBox2.Visible = true;
      textBox3.Visible = true;
      chart1.Visible = true;
    }
  }
}
