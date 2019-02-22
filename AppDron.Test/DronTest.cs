using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppDron.Test
{
    [TestClass]
    public sealed class DronTest
    {
        private Mock<IFlyingArea> flyingArea;
        private IDron service;

        [TestInitialize]
        public void Init()
        {
            var mocker = new AutoMocker();
            flyingArea = mocker.GetMock<IFlyingArea>();
            service = mocker.CreateInstance<Dron>();
        }

        [TestMethod]
        public void Dron_Rotate_Left_From_N()
        {
            Dron_Rotate(CardinalDirection.N, CardinalDirection.W, Orders.L);
        }

        [TestMethod]
        public void Dron_Rotate_Left_From_W()
        {
            Dron_Rotate(CardinalDirection.W, CardinalDirection.S, Orders.L);
        }

        [TestMethod]
        public void Dron_Rotate_Left_From_E()
        {
            Dron_Rotate(CardinalDirection.E, CardinalDirection.N, Orders.L);
        }

        [TestMethod]
        public void Dron_Rotate_Left_From_S()
        {
            Dron_Rotate(CardinalDirection.S, CardinalDirection.E, Orders.L);
        }

        [TestMethod]
        public void Dron_Rotate_Right_From_N()
        {
            Dron_Rotate(CardinalDirection.N, CardinalDirection.E, Orders.R);
        }

        [TestMethod]
        public void Dron_Rotate_Right_From_W()
        {
            Dron_Rotate(CardinalDirection.W, CardinalDirection.N, Orders.R);
        }

        [TestMethod]
        public void Dron_Rotate_Right_From_E()
        {
            Dron_Rotate(CardinalDirection.E, CardinalDirection.S, Orders.R);
        }

        [TestMethod]
        public void Dron_Rotate_Right_From_S()
        {
            Dron_Rotate(CardinalDirection.S, CardinalDirection.W, Orders.R);
        }

        [TestMethod]
        public void Dron_Move_N()
        {
            Dron_Move(CardinalDirection.N, new Position(0, 1, CardinalDirection.N));
        }

        [TestMethod]
        public void Dron_Move_S()
        {
            Dron_Move(CardinalDirection.S, new Position(0, -1, CardinalDirection.S));
        }

        [TestMethod]
        public void Dron_Move_W()
        {
            Dron_Move(CardinalDirection.W, new Position(-1, 0, CardinalDirection.W));
        }

        [TestMethod]
        public void Dron_Move_E()
        {
            Dron_Move(CardinalDirection.E, new Position(1, 0, CardinalDirection.E));
        }

        [TestMethod]
        public void Dron_Move_Rotate_ShortWay()
        {
            Dron_Move_Rotate(new Position(3,3,CardinalDirection.E), new Position(5, 1, CardinalDirection.E), new List<Orders>()
            {
                Orders.M,
                Orders.M,
                Orders.R,
                Orders.M,
                Orders.M,
                Orders.R,
                Orders.M,
                Orders.R,
                Orders.R,
                Orders.M,
            });
        }

        [TestMethod]
        public void Dron_Move_Rotate_LongWay()
        {
            Dron_Move_Rotate(new Position(1, 2, CardinalDirection.N), new Position(1, 4, CardinalDirection.N), new List<Orders>()
            {
                 Orders.L,
                Orders.M,
                Orders.L,
                Orders.M,
                Orders.L,
                Orders.M,
                Orders.L,
                Orders.M,
                Orders.M,
                Orders.L,
                Orders.M,
                Orders.L,
                Orders.M,
                Orders.L,
                Orders.M,
                Orders.L,
                Orders.M,
                Orders.M,
            });
        }

        private void Dron_Rotate(CardinalDirection initial, CardinalDirection expected, Orders order)
        {
            var result = service.ExecuteFlightOrders(new FlightOrders(new Position(0, 0, initial), new List<Orders>() { order }));
            Assert.AreEqual(expected, result.Direction, $"Dron_Rotate_Left --> Initial: {initial.ToString()}, Expected: {expected.ToString()}");
        }

        private void Dron_Move(CardinalDirection initial, Position expected)
        {
            var result = service.ExecuteFlightOrders(new FlightOrders(new Position(0, 0, initial), new List<Orders>() { Orders.M }));
            Assert.AreEqual(expected.X, result.X, $"Dron_Rotate_Move X--> Expected: {expected.X}, Actual: {result.X}");
            Assert.AreEqual(expected.Y, result.Y, $"Dron_Rotate_Move Y--> Expected: {expected.Y}, Actual: {result.Y}");
            Assert.AreEqual(expected.Direction, result.Direction, $"Dron_Rotate_Move Direction--> Expected: {expected.Direction}, Actual: {result.Direction.ToString()}");
        }

        private void Dron_Move_Rotate(Position initial, Position expected, List<Orders> orders)
        {
            var result = service.ExecuteFlightOrders(new FlightOrders(initial, orders));
            Assert.AreEqual(expected.X, result.X, $"Dron_Rotate_Move X--> Expected: {expected.X}, Actual: {result.X}");
            Assert.AreEqual(expected.Y, result.Y, $"Dron_Rotate_Move Y--> Expected: {expected.Y}, Actual: {result.Y}");
            Assert.AreEqual(expected.Direction, result.Direction, $"Dron_Rotate_Move Direction--> Expected: {expected.Direction}, Actual: {result.Direction.ToString()}");
        }
    }
}
