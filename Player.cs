namespace _241018_CaroChess_WinForm
{
    public class Player
    {
        public string Name { get; set; }
        public Image Mark { get; set; }

        public Player(string name, Image mark)
        {
            Name = name;
            Mark = mark;
        }
    }
}
