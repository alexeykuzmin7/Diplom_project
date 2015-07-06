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
    public partial class Form1 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Form RefToForm7 { get; set; }
        public Form RefToForm4 { get; set; }
        public Form1()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataBase2.accdb;
Persist Security Info=False;";
        }
        DataTable dt;
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
        private void Form1_Load(object sender, EventArgs e)
        {

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
            string pwNow;
            string pwSalt;
            string hNow;
            try
            {
                if (comboBox1.Text == "Администратор")
                {
                    DF();
                    string check = dt.Rows[0][2].ToString();
                    if (check == "")
                    {
                        Form5 obj5 = new Form5();
                        obj5.RefToForm1 = this;
                        this.Visible = false;
                        obj5.Show();
                    }
                    pwNow = textBox1.Text;
                    pwSalt = dt.Rows[0][3].ToString();
                    hNow = sha1(pwNow + pwSalt);
                    if(hNow == check)
                    {
                        MessageBox.Show("Здравствуйте Администратор");
                        Hide();
                        new Form2("").Show();
                    }
                }
                else if (comboBox1.Text == "Пользователь")
                {
                    DF();
                    string check = dt.Rows[1][2].ToString();
                    if (check == "")
                    {
                        Form6 obj6 = new Form6();
                        obj6.RefToForm1 = this;
                        this.Visible = false;
                        obj6.Show();
                    }
                    pwNow = textBox1.Text;
                    pwSalt = dt.Rows[1][3].ToString();
                    hNow = sha1(pwNow + pwSalt);
                    if (hNow == check)
                    {
                        MessageBox.Show("Здравствуйте Пользователь");
                        Hide();
                        new Form2("0").Show();
                    }
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form7 obj7 = new Form7(this);
            obj7.RefToForm1 = this;
            Form4 obj4 = new Form4(obj7);
            obj4.RefToForm1 = this;
            this.Visible = false;
            obj7.Show();
        }
    }
}
