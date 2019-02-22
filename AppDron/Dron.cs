using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppDron
{
    public interface IDron
    {
        Position ExecuteFlightOrders(FlightOrders order);
        Position GetCurrentPosition();
    }

    public sealed class Dron : IDron
    {
        #region Attributes
        private readonly IFlyingArea flyingArea;
        private Position currentPosition;

        private static Dictionary<Orders, Action<Position>> runOrders = new Dictionary<Orders, Action<Position>>()
        {
            { Orders.L, (a) => a.Direction = rotate[(int)a.Direction -1] },
            { Orders.R, (a) => a.Direction = rotate[(int)a.Direction +1] },
            { Orders.M, (a) => runMove[a.Direction](a) },
        };

        private static Dictionary<int, CardinalDirection> rotate = new Dictionary<int, CardinalDirection>()
        {
            { -1, CardinalDirection.W },
            { 0, CardinalDirection.N },
            { 1, CardinalDirection.E },
            { 2, CardinalDirection.S },
            { 3, CardinalDirection.W },
            { 4, CardinalDirection.N },
        };

        private static Dictionary<CardinalDirection, Action<Position>> runMove = new Dictionary<CardinalDirection, Action<Position>>()
        {
            { CardinalDirection.N, (a) => {a.Y += 1; } },
            { CardinalDirection.S, (a) => {a.Y -= 1; } },
            { CardinalDirection.W, (a) => {a.X -= 1; } },
            { CardinalDirection.E, (a) => {a.X += 1; } },
        };
        #endregion

        #region Constructor
        public Dron(IFlyingArea area)
        {
            this.flyingArea = area;
            this.currentPosition = new Position();
        }
        #endregion

        #region Methods Interface
        public Position ExecuteFlightOrders(FlightOrders order)
        {
            this.flyingArea.PositionIntoFlyingArea(order.StartPosition);
            this.currentPosition = order.StartPosition;

            order.Orders.ForEach(r =>
            {
                runOrders[r](this.currentPosition);
                this.flyingArea.PositionIntoFlyingArea(this.currentPosition);
            });

            return this.currentPosition;
        }

        public Position GetCurrentPosition()
        {
            return this.currentPosition;
        }
        #endregion
    }
}

