namespace HHPWBE.Models
{
    public class UpdateOrderDTO
    {
        public int id {  get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime Created { get; set; }
        public int OrderTypeId { get; set; }
        public decimal Tip { get; set; }
    }
}
