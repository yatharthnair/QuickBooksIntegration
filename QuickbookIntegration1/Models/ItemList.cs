using System;
using System.Collections.Generic;

namespace QuickbookIntegration1.Models
{
    public partial class ItemList
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int SyncToken { get; set; }
        public DateTime InvStartDate { get; set; }
        public string Type { get; set; } = null!;
        public decimal QtyOnHand { get; set; }
        public string AssetAccountRef { get; set; } = null!;
    }
}
