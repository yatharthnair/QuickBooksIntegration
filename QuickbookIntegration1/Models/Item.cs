using System;
using System.Collections.Generic;

namespace QuickbookIntegration1.Models
{
    public partial class _Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? QBitem { get; set; }
    }
}
