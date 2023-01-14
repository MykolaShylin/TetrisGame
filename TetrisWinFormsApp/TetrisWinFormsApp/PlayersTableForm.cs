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
    public partial class PlayersTableForm : Form
    {
        private int _tableWidth;
        private int _tableHeight;
        private List<User> playersResults;
        public PlayersTableForm()
        {

            InitializeComponent();
            _tableWidth = 700;
            _tableHeight = 700;
            ClientSize = new Size(_tableWidth, _tableHeight);
        }
        private void InitTable()
        {
            ScoreTable.Size = new Size(_tableWidth, _tableHeight);
            DataGridViewTextBoxColumn number = new DataGridViewTextBoxColumn();
            number.HeaderText = "№";
            number.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            number.ReadOnly = true;
            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.HeaderText = "Имя";
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            nameColumn.ReadOnly = true;
            DataGridViewTextBoxColumn secondNameColumn = new DataGridViewTextBoxColumn();
            secondNameColumn.HeaderText = "Фамилия";
            secondNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            secondNameColumn.ReadOnly = true;
            DataGridViewTextBoxColumn ageColumn = new DataGridViewTextBoxColumn();
            ageColumn.HeaderText = "Возраст";
            ageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ageColumn.ReadOnly = true;
            DataGridViewTextBoxColumn scoreColumn = new DataGridViewTextBoxColumn();
            scoreColumn.HeaderText = "Количество очков";
            scoreColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            scoreColumn.ReadOnly = true;
            DataGridViewTextBoxColumn countFallenBlocksColumn = new DataGridViewTextBoxColumn();
            countFallenBlocksColumn.HeaderText = "Количество упавших фигур";
            countFallenBlocksColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            countFallenBlocksColumn.ReadOnly = true;

            ScoreTable.Columns.AddRange(number, nameColumn, secondNameColumn, ageColumn, scoreColumn, countFallenBlocksColumn);

            var results = new ResultsStorage();
            playersResults = results.GetUsersResults().OrderBy(x => x.Score).ToList();
            var num = 0;
            
            foreach(var user in playersResults)
            {
                DataGridViewCell numberCell = new DataGridViewTextBoxCell();
                DataGridViewCell nameCell = new DataGridViewTextBoxCell();
                DataGridViewCell secondNameCell = new DataGridViewTextBoxCell();
                DataGridViewCell ageCell = new DataGridViewTextBoxCell();
                DataGridViewCell scoreCell = new DataGridViewTextBoxCell();
                DataGridViewCell fallenBlocksCountCell = new DataGridViewTextBoxCell();

                numberCell.Value = (num++).ToString();
                nameCell.Value = user.Name;
                secondNameCell.Value = user.SecondName;
                ageCell.Value = user.Age;
                scoreCell.Value = user.Score;
                fallenBlocksCountCell.Value = user.FallenBlocksCount;

                DataGridViewRow row = new DataGridViewRow();
                row.Cells.AddRange(numberCell, nameCell, secondNameCell, ageCell, scoreCell, fallenBlocksCountCell);
                ScoreTable.Rows.Add(row);                
            }
        }

        private void PlayersTableForm_Load(object sender, EventArgs e)
        {

        }
    }
}
