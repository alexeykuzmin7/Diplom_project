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
    public partial class Form4 : Form
    {
        Form7 formLink;
        private OleDbConnection connection = new OleDbConnection();
        public Form RefToForm1 { get; set; }
        public Form4(Form7 formLink)
        {
            this.formLink = formLink;
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataBase2.accdb;
Persist Security Info=False;";
        }
        DataTable dt;
        private void Form4_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }
        private void DF()
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "select * from Пользователи";
            command.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            dt = new DataTable();
            da.Fill(dt);
            connection.Close();
        }
        public string sha1(string input)
        {
            byte[] hash;
            using (var sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider())
                hash = sha1.ComputeHash(Encoding.Unicode.GetBytes(input));
            var sb = new StringBuilder();
            foreach (byte b in hash) sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                if(textBox1.Text.Length > 5)
                {
                    string pwNow = textBox1.Text;
                    string pwSalt;
                    string hNow;
                    DF();
                    Random rand = new Random();
                    int r = rand.Next(5, 20);
                    var chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
                    var random = new Random();
                    var result = new string(
                        Enumerable.Repeat(chars, r)
                                  .Select(s => s[random.Next(s.Length)])
                                  .ToArray());
                    pwSalt = result;
                    hNow = sha1(pwNow + pwSalt);
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string query = "update Пользователи set Хеш='" + hNow + "', Соль='" + pwSalt + "' where Код=1";
                    command.CommandText = query;
                    MessageBox.Show("Пароль изменен");
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                else
                {
                    MessageBox.Show("Придумайте более сложный пароль");
                }
            }
            else
            {
                MessageBox.Show("Введите новый пароль");
            }
        }
 
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (textBox1.Text.Length > 5)
                {
                    string pwNow = textBox2.Text;
                    string pwSalt;
                    string hNow;
                    DF();
                    string check = dt.Rows[1][2].ToString();
                    Random rand = new Random();
                    int r = rand.Next(5, 20);
                    var chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
                    var random = new Random();
                    var result = new string(
                        Enumerable.Repeat(chars, r)
                                  .Select(s => s[random.Next(s.Length)])
                                  .ToArray());
                    pwSalt = result;
                    hNow = sha1(pwNow + pwSalt);
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string query = "update Пользователи set Хеш='" + hNow + "', Соль='" + pwSalt + "' where Код=2";
                    command.CommandText = query;
                    MessageBox.Show("Пароль изменен");
                    command.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Придумайте более сложный пароль");
                }
            }
            else
            {
                MessageBox.Show("Введите новый пароль");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 obj1 = new Form1();
            obj1.RefToForm4 = this;
            this.RefToForm1.Show();
            this.Close();
        }
    }
}
