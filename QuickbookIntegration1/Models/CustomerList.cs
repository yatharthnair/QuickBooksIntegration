using System;
using System.Collections.Generic;

namespace QuickbookIntegration1.Models
{
    public partial class CustomerList
    {
        public int Id { get; set; }
        public int SyncToken { get; set; }
        public string DisplayName { get; set; } = null!;
        public string PrimaryEmailAddress { get; set; } = null!;
    }
}
