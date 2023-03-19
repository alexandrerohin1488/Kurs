using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string sql = "SELECT * FROM DocType";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);

                ds = new DataTable();
                adapter.Fill(ds);
                comboBox1.DataSource = ds;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "ID";
                connection.Close();
            }

            sql = "SELECT * FROM Auto";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);

                ds = new DataTable();
                adapter.Fill(ds);
                comboBox2.DataSource = ds;
                comboBox2.DisplayMember = "Name";
                comboBox2.ValueMember = "ID";
                connection.Close();
            }


        }

        Form2 form = new Form2();
        bool dostup = false; //Авторизован или нет
        string nameP = "";

        DataTable ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=DESKTOP-1RKGT1L\SQLEXPRESS;Initial Catalog=KursBD;Integrated Security=True";


        private void button3_Click(object sender, EventArgs e)
        {
            if (dostup == false)
            {
                MessageBox.Show("Вы должны авторизоваться!", "Предупреждение!");
                return;
            }
            string imenov1 = textBox1.Text;
            string imenov2 = textBox4.Text;
            string category1 = comboBox1.SelectedValue.ToString();
            string imenov3 = textBox5.Text;
            string imenov4 = textBox3.Text;
            string category2 = comboBox2.SelectedValue.ToString();
            string imenov5 = textBox7.Text;
            string imenov6 = textBox8.Text;
            if (imenov1 != "" & imenov2 != "" & category1 != "" & imenov3 != "" & imenov4 != "" & category2 != "" & imenov5 != "" & imenov6 != "")
            {
                string sqlExpression = "INSERT INTO [dbo].[Procat]"
                + "([NameClient]"
               + "        ,[PhoneNumber]"
                + "       ,[DocType_ID]"
                + "       ,[NumberDoc]"
               + "        ,[Adress]"
                + "       ,[Auto_ID]"
                + "       ,[Price]"
                  + "     ,[Time])"
           + "  VALUES"
                 + "  ( '"+imenov1+"'"
                 + "  , '"+imenov2+"'"
                  + " , "+category1+""
                 + "  , '"+imenov3+"'"
                 + "  ,'"+imenov4+"'"
                  + " , "+category2+""
                  + " ,"+imenov5+""
                   + ", "+imenov6+")";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    int number = command.ExecuteNonQuery();
                    MessageBox.Show("Добавлено объектов: " + number);
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Предупреждение!");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Users";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);

                ds = new DataTable();
                adapter.Fill(ds);
                connection.Close();
            }
            foreach (DataRow row in ds.Rows)
            {
 
                if (textBox2.Text.ToLower() == row[1].ToString().ToLower() && textBox9.Text.ToLower() == row[2].ToString().ToLower())
                {
                    nameP = textBox2.Text;
                    dostup = true;
                    groupBox1.Visible = true; //Открываем рабочую область
                    groupBox3.Visible = true;
                    pictureBox2.Visible = true;
                    groupBox2.Visible = false; //Скрываем объекты
                    label9.Visible = false;
                    pictureBox1.Visible = false;
                    label12.Text = nameP;
                    return;

                }
                else
                {
                    MessageBox.Show("Такого менеджера не существует, возможно вы ошиблись при вводе данных!", "Предупреждение!");
                }


            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close(); //Выход из программы
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (nameP != "")
            {
                form.Show();
            }
            else
            {
                MessageBox.Show("Вы должны авторизоваться, чтобы просматривать Базу Данных Прокат автомобилей!", "Уведомление");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            button2.Visible = false;
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
