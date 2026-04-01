namespace cursov
{
    partial class frmSessionsSchedule
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvSessions = new System.Windows.Forms.DataGridView();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAddSession = new System.Windows.Forms.Button();
            this.btnAchievements = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSessions)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSessions
            // 
            this.dgvSessions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSessions.Location = new System.Drawing.Point(12, 12);
            this.dgvSessions.Name = "dgvSessions";
            this.dgvSessions.RowHeadersWidth = 51;
            this.dgvSessions.RowTemplate.Height = 24;
            this.dgvSessions.Size = new System.Drawing.Size(776, 365);
            this.dgvSessions.TabIndex = 0;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Gold;
            this.btnEdit.Location = new System.Drawing.Point(12, 415);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(112, 23);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Изменить";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAddSession
            // 
            this.btnAddSession.BackColor = System.Drawing.Color.Gold;
            this.btnAddSession.Location = new System.Drawing.Point(13, 386);
            this.btnAddSession.Name = "btnAddSession";
            this.btnAddSession.Size = new System.Drawing.Size(111, 23);
            this.btnAddSession.TabIndex = 2;
            this.btnAddSession.Text = "Добавить";
            this.btnAddSession.UseVisualStyleBackColor = false;
            this.btnAddSession.Click += new System.EventHandler(this.btnAddSession_Click);
            // 
            // btnAchievements
            // 
            this.btnAchievements.BackColor = System.Drawing.Color.Gold;
            this.btnAchievements.Location = new System.Drawing.Point(548, 415);
            this.btnAchievements.Name = "btnAchievements";
            this.btnAchievements.Size = new System.Drawing.Size(240, 23);
            this.btnAchievements.TabIndex = 3;
            this.btnAchievements.Text = "Достижения";
            this.btnAchievements.UseVisualStyleBackColor = false;
            this.btnAchievements.Click += new System.EventHandler(this.btnAchievements_Click);
            // 
            // frmSessionsSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAchievements);
            this.Controls.Add(this.btnAddSession);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.dgvSessions);
            this.Name = "frmSessionsSchedule";
            this.Text = "Расписание тренировок";
            this.Load += new System.EventHandler(this.frmSessionsSchedule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSessions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSessions;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAddSession;
        private System.Windows.Forms.Button btnAchievements;
    }
}