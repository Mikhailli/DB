using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLogic;
using Ninject;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public static IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());
        BL bL = ninjectKernel.Get<BL>();

        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            bL.GetAll();
            comboBox1.DataSource = bL.TransformCitiesToString();           
            visualizeComponent();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            bL.AddEmployee(textBox1.Text, Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text),
                Convert.ToInt32(comboBox1.SelectedItem.ToString().Substring(0,1)));            
            visualizeComponent();
        }

        void visualizeComponent()
        {           
            label4.Text = "Средний возраст: " + bL.AverageAge().ToString();
            label5.Text = "Средняя зарплата: " + bL.AverageSalary().ToString();
            listView1.Items.Clear();
            foreach (string str in bL.TransformEmployeesToString())
            {
                string[] name = str.Split();
                string[] s = new string[4];
                s[0] = name[1];
                s[1] = name[2];
                s[2] = name[3];
                s[3] = name[4];
                listView1.Items.Add(name[0]).SubItems.AddRange(s);
            }
            comboBox1.DataSource = bL.TransformCitiesToString();
        }

        void buttonCheck()
        {
            if (textBox2.Text.Length == 0 || textBox1.Text.Length == 0 || textBox3.Text.Length == 0) //|| textBox4.Text.Length == 0)
            {
                button1.Enabled = false;
            }
            else button1.Enabled = true;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            buttonCheck();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            buttonCheck();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            buttonCheck();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                string[] str = item.Text.Split();
                bL.DeleteEmployees(Convert.ToInt32(str[0]));
            }
            visualizeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label6.Text = bL.GetById(Convert.ToInt32(textBox4.Text));
        }

        

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length != 0)
                button3.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bL.AddCity(textBox5.Text);
            visualizeComponent();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Length != 0)
                button4.Enabled = true;
        }
    }
}
