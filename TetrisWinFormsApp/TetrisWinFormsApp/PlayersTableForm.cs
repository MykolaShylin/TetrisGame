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
        private ResultsStorage results;
        public PlayersTableForm()
        {

            InitializeComponent();
            _tableWidth = 700;
            _tableHeight = 700;            
        }
        private void InitTable()
        {
            ClientSize = new Size(_tableWidth, _tableHeight);
            ScoreTable.Size = new Size(_tableWidth, _tableHeight);
            DataGridViewTextBoxColumn number = new DataGridViewTextBoxColumn();
            number.HeaderText = "№";
            number.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            number.ReadOnly = true;
            number.SortMode= DataGridViewColumnSortMode.NotSortable;
            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.HeaderText = "Имя";
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            nameColumn.ReadOnly = true;
            nameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewTextBoxColumn secondNameColumn = new DataGridViewTextBoxColumn();
            secondNameColumn.HeaderText = "Фамилия";
            secondNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            secondNameColumn.ReadOnly = true;
            secondNameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewTextBoxColumn ageColumn = new DataGridViewTextBoxColumn();
            ageColumn.HeaderText = "Возраст";
            ageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ageColumn.ReadOnly = true;
            ageColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewTextBoxColumn scoreColumn = new DataGridViewTextBoxColumn();
            scoreColumn.HeaderText = "Количество очков";
            scoreColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            scoreColumn.ReadOnly = true;
            scoreColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewTextBoxColumn countFallenBlocksColumn = new DataGridViewTextBoxColumn();
            countFallenBlocksColumn.HeaderText = "Количество упавших фигур";
            countFallenBlocksColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            countFallenBlocksColumn.ReadOnly = true;
            countFallenBlocksColumn.SortMode = DataGridViewColumnSortMode.NotSortable;

            ScoreTable.Columns.AddRange(number, nameColumn, secondNameColumn, ageColumn, scoreColumn, countFallenBlocksColumn);

            results = new ResultsStorage();
            playersResults = results.GetUsersResults().OrderBy(x => x.Score).ToList();
            var num = 1;
            
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
            InitTable();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            playersResults.Clear();
            results.Save(playersResults);
            ScoreTable.Rows.Clear();
            MessageBox.Show("Таблица очищена!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
