namespace HHPWBE.Models
{
    public class CloseOrderDTO
    {
        public bool IsClosed { get; set; }
        public decimal Tip {  get; set; }
        public DateTime? DateClosed { get; set; }
    }
}
