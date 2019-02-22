using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppDron
{
    public sealed class FlightOrders
    {
        #region Properties
        public Position StartPosition { get; }
        public List<Orders> Orders { get; }
        #endregion

        #region Constructor
        public FlightOrders(Position startPosition, List<Orders> orders)
        {
            this.StartPosition = startPosition;
            this.Orders = orders ?? new List<Orders>();
        }
        #endregion

        public override string ToString() => $"StartPosition: {StartPosition}, Orders: {string.Join("",Orders.Select(r => r.ToString()))}";
    }
}
