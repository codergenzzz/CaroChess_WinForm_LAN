
namespace _241018_CaroChess_WinForm
{
    public class ChessBoardManager
    {
        #region Properties
        private Panel _chessBoard { get; set; }
        private List<Player> _players;
        public int CurrentPlayer { get; set; }
        public TextBox PlayerName { get; set; }
        public PictureBox PlayerMark { get; set; }

        public List<List<Button>> Matrix { get; set; }  // ma trận lưu vị trí các điểm

        private event EventHandler playerMarked;
        private event EventHandler endedGame;

        public event EventHandler PlayerMarked { add { playerMarked += value; } remove { playerMarked -= value; } }

        public event EventHandler EndedGame { add { endedGame += value; } remove { endedGame -= value; } }
        #endregion

        #region Initialize
        public ChessBoardManager(Panel chessBoard, TextBox playerName, PictureBox playerMark)
        {
            _chessBoard = chessBoard;
            PlayerName = playerName;
            PlayerMark = playerMark;

            _players = new List<Player>()
            {
                new Player("O", Image.FromFile(Application.StartupPath + "\\img\\o.png")),
                new Player("X", Image.FromFile(Application.StartupPath + "\\img\\x-player.jpg")),
            };
            CurrentPlayer = 0;
            ChangePlayer();
        }
        #endregion

        #region Methods
        public void DrawChessBoard()
        {
            _chessBoard.Enabled = true;
            Matrix = new List<List<Button>>();  // khởi tạo matrix khi bắt đầu vẽ board

            for (int i = 0; i < Constants.CHESS_BOARD_HEIGHT; i++)
            {
                Matrix.Add(new List<Button>());

                for (int j = 0; j < Constants.CHESS_BOARD_WIDTH; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Constants.CHESS_WIDTH,
                        Height = Constants.CHESS_HEIGHT,
                        Location = new Point(j * Constants.CHESS_WIDTH, i * Constants.CHESS_HEIGHT),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString()  // Đánh dấu vị trí của Y
                    };

                    btn.Click += btn_clicked;
                    _chessBoard.Controls.Add(btn);
                    Matrix[i].Add(btn);

                    //ShowPoint(btn);
                }
            }

        }

        private void btn_clicked(object? sender, EventArgs e)
        {
            Button? btn = sender as Button;

            // MessageBox.Show(GetChessPoint(btn).ToString());

            // nếu đã có người chơi thì không làm gì cả
            if (btn.BackgroundImage != null)
            {
                return;
            }

            Mark(btn);

            if (playerMarked != null)
            {
                playerMarked(this, new EventArgs());
            }

            if (IsEndGame(btn))
            {
                EndGame();
            }

            ChangePlayer();

        }

        public void EndGame()
        {
            if (endedGame != null)
            {
                endedGame(this, new EventArgs());
            }
        }

        private bool IsEndGame(Button btn)
        {
            return IsEndVer(btn) || IsEndHor(btn) || IsEndPrim(btn) || IsEndSub(btn);
        }

        /// <summary>
        /// Lấy tọa độ của ô
        /// </summary>
        /// <param name="btn">Button</param>
        /// <returns></returns>
        private Point GetChessPoint(Button btn)
        {
            int vertical = Convert.ToInt32(btn.Tag);    // Y
            int horizontal = Matrix[vertical].IndexOf(btn); // X

            Point point = new Point(horizontal, vertical);

            return point;
        }

        private bool IsEndHor(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countLeft = 0;
            for (int i = point.X; i >= 0; i--)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countLeft++;
                }
                else
                {
                    break;
                }
            }

            int countRight = 0;
            for (int i = point.X + 1; i < Constants.CHESS_BOARD_WIDTH; i++)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countRight++;
                }
                else
                {
                    break;
                }
            }

            return countLeft + countRight >= 5;
        }

        private bool IsEndVer(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                {
                    break;
                }
            }

            int countBottom = 0;
            for (int i = point.Y + 1; i < Constants.CHESS_BOARD_HEIGHT; i++)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                {
                    break;
                }
            }

            return countBottom + countTop >= 5;
        }

        private bool IsEndPrim(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.Y - i < 0 || point.X - i < 0)
                {
                    break;
                }

                if (Matrix[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                {
                    break;
                }
            }

            int countBottom = 0;
            for (int i = 1; i <= Constants.CHESS_BOARD_WIDTH - point.X; i++)
            {
                if (point.Y + i >= Constants.CHESS_BOARD_HEIGHT || point.X + i >= Constants.CHESS_BOARD_WIDTH)
                {
                    break;
                }

                if (Matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                {
                    break;
                }
            }

            return countBottom + countTop >= 5;
        }

        private bool IsEndSub(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countRight = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.Y - i < 0 || point.X + i >= Constants.CHESS_BOARD_WIDTH)
                {
                    break;
                }

                if (Matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countRight++;
                }
                else
                {
                    break;
                }
            }

            int countLeft = 0;
            for (int i = 1; i <= Constants.CHESS_BOARD_WIDTH - point.X; i++)
            {
                if (point.Y + i >= Constants.CHESS_BOARD_HEIGHT || point.X - i < 0)
                {
                    break;
                }

                if (Matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countLeft++;
                }
                else
                {
                    break;
                }
            }

            return countRight + countLeft >= 5;
        }

        /// <summary>
        /// Đánh dấu ô đã chọn
        /// </summary>
        private void Mark(Button btn)
        {
            // Khi đánh thì đổi image của ô đã chọn
            btn.BackgroundImage = _players[CurrentPlayer].Mark;

            // Đổi lượt đánh
            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
        }

        /// <summary>
        /// Đổi lượt chơi
        /// </summary>
        private void ChangePlayer()
        {
            // Set text cho ô tên người chơi
            PlayerName.Text = _players[CurrentPlayer].Name;

            // Set img cho ô hình ảnh của người chơi
            PlayerMark.Image = _players[CurrentPlayer].Mark;
        }

        void ShowPoint(Button btn)
        {
            btn.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn.Text = $"{GetChessPoint(btn).X}, {GetChessPoint(btn).Y}";
        }
        #endregion
    }
}
