namespace AgriEnergyConnect.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Farmer" or "Employee"

        public ICollection<Product>? Products { get; set; }
    }
}
