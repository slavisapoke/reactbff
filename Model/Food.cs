namespace Model;

public class Food
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public float Price { get; set; }
    public string PhotoName { get; set; }
    public bool SoldOut { get; set; }
    public string Type { get; set; }
}

