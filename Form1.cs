namespace _241018_CaroChess_WinForm
{
    public partial class Form1 : Form
    {
        ChessBoardManager ChessBoardManager;

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

            ChessBoardManager.DrawChessBoard();
        }

        void EndGame()
        {
            timerCoolDown.Stop();
            pnlChessBoard.Enabled = false;
            MessageBox.Show("Kết thúc!!!");
        }

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
    }
}
