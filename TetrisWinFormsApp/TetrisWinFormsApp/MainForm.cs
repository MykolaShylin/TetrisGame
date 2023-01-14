using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Threading;
using Timer = System.Windows.Forms.Timer;

namespace TetrisWinFormsApp
{
    public partial class MainForm : Form
    {
        private int _cellSize;
        private int _cellsWidthCount;
        private int _cellsHeightCount;
        private PictureBox[,] block;
        private PictureBox[,] blocksOnMap;
        private Random random = new Random();
        Timer movingTimer = new Timer();
        Timer gameTimer = new Timer();
        private Color _blockColor;        
        private BlockType _currentBlockType;
        private BlockType _nextBlockType;
        private bool _isNextBlockMoving;
        private int _blockMapSize = 3;
        private int _blockIndex;
        private PictureBox _blockHead;
        private PictureBox _centerBlock;
        private PictureBox _blockLeftBorder;
        private PictureBox _blockRightBorder;
        private User _user;

        public MainForm(User user)
        {
            InitializeComponent();
            _user = user;
            _cellSize = 25;
            _cellsWidthCount = 12;
            _cellsHeightCount = 20;
            ClientSize = new Size(_cellSize * (_cellsWidthCount + 1) , _cellSize * _cellsHeightCount);
            label1.Location = new Point(_cellSize * (_cellsWidthCount + 1), 0);
            nameLabel.Location = new Point(label1.Width + label1.Location.X, 0);
            movingTimer.Interval = 500;
            movingTimer.Tick += MovingTimer_Tick;
            movingTimer.Enabled = false;
            gameTimer.Interval = 500;
            gameTimer.Tick += GameTimer_Tick;
            CreateMap();
            NewGame();
        }
        private void NewGame()
        {
            nameLabel.Text = _user.Name;
            scoreLabel.Text = "0";
            blocksOnMap = new PictureBox[_cellsHeightCount, _cellsWidthCount];
            _nextBlockType = (BlockType)random.Next(7);
            gameTimer.Start();
        }
        private PictureBox CreateOnePieceOfBlock(int pointX, int pointY, Color color)
        {
            var newPiece = new PictureBox();
            newPiece.Size = new Size(_cellSize, _cellSize);
            newPiece.BackColor = color;
            newPiece.BorderStyle = BorderStyle.FixedSingle;
            newPiece.Location = new Point(pointX, pointY);
            return newPiece;
        }
        private void GetRotatedBlock(int index, BlockType type)
        {
            var _centerX = _centerBlock.Location.X;
            var _centerY = _centerBlock.Location.Y;
            var downSideLeft = _blockLeftBorder.Location.Y / _cellSize;
            var downSideRight = _blockRightBorder.Location.Y / _cellSize;
            if (_centerY >= 0  && _centerX > 0 && blocksOnMap[downSideLeft, _centerX / _cellSize - 1] != null || _centerX == LeftBorder())
            {
                _centerX += _cellSize;
            }
            else if (_centerY >= 0 && _centerX / _cellSize < RightBorder() && blocksOnMap[downSideRight, _centerX / _cellSize + 1] != null || _centerX / _cellSize == RightBorder())
            {
                _centerX -= _cellSize;
            }
            var checkingBlock = new PictureBox[_blockMapSize, _blockMapSize];
            switch (_currentBlockType)
            {
                case BlockType.GBlock:
                    {
                        switch (index)
                        {
                            case 2:
                            case 0:
                                _blockColor = Color.Red;
                                checkingBlock[1, 0] = CreateOnePieceOfBlock(_centerX - _cellSize, _centerY + _cellSize, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[1, 0].Location.X + _cellSize, checkingBlock[1, 0].Location.Y, _blockColor);
                                checkingBlock[1, 2] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X + _cellSize, checkingBlock[1, 1].Location.Y, _blockColor);
                                _blockHead = checkingBlock[1, 0];
                                _blockLeftBorder = checkingBlock[1, 0];
                                _blockRightBorder = checkingBlock[1, 2];
                                break;
                            case 3:
                            case 1:
                                _blockColor = Color.Red;
                                checkingBlock[0, 1] = CreateOnePieceOfBlock(_centerX, _centerY - _cellSize, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[0, 1].Location.X, checkingBlock[0, 1].Location.Y + _cellSize, _blockColor);
                                checkingBlock[2, 1] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X, checkingBlock[1, 1].Location.Y + _cellSize, _blockColor);
                                _blockHead = checkingBlock[0, 1];
                                _blockLeftBorder = checkingBlock[2, 1];
                                _blockRightBorder = checkingBlock[2, 1];
                                break;
                        }
                    }
                    break;
                case BlockType.VBlock:
                    {
                        switch (index)
                        {
                            case 2:
                            case 0:
                                _blockColor = Color.Red;
                                checkingBlock[0, 1] = CreateOnePieceOfBlock(_centerX, _centerY - _cellSize, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[0, 1].Location.X, checkingBlock[0, 1].Location.Y + _cellSize, _blockColor);
                                checkingBlock[2, 1] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X, checkingBlock[1, 1].Location.Y + _cellSize, _blockColor);
                                _blockHead = checkingBlock[0, 1];
                                _blockLeftBorder = checkingBlock[2, 1];
                                _blockRightBorder = checkingBlock[2, 1];
                                break;
                            case 3:
                            case 1:
                                _blockColor = Color.Red;
                                checkingBlock[1, 0] = CreateOnePieceOfBlock(_centerX - _cellSize, _centerY + _cellSize, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[1, 0].Location.X + _cellSize, checkingBlock[1, 0].Location.Y, _blockColor);
                                checkingBlock[1, 2] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X + _cellSize, checkingBlock[1, 1].Location.Y, _blockColor);
                                _blockHead = checkingBlock[1, 0];
                                _blockLeftBorder = checkingBlock[1, 0];
                                _blockRightBorder = checkingBlock[1, 2];
                                break;
                        }
                    }
                    break;
                case BlockType.LBlockR:
                    {
                        switch (index)
                        {
                            case 0:
                                _blockColor = Color.Yellow;
                                checkingBlock[0, 1] = CreateOnePieceOfBlock(_centerX, _centerY - _cellSize, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[0, 1].Location.X, checkingBlock[0, 1].Location.Y + _cellSize, _blockColor);
                                checkingBlock[2, 1] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X, checkingBlock[1, 1].Location.Y + _cellSize, _blockColor);
                                checkingBlock[2, 2] = CreateOnePieceOfBlock(checkingBlock[2, 1].Location.X + _cellSize, checkingBlock[2, 1].Location.Y, _blockColor);
                                _blockHead = checkingBlock[0, 1];
                                _blockLeftBorder = checkingBlock[2, 1];
                                _blockRightBorder = checkingBlock[2, 2];
                                break;
                            case 1:
                                _blockColor = Color.Yellow;
                                checkingBlock[1, 0] = CreateOnePieceOfBlock(_centerX - _cellSize, _centerY, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[1, 0].Location.X + _cellSize, checkingBlock[1, 0].Location.Y, _blockColor);
                                checkingBlock[1, 2] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X + _cellSize, checkingBlock[1, 1].Location.Y, _blockColor);
                                checkingBlock[0, 2] = CreateOnePieceOfBlock(checkingBlock[1, 2].Location.X, checkingBlock[1, 2].Location.Y - _cellSize, _blockColor);
                                _blockHead = checkingBlock[1, 0];
                                _blockLeftBorder = checkingBlock[1, 0];
                                _blockRightBorder = checkingBlock[1, 2];
                                break;
                            case 2:
                                _blockColor = Color.Yellow;
                                checkingBlock[0, 0] = CreateOnePieceOfBlock(_centerX - _cellSize, _centerY - _cellSize, _blockColor);
                                checkingBlock[0, 1] = CreateOnePieceOfBlock(checkingBlock[0, 0].Location.X + _cellSize, checkingBlock[0, 0].Location.Y, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[0, 1].Location.X, checkingBlock[0, 1].Location.Y + _cellSize, _blockColor);
                                checkingBlock[2, 1] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X, checkingBlock[1, 1].Location.Y + _cellSize, _blockColor);
                                _blockHead = checkingBlock[0, 0];
                                _blockLeftBorder = checkingBlock[0, 0];
                                _blockRightBorder = checkingBlock[2, 1];
                                break;
                            case 3:
                                _blockColor = Color.Yellow;
                                checkingBlock[2, 0] = CreateOnePieceOfBlock(_centerX - _cellSize, _centerY + _cellSize, _blockColor);
                                checkingBlock[1, 0] = CreateOnePieceOfBlock(checkingBlock[2, 0].Location.X, checkingBlock[2, 0].Location.Y - _cellSize, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[1, 0].Location.X + _cellSize, checkingBlock[1, 0].Location.Y, _blockColor);
                                checkingBlock[1, 2] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X + _cellSize, checkingBlock[1, 1].Location.Y, _blockColor);
                                _blockHead = checkingBlock[1, 0];
                                _blockLeftBorder = checkingBlock[2, 0];
                                _blockRightBorder = checkingBlock[1, 2];
                                break;
                        }
                    }
                    break;
                case BlockType.LBlockL:
                    {
                        switch (index)
                        {
                            case 0:
                                _blockColor = Color.Yellow;
                                checkingBlock[0, 1] = CreateOnePieceOfBlock(_centerX, _centerY - _cellSize, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[0, 1].Location.X, checkingBlock[0, 1].Location.Y + _cellSize, _blockColor);
                                checkingBlock[2, 1] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X, checkingBlock[1, 1].Location.Y + _cellSize, _blockColor);
                                checkingBlock[2, 0] = CreateOnePieceOfBlock(checkingBlock[2, 1].Location.X - _cellSize, checkingBlock[2, 1].Location.Y, _blockColor);
                                _blockHead = checkingBlock[0, 1];
                                _blockLeftBorder = checkingBlock[2, 0];
                                _blockRightBorder = checkingBlock[2, 1];
                                break;
                            case 1:
                                _blockColor = Color.Yellow;
                                checkingBlock[1, 0] = CreateOnePieceOfBlock(_centerX - _cellSize, _centerY, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[1, 0].Location.X + _cellSize, checkingBlock[1, 0].Location.Y, _blockColor);
                                checkingBlock[1, 2] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X + _cellSize, checkingBlock[1, 1].Location.Y, _blockColor);
                                checkingBlock[2, 2] = CreateOnePieceOfBlock(checkingBlock[1, 2].Location.X, checkingBlock[1, 2].Location.Y + _cellSize, _blockColor);
                                _blockHead = checkingBlock[1, 0];
                                _blockLeftBorder = checkingBlock[1, 0];
                                _blockRightBorder = checkingBlock[2, 2];
                                break;
                            case 2:
                                _blockColor = Color.Yellow;
                                checkingBlock[0, 2] = CreateOnePieceOfBlock(_centerX + _cellSize, _centerY - _cellSize, _blockColor);
                                checkingBlock[0, 1] = CreateOnePieceOfBlock(checkingBlock[0, 2].Location.X - _cellSize, checkingBlock[0, 2].Location.Y, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[0, 1].Location.X, checkingBlock[0, 1].Location.Y + _cellSize, _blockColor);
                                checkingBlock[2, 1] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X, checkingBlock[1, 1].Location.Y + _cellSize, _blockColor);
                                _blockHead = checkingBlock[0, 1];
                                _blockLeftBorder = checkingBlock[2, 1];
                                _blockRightBorder = checkingBlock[0, 2];
                                break;
                            case 3:
                                _blockColor = Color.Yellow;
                                checkingBlock[0, 0] = CreateOnePieceOfBlock(_centerX - _cellSize, _centerY - _cellSize, _blockColor);
                                checkingBlock[1, 0] = CreateOnePieceOfBlock(checkingBlock[0, 0].Location.X, checkingBlock[0, 0].Location.Y + _cellSize, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[1, 0].Location.X + _cellSize, checkingBlock[1, 0].Location.Y, _blockColor);
                                checkingBlock[1, 2] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X + _cellSize, checkingBlock[1, 1].Location.Y, _blockColor);
                                _blockHead = checkingBlock[0, 0];
                                _blockLeftBorder = checkingBlock[1, 0];
                                _blockRightBorder = checkingBlock[1, 2];
                                break;
                        }
                    }
                    break;
                case BlockType.TBlock:
                    {
                        switch (index)
                        {
                            case 0:
                                _blockColor = Color.Violet;
                                checkingBlock[1, 0] = CreateOnePieceOfBlock(_centerX - _cellSize, _centerY, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[1, 0].Location.X + _cellSize, checkingBlock[1, 0].Location.Y, _blockColor);
                                checkingBlock[1, 2] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X + _cellSize, checkingBlock[1, 1].Location.Y, _blockColor);
                                checkingBlock[2, 1] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X, checkingBlock[1, 1].Location.Y + _cellSize, _blockColor);
                                _blockHead = checkingBlock[1, 0];
                                _blockLeftBorder = checkingBlock[1, 0];
                                _blockRightBorder = checkingBlock[1, 2];
                                break;
                            case 1:
                                _blockColor = Color.Violet;
                                checkingBlock[0, 1] = CreateOnePieceOfBlock(_centerX, _centerY - _cellSize, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[0, 1].Location.X, checkingBlock[0, 1].Location.Y + _cellSize, _blockColor);
                                checkingBlock[2, 1] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X, checkingBlock[1, 1].Location.Y + _cellSize, _blockColor);
                                checkingBlock[1, 2] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X + _cellSize, checkingBlock[1, 1].Location.Y, _blockColor);
                                _blockHead = checkingBlock[0, 1];
                                _blockLeftBorder = checkingBlock[2, 1];
                                _blockRightBorder = checkingBlock[1, 2];
                                break;
                            case 2:
                                _blockColor = Color.Violet;
                                checkingBlock[1, 0] = CreateOnePieceOfBlock(_centerX - _cellSize, _centerY, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[1, 0].Location.X + _cellSize, checkingBlock[1, 0].Location.Y, _blockColor);
                                checkingBlock[0, 1] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X, checkingBlock[1, 1].Location.Y - _cellSize, _blockColor);
                                checkingBlock[1, 2] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X + _cellSize, checkingBlock[1, 1].Location.Y, _blockColor);
                                _blockHead = checkingBlock[1, 0];
                                _blockLeftBorder = checkingBlock[1, 0];
                                _blockRightBorder = checkingBlock[1, 2];
                                break;
                            case 3:
                                _blockColor = Color.Violet;
                                checkingBlock[0, 1] = CreateOnePieceOfBlock(_centerX, _centerY - _cellSize, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[0, 1].Location.X, checkingBlock[0, 1].Location.Y + _cellSize, _blockColor);
                                checkingBlock[2, 1] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X, checkingBlock[1, 1].Location.Y + _cellSize, _blockColor);
                                checkingBlock[1, 0] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X - _cellSize, checkingBlock[1, 1].Location.Y, _blockColor);
                                _blockHead = checkingBlock[0, 1];
                                _blockLeftBorder = checkingBlock[1, 0];
                                _blockRightBorder = checkingBlock[2, 1];
                                break;
                        }
                    }
                    break;
                case BlockType.ZBlockL:
                    {
                        switch (index)
                        {
                            case 2:
                            case 0:
                                _blockColor = Color.Blue;
                                checkingBlock[1, 0] = CreateOnePieceOfBlock(_centerX - _cellSize, _centerY, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[1, 0].Location.X + _cellSize, checkingBlock[1, 0].Location.Y, _blockColor);
                                checkingBlock[2, 1] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X, checkingBlock[1, 1].Location.Y + _cellSize, _blockColor);
                                checkingBlock[2, 2] = CreateOnePieceOfBlock(checkingBlock[2, 1].Location.X + _cellSize, checkingBlock[2, 1].Location.Y, _blockColor);
                                _blockHead = checkingBlock[1, 0];
                                _blockLeftBorder = checkingBlock[1, 0];
                                _blockRightBorder = checkingBlock[2, 2];
                                break;
                            case 3:
                            case 1:
                                _blockColor = Color.Blue;
                                checkingBlock[2, 0] = CreateOnePieceOfBlock(_centerX, _centerY + _cellSize, _blockColor);
                                checkingBlock[1, 0] = CreateOnePieceOfBlock(checkingBlock[2, 0].Location.X, checkingBlock[2, 0].Location.Y - _cellSize, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[1, 0].Location.X + _cellSize, checkingBlock[1, 0].Location.Y, _blockColor);
                                checkingBlock[0, 1] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X, checkingBlock[1, 1].Location.Y - _cellSize, _blockColor);
                                _blockHead = checkingBlock[0, 1];
                                _blockLeftBorder = checkingBlock[2, 0];
                                _blockRightBorder = checkingBlock[1, 1];
                                break;
                        }
                    }
                    break;
                case BlockType.ZBlockR:
                    {
                        switch (index)
                        {
                            case 2:
                            case 0:
                                _blockColor = Color.Blue;
                                checkingBlock[1, 2] = CreateOnePieceOfBlock(_centerX + _cellSize, _centerY, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[1, 2].Location.X - _cellSize, checkingBlock[1, 2].Location.Y, _blockColor);
                                checkingBlock[2, 1] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X, checkingBlock[1, 1].Location.Y + _cellSize, _blockColor);
                                checkingBlock[2, 0] = CreateOnePieceOfBlock(checkingBlock[2, 1].Location.X - _cellSize, checkingBlock[2, 1].Location.Y, _blockColor);
                                _blockHead = checkingBlock[1, 2];
                                _blockLeftBorder = checkingBlock[2, 0];
                                _blockRightBorder = checkingBlock[1, 2];
                                break;
                            case 3:
                            case 1:
                                _blockColor = Color.Blue;
                                checkingBlock[0, 1] = CreateOnePieceOfBlock(_centerX, _centerY - _cellSize, _blockColor);
                                checkingBlock[1, 1] = CreateOnePieceOfBlock(checkingBlock[0, 1].Location.X, checkingBlock[0, 1].Location.Y + _cellSize, _blockColor);
                                checkingBlock[1, 2] = CreateOnePieceOfBlock(checkingBlock[1, 1].Location.X + _cellSize, checkingBlock[1, 1].Location.Y, _blockColor);
                                checkingBlock[2, 2] = CreateOnePieceOfBlock(checkingBlock[1, 2].Location.X, checkingBlock[1, 2].Location.Y + _cellSize, _blockColor);
                                _blockHead = checkingBlock[0, 1];
                                _blockLeftBorder = checkingBlock[1, 1];
                                _blockRightBorder = checkingBlock[2, 2];
                                break;
                        }
                    }
                    break;
            }
            _centerBlock = checkingBlock[1, 1];

            if (_centerBlock.Location.Y / _cellSize + 1 <= BottomBorder() && _blockLeftBorder.Location.X >= LeftBorder() && _blockRightBorder.Location.X / _cellSize <= RightBorder())
            {
                Array.Copy(checkingBlock, block, checkingBlock.Length);
            }
        }
        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            if (!movingTimer.Enabled)
            {
                GetBlock(_nextBlockType);
                movingTimer.Start();
            }
            if(_isNextBlockMoving)
            {
                _nextBlockType = (BlockType)random.Next(7);
                switch (_nextBlockType)
                {
                    case BlockType.GBlock:
                        {
                            nextBlockTypePictureBox.Image = Properties.Resources.GBlock;
                        }
                        break;
                    case BlockType.VBlock:
                        {
                            nextBlockTypePictureBox.Image = Properties.Resources.VBlock;
                        }
                        break;
                    case BlockType.LBlockR:
                        {
                            nextBlockTypePictureBox.Image = Properties.Resources.LLeft;
                        }
                        break;
                    case BlockType.LBlockL:
                        {
                            nextBlockTypePictureBox.Image = Properties.Resources.LRight;
                        }
                        break;
                    case BlockType.ZBlockL:
                        {
                            nextBlockTypePictureBox.Image = Properties.Resources.ZBlockL;
                        }
                        break;
                    case BlockType.ZBlockR:
                        {
                            nextBlockTypePictureBox.Image = Properties.Resources.ZBlockR;
                        }
                        break;
                    case BlockType.Cube:
                        {
                            nextBlockTypePictureBox.Image = Properties.Resources.Cube;
                        }
                        break;
                    case BlockType.TBlock:
                        {
                            nextBlockTypePictureBox.Image = Properties.Resources.TBlock;
                        }
                        break;

                }
            }
            _isNextBlockMoving = false;
        }
        private void GetBlock(BlockType blockType)
        {
            _currentBlockType = blockType;
            block = new PictureBox[_blockMapSize, _blockMapSize];
            do
            {
                var randomX = random.Next(1, _cellsWidthCount - 1) * _cellSize;
                switch (blockType)
                {
                    case BlockType.GBlock:
                        {
                            _blockColor = Color.Red;
                            block[1, 1] = CreateOnePieceOfBlock(randomX, -_cellSize, _blockColor);
                            block[1, 0] = CreateOnePieceOfBlock(block[1, 1].Location.X - _cellSize, block[1, 1].Location.Y, _blockColor);
                            block[1, 2] = CreateOnePieceOfBlock(block[1, 1].Location.X + _cellSize, block[1, 1].Location.Y, _blockColor);
                            _blockHead = block[1, 0];
                            _blockLeftBorder = block[1, 0];
                            _blockRightBorder = block[1, 2];
                        }
                        break;
                    case BlockType.VBlock:
                        {
                            _blockColor = Color.Red;
                            block[1, 1] = CreateOnePieceOfBlock(randomX, -_cellSize * 2, _blockColor);
                            block[0, 1] = CreateOnePieceOfBlock(block[1, 1].Location.X, block[1, 1].Location.Y - _cellSize, _blockColor);
                            block[2, 1] = CreateOnePieceOfBlock(block[1, 1].Location.X, block[1, 1].Location.Y + _cellSize, _blockColor);
                            _blockHead = block[0, 1];
                            _blockLeftBorder = block[2, 1];
                            _blockRightBorder = block[2, 1];
                        }
                        break;
                    case BlockType.LBlockR:
                        {
                            _blockColor = Color.Yellow;
                            block[1, 1] = CreateOnePieceOfBlock(randomX, -_cellSize * 2, _blockColor);
                            block[0, 1] = CreateOnePieceOfBlock(block[1, 1].Location.X, block[1, 1].Location.Y - _cellSize, _blockColor);
                            block[2, 1] = CreateOnePieceOfBlock(block[1, 1].Location.X, block[1, 1].Location.Y + _cellSize, _blockColor);
                            block[2, 2] = CreateOnePieceOfBlock(block[2, 1].Location.X + _cellSize, block[2, 1].Location.Y, _blockColor);
                            _blockHead = block[0, 1];
                            _blockLeftBorder = block[2, 1];
                            _blockRightBorder = block[2, 2];
                        }
                        break;
                    case BlockType.LBlockL:
                        {
                            _blockColor = Color.Yellow;
                            block[1, 1] = CreateOnePieceOfBlock(randomX, -_cellSize * 2, _blockColor);
                            block[0, 1] = CreateOnePieceOfBlock(block[1, 1].Location.X, block[1, 1].Location.Y - _cellSize, _blockColor);
                            block[2, 1] = CreateOnePieceOfBlock(block[1, 1].Location.X, block[1, 1].Location.Y + _cellSize, _blockColor);
                            block[2, 0] = CreateOnePieceOfBlock(block[2, 1].Location.X - _cellSize, block[2, 1].Location.Y, _blockColor);
                            _blockHead = block[0, 1];
                            _blockLeftBorder = block[2, 1];
                            _blockRightBorder = block[2, 0];
                        }
                        break;
                    case BlockType.TBlock:
                        {
                            _blockColor = Color.Violet;
                            block[1, 1] = CreateOnePieceOfBlock(randomX, -_cellSize * 2, _blockColor);
                            block[1, 0] = CreateOnePieceOfBlock(block[1, 1].Location.X - _cellSize, block[1, 1].Location.Y, _blockColor);
                            block[1, 2] = CreateOnePieceOfBlock(block[1, 1].Location.X + _cellSize, block[1, 1].Location.Y, _blockColor);
                            block[2, 1] = CreateOnePieceOfBlock(block[1, 1].Location.X, block[1, 1].Location.Y + _cellSize, _blockColor);
                            _blockHead = block[1, 0];
                            _blockLeftBorder = block[1, 0];
                            _blockRightBorder = block[1, 2];
                        }
                        break;
                    case BlockType.Cube:
                        {
                            _blockColor = Color.Green;
                            block[1, 1] = CreateOnePieceOfBlock(randomX, -_cellSize * 2, _blockColor);
                            block[1, 0] = CreateOnePieceOfBlock(block[1, 1].Location.X - _cellSize, block[1, 1].Location.Y, _blockColor);
                            block[2, 0] = CreateOnePieceOfBlock(block[1, 0].Location.X, block[1, 0].Location.Y + _cellSize, _blockColor);
                            block[2, 1] = CreateOnePieceOfBlock(block[2, 0].Location.X + _cellSize, block[2, 0].Location.Y, _blockColor);
                            _blockHead = block[1, 0];
                            _blockLeftBorder = block[2, 0];
                            _blockRightBorder = block[2, 1];
                        }
                        break;
                    case BlockType.ZBlockL:
                        {
                            _blockColor = Color.Blue;
                            block[1, 1] = CreateOnePieceOfBlock(randomX, -_cellSize * 2, _blockColor);
                            block[1, 0] = CreateOnePieceOfBlock(block[1, 1].Location.X - _cellSize, block[1, 1].Location.Y, _blockColor);
                            block[2, 1] = CreateOnePieceOfBlock(block[1, 1].Location.X, block[1, 1].Location.Y + _cellSize, _blockColor);
                            block[2, 2] = CreateOnePieceOfBlock(block[2, 1].Location.X + _cellSize, block[2, 1].Location.Y, _blockColor);
                            _blockHead = block[1, 0];
                            _blockLeftBorder = block[1, 0];
                            _blockRightBorder = block[2, 2];
                        }
                        break;
                    case BlockType.ZBlockR:
                        {
                            _blockColor = Color.Blue;
                            block[1, 1] = CreateOnePieceOfBlock(randomX, -_cellSize * 2, _blockColor);
                            block[1, 2] = CreateOnePieceOfBlock(block[1, 1].Location.X + _cellSize, block[1, 1].Location.Y, _blockColor);
                            block[2, 1] = CreateOnePieceOfBlock(block[1, 1].Location.X, block[1, 1].Location.Y + _cellSize, _blockColor);
                            block[2, 0] = CreateOnePieceOfBlock(block[2, 1].Location.X - _cellSize, block[2, 1].Location.Y, _blockColor);
                            _blockHead = block[1, 1];
                            _blockLeftBorder = block[2, 0];
                            _blockRightBorder = block[1, 2];
                        }
                        break;
                }
                _centerBlock = block[1, 1];
                _blockIndex = 0;                

                if (!IsExistingFreePlaces())
                {
                    EndGame();
                }
            }
            while (!IsFindingFreePlaces());            
            ShowBlock();
            _isNextBlockMoving = true;
        }

        private bool IsExistingFreePlaces()
        {
            var freePlaces = 0;
            for (int i = 0; i < _cellsWidthCount; i++)
            {
                if (blocksOnMap[0, i] == null)
                {
                    freePlaces++;
                    if (freePlaces == _blockMapSize)
                    {
                        return true;
                    }
                }
                else
                {
                    freePlaces = 0;
                    continue;
                }
            }
            return false;
        }

        private bool IsFindingFreePlaces()
        {
            var columnLocation = _centerBlock.Location.X / _cellSize;
            if (blocksOnMap[0, columnLocation - 1] == null && blocksOnMap[0, columnLocation + 1] == null && blocksOnMap[0, columnLocation] == null)
            {
                return true;
            }
            return false;
        }
        private void EndGame()
        {
            gameTimer.Stop();
            movingTimer.Stop();
            var answer = MessageBox.Show($"Конец игры! {_user.Name}, вы набрали {scoreLabel.Text} очка(-ов). Желаете повторить?", "Конец игры", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            
            if (answer == DialogResult.Retry)
            {
                ClearBlock();
                ClearMap();
                NewGame();
            }
            else
            {
                var gameResult = new ResultsStorage();
                _user.Score = scoreLabel.Text;
                _user.FallenBlocksCount = blocksFallenCountLabel.Text;
                gameResult.SaveResultOfTesting(_user);
                Close();
            }
        }

        private void MoveBlock()
        {
            ClearBlock();
            for (int i = 0; i < _blockMapSize; i++)
            {
                for (int j = 0; j < _blockMapSize; j++)
                {
                    if (block[i, j] != null)
                    {
                        block[i, j].Location = new Point(block[i, j].Location.X, block[i, j].Location.Y + _cellSize);
                    }
                }
            }
            ShowBlock();
        }

        private void MovingTimer_Tick(object? sender, EventArgs e)
        {
            MoveBlock();
            CheckForBlockFallen();
        }
        private bool IsBlockFallen()
        {
            bool isFallen = false;
            for (int i = _blockMapSize - 1; i >= 0 && !isFallen; i--)
            {
                for (int j = 0; j < _blockMapSize; j++)
                {
                    if (block[i, j] != null)
                    {
                        var rowLocation = block[i, j].Location.Y / _cellSize;
                        var columLocation = block[i, j].Location.X / _cellSize;
                        if (rowLocation == BottomBorder() || rowLocation >= 0 && blocksOnMap[rowLocation + 1, columLocation] != null)
                        {
                            if (_blockHead.Location.Y < 0)
                            {
                                EndGame();
                                return false;
                            }
                            isFallen = true;
                            blocksFallenCountLabel.Text = (Convert.ToInt32(blocksFallenCountLabel.Text) + 1).ToString();
                            break;
                        }
                    }
                }
            }
            return isFallen;
        }
        private void ClearFullBorder()
        {
            var countLineForCleaning = 0;
            for (int i = BottomBorder(); i >= TopBorder(); i--)
            {
                var lineIsFulled = true;
                for (int j = LeftBorder(); j <= RightBorder(); j++)
                {
                    if (blocksOnMap[i, j] == null)
                    {
                        lineIsFulled = false;
                        break;
                    }
                }
                if (lineIsFulled)
                {
                    countLineForCleaning++;
                    scoreLabel.Text = (Convert.ToInt32(scoreLabel.Text) + 1).ToString();
                    for (int j = 0; j < _cellsWidthCount; j++)
                    {
                        Controls.Remove(blocksOnMap[i, j]);
                        blocksOnMap[i, j] = null;                        
                    }
                }
                else if (!lineIsFulled && countLineForCleaning > 0)
                {
                    int q = i + countLineForCleaning;
                    for (int j = 0; j < _cellsWidthCount; j++)
                    {
                        if (blocksOnMap[i, j] != null)
                        {
                            Controls.Remove(blocksOnMap[i, j]);
                            int newX = blocksOnMap[i, j].Location.X;
                            int newY = blocksOnMap[i, j].Location.Y + _cellSize * countLineForCleaning;
                            var color = blocksOnMap[i, j].BackColor;
                            blocksOnMap[i, j] = null;
                            blocksOnMap[q, j] = CreateOnePieceOfBlock(newX, newY, color);
                            Controls.Add(blocksOnMap[q, j]);
                        }
                    }
                }
            }
        }  
        private void FillBottom()
        {
            for (int i = 0; i < _blockMapSize; i++)
            {
                for (int j = 0; j < _blockMapSize; j++)
                {
                    if (block[i, j] != null)
                    {
                        var rowLocation = block[i, j].Location.Y / _cellSize;
                        var columLocation = block[i, j].Location.X / _cellSize;
                        blocksOnMap[rowLocation, columLocation] = CreateOnePieceOfBlock(block[i, j].Location.X, block[i, j].Location.Y, _blockColor);
                        Controls.Add(blocksOnMap[rowLocation, columLocation]);
                    }
                }
            }
        }
        private void ClearMap()
        {
            for (int i = 0; i < _cellsHeightCount; i++)
            {
                for (int j = 0; j < _cellsWidthCount; j++)
                {
                    if (blocksOnMap[i, j] != null)
                    {
                        Controls.Remove(blocksOnMap[i, j]);
                        blocksOnMap[i, j] = null;
                    }
                }
            }
        }
        private void ShowBlock()
        {
            for (int i = 0; i < _blockMapSize; i++)
            {
                for (int j = 0; j < _blockMapSize; j++)
                {
                    if (block[i, j] != null)
                    {
                        Controls.Add(block[i, j]);
                    }
                }
            }
        }
        private void ClearBlock()
        {
            for (int i = 0; i < _blockMapSize; i++)
            {
                for (int j = 0; j < _blockMapSize; j++)
                {
                    if (block[i, j] != null)
                    {
                        Controls.Remove(block[i, j]);
                    }
                }
            }
        }
        private void CreateMap()
        {
            for (int i = 0; i <= _cellsWidthCount; i++)
            {
                var line = new PictureBox();
                line.BackColor = Color.Black;
                line.Location = new Point(i * _cellSize, 0);
                line.Size = new Size(1, _cellsHeightCount * _cellSize);
                Controls.Add(line);
            }
            for (int i = 0; i <= _cellsHeightCount; i++)
            {
                var line = new PictureBox();
                line.BackColor = Color.Black;
                line.Location = new Point(0, i * _cellSize);
                line.Size = new Size(_cellsWidthCount * _cellSize, 1);
                Controls.Add(line);
            }
        }
        private int TopBorder()
        {
            return 0;
        }
        private int LeftBorder()
        {
            return 0;
        }
        private int RightBorder()
        {
            return _cellsWidthCount - 1;
        }
        private int BottomBorder()
        {
            return _cellsHeightCount - 1;
        }

        private bool IsClearLeftOnBlock()
        {
            if(_blockLeftBorder.Location.X <= LeftBorder())
            {
                return false;
            }
            for (int i = 0; i < _blockMapSize; i++)
            {
                for (int j = 0; j < _blockMapSize; j++)
                {
                    if (block[i, j] != null && block[i, j].Location.Y >= 0 && block[i, j].Location.X > 0)
                    {
                        var columnLoction = block[i, j].Location.X / _cellSize;
                        var rowLocation = block[i, j].Location.Y / _cellSize;
                        if (blocksOnMap[rowLocation, columnLoction - 1] != null)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private bool IsClearRightOnBlock()
        {
            if(_blockRightBorder.Location.X / _cellSize >= RightBorder())
            {
                return false;
            }
            for (int i = 0; i < _blockMapSize; i++)
            {
                for (int j = 0; j < _blockMapSize; j++)
                {
                    if (block[i, j] != null && block[i, j].Location.Y >= 0 && block[i, j].Location.X < RightBorder() * _cellSize)
                    {
                        var locationX = block[i, j].Location.X / _cellSize;
                        var locationY = block[i, j].Location.Y / _cellSize;
                        if (blocksOnMap[locationY, locationX + 1] != null)
                        {                            
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (IsClearLeftOnBlock() && movingTimer.Enabled)
                    {
                        ClearBlock();
                        for (int i = 0; i < _blockMapSize; i++)
                        {
                            for (int j = 0; j < _blockMapSize; j++)
                            {
                                if (block[i, j] != null)
                                {
                                    block[i, j].Location = new Point(block[i, j].Location.X - _cellSize, block[i, j].Location.Y);
                                }
                            }
                        }
                    }
                    break;
                case Keys.Right:
                    if (IsClearRightOnBlock() && movingTimer.Enabled)
                    {
                        ClearBlock();
                        for (int i = 0; i < _blockMapSize; i++)
                        {
                            for (int j = 0; j < _blockMapSize; j++)
                            {
                                if (block[i, j] != null)
                                {
                                    block[i, j].Location = new Point(block[i, j].Location.X + _cellSize, block[i, j].Location.Y);
                                }
                            }
                        }
                    }
                    break;
                case Keys.Up:
                    if (_currentBlockType != BlockType.Cube && movingTimer.Enabled)
                    {
                        Rotate();
                    }
                    CheckForBlockFallen();
                    break;
            }
        }

        private async void CheckForBlockFallen()
        {
            if (IsBlockFallen())
            {
                gameTimer.Stop();
                movingTimer.Stop();
                ClearBlock();
                FillBottom();
                await Task.Delay(500);
                ClearFullBorder();
                gameTimer.Start();
            }
        }

        private void Rotate()
        {
            ClearBlock();
            _blockIndex++;
            if (_blockIndex > 3)
            {
                _blockIndex = 0;
            }
            GetRotatedBlock(_blockIndex, _currentBlockType);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            movingTimer.Stop();
            gameTimer.Stop();
            Array.Clear(block);
            Array.Clear(blocksOnMap);
        }
    }
}