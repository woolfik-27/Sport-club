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

            string sql = @"
                SELECT id, full_name, role
                FROM users
                WHERE login = @login AND password_hash = @password
    ";

            try
            {
                using (MySqlCommand cmd = DbClass.Database.GetCommand(sql))
                {
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);

                    bool success = false;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            CurrentUser.Id = reader.GetInt32("id");
                            CurrentUser.Name = reader.GetString("full_name");
                            CurrentUser.Role = reader.GetString("role");

                            success = true;
                        }
                    }

                    if (success)
                    {
                        MessageBox.Show($"Добро пожаловать, {CurrentUser.Name}!");

                        this.Hide();
                        frmSessionsSchedule schedule = new frmSessionsSchedule();
                        schedule.Show();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOpenRegistration_Click(object sender, EventArgs e)
        {
            this.Hide(); 

            Registration registration = new Registration(this);
            registration.Show();
        }

        private void Authorization_Load(object sender, EventArgs e)
        {

        }
    }
}
