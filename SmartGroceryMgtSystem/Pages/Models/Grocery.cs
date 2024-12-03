namespace SmartGroceryMgtSystem.Pages.Models
{
    public class Grocery
    {
        private int grocery_id;
        private string? name;
        private string? category;
        private float price;
        private string? imageUrl;
        private string? available;


        public int Grocery_id { get => grocery_id; set => grocery_id = value; }
        public string? Name { get => name; set => name = value; }
        public string? Category { get => category; set => category = value; }
        public float Price { get => price; set => price = value; }
        public string? ImageUrl { get => imageUrl; set => imageUrl = value; }
        public string? Available { get => available; set => available = value; }
    }
}
