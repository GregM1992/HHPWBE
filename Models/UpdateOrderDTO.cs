namespace HHPWBE.Models
{
    public class UpdateOrderDTO
    {
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public int OrderType { get; set; }
        public decimal Tip { get; set; }
    }
}
