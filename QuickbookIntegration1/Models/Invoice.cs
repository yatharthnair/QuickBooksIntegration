namespace QuickbookIntegration1.Models
{
    public partial class invoice

    {
        public int id { get; set; }
        public string customerref { get; set; }
        public int qty {get; set; }
        public int rate { get; set; }
        public string Email { get; set; }
        public string itemref { get; set; }
        public string? QBInvid { get; set; }
    }
}
