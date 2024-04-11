namespace HHPWBE.Models
{
    public class CreateOrderDTO
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public int OrderTypeId { get; set; }
        public DateTime DateCreated { get; set; }
        
    }
}
