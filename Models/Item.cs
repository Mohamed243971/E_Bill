namespace E_Bill.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        //Navegation property
        public Bill bill { get; set; }
        public int BillId { get; set; }
    }
}
