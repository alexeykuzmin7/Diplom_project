using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace Diplom_Project_v2
{
    public partial class Form3 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        Form2 formLink;
        public Form3(Form2 formLink)
        {
            this.formLink = formLink;
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataBase.accdb;
Persist Security Info=False;";
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "insert into Медикаменты (Наименование,Единица,Количество,Пр,Цена,Страна) values ('" + textBox1.Text + "','" + comboBox1.Text + "','" + textBox2.Text + "','" + comboBox2.Text + "','" + textBox3.Text + "','" + comboBox3.Text + "')";
                command.ExecuteNonQuery();
                string query2 = "select * from Медикаменты";
                command.CommandText = query2;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                formLink.dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Похоже что Вы ввели что то не верно");
            }
            connection.Close();
        }
    }
}
