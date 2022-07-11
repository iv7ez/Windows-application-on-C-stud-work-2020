using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите город!");
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("Введите цену билета!");
            }
            else
            {
                try
                {
                    foreach (var temp in Form1.FlightSystemObject)
                    {
                        if ((Convert.ToInt32(temp.price) < Convert.ToInt32(textBox2.Text)) && (textBox1.Text == temp.city))
                        {
                            listBox1.Items.Add("город: " + temp.city + ", номер рейса: " + temp.number + ", количество мест: " + temp.spaces);
                        }
                    }
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("Вы ввели символ! Пожалуйста,введите цифрy");
                }
                textBox2.Clear();
                if (listBox1.Items.Count == 0) MessageBox.Show("Записей по запросу не найдено!");
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

