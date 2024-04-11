namespace HHPWBE.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public int OrderTypeId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateClosed { get; set; }
        public decimal? SubTotal
        {
            get
            {
                return Items.Sum(i => i.Price);
            }
            
        }
        public decimal? Tip {  get; set; }
        public decimal? Total
        {
            get
            {
                return (Tip + SubTotal);
            }
        }
        public Boolean IsClosed { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public OrderType OrderType { get; set; }
    }
}

