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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace cursov
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();

            string sqlCheck = "SELECT COUNT(*) FROM users WHERE login = @login AND password_hash = @password";
            try
            {
                using (MySqlCommand cmdCheck = DbClass.Database.GetCommand(sqlCheck))
                {
                    cmdCheck.Parameters.AddWithValue("@login", login);
                    cmdCheck.Parameters.AddWithValue("@password", password);
                    int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (count > 0)
                    {
                        string sqlUser = "SELECT id, full_name, role FROM users WHERE login = @login AND password_hash = @password";
                        using (MySqlCommand cmdUser = DbClass.Database.GetCommand(sqlUser))
                        {
                            cmdUser.Parameters.AddWithValue("@login", login);
                            cmdUser.Parameters.AddWithValue("@password", password);
                            using (var reader = cmdUser.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    CurrentUser.Id = reader.GetInt32("id");
                                    CurrentUser.Name = reader.GetString("full_name");
                                    CurrentUser.Role = reader.GetString("role");
                                }
                            }
                        }

                        string roleMsg = CurrentUser.Role == "client" ? "клиент" : "тренер";
                        MessageBox.Show($"Добро пожаловать, {CurrentUser.Name}! Вы вошли как {roleMsg}.");
                        this.Hide();
                        new frmSessionsSchedule().Show();
                        return;
                    }
                }
                MessageBox.Show("Неверный логин или пароль");
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message); }
        }

        private void btnOpenRegistration_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Registration(this).Show();
        }
    }
}
