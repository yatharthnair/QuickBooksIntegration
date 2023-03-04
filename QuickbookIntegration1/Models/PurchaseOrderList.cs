using System;
using System.Collections.Generic;

namespace QuickbookIntegration1.Models
{
    public partial class PurchaseOrderList
    {
        public int PurchaseOrderId { get; set; }
        public int Id { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public int ItemId { get; set; }
        public int ItemQuantity { get; set; }
        public int PriceItem { get; set; }
        public int TotalCost { get; set; }
    }
}
