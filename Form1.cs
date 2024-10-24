using System.Net.NetworkInformation;

namespace _241018_CaroChess_WinForm
{
    public partial class Form1 : Form
    {
        ChessBoardManager ChessBoardManager;
        SocketManager SocketManager;

        public Form1()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;

            ChessBoardManager = new ChessBoardManager(pnlChessBoard, txbPlayerName, ptbMark);
            ChessBoardManager.EndedGame += chessBoard_EndedGame;
            ChessBoardManager.PlayerMarked += chessBoard_PlayerMarked;


            pgbCoolDown.Step = Constants.COOL_DOWN_STEP;
            pgbCoolDown.Maximum = Constants.COOL_DOWN_TIME;
            pgbCoolDown.Value = 0;

            timerCoolDown.Interval = Constants.COOL_DOWN_INTERVAL;
            SocketManager = new SocketManager();

            NewGame();
            pnlChessBoard.Enabled = false;
        }

        #region Methods

        void EndGame()
        {
            timerCoolDown.Stop();
            pnlChessBoard.Enabled = false;
            undoToolStripMenuItem.Enabled = false;
            //MessageBox.Show("Kết thúc!!!");
        }
        void NewGame()
        {
            // Dừng timer và reset progressbar
            pgbCoolDown.Value = 0;
            timerCoolDown.Stop();

            // Mở lại undo
            //undoToolStripMenuItem.Enabled = true;

            // Đóng undo
            undoToolStripMenuItem.Enabled = false;

            // Vẽ lại bàn cờ
            ChessBoardManager.DrawChessBoard();
        }
        void Undo()
        {
            if (!ChessBoardManager.Undo())
            {
                MessageBox.Show("Không thể undo nữa!");
            }
            //ChessBoardManager.Undo();
            pgbCoolDown.Value = 0;
        }
        void Quit()
        {
            Application.Exit();
        }

        void Listen()
        {
            Thread listenThread = new Thread(() =>
            {
                try
                {
                    SocketData data = (SocketData)SocketManager.Receive();

                    ProcessData(data);
                }
                catch { }

            });
            listenThread.IsBackground = true;
            listenThread.Start();
        }

        void ProcessData(SocketData data)
        {
            switch (data.Command)
            {
                case (int)SocketCommand.NOTIFY:
                    MessageBox.Show(data.Message);
                    break;
                case (int)SocketCommand.NEW_GAME:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        NewGame();
                        pnlChessBoard.Enabled = false;
                    }));

                    break;
                case (int)SocketCommand.SEND_POINT:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        pgbCoolDown.Value = 0;
                        pnlChessBoard.Enabled = true;
                        timerCoolDown.Start();
                        ChessBoardManager.OtherPlayerMark(data.Point);
                        undoToolStripMenuItem.Enabled = true;
                    }));

                    break;
                case (int)SocketCommand.UNDO:
                    Undo();
                    pgbCoolDown.Value = 0;
                    break;
                case (int)SocketCommand.END_GAME:
                    if (data.Message == "0")
                    {
                        MessageBox.Show("Người chơi X chiến thắng!!!");
                    }
                    else
                    {
                        MessageBox.Show("Người chơi O chiến thắng!!!");
                    }
                    break;
                case (int)SocketCommand.TIME_OUT:
                    MessageBox.Show("Thông báo timeout!!!");
                    break;
                case (int)SocketCommand.QUIT:
                    timerCoolDown.Stop();
                    MessageBox.Show("Người chơi đã thoát");
                    break;
                default:
                    break;
            }
            Listen();
        }

        #endregion

        #region EventHandlers

        private void chessBoard_PlayerMarked(object? sender, ButtonClickEvent e)
        {
            timerCoolDown.Start();
            pnlChessBoard.Enabled = false;
            pgbCoolDown.Value = 0;

            SocketManager.Send(new SocketData((int)SocketCommand.SEND_POINT, e.ClickedPoint, null));
            undoToolStripMenuItem.Enabled = false;
            Listen();
        }

        private void chessBoard_EndedGame(object? sender, EventArgs e)
        {
            EndGame();
            var winner = ChessBoardManager.CurrentPlayer.ToString();
            SocketManager.Send(new SocketData((int)SocketCommand.END_GAME, new Point(), $"{winner}"));

        }

        private void timerCoolDown_Tick(object sender, EventArgs e)
        {
            pgbCoolDown.PerformStep();

            if (pgbCoolDown.Value >= pgbCoolDown.Maximum)
            {
                EndGame();
                SocketManager.Send(new SocketData((int)SocketCommand.TIME_OUT, new Point(), null));

            }
        }

        private void newGame_Click(object sender, EventArgs e)
        {
            NewGame();
            SocketManager.Send(new SocketData((int)SocketCommand.NEW_GAME, new Point(), null));
            pnlChessBoard.Enabled = true;
        }

        private void undo_Click(object sender, EventArgs e)
        {
            Undo();
            SocketManager.Send(new SocketData((int)SocketCommand.UNDO, new Point(), null));
        }

        private void quit_Click(object sender, EventArgs e)
        {
            Quit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát!", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
            else
            {
                try
                {
                    SocketManager.Send(new SocketData((int)SocketCommand.QUIT, new Point(), ""));
                }
                catch
                {
                }
            }

        }
        private void Btn_LANConnect_Click(object sender, EventArgs e)
        {
            SocketManager.IP = txbIP.Text;

            if (!SocketManager.ConnectServer())
            {
                SocketManager.IsServer = true;
                pnlChessBoard.Enabled = true;
                SocketManager.CreateServer();
            }
            else
            {
                SocketManager.IsServer = false;
                pnlChessBoard.Enabled = false;
                Listen();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            txbIP.Text = SocketManager.GetLocalIPv4(NetworkInterfaceType.Wireless80211);

            if (string.IsNullOrEmpty(txbIP.Text))
            {
                txbIP.Text = SocketManager.GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }
        }
        #endregion
    }
}
