namespace lab_11.Models
{
    public class Order
    {
        private static int _id = 1;
        public int OrderId { get; private set; }
        public string OrderName { get; set; }
        public int OrderPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public Order()
        {
            OrderName = "";
            OrderId = _id++;
            CreatedAt = DateTime.Now;
        }
        public override string ToString()
        {
            return $"Order Id:{OrderId} Name:{OrderName} Price:{OrderPrice} CreatedAt:{CreatedAt}";
        }
    }
}
