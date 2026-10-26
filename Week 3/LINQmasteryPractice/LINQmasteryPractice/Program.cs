using LinqMastery.Data;
using System.Linq;

namespace LinqMastery;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("LINQ Mastery Practice Project");
        Console.WriteLine("Uncomment one exercise method at a time in Main().");

        // Uncomment an exercise below to run it:
        Exercise01_Filtering();
        // Exercise02_Projection();
        // Exercise03_SelectMany();
        // Exercise04_Sorting();
        // Exercise05_ElementOperators();
        // Exercise06_Aggregation();
        // Exercise07_AnyAll();
        // Exercise08_Grouping();
        // Exercise09_SetOperations();
        // Exercise10_InnerJoin();
        // Exercise11_LeftJoin();
        // Exercise12_GroupJoin();
        // Exercise13_Partitioning();
        // Exercise14_QuantifiersAndMembership();
        // Exercise15_StringAndNumericQueries();
        // Exercise16_NestedQueries();
        // Exercise17_CombinedQueries();
        // Exercise18_DictionaryAndLookup();
        // Exercise19_JoinOrdersAndCustomers();
        // Exercise20_Reports();
    }

    static void Exercise01_Filtering()
    {
        var products = StoreData.Products;

        Console.WriteLine("--- Exercise 01: Filtering ---");

        // Task 1: Products priced above 5,000
        var t1_method = products.Where(p => p.Price > 5000);
        var t1_query = from p in products where p.Price > 5000 select p;

        Console.Write("Task 1 (Method): ");
        foreach (var p in t1_method) Console.Write(p.ProductName + " ");
        Console.WriteLine();

        Console.Write("Task 1 (Query):  ");
        foreach (var p in t1_query) Console.Write(p.ProductName + " ");
        Console.WriteLine();

        // Task 2: Electronics in stock
        var t2_method = products.Where(p => p.Category == "Electronics" && p.InStock);
        var t2_query = from p in products where p.Category == "Electronics" && p.InStock select p;

        Console.Write("Task 2 (Method): ");
        foreach (var p in t2_method) Console.Write(p.ProductName + " ");
        Console.WriteLine();

        Console.Write("Task 2 (Query):  ");
        foreach (var p in t2_query) Console.Write(p.ProductName + " ");
        Console.WriteLine();
    }

    static void Exercise02_Projection()
    {
        var products = StoreData.Products;

        Console.WriteLine("--- Exercise 02: Projection ---");

        // Task 1: Product names
        var t1_method = products.Select(p => p.ProductName);
        var t1_query = from p in products select p.ProductName;

        Console.Write("Task 1 (Method): ");
        foreach (var name in t1_method) Console.Write(name + " ");
        Console.WriteLine();

        Console.Write("Task 1 (Query):  ");
        foreach (var name in t1_query) Console.Write(name + " ");
        Console.WriteLine();

        // Task 3: ProductName, Price, PriceWithTax
        var t3_method = products.Select(p => new { p.ProductName, PriceWithTax = p.Price * 1.18m });
        var t3_query = from p in products select new { p.ProductName, PriceWithTax = p.Price * 1.18m };

        Console.WriteLine("Task 3 (Method Count): " + t3_method.Count());
        Console.WriteLine("Task 3 (Query Count):  " + t3_query.Count());
    }

    static void Exercise03_SelectMany()
    {
        var batches = StoreData.TrainingBatches;

        Console.WriteLine("--- Exercise 03: SelectMany ---");

        // Task 1: Flatten batches
        var t1_method = batches.SelectMany(b => b);
        var t1_query = from b in batches from item in b select item;

        Console.Write("Task 1 (Method): ");
        foreach (var item in t1_method) Console.Write(item + " ");
        Console.WriteLine();

        Console.Write("Task 1 (Query):  ");
        foreach (var item in t1_query) Console.Write(item + " ");
        Console.WriteLine();
    }

    static void Exercise04_Sorting()
    {
        var products = StoreData.Products;

        Console.WriteLine("--- Exercise 04: Sorting ---");

        // Task 3: Category ascending, Price descending
        var t3_method = products.OrderBy(p => p.Category).ThenByDescending(p => p.Price);
        var t3_query = from p in products orderby p.Category ascending, p.Price descending select p;

        Console.Write("Task 3 (Method): ");
        foreach (var p in t3_method) Console.Write($"{p.Category}-{p.ProductName} ");
        Console.WriteLine();

        Console.Write("Task 3 (Query):  ");
        foreach (var p in t3_query) Console.Write($"{p.Category}-{p.ProductName} ");
        Console.WriteLine();
    }

    static void Exercise05_ElementOperators()
    {
        var products = StoreData.Products;

        Console.WriteLine("--- Exercise 05: Element Operators ---");

        var t1_method = products.First(p => p.Price > 10000);
        var t1_query = (from p in products where p.Price > 10000 select p).First();

        Console.WriteLine("Task 1 (Method): " + t1_method.ProductName);
        Console.WriteLine("Task 1 (Query):  " + t1_query.ProductName);
    }

    static void Exercise06_Aggregation()
    {
        var products = StoreData.Products;

        Console.WriteLine("--- Exercise 06: Aggregation ---");

        var t1_method = products.Count();
        var t1_query = (from p in products select p).Count();

        Console.WriteLine("Task 1 Count (Method): " + t1_method);
        Console.WriteLine("Task 1 Count (Query):  " + t1_query);

        var t2_method = products.Sum(p => p.Stock);
        var t2_query = (from p in products select p).Sum(p => p.Stock);

        Console.WriteLine("Task 2 Stock (Method): " + t2_method);
        Console.WriteLine("Task 2 Stock (Query):  " + t2_query);
    }

    static void Exercise07_AnyAll()
    {
        var products = StoreData.Products;

        Console.WriteLine("--- Exercise 07: Any / All ---");

        var t1_method = products.Any(p => p.Price > 50000);
        var t1_query = (from p in products select p).Any(p => p.Price > 50000);

        Console.WriteLine("Task 1 (Method): " + t1_method);
        Console.WriteLine("Task 1 (Query):  " + t1_query);
    }

    static void Exercise08_Grouping()
    {
        var products = StoreData.Products;

        Console.WriteLine("--- Exercise 08: Grouping ---");

        var t2_method = products.GroupBy(p => p.Category).Select(g => new { Category = g.Key, Count = g.Count() });
        var t2_query = from p in products group p by p.Category into g select new { Category = g.Key, Count = g.Count() };

        Console.WriteLine("Task 2 (Method Groups): " + t2_method.Count());
        Console.WriteLine("Task 2 (Query Groups):  " + t2_query.Count());
    }

    static void Exercise09_SetOperations()
    {
        var a = StoreData.ListA;
        var b = StoreData.ListB;

        Console.WriteLine("--- Exercise 09: Set Operations ---");

        var t2_method = a.Union(b);
        var t2_query = (from x in a select x).Union(b);

        Console.Write("Union (Method): ");
        foreach (var item in t2_method) Console.Write(item + " ");
        Console.WriteLine();

        Console.Write("Union (Query):  ");
        foreach (var item in t2_query) Console.Write(item + " ");
        Console.WriteLine();
    }

    static void Exercise10_InnerJoin()
    {
        var employees = StoreData.Employees;
        var departments = StoreData.Departments;

        Console.WriteLine("--- Exercise 10: Inner Join ---");

        var t1_method = employees.Join(departments, e => e.DepartmentId, d => d.DepartmentId, (e, d) => d.DepartmentName);
        var t1_query = from e in employees join d in departments on e.DepartmentId equals d.DepartmentId select d.DepartmentName;

        Console.WriteLine("Count (Method): " + t1_method.Count());
        Console.WriteLine("Count (Query):  " + t1_query.Count());
    }

    static void Exercise11_LeftJoin()
    {
        var employees = StoreData.Employees;
        var departments = StoreData.Departments;

        Console.WriteLine("--- Exercise 11: Left Join ---");

        var t1_method = employees.GroupJoin(departments, e => e.DepartmentId, d => d.DepartmentId, (e, depts) => new { e, depts })
            .SelectMany(x => x.depts.DefaultIfEmpty(), (x, d) => d?.DepartmentName ?? "Unassigned");

        var t1_query = from e in employees
                       join d in departments on e.DepartmentId equals d.DepartmentId into deptGroup
                       from d in deptGroup.DefaultIfEmpty()
                       select d?.DepartmentName ?? "Unassigned";

        Console.Write("Method: ");
        foreach (var name in t1_method) Console.Write(name + " ");
        Console.WriteLine();

        Console.Write("Query:  ");
        foreach (var name in t1_query) Console.Write(name + " ");
        Console.WriteLine();
    }

    static void Exercise12_GroupJoin()
    {
        var employees = StoreData.Employees;
        var departments = StoreData.Departments;

        Console.WriteLine("--- Exercise 12: Group Join ---");

        var t1_method = departments.GroupJoin(employees, d => d.DepartmentId, e => e.DepartmentId, (d, emps) => emps.Count());
        var t1_query = from d in departments join e in employees on d.DepartmentId equals e.DepartmentId into emps select emps.Count();

        Console.Write("Count (Method): ");
        foreach (var count in t1_method) Console.Write(count + " ");
        Console.WriteLine();

        Console.Write("Count (Query):  ");
        foreach (var count in t1_query) Console.Write(count + " ");
        Console.WriteLine();
    }

    static void Exercise13_Partitioning()
    {
        var products = StoreData.Products.OrderBy(p => p.ProductId);

        Console.WriteLine("--- Exercise 13: Partitioning ---");

        var t1_method = products.Take(3);
        var t1_query = (from p in products select p).Take(3);

        Console.Write("Take 3 (Method): ");
        foreach (var p in t1_method) Console.Write(p.ProductName + " ");
        Console.WriteLine();

        Console.Write("Take 3 (Query):  ");
        foreach (var p in t1_query) Console.Write(p.ProductName + " ");
        Console.WriteLine();
    }

    static void Exercise14_QuantifiersAndMembership()
    {
        var products = StoreData.Products;
        List<int> requestedIds = new() { 1, 3, 5 };

        Console.WriteLine("--- Exercise 14: Quantifiers & Membership ---");

        var t1_method = products.Where(p => requestedIds.Contains(p.ProductId));
        var t1_query = from p in products where requestedIds.Contains(p.ProductId) select p;

        Console.WriteLine("Method Count: " + t1_method.Count());
        Console.WriteLine("Query Count:  " + t1_query.Count());
    }

    static void Exercise15_StringAndNumericQueries()
    {
        var products = StoreData.Products;

        Console.WriteLine("--- Exercise 15: String & Numeric ---");

        var t1_method = products.Where(p => p.ProductName.StartsWith("O"));
        var t1_query = from p in products where p.ProductName.StartsWith("O") select p;

        Console.Write("Starts with O (Method): ");
        foreach (var p in t1_method) Console.Write(p.ProductName + " ");
        Console.WriteLine();

        Console.Write("Starts with O (Query):  ");
        foreach (var p in t1_query) Console.Write(p.ProductName + " ");
        Console.WriteLine();
    }

    static void Exercise16_NestedQueries()
    {
        var products = StoreData.Products;

        Console.WriteLine("--- Exercise 16: Nested Queries ---");

        var t1_method = products.Where(p => p.Price > products.Average(x => x.Price));
        var t1_query = from p in products where p.Price > (from x in products select x.Price).Average() select p;

        Console.WriteLine("Above Avg Count (Method): " + t1_method.Count());
        Console.WriteLine("Above Avg Count (Query):  " + t1_query.Count());
    }

    static void Exercise17_CombinedQueries()
    {
        var products = StoreData.Products;

        Console.WriteLine("--- Exercise 17: Combined Queries ---");

        var t1_method = products.Where(p => p.Category == "Electronics" && p.InStock).OrderByDescending(p => p.Price).Select(p => p.ProductName);
        var t1_query = from p in products where p.Category == "Electronics" && p.InStock orderby p.Price descending select p.ProductName;

        Console.Write("Method: ");
        foreach (var name in t1_method) Console.Write(name + " ");
        Console.WriteLine();

        Console.Write("Query:  ");
        foreach (var name in t1_query) Console.Write(name + " ");
        Console.WriteLine();
    }

    static void Exercise18_DictionaryAndLookup()
    {
        var products = StoreData.Products;

        Console.WriteLine("--- Exercise 18: Dictionary & Lookup ---");

        var t1_method = products.ToDictionary(p => p.ProductId);
        var t1_query = (from p in products select p).ToDictionary(p => p.ProductId);

        Console.WriteLine("Dictionary Count (Method): " + t1_method.Count);
        Console.WriteLine("Dictionary Count (Query):  " + t1_query.Count);
    }

    static void Exercise19_JoinOrdersAndCustomers()
    {
        var orders = StoreData.Orders;
        var customers = StoreData.Customers;

        Console.WriteLine("--- Exercise 19: Join Orders & Customers ---");

        var t1_method = orders.Join(customers, o => o.CustomerId, c => c.CustomerId, (o, c) => c.CustomerName);
        var t1_query = from o in orders join c in customers on o.CustomerId equals c.CustomerId select c.CustomerName;

        Console.WriteLine("Count (Method): " + t1_method.Count());
        Console.WriteLine("Count (Query):  " + t1_query.Count());
    }

    static void Exercise20_Reports()
    {
        var orders = StoreData.Orders;

        Console.WriteLine("--- Exercise 20: Reports ---");

        var t1_method = orders.GroupBy(o => o.Status).Select(g => new { g.Key, Count = g.Count() });
        var t1_query = from o in orders group o by o.Status into g select new { g.Key, Count = g.Count() };

        Console.WriteLine("Status Groups Count (Method): " + t1_method.Count());
        Console.WriteLine("Status Groups Count (Query):  " + t1_query.Count());
    }
}