using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization; // добавили новый using для сериализации в формат XML
using System.IO; // для более низкоуровневого доступа к файлам

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        // Добавление поля для для хранения объектов 
        public static BindingList<FlightObject> FlightSystemObject = new BindingList<FlightObject>();
        //BindingList обеспечивает универсальную коллекцию, поддерживающую привязку данных
        public Form1(string fileName)
        {
            InitializeComponent();
            dataGridView.DataSource = FlightSystemObject; // Подключаем к таблице
            OpenFile();
        }
        private void OpenFile()
        { // Открываем файл по умолчанию
            try
            {
                var serializer = new XmlSerializer(typeof(FlightObject[]));
                using (var fs = new FileStream("123.txt", FileMode.Open))
                { // Serializer.Deserialize - считывает данные из файла
                    var arrayOfFlightObjects = (FlightObject[])serializer.Deserialize(fs);
                    Form1.FlightSystemObject.Clear(); // Очищаем с объектами
                    foreach (var obj in arrayOfFlightObjects)
                    { // arrayOfFlightObjects содержит набор объектов считанных из файла
                        Form1.FlightSystemObject.Add(obj);
                    }
                }
            }
            catch (IOException) // Искл. при ошибке ввода-вывода
            {
                MessageBox.Show("Файл не найден", "Ошибка ввода-вывода",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        [Serializable] // Переводим в более удобный для хранения формат
        public class FlightObject
        {
            public string city { get; set; } // город
            public string model { get; set; } // модель самолета
            public int number { get; set; } // номер рейса
            public int price { get; set; } // цена
            public int spaces { get; set; } // свободные места
            public string arrive { get; set; } // время посадки
            public string takeoff { get; set; } // вылет
        }
        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            var serializer = new XmlSerializer(typeof(FlightObject[]));
            using (var fs = new FileStream(openFileDialog.FileName, FileMode.Open))
            { // Serializer.Deserialize - считывает данные из файла
                var arrayOfFlightObjects = (FlightObject[])serializer.Deserialize(fs);
                Form1.FlightSystemObject.Clear();
                foreach (var obj in arrayOfFlightObjects)
                {
                    Form1.FlightSystemObject.Add(obj);
                }
            }
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count == 0) 
            {
                MessageBox.Show("Вы не можете сохранить пустой файл");
                return;
            } // SaveFileDialog позволяет выбирать не существующие файлы
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() != DialogResult.OK) // проверка нажатия ОК
                return;
            // сериализатор в XML - преобразовывает данные из 
            // [Serializable] в текстовые файлы особого вида XML
            // массив объектов типа FlightSystemObject
            var serializer = new XmlSerializer(typeof(FlightObject[]));          
            using (var fs = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate))
            { // преобразуем список в массив
                serializer.Serialize(fs, Form1.FlightSystemObject.ToArray());
            }
            MessageBox.Show("Файл сохранен");
        }
        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new Form3(); 
            var obj = new FlightObject(); // экз. кл. fl-obj
            form.Field = obj; // Передаем объект на форму       
            if (form.ShowDialog() == DialogResult.OK)// модал. диалог. окно
            { // добавляем объект в список
                Form1.FlightSystemObject.Add(obj); 
            }
        }
        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView.CurrentRow == null) // выходим, если нет активных строк
                return; // по номеру активной строки выбираем в списке объект
            var obj = Form1.FlightSystemObject[this.dataGridView.CurrentRow.Index];
            var form = new Form3(); // Форма редактирования
            form.Field = obj; // Привязываем к ней активный объект
            form.ShowDialog(); // модал. окно
            dataGridView.InvalidateRow(this.dataGridView.CurrentRow.Index); // чтобы строка изменялась полностью, а не только одна ячейка
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var message = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение", MessageBoxButtons.YesNo);
            if (message == DialogResult.Yes) 
                Form1.FlightSystemObject.RemoveAt(this.dataGridView.CurrentRow.Index);// Удаляем
            dataGridView.Refresh();
        }
        private void запрос1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("Добавьте записи в файл!");
                return;
            }
            Form4 zap = new Form4();
            zap.ShowDialog();
        }
        private void Zapros2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count == 0)//Если в файле нет записей
            {
                MessageBox.Show("Добавьте записи в файл!");
                return;
            }
            Form2 fr2 = new Form2();
            fr2.ShowDialog();
        }
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }

}
