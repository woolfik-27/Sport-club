using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cursov
{
    public partial class Registration : Form
    {
        private Authorization authorizationForm;

        public Registration(Authorization authorizationForm)
        {
            InitializeComponent();
            this.authorizationForm = authorizationForm;
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            cmbRole.Items.Add("Клиент");
            cmbRole.Items.Add("Тренер");
            cmbRole.SelectedIndex = 0;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (name == "" || login == "" || password == "")
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            
            string role = cmbRole.SelectedIndex == 1 ? "trainer" : "client";

            try
            {
              
                string checkSql = "SELECT COUNT(*) FROM users WHERE login = @login";

                using (MySqlCommand cmd = DbClass.Database.GetCommand(checkSql))
                {
                    cmd.Parameters.AddWithValue("@login", login);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Логин уже занят");
                        return;
                    }
                }

                string insertSql = @"
                    INSERT INTO users (login, password_hash, full_name, role)
                    VALUES (@login, @password, @name, @role)
                ";

                using (MySqlCommand cmd = DbClass.Database.GetCommand(insertSql))
                {
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@role", role);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Регистрация успешна!");

                authorizationForm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            authorizationForm.Show();
            this.Close();
        }
    }
}
