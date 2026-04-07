using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
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
    public partial class frmSessionEdit : Form
    {
        private int sessionId = -1;

        public frmSessionEdit()
        {
            InitializeComponent();
        }

        public frmSessionEdit(int id)
        {
            InitializeComponent();
            sessionId = id;
        }

        private void frmSessionEdit_Load(object sender, EventArgs e)
        {
            LoadClients();
            LoadTrainers();
            LoadSports();

            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Запланирована");
            cmbStatus.Items.Add("Завершена");
            cmbStatus.Items.Add("Отменена");
            cmbStatus.SelectedIndex = 0;

            if (sessionId != -1)
                LoadSessionData();
        }

        private void LoadClients()
        {
            string sql = "SELECT id, full_name FROM users WHERE role='client'";
            LoadComboBox(sql, cmbClient, "full_name");
        }

        private void LoadTrainers()
        {
            string sql = "SELECT id, full_name FROM users WHERE role='trainer'";
            LoadComboBox(sql, cmbTrainer, "full_name");
        }

        private void LoadSports()
        {
            string sql = "SELECT id, name FROM sports";
            LoadComboBox(sql, cmbSport, "name");
        }

        private void LoadComboBox(string sql, ComboBox cmb, string displayMember)
        {
            try
            {
                using (MySqlCommand cmd = DbClass.Database.GetCommand(sql))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    cmb.DataSource = dt;
                    cmb.DisplayMember = displayMember;
                    cmb.ValueMember = "id";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки списка: " + ex.Message);
            }
        }
        private void LoadSessionData()
        {
            string sql = @"
                SELECT s.*, sp.name as sport_name, uc.full_name as client_name, 
                       ut.full_name as trainer_name
                FROM sessions s
                JOIN sports sp ON sp.id = s.sport_id
                JOIN users uc ON uc.id = s.client_id
                JOIN users ut ON ut.id = s.trainer_id
                WHERE s.id = @id";

            try
            {
                using (MySqlCommand cmd = DbClass.Database.GetCommand(sql))
                {
                    cmd.Parameters.AddWithValue("@id", sessionId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cmbClient.SelectedValue = reader["client_id"];
                            cmbTrainer.SelectedValue = reader["trainer_id"];
                            cmbSport.SelectedValue = reader["sport_id"];

                            dtDate.Value = reader.GetDateTime("session_date");
                            dtStart.Value = DateTime.Today + reader.GetTimeSpan("start_time");
                            dtEnd.Value = DateTime.Today + reader.GetTimeSpan("end_time");

                            string statusDb = reader["status"].ToString();
                            switch (statusDb)
                            {
                                case "planned": cmbStatus.SelectedItem = "Запланирована"; break;
                                case "completed": cmbStatus.SelectedItem = "Завершена"; break;
                                case "canceled": cmbStatus.SelectedItem = "Отменена"; break;
                                default: cmbStatus.SelectedIndex = 0; break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных тренировки: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TimeSpan startTime = dtStart.Value.TimeOfDay;
            TimeSpan endTime = dtEnd.Value.TimeOfDay;

            if (endTime <= startTime)
            {
                MessageBox.Show("Время окончания должно быть больше начала!");
                return;
            }
            if (cmbClient.SelectedValue == null || cmbTrainer.SelectedValue == null || cmbSport.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента, тренера и спорт!");
                return;
            }

            string sql;
            if (sessionId == -1)
            {
                sql = @"
                    INSERT INTO sessions (trainer_id, client_id, sport_id, session_date, 
                    start_time, end_time, status, created_by)
                    VALUES (@trainer, @client, @sport, @date, @start, @end, @status, @creator)";
            }
            else
            {
                sql = @"
                    UPDATE sessions SET trainer_id=@trainer, client_id=@client, sport_id=@sport,
                    session_date=@date, start_time=@start, end_time=@end, status=@status
                    WHERE id=@id";
            }

            try
            {
                using (MySqlCommand cmd = DbClass.Database.GetCommand(sql))
                {
                    cmd.Parameters.AddWithValue("@trainer", cmbTrainer.SelectedValue);
                    cmd.Parameters.AddWithValue("@client", cmbClient.SelectedValue);
                    cmd.Parameters.AddWithValue("@sport", cmbSport.SelectedValue);
                    cmd.Parameters.AddWithValue("@date", dtDate.Value.Date);
                    cmd.Parameters.AddWithValue("@start", startTime);
                    cmd.Parameters.AddWithValue("@end", endTime);

                    string statusDb = cmbStatus.SelectedItem.ToString();
                    switch (statusDb)
                    {
                        case "Запланирована": cmd.Parameters.AddWithValue("@status", "planned"); break;
                        case "Завершена": cmd.Parameters.AddWithValue("@status", "completed"); break;
                        case "Отменена": cmd.Parameters.AddWithValue("@status", "canceled"); break;
                        default: cmd.Parameters.AddWithValue("@status", "planned"); break;
                    }

                    if (sessionId == -1)
                        cmd.Parameters.AddWithValue("@creator", CurrentUser.Id);
                    else
                        cmd.Parameters.AddWithValue("@id", sessionId);

                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Тренировка сохранена!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("Тренер уже занят в это время!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message);
            }
        }
    }
}
