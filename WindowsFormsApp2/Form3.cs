using System;
using System.Windows.Forms;


namespace WindowsFormsApp2
{
    public partial class Form3 : Form
    { // это скрытое поле, в котором будет хранится непосредственно объект
        public Form1.FlightObject _field; 
        // свойство, которое взаимодействует между формой и скрытым полем
        // св-во ничего хранить не может, оно использует для этого скрытое поле 
        public Form1.FlightObject Field
        { // когда мы обращаемся к Form1.FlightObject.Field вызывается этот метод
            get 
            {
                return this._field;             }
            set // метод вызывается, когда мы присваиваем значения
            { //присваиваем скрытому полю значение value
                this._field = value; 
                //заполняем поля на форме
                textBox1.Text = this._field.city;
                textBox2.Text = this._field.model;
                textBox3.Text = this._field.number.ToString();
                textBox4.Text = this._field.price.ToString();
                textBox5.Text = this._field.spaces.ToString();
                textBox6.Text = this._field.arrive;
                textBox7.Text = this._field.takeoff;
            }
        }
        public Form3()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control s in groupBox1.Controls)
            {
                if (s.GetType().Name=="TextBox")
                {
                    if (String.IsNullOrEmpty(s.Text))
                    {
                        MessageBox.Show("Не все поля заполнены");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                }
            }
            try
            { //Если в поля вводятся не целые числа
                Convert.ToUInt32(textBox3.Text); // 0-65535
                Convert.ToUInt32(textBox4.Text);
                Convert.ToUInt32(textBox5.Text);
            }
            catch
            {
                MessageBox.Show("Введено некорректное значение");
                this.DialogResult = DialogResult.None;
                return;
            }
            Field.city = textBox1.Text;
            Field.model = textBox2.Text;
            Field.number = int.Parse(textBox3.Text);
            Field.price = int.Parse(textBox4.Text);
            Field.spaces = int.Parse(textBox5.Text);
            Field.arrive = textBox6.Text;
            Field.takeoff = textBox7.Text;
        }
    }
}
