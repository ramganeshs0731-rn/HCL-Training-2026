/*class Program
{
    static void Main()
    {
        int orderId;
        string customerName;
        string productName;
        int quantity;
        decimal unitPrice;
        decimal subtotal;

        while (true)
        {
            Console.Write("Enter Order ID: ");
            if (int.TryParse(Console.ReadLine(), out orderId)&& orderId > 0)
            {
                break;
            }
            Console.WriteLine("Invalid Order ID. Enter a positive integer.");
        }
        while (true)
        {
            Console.Write("Enter Customer Name: ");
            customerName = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(customerName))
            {
                break;
            }
            Console.WriteLine("Customer name cannot be empty.");
        }
        while (true)
        {
            Console.Write("Enter Product Name: ");
            productName = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(productName))
            {
                break;
            }
            Console.WriteLine("Product name cannot be empty.");
        }
        while (true)
        {
            Console.Write("Enter Quantity: ");

            if (int.TryParse(Console.ReadLine(), out quantity)&& quantity > 0)
            {
                break;
            }
            Console.WriteLine("Invalid quantity. Enter a positive integer.");
        }
        while (true)
        {
            Console.Write("Enter Unit Price: ");

            if (decimal.TryParse(Console.ReadLine(), out unitPrice)&& unitPrice > 0)
            {
                break;
            }
            Console.WriteLine("Invalid price. Enter a positive decimal number.");
        }
        subtotal = quantity * unitPrice;
        Console.WriteLine($"The total is {subtotal}");
    }
}
*/


class Product
{
    Private string name
    int ID { get; set; }
    string Name { get; set{
        name = value}

}





    