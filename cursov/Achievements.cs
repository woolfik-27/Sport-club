using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cursov
{
    public partial class Achievements : Form
    {
        public Achievements()
        {
            InitializeComponent();
        }
        
        private void Achievements_Load(object sender, EventArgs e)
        {
            btnAdd.Visible = CurrentUser.Role == "trainer";
            LoadAchievements();
        }

        private void LoadAchievements()
        {
            string sql = @"
                SELECT a.id, a.title AS 'Название', a.description AS 'Описание', 
                       a.achievement_date AS 'Дата',
                       CASE WHEN a.client_id = @userId THEN 'Моё' ELSE 'Общее' END AS 'Тип'
                FROM achievements a 
                WHERE 1=1  -- Всегда что-то покажет
                ORDER BY a.achievement_date DESC
                LIMIT 10";  // Ограничение для теста

            try
            {
                using (MySqlCommand cmd = DbClass.Database.GetCommand(sql))
                {
                    cmd.Parameters.AddWithValue("@userId", CurrentUser.Id);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        if (table.Rows.Count == 0)
                        {
                            table.Rows.Add(0, "Нет достижений", "Добавьте первое через кнопку", DateTime.Now.Date, "Инфо");
                            MessageBox.Show($"Пустая таблица achievements.\nCurrentUser.Id = {CurrentUser.Id}\nДобавьте тестовые данные!");
                        }

                        dgvAchievements.DataSource = table;
                        if (dgvAchievements.Columns.Contains("id"))
                            dgvAchievements.Columns["id"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка БД: " + ex.Message + "\nID=" + CurrentUser.Id);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAchievementEdit form = new frmAchievementEdit();
            form.ShowDialog();
            LoadAchievements(); 
            form.Dispose();
        }

        private void btnBackToTrain_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

