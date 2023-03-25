using System;
using System.Collections.Generic;

namespace QuickbookIntegration1.Models
{
    public partial class _Bill
    {
        public int Id { get; set; }
        public int VendorRef { get; set; }
        public int apaccountRef { get; set; }
        public DateTime due { get; set; }
        public int itemref { get; set; }
        public string? QBbillid { get; set; }
        public int Billno { get; set; }
        public int Qty { get; set; }
        public int rate { get; set; }
    }
}
