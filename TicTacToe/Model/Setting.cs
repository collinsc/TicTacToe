namespace TicTacToe.Model
{
    public enum GameMode
    {
        SinglePlayer = 0,
        TwoPlayer = 1 
    }

    public struct Setting<T>
    {
        public string Description { get; internal set; }
        public string Name { get; internal set; }
        public T Value { get; internal set; }
    }
}