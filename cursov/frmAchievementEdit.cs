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

        private void frmAchievementEdit_Load(object sender, EventArgs e)
        {
            LoadSports();
        }

        private void LoadSports()
        {
            try
            {
                using (MySqlCommand cmd = DbClass.Database.GetCommand("SELECT id,name FROM sports"))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    var dt = new System.Data.DataTable();
                    adapter.Fill(dt);
                    cmbSport.DataSource = dt;
                    cmbSport.DisplayMember = "name";
                    cmbSport.ValueMember = "id";
                }
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || dtDate.Value.Date > DateTime.Today)
            {
                MessageBox.Show("Проверьте название и дату!");
                return;
            }

            string sql = @"INSERT INTO achievements(client_id,sport_id,title,description,achievement_date,created_by) 
                          VALUES(@c,@s,@t,@d,@ad,@cb)";
            try
            {
                using (MySqlCommand cmd = DbClass.Database.GetCommand(sql))
                {
                    cmd.Parameters.AddWithValue("@c", CurrentUser.Id);
                    cmd.Parameters.AddWithValue("@s", cmbSport.SelectedValue ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@t", txtTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@d", txtDescription.Text.Trim() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ad", dtDate.Value.Date);
                    cmd.Parameters.AddWithValue("@cb", CurrentUser.Id);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Добавлено!");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message); }
        }
    }
}
