namespace lab_6.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string NameTovar { get; set; }
        public string DescrTovar { get; set; }
        public User User { get; set; }
        public Product() { }
        public Product(int id, string nameTovar, string descrTovar, User user)
        {
            Id = id;
            NameTovar = nameTovar;
            DescrTovar = descrTovar;
            User = user;
        }
    }
}
