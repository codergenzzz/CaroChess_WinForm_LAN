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

            ChessBoardManager = new ChessBoardManager(pnlChessBoard, txbPlayerName, ptbMark);
            ChessBoardManager.EndedGame += chessBoard_EndedGame;
            ChessBoardManager.PlayerMarked += chessBoard_PlayerMarked;


            pgbCoolDown.Step = Constants.COOL_DOWN_STEP;
            pgbCoolDown.Maximum = Constants.COOL_DOWN_TIME;
            pgbCoolDown.Value = 0;

            timerCoolDown.Interval = Constants.COOL_DOWN_INTERVAL;
            SocketManager = new SocketManager();

            NewGame();
        }

        #region Methods

        void EndGame()
        {
            timerCoolDown.Stop();
            pnlChessBoard.Enabled = false;
            undoToolStripMenuItem.Enabled = false;
            MessageBox.Show("Kết thúc!!!");
        }

        void NewGame()
        {
            // Dừng timer và reset progressbar
            pgbCoolDown.Value = 0;
            timerCoolDown.Stop();

            // Mở lại undo
            undoToolStripMenuItem.Enabled = true;

            // Vẽ lại bàn cờ
            ChessBoardManager.DrawChessBoard();
        }
        void Undo()
        {
            if (!ChessBoardManager.Undo())
            {
                MessageBox.Show("Không thể undo nữa!");
            }
        }
        void Quit()
        {
            Application.Exit();
        }

        void Listen()
        {
            string revData = (string)SocketManager.Receive();

            MessageBox.Show(revData);
        }

        #endregion

        #region EventHandlers

        private void chessBoard_PlayerMarked(object? sender, EventArgs e)
        {
            timerCoolDown.Start();
            pgbCoolDown.Value = 0;
        }

        private void chessBoard_EndedGame(object? sender, EventArgs e)
        {
            EndGame();
        }

        private void timerCoolDown_Tick(object sender, EventArgs e)
        {
            pgbCoolDown.PerformStep();

            if (pgbCoolDown.Value >= pgbCoolDown.Maximum)
            {
                EndGame();
            }
        }

        private void newGame_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void undo_Click(object sender, EventArgs e)
        {
            Undo();
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

        }
        private void Btn_LANConnect_Click(object sender, EventArgs e)
        {
            SocketManager.IP = txbIP.Text;

            if (!SocketManager.ConnectServer())
            {
                SocketManager.CreateServer();

                Thread listenThread = new Thread(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(500);
                        try
                        {
                            Listen();
                            break;
                        }
                        catch { }
                    }
                });
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            else
            {
                Thread listenThread = new Thread(() =>
                {
                    Listen();
                });
                listenThread.IsBackground = true;
                listenThread.Start();
                SocketManager.Send("Thong tin tu client");
            }

        }

        #endregion

        private void Form1_Shown(object sender, EventArgs e)
        {
            txbIP.Text = SocketManager.GetLocalIPv4(NetworkInterfaceType.Wireless80211);

            if (string.IsNullOrEmpty(txbIP.Text))
            {
                txbIP.Text = SocketManager.GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }
        }
    }
}
