namespace E_Bill.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public int Amount { get; set; }
        public List<Item> Items{ get; set; }



        public Bill() 
        {
            Items = new List<Item>();
        }
    }
}
