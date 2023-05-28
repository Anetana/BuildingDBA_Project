using Project.Repositories.ArticleRepository;
using Project.Repositories.CustomerRepository;
using Project.Repositories.SalesInvoiceRepository;

namespace Project.Logic
{
    public class Menu
    {
        public static void Run()
        {
            Tables.CreateTable("Customer", "CREATE TABLE Customer(customerId int primary key identity,name varchar(30) not null,address varchar(80))");
            Tables.CreateTable("Article", "CREATE TABLE Article(articleId int primary key identity,name varchar(80) not null,price decimal (14, 2))");
            Tables.CreateTable("SalesInvoice", "CREATE TABLE SalesInvoice(salesInvoiceId int primary key identity, customerId int foreign key references dbo.Customer(customerId), articleId int foreign key references dbo.Article(articleId), quantity int)");

            int x = -1;
            while(x == -1)
            {
                Console.WriteLine("Choose one of the options: (enter the number)");
                Console.WriteLine("1. Add a Customer");
                Console.WriteLine("2. Add an Article");
                Console.WriteLine("3. Add a Sales Invoice");
                Console.WriteLine("4. See all Articles");
                Console.WriteLine("5. See all Customers");
                Console.WriteLine("6. See all Sales Invoices");
                Console.WriteLine("7. Exit");

                string choice = Console.ReadLine();
                switch(choice)
                {
                    case "1":
                        CustomerFunctional.AddCustomer();
                        break;
                    case "2":
                        ArticleFunctional.AddArticle();
                        break;
                    case "3":
                        SalesInvoiceFunctional.AddSalesInvoice();
                        break;
                    case "4":
                        ArticleFunctional.GetAllArticles();
                        break;
                    case "5":
                        CustomerFunctional.GetAllCustomers();
                        break;
                    case "6":
                        SalesInvoiceFunctional.GetAllSalesInvoices();
                        break;
                    case "7":
                        x = 0;
                        break;
                    default: 
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }

            }
        }

        
    }
}