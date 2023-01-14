namespace TetrisWinFormsApp
{
    partial class RegistrationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param Name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.startGameButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.secondNameTextBox = new System.Windows.Forms.TextBox();
            this.ageTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // startGameButton
            // 
            this.startGameButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.startGameButton.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.startGameButton.Location = new System.Drawing.Point(0, 168);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(550, 49);
            this.startGameButton.TabIndex = 0;
            this.startGameButton.Text = "Начать игру";
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.Click += new System.EventHandler(this.startGameButton_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(550, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "Введите ваше имя, фамилию и возраст";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nameTextBox.Location = new System.Drawing.Point(0, 86);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.PlaceholderText = "Имя";
            this.nameTextBox.Size = new System.Drawing.Size(151, 27);
            this.nameTextBox.TabIndex = 2;
            this.nameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameTextBox_KeyPress);
            // 
            // secondNameTextBox
            // 
            this.secondNameTextBox.Location = new System.Drawing.Point(201, 86);
            this.secondNameTextBox.Name = "secondNameTextBox";
            this.secondNameTextBox.PlaceholderText = "Фамилия";
            this.secondNameTextBox.Size = new System.Drawing.Size(151, 27);
            this.secondNameTextBox.TabIndex = 3;
            this.secondNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.secondNameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.secondNameTextBox_KeyPress);
            // 
            // ageTextBox
            // 
            this.ageTextBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ageTextBox.Location = new System.Drawing.Point(399, 86);
            this.ageTextBox.Name = "ageTextBox";
            this.ageTextBox.PlaceholderText = "Возраст (лет)";
            this.ageTextBox.Size = new System.Drawing.Size(151, 27);
            this.ageTextBox.TabIndex = 4;
            this.ageTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ageTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ageTextBox_KeyPress);
            // 
            // RegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 217);
            this.Controls.Add(this.ageTextBox);
            this.Controls.Add(this.secondNameTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startGameButton);
            this.Name = "RegistrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Регистрация";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button startGameButton;
        private Label label1;
        private TextBox nameTextBox;
        private TextBox secondNameTextBox;
        private TextBox ageTextBox;
    }
}