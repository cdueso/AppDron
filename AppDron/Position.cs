namespace AppDron
{
    public sealed class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public CardinalDirection Direction { get; set; }

        public Position() : this(0, 0, CardinalDirection.N) { }

        public Position(int x, int y, CardinalDirection direction)
        {
            this.X = x;
            this.Y = y;
            this.Direction = direction;
        }

        public override string ToString() => $"{X} {Y} {Direction.ToString()}";
    }
}
