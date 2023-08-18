namespace BO {
    public class Ingredient {
        public int Id { get; set; }
        public string Nom { get; set; }

        public override string ToString()
        {
            return Nom;
        }
    }
}
