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
        int sessionId = -1;
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
            cmbStatus.Items.Add("planned");
            cmbStatus.Items.Add("completed");
            cmbStatus.Items.Add("canceled");

            cmbStatus.SelectedIndex = 0;
            if (dtEnd.Value <= dtStart.Value)
            {
                MessageBox.Show("Время окончания должно быть больше времени начала");
                return;
            }
        }
        void LoadClients()
        {
            string sql = "SELECT id, full_name FROM users WHERE role='client'";
            MySqlCommand cmd = DbClass.Database.GetCommand(sql);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();

            adapter.Fill(table);

            cmbClient.DataSource = table;
            cmbClient.DisplayMember = "full_name";
            cmbClient.ValueMember = "id";
        }

        void LoadTrainers()
        {
            string sql = "SELECT id, full_name FROM users WHERE role='trainer'";
            MySqlCommand cmd = DbClass.Database.GetCommand(sql);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();

            adapter.Fill(table);

            cmbTrainer.DataSource = table;
            cmbTrainer.DisplayMember = "full_name";
            cmbTrainer.ValueMember = "id";
        }
        void LoadSports()
        {
            string sql = "SELECT id, name FROM sports";

            MySqlCommand cmd = DbClass.Database.GetCommand(sql);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();

            adapter.Fill(table);

            cmbSport.DataSource = table;
            cmbSport.DisplayMember = "name";
            cmbSport.ValueMember = "id";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sql;

            if (sessionId == -1)
            {
            
                sql = @"
                INSERT INTO sessions
                (trainer_id, client_id, sport_id, session_date, start_time, end_time, status, created_by)
                VALUES
                (@trainer,@client,@sport,@date,@start,@end,@status,@creator)";
            }
            else
            {
       
                sql = @"
                UPDATE sessions
                SET trainer_id=@trainer,
                client_id=@client,
                sport_id=@sport,
                session_date=@date,
                start_time=@start,
                end_time=@end,
                status=@status
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
                    cmd.Parameters.AddWithValue("@start", dtStart.Value.TimeOfDay);
                    cmd.Parameters.AddWithValue("@end", dtEnd.Value.TimeOfDay);
                    cmd.Parameters.AddWithValue("@status", cmbStatus.Text);

                    if (sessionId == -1)
                    {
                        cmd.Parameters.AddWithValue("@creator", CurrentUser.Id);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@id", sessionId);
                    }

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Сохранено");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
