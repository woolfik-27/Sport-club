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
        private frmSessionsSchedule scheduleForm;

        public Achievements()
        {
            InitializeComponent();
            this.scheduleForm = scheduleForm;

        }

        private void btnBackToTrain_Click(object sender, EventArgs e)
        {
            scheduleForm.Show();
            this.Close();
        }

        private void Achievements_Load(object sender, EventArgs e)
        {
            LoadAchievements();

            if (CurrentUser.Role == "client")
            {
                btnAdd.Visible = false;
            }
        }
        private void LoadAchievements()
        {
            string sql = @"
                SELECT 
                a.id,
                a.title AS 'Название',
                a.description AS 'Описание',
                a.achievement_date AS 'Дата'
                FROM achievements a
                WHERE a.client_id = 5
                ORDER BY a.achievement_date DESC";

            try
            {
                using (MySqlCommand cmd = DbClass.Database.GetCommand(sql))
                {
                    
                    cmd.Parameters.AddWithValue("@clubId", 5);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable table = new DataTable();

                    adapter.Fill(table);

                    dgvAchievements.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAchievementEdit form = new frmAchievementEdit();
            form.ShowDialog();

            LoadAchievements();
        }
    }
}

