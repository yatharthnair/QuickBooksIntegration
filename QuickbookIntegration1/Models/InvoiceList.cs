using System;
using System.Collections.Generic;

namespace QuickbookIntegration1.Models
{
    public partial class InvoiceList
    {
        public int Id { get; set; }
        public string Line0N { get; set; } = null!;
        public int CustomerRef { get; set; }
        public int SyncToken { get; set; }
        public string CurrencyRef { get; set; } = null!;
        public int DocNumber { get; set; }
        public string BillEmail { get; set; } = null!;
        public int CustumerRef { get; set; }
    }
}
