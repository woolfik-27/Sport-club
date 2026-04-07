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
using static cursov.DbClass;

namespace cursov
{
    public partial class frmSessionsSchedule : Form
    {
        public frmSessionsSchedule()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(frmSessionsSchedule_FormClosing);
        }

        private void frmSessionsSchedule_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
        private void LoadSessions()
        {
            string sql;
            if (CurrentUser.Role == "client")
            {
                sql = @"
                    SELECT s.id, s.session_date AS 'Дата', s.start_time AS 'Начало', 
                           s.end_time AS 'Конец', sp.name AS 'Спорт', 
                           u.full_name AS 'Тренер',
                           CASE WHEN s.status = 'planned' THEN 'Запланирована'
                                WHEN s.status = 'completed' THEN 'Завершена'
                                WHEN s.status = 'canceled' THEN 'Отменена' END AS 'Статус'
                    FROM sessions s JOIN sports sp ON sp.id = s.sport_id 
                    JOIN users u ON u.id = s.trainer_id WHERE s.client_id = @userId 
                    ORDER BY s.session_date, s.start_time";
            }
            else
            {
                sql = @"
                    SELECT s.id, s.session_date AS 'Дата', s.start_time AS 'Начало', 
                           s.end_time AS 'Конец', sp.name AS 'Спорт', 
                           uc.full_name AS 'Клиент', ut.full_name AS 'Тренер',
                           CASE WHEN s.status = 'planned' THEN 'Запланирована'
                                WHEN s.status = 'completed' THEN 'Завершена'
                                WHEN s.status = 'canceled' THEN 'Отменена' END AS 'Статус'
                    FROM sessions s JOIN sports sp ON sp.id = s.sport_id 
                    JOIN users uc ON uc.id = s.client_id JOIN users ut ON ut.id = s.trainer_id 
                    ORDER BY s.session_date, s.start_time";
            }

            try
            {
                using (MySqlCommand cmd = DbClass.Database.GetCommand(sql))
                {
                    if (CurrentUser.Role == "client")
                        cmd.Parameters.AddWithValue("@userId", CurrentUser.Id);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dgvSessions.DataSource = table;
                        if (dgvSessions.Columns.Contains("id"))
                            dgvSessions.Columns["id"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки расписания: " + ex.Message);
            }
        }

        private void frmSessionsSchedule_Load(object sender, EventArgs e)
        {
            // ⭐ Кнопки по ролям
            if (CurrentUser.Role == "client")
            {
                btnEdit.Visible = false;  
            }
            else
            {
                btnEdit.Visible = true; 
            }

            LoadSessions();
        }

        private void btnAddSession_Click(object sender, EventArgs e)
        {
            frmSessionEdit form = new frmSessionEdit();
            if (form.ShowDialog() == DialogResult.OK)
                LoadSessions();
            form.Dispose();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSessions.CurrentRow == null || dgvSessions.CurrentRow.Cells["id"].Value == null)
            {
                MessageBox.Show("Выберите тренировку для редактирования!");
                return;
            }

            int id = Convert.ToInt32(dgvSessions.CurrentRow.Cells["id"].Value);
            frmSessionEdit form = new frmSessionEdit(id);
            if (form.ShowDialog() == DialogResult.OK)
                LoadSessions();
            form.Dispose();
        }

        private void btnAchievements_Click(object sender, EventArgs e)
        {
            this.Hide();  

            Achievements form = new Achievements();
            form.ShowDialog();

            this.Show();
        }
    }
}
