using System;
using System.Collections.Generic;

namespace QuickbookIntegration1.Models
{
    public partial class Po
    {
        public int Id { get; set; }
        public int ApaccountRef { get; set; }
        public int VendorRef { get; set; }
        public string? Line0N { get; set; } = null!;
        public string? CurrencyRef { get; set; } = null!;

        public string? QBid { get; set; }

        public virtual vendor VendorRefNavigation { get; set; } = null!;
    }
}
