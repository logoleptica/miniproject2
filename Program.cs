// See https://aka.ms/new-console-template for more information
class Program
{
    public static List<Product> productList = new List<Product>();
    static void Main(string[] args)
    {

        Greeting();
        while (true)
        {
            HandleInput();

        }

    }
    static void Greeting()
    {
        Console.WriteLine("Welcome to the Product List");
    }
    static void HandleInput()
    {
        Console.WriteLine("Press 'A' to Add");
        Console.WriteLine("Press 'D' to Display");
        Console.WriteLine("Press 'S' to Search");
        Console.WriteLine("Press 'Q' to Quit");
        Console.WriteLine("------------------");
        string input = Console.ReadLine();
        switch (input.ToUpper())
        {
            case "A":
                Product.AddProducts(productList);
                break;
            case "D":
                Product.DisplayProducts(productList);
                break;
            case "S":
                Product.SearchProducts(productList);
                break;
            case "Q":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid Input. Please try again.");
                break;
        }
    }
}



class Product

{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }

    public Product(string category, string name, decimal price)
    {
        Category = category;
        Name = name;
        Price = price;
    }
    public static void AddProducts(List<Product> productList)
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;

            Console.WriteLine("Enter Input Or To Quit Press 'Q': ");
            Console.ResetColor();
            Console.WriteLine("===================");
            Console.Write("Enter category: ");

            string category = Console.ReadLine();
            if (category.ToLower() == "q")
                break;

            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            decimal price;
            while (true)
            {
                Console.Write("Enter price: ");
                Console.ResetColor();
                if (decimal.TryParse(Console.ReadLine(), out price) && price >= 0)
                    break;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid price. Please enter a non-negative number.");
                Console.ResetColor();

            }


            try
            {
                Product product = new Product(category, name, price);
                productList.Add(product);
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Product added successfully.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error adding product: {ex.Message}");
                Console.ResetColor();
            }
        }
    }

    public static void DisplayProducts(List<Product> productsList)
    {

        var sortedProducts = productsList.OrderBy(p => p.Price).ToList();
        Console.WriteLine("\nProduct List (Sorted by Price):");
        Console.WriteLine("-------------------------------");
        foreach (var product in sortedProducts)
        {
            Console.WriteLine($"Category: {product.Category}, Name: {product.Name}, Price: ${product.Price}");
        }
        Console.WriteLine("-------------------------------");
        Console.WriteLine($"Total Price: ${GetTotalPrice(productsList)}");


    }
    public static void SearchProducts(List<Product> productsList)

    {
        Console.WriteLine("\nEnter search term:");
        string searchTerm = Console.ReadLine();
        DisplayProducts(productsList.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                   p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                       .ToList());
    }
    static public decimal GetTotalPrice(List<Product> productsList)
    {
        return productsList.Sum(p => p.Price);
    }
}

