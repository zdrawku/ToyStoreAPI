namespace ToyStoreAPI.Models
{
    public class Toy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }  // From JSON
        public decimal Price { get; set; }
    }
}
