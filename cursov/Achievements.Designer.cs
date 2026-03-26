namespace cursov
{
    partial class Achievements
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
            this.dgvAchievements = new System.Windows.Forms.DataGridView();
            this.btnBackToTrain = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAchievements)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAchievements
            // 
            this.dgvAchievements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAchievements.Location = new System.Drawing.Point(12, 50);
            this.dgvAchievements.Name = "dgvAchievements";
            this.dgvAchievements.RowHeadersWidth = 51;
            this.dgvAchievements.RowTemplate.Height = 24;
            this.dgvAchievements.Size = new System.Drawing.Size(776, 347);
            this.dgvAchievements.TabIndex = 0;
            // 
            // btnBackToTrain
            // 
            this.btnBackToTrain.BackColor = System.Drawing.Color.Gold;
            this.btnBackToTrain.Location = new System.Drawing.Point(13, 13);
            this.btnBackToTrain.Name = "btnBackToTrain";
            this.btnBackToTrain.Size = new System.Drawing.Size(75, 23);
            this.btnBackToTrain.TabIndex = 1;
            this.btnBackToTrain.Text = "Назад";
            this.btnBackToTrain.UseVisualStyleBackColor = false;
            this.btnBackToTrain.Click += new System.EventHandler(this.btnBackToTrain_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Gold;
            this.btnAdd.Location = new System.Drawing.Point(309, 415);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(153, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // Achievements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnBackToTrain);
            this.Controls.Add(this.dgvAchievements);
            this.Name = "Achievements";
            this.Text = "Достижения";
            this.Load += new System.EventHandler(this.Achievements_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAchievements)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAchievements;
        private System.Windows.Forms.Button btnBackToTrain;
        private System.Windows.Forms.Button btnAdd;
    }
}