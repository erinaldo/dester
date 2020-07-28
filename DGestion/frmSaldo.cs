using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DGestion
{
    public partial class frmSaldo : Form
    {
        public frmSaldo()
        {
            InitializeComponent();
        }

        private void frmSaldo_Load(object sender, EventArgs e) {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (GBox1.Visible == false)
                GBox1.Visible = true;
            else
                GBox1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (GBox2.Visible == false)
                GBox2.Visible = true;
            else
                GBox2.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (GBox3.Visible == false)
                GBox3.Visible = true;
            else
                GBox3.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (GBox4.Visible == false)
                GBox4.Visible = true;
            else
                GBox4.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (GBox5.Visible == false)
                GBox5.Visible = true;
            else
                GBox5.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (GBox6.Visible == false)
                GBox6.Visible = true;
            else
                GBox6.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (GBox7.Visible == false)
                GBox7.Visible = true;
            else
                GBox7.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (GBox8.Visible == false)
                GBox8.Visible = true;
            else
                GBox8.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (GBox9.Visible == false)
                GBox9.Visible = true;
            else
                GBox9.Visible = false;
        }

    }
}
