namespace PrsServer.Models
{
    public class PO{

        public Vendor? Vendor { get; set; }
        public IEnumerable<POline> POlines { get; set; } = null!;
        public decimal PoTotal { get; set; }

    }

}
