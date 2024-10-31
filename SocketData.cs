namespace _241018_CaroChess_WinForm
{
    [Serializable]
    public class SocketData
    {
        private int command;
        private Point point;
        private string? message;

        public int Command { get => command; set => command = value; }
        public Point Point { get => point; set => point = value; }
        public string? Message { get => message; set => message = value; }

        public SocketData(int command, Point point, string? Message = null)
        {
            Command = command;
            Point = point;
            Message = message;
        }
    }

    public enum SocketCommand
    {
        SEND_POINT,
        NOTIFY,
        NEW_GAME,
        UNDO,
        END_GAME,
        TIME_OUT,
        QUIT
    }
}
