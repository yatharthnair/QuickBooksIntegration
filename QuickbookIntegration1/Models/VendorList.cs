using System;
using System.Collections.Generic;

namespace QuickbookIntegration1.Models
{
    public partial class VendorList
    {
        public int Id { get; set; }
        public string Suffix { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string PrimaryEmailAddress { get; set; } = null!;
        public string PanId { get; set; } = null!;
        public string Gstno { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string VendorBusiness { get; set; } = null!;
    }
}
