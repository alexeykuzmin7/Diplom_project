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
    public partial class Form7 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        Form1 formLink;
        public Form RefToForm1 { get; set; }
        public Form7(Form1 formLink)
        {
            this.formLink = formLink;
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
            DF();
            string check = dt.Rows[0][2].ToString();
            pwNow = textBox1.Text;
            pwSalt = dt.Rows[0][3].ToString();
            hNow = sha1(pwNow + pwSalt);
            if (hNow == check)
            {
                this.Close();
                Form4 form4 = new Form4(this);
                form4.RefToForm1 = RefToForm1;
                form4.Show();
            }
            else
            {
                MessageBox.Show("Неправильный пароль");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 obj1 = new Form1();
            obj1.RefToForm7 = this;
            this.Close();
            this.RefToForm1.Show();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }
    }
}
