using System;
using System.Collections.Generic;

namespace QuickbookIntegration1.Models
{
    public partial class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int SyncToken { get; set; }
        public DateTime InvStartDate { get; set; }
    }
}
