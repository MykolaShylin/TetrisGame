using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisWinFormsApp
{
    public partial class RegistrationForm : Form
    {
        private string name = string.Empty;
        private string secondName = string.Empty;
        private string age = string.Empty;
        private User user;
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void startGameButton_Click(object sender, EventArgs e)
        {
            var question = MessageBox.Show("Вы уверены в коректности введенныйх данных?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (question == DialogResult.Yes)
            {
                name = nameTextBox.Text;
                secondName = secondNameTextBox.Text;
                age = ageTextBox.Text;
                if(name != string.Empty && secondName != string.Empty && age != string.Empty)
                {
                    user = new User(name, secondName, age);
                    Close();
                    var newGame = new MainForm(user);
                    newGame.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void nameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);            
        }

        private void secondNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void ageTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }
    }
}
