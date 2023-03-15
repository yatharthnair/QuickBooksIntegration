using System;
using System.Collections.Generic;

namespace QuickbookIntegration1.Models
{
    public partial class vendor
    {
        public vendor()
        {
            Bills = new HashSet<Bill>();
            Pos = new HashSet<Po>();
        }

        public int Id { get; set; }
        public int SyncToken { get; set; }
        public string DisplayName { get; set; } = null!;
        public string PrimaryEmailAddress { get; set; } = null!;
        public int OtherContactInfo { get; set; }
        public string Gstin { get; set; } = null!;
        public int BusinessNumber { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Po> Pos { get; set; }
    }
}
