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
    public partial class Form2 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        private string user;
        public Form2(string user)
        {
            this.user = user;
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataBase.accdb;
Persist Security Info=False;";
            if(user == "0")
            {
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                textBox4.Visible = false;
                textBox1.Visible = false;
                comboBox1.Visible = false;
                textBox2.Visible = false;
                comboBox2.Visible = false;
                textBox3.Visible = false;
                comboBox3.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
            }
        }
        DataTable dt;
        private void TableFill()
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "select * from Медикаменты";
            command.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            TableFill();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form3(this).Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Developed by Кузьмин А.В. (alexey.kuzmin.7@mail.ru)");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "update Медикаменты set Наименование='" + textBox1.Text + "', Единица='" + comboBox1.Text + "', Количество='" + textBox2.Text + "', Пр='" + comboBox2.Text + "', Цена='" + textBox3.Text + "', Страна='" + comboBox3.Text + "' where Код=" + textBox4.Text + "";
                command.CommandText = query;
                MessageBox.Show("Запись изменена");
                command.ExecuteNonQuery();
                string query2 = "select * from Медикаменты";
                command.CommandText = query2;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Похоже что Вы ввели что то не верно");
            }
            connection.Close();
        }

        private bool IsNumeric(string text)
        {
            if (text.Length == 0)
                return false;

            foreach (char c in text)
                if (c < '0' || c > '9')
                    return false;

            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "delete from Медикаменты where Код=" + textBox4.Text + "";
                if (textBox4.Text != "1")
                {
                    if (IsNumeric(textBox4.Text))
                    {
                        textBox1.Text = "";
                        comboBox1.Text = "";
                        textBox2.Text = "";
                        comboBox2.Text = "";
                        textBox3.Text = "";
                        comboBox3.Text = "";
                        command.CommandText = query;
                        MessageBox.Show("Запись удалена");
                    }
                    else
                    {
                        MessageBox.Show("Код может быть только числом");
                    }
                }
                else
                {
                    MessageBox.Show("");
                }
                command.ExecuteNonQuery();
                string query2 = "select * from Медикаменты";
                command.CommandText = query2;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Введите верный код строки");
            }
            connection.Close();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(dataGridView1.Size.Width + 10, dataGridView1.Size.Height + 10);
            dataGridView1.DrawToBitmap(bmp, dataGridView1.Bounds);
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = null;
            foreach (DataGridViewCell selectedCell in dataGridView1.SelectedCells)
            {
                cell = selectedCell;
                break;
            }
            if (cell != null)
            {
                DataGridViewRow row = cell.OwningRow;
                textBox4.Text = row.Cells[0].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                comboBox1.Text = row.Cells[2].Value.ToString();
                textBox2.Text = row.Cells[3].Value.ToString();
                comboBox2.Text = row.Cells[4].Value.ToString();
                textBox3.Text = row.Cells[5].Value.ToString();
                comboBox3.Text = row.Cells[6].Value.ToString();
            }
        }

        private void TableSearch()
        {
            string q = "1=1";
            if (textBox5.Text != "")
            {
                q += " AND Код like('%" + textBox5.Text + "%')";
            }
            if (textBox8.Text != "")
            {
                q += " AND Наименование like('%" + textBox8.Text + "%')";
            }
            if (textBox9.Text != "")
            {
                q += " AND Единица like('%" + textBox9.Text + "%')";
            }
            if (textBox7.Text != "")
            {
                q += " AND Количество like('%" + textBox7.Text + "%')";
            }
            if (textBox10.Text != "")
            {
                q += " AND Пр like('%" + textBox10.Text + "%')";
            }
            if (textBox6.Text != "")
            {
                q += " AND Цена like('%" + textBox6.Text + "%')";
            }
            if (textBox11.Text != "")
            {
                q += " AND Страна like('%" + textBox11.Text + "%')";
            }

            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "select * from Медикаменты where " + q;
            command.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            TableSearch();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            TableSearch();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            TableSearch();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            TableSearch();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            TableSearch();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            TableSearch();
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            TableSearch();
        }
    }
}
