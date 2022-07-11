using System.Windows.Forms;
using System.Collections.Generic;


namespace WindowsFormsApp2
{
    public partial class Form4 : Form
    {
        public string Search // Свойство для поиска
        {
            get // Возвращает ComboBox1.текст
            {
                return ComboBox1.Text;
            }
        }
        public Form4()
        {
            InitializeComponent();
        }
        private void zapros_Shown(object sender, System.EventArgs e) //Заполнение ComboBox
        {
            IEnumerator<Form1.FlightObject> Enum = Form1.FlightSystemObject.GetEnumerator();
            Enum.Reset();
            while (Enum.MoveNext())
            {
                if (ComboBox1.Items.IndexOf(Enum.Current.city) == -1)
                {
                    ComboBox1.Items.Add(Enum.Current.city);
                }
            }
        }
        private void button2_Click(object sender, System.EventArgs e)
        {
            listBox1.Items.Clear();
            if (Search == "") 
                {
                    MessageBox.Show("Вы ничего не ввели");
                    return;
                }
                foreach (var temp in Form1.FlightSystemObject)
                {
                    if (Search == temp.city) 
                    {
                        listBox1.Items.Add("Рейс номер " + temp.number + "; Цена билета: " + temp.price + "\n");
                    }
                }
            if (listBox1.Items.Count == 0) MessageBox.Show("Записей по запросу не найдено!");
        }
        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
