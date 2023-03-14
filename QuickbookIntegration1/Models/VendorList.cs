using System;
using System.Collections.Generic;

namespace QuickbookIntegration1.Models
{
    public partial class VendorList
    {
        public int Id { get; set; }
        public int SyncToken { get; set; }
        public string PrimaryEmailAddress { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public int OtherContactInfo { get; set; }
        public string Gstin { get; set; } = null!;
        public string BusinessNumber { get; set; } = null!;
        public string CurrencyRef { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public int AcctNum { get; set; }
        public string BillAddr { get; set; } = null!;
        public int Balance { get; set; }
    }
}
