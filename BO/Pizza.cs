namespace BO
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public Pate Pate { get; set; } = new();
        public List<Ingredient> Ingredients { get; set; } = new();

        public Pizza()
        {
        }

        public Pizza(int id, string nom, Pate pate)
        {
            Id = id;
            Nom = nom;
            Pate = pate;
        }

        public Pizza(string nom, Pate pate)
        {
            Nom = nom;
            Pate = pate;
        }
    }
}