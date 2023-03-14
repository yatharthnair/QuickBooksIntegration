using System;
using System.Collections.Generic;

namespace QuickbookIntegration1.Models
{
    public partial class purchaseorder
    {
        public int Id { get; set; }
        public int ApaccountRef { get; set; }
        public int VendorRef { get; set; }
        public string Line0N { get; set; } = null!;
        public int SyncToken { get; set; }
        public string CurrencyRef { get; set; } = null!;

        public virtual VendorList VendorRefNavigation { get; set; } = null!;
    }
}
