namespace ObjectFactoryWithExpression.Models;

public class Cat
{
    public Cat()
    {
        
    }
    public Cat(string name)
    {
        Name = name;
    }
    
    public string Name { get; set; }
}