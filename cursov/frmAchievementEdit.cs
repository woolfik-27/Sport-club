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
    public partial class frmAchievementEdit : Form
    {
        public frmAchievementEdit()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sql = @"
            INSERT INTO achievements
            (client_id, sport_id, title, description, achievement_date, created_by)
            VALUES
            (@client,@sport,@title,@desc,@date,@creator)";

            using (MySqlCommand cmd = DbClass.Database.GetCommand(sql))
            {
                
                cmd.Parameters.AddWithValue("@sport", cmbSport.SelectedValue);
                cmd.Parameters.AddWithValue("@title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@desc", txtDescription.Text);
                cmd.Parameters.AddWithValue("@date", dtDate.Value.Date);
                cmd.Parameters.AddWithValue("@creator", CurrentUser.Id);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Достижение добавлено");
            this.Close();
        }
        private void LoadSports()
        {
            string sql = "SELECT id, name FROM sports";

            using (MySqlCommand cmd = DbClass.Database.GetCommand(sql))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                cmbSport.DataSource = table;
                cmbSport.DisplayMember = "name";   // что показывать
                cmbSport.ValueMember = "id";       // что отправлять в БД
            }
        }

        private void frmAchievementEdit_Load(object sender, EventArgs e)
        {
            LoadSports();
        }
    }
}
