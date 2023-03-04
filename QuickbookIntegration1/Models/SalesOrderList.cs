using System;
using System.Collections.Generic;

namespace QuickbookIntegration1.Models
{
    public partial class SalesOrderList
    {
        public int SalesOrderId { get; set; }
        public DateTime DateOfOrder { get; set; }
        public int? Id { get; set; }
        public int ItemId { get; set; }
        public int ItemQuantity { get; set; }
        public int PriceItem { get; set; }
        public int TotalCost { get; set; }
    }
}
