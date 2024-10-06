namespace ToyStoreAPI.Models
{
    public class Toy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public Toy(int id, string name, Category category, decimal price, string description, string imageUrl)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
            Description = description;
            ImageUrl = imageUrl;
        }
    }
}
