namespace _241018_CaroChess_WinForm
{
    public class PlayInfo
    {
        public Point Point { get; set; }
        public int CurrentPlayer { get; set; }

        public PlayInfo(Point point, int currentPlayer)
        {
            Point = point;
            CurrentPlayer = currentPlayer;
        }
    }
}
