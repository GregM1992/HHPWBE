namespace HHPWBE.Models
{
    public class OrderDTO
    {
        public string customerEmail {  get; set; }
        public string customerName { get; set; }
        public string customerPhone { get; set; }
        public int id { get; set; }
        public DateTime dateCreated { get; set; }
        public int? orderTypeId { get; set; }

    }
}
