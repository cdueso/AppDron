using System;
using System.Collections.Generic;
using System.Text;

namespace AppDron
{
    public interface IFlyingArea
    {
        void PositionIntoFlyingArea(Position position);
    }

    public sealed class FlyingArea : IFlyingArea
    {
        #region Attributes
        public int x;
        public int y;
        #endregion

        #region Constructor
        public FlyingArea() : this(0, 0) { }
        public FlyingArea(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion

        #region Methods Interface
        public void PositionIntoFlyingArea(Position position)
        {
            if (this.x < position.X || position.X < 0 || this.y < position.Y || position.Y < 0)
            {
                throw new OutFlyingAreaException("The drone is outside the flight area. CurrentPosition: {0}", position);
            }
        }
        #endregion
    }
}
