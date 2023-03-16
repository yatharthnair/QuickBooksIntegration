using System;
using System.Collections.Generic;

namespace QuickbookIntegration1.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int AcctNum { get; set; }
        public string AccountType { get; set; } = null!;
        public string AccountSubType { get; set; } = null!;
        public int SyncToken { get; set; }
    }
}
