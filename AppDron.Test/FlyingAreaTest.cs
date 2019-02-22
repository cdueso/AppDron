using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AppDron.Test
{
    [TestClass]
    public sealed class FlyingAreaTest
    {
        [TestMethod]
        public void FlyingArea_Position_Into()
        {
            var fArea = new FlyingArea(5, 5);
            Enumerable.Range(1, 5).ToList().ForEach(r =>
            {
                 fArea.PositionIntoFlyingArea(new Position(r, r, CardinalDirection.E));
            });
        }

        [TestMethod]
        [ExpectedException(typeof(OutFlyingAreaException))]
        public void FlyingArea_Position_Out_X()
        {
            var fArea = new FlyingArea(5, 5);
            fArea.PositionIntoFlyingArea(new Position(6, 1, CardinalDirection.E));
        }

        [TestMethod]
        [ExpectedException(typeof(OutFlyingAreaException))]
        public void FlyingArea_Position_Out_Y()
        {
            var fArea = new FlyingArea(5, 5);
            fArea.PositionIntoFlyingArea(new Position(3, 6, CardinalDirection.E));
        }


        [TestMethod]
        [ExpectedException(typeof(OutFlyingAreaException))]
        public void FlyingArea_Position_Out_XY()
        {
            var fArea = new FlyingArea(5, 5);
            fArea.PositionIntoFlyingArea(new Position(6, 6, CardinalDirection.E));
        }
    }
}
