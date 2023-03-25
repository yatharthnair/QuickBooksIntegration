using System;
using System.Collections.Generic;

namespace QuickbookIntegration1.Models
{
    public partial class vendor
    {
        public vendor()
        {
            Bills = new HashSet<_Bill>();
            Pos = new HashSet<Po>();
        }

        public int Id { get; set; }
      /*  public int SyncToken { get; set; }*/
        public string DisplayName { get; set; } = null!;
        public string PrimaryEmailAddress { get; set; } = null!;
        public string? OtherContactInfo { get; set; }
        public string Gstin { get; set; } = null!;
        public int BusinessNumber { get; set; }
        public string? QBid {get; set; }
        public virtual ICollection<_Bill> Bills { get; set; }
        public virtual ICollection<Po> Pos { get; set; }
    }
}
