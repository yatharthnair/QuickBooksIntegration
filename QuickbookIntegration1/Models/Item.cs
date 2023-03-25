using System;
using System.Collections.Generic;

namespace QuickbookIntegration1.Models
{
    public partial class _Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? QBitem { get; set; }
        public int? expenseaccref { get; set; }
        public int? SKU {  get; set; }
        public decimal? cost { get; set; }

    }
}
