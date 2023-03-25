using System;
using System.Collections.Generic;

namespace QuickbookIntegration1.Models
{
    public partial class Po
    {
        public int Id { get; set; }
       /* public int ApaccountRef { get; set; }*/
        public int? VendorRef { get; set; }
/*            public int VendorId { get; set; }    */
        public int? itemid {get; set; }
        /*public int item*/
        public DateTime due {get; set; }
        public int qty { get; set; }
        public int rate { get; set; }
        public int? itemaccref { get; set; }
/*        public string? CurrencyRef { get; set; } = null!;*/

        public string? QBid { get; set; }
        /*public int vendorid { get; set; }*/

        /*public virtual vendor VendorRefNavigation { get; set; } = null!;*/
    }
}
