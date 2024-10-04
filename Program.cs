using CC = System.ConsoleColor;

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
        Program.Print("============================");
        Program.Print("Welcome to the Product List", CC.Magenta);
        Program.Print("============================");

    }
    static void HandleInput()
    {
        Program.Print("------------------");
        Program.Print("Press 'A' to Add", CC.DarkYellow);
        Program.Print("Press 'D' to Display", CC.DarkYellow);
        Program.Print("Press 'S' to Search", CC.DarkYellow);
        Program.Print("Press 'Q' to Quit", CC.DarkYellow);
        Program.Print("------------------");
        Program.Print("Enter your choice:", CC.DarkGreen);
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
                Program.Print("Invalid Input. Please try again.", CC.Red);

                break;
        }
    }
    public static void Print(string input, CC fgColor = CC.White)
    {
        Console.ForegroundColor = fgColor;
        Console.WriteLine("  " + input);
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

            Program.Print("Enter Input Or To Quit Press 'Q': ");
            Console.ResetColor();
            Program.Print("===================");
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

                Program.Print("Invalid price. Please enter a non-negative number.", CC.Red);
                Console.ResetColor();

            }


            try
            {
                Product product = new Product(category, name, price);
                productList.Add(product);
                Program.Print("Product added successfully.", CC.Green);
            }
            catch (Exception ex)
            {
                Program.Print($"Error adding product: {ex.Message}");
            }
        }
    }

    public static void DisplayProducts(List<Product> productsList)
    {

        var sortedProducts = productsList.OrderBy(p => p.Price).ToList();
        Program.Print("\nProduct List (Sorted by Price):");
        Program.Print("-------------------------------");
        foreach (var product in sortedProducts)
        {
            Program.Print($"Category: {product.Category}, Name: {product.Name}, Price: ${product.Price}");
        }
        Program.Print("-------------------------------");
        Program.Print($"Total Price: ${GetTotalPrice(productsList)}");


    }
    public static void SearchProducts(List<Product> productsList)

    {
        Program.Print("\nEnter search term: ", CC.DarkYellow);
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

