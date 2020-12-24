using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Restaurant_Manager.Duomenys;

namespace Restaurant_Manager
{
    class Program
    {
        const string fdStock = "Data\\Stock.csv";
        const string fdMenu = "Data\\Menu.csv";
        const string fdOrder = "Data\\Order.csv";
        static void Main(string[] args)
        {
            bool showDisplayMenu = true;
            Restaurant restaurant = new Restaurant();
            ReadStock(fdStock, restaurant);
            ReadMenu(fdMenu, restaurant);
            ReadOrder(fdOrder, restaurant);
            /*PrintStock(restaurant);
            PrintMenu(restaurant);
            PrintOrder(restaurant);*/
           
            while (showDisplayMenu)
            {
                showDisplayMenu = DisplayMenu(restaurant);
            }
        }
        //perskaitom duomenu failus
        static void ReadStock(string fv, Restaurant r)
        {
            using (StreamReader reader = new StreamReader(fv))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    Stock st = new Stock(int.Parse(parts[0].Trim()), parts[1].Trim(), int.Parse(parts[2].Trim()), parts[3].Trim(), double.Parse(parts[4].Trim()));
                    r.stocks.Add(st);
                }
            }
        }
        static void ReadMenu(string fv, Restaurant r)
        {
            using (StreamReader reader = new StreamReader(fv))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    Menu st = new Menu(int.Parse(parts[0].Trim()), parts[1].Trim(), parts[2].Trim());
                    r.menus.Add(st);
                }
            }
        }
        static void ReadOrder(string fv, Restaurant r)
        {
            using (StreamReader reader = new StreamReader(fv))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    Order st = new Order(int.Parse(parts[0].Trim()), parts[1].Trim(), parts[2].Trim());
                    r.orders.Add(st);
                }
            }
        }
        //Atvaizduoju tam tikrus sarasus
        static void PrintStock(Restaurant r)
        {
            const string top =
               "----------------------------------------------------------------------\r\n"
               + "|ID    |       Name      |  Portion Count  |     Unit | Portion Size |\r\n"
               + "----------------------------------------------------------------------";
            Console.WriteLine("Stock List");
            Console.WriteLine(top);
            if (r.stocks.Count > 0)
            {
                for (int i = 0; i < r.stocks.Count; i++)
                    Console.WriteLine(r.stocks[i].ToString());
                Console.WriteLine("----------------------------------------------------------------------");
            }
            else
            {
                Console.WriteLine("     You should restock! :)     ");
                Console.WriteLine("----------------------------------------------------------------------");
            }
            Console.WriteLine();
        }
        static void PrintMenu(Restaurant r)
        {
            Console.WriteLine("Menu List");
            const string top =
               "----------------------------------------------------------\r\n"
               + "|ID    |             Name               |    Products    |\r\n"
               + "----------------------------------------------------------";
            Console.WriteLine(top);
            if (r.menus.Count > 0)
            {
                for (int i = 0; i < r.menus.Count; i++)
                    Console.WriteLine(r.menus[i].ToString());
                Console.WriteLine("----------------------------------------------------------");
            }
            else
            {
                Console.WriteLine("     There is no menu :(     ");
                Console.WriteLine("----------------------------------------------------------");
            }
            Console.WriteLine();
        }
        static void PrintOrder(Restaurant r)
        {
            Console.WriteLine("Order List");
            const string top =
               "-----------------------------------------------------\r\n"
               + "|ID    |         Date         |      Menu Items     |\r\n"
               + "-----------------------------------------------------";
            Console.WriteLine(top);
            if (r.orders.Count > 0)
            {
                for (int i = 0; i < r.orders.Count; i++)
                    Console.WriteLine(r.orders[i].ToString());
                Console.WriteLine("-----------------------------------------------------");
            }
            else
            {
                Console.WriteLine("     There is no orders :(     ");
                Console.WriteLine("-----------------------------------------------------");
            }
            Console.WriteLine();
        }
        //Save methods
        static void SaveStock(string fv, Restaurant r)
        {
            if (File.Exists(fv))
                File.Delete(fv);
            using (var fr = File.AppendText(fv))
            {
                foreach(Stock x in r.stocks)
                {
                    fr.WriteLine("{0},{1},{2},{3},{4}", x.id, x.name, x.portionCount, x.unit, x.portionSize);
                }
            }
        }
        static void SaveMenu(string fv, Restaurant r)
        {
            if (File.Exists(fv))
                File.Delete(fv);
            using (var fr = File.AppendText(fv))
            {
                foreach (Menu x in r.menus)
                {
                    fr.WriteLine("{0},{1},{2}", x.id, x.name, x.products);
                }
            }
        }
        static void SaveOrder(string fv, Restaurant r)
        {
            if (File.Exists(fv))
                File.Delete(fv);
            using (var fr = File.AppendText(fv))
            {
                foreach (Order x in r.orders)
                {
                    fr.WriteLine("{0},{1},{2}", x.id, x.dateTime, x.menuItems);
                }
            }
        }
        //toliau kazkas
        private static bool DisplayMenu(Restaurant r)
        {
            bool showMenus = true;
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) 'Stock' MENU");
            Console.WriteLine("2) 'Menu' MENU");
            Console.WriteLine("3) 'Order' MENU");
            Console.WriteLine("4) Print All Information");
            Console.WriteLine("5) Clear Console");
            Console.WriteLine("6) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    while (showMenus)
                    {
                        showMenus = StockMenu(r);
                    }
                    return true;
                case "2":
                    while (showMenus)
                    {
                        showMenus = MenuMenu(r);
                    }
                    return true;
                case "3":
                    while (showMenus)
                    {
                        showMenus = OrderMenu(r);
                    }
                    return true;
                case "4":
                    PrintStock(r);
                    PrintMenu(r);
                    PrintOrder(r);
                    return true;
                case "5":
                    Console.Clear();
                    return true;
                case "6":
                    return false;
                default:
                    return true;
            }
        }
        // Stock functions
        private static bool StockMenu(Restaurant r)
        {
            Console.Clear();
            PrintStock(r);
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Add New Product");
            Console.WriteLine("2) Update Existing Stock");
            Console.WriteLine("3) Remove Existing Stock");
            Console.WriteLine("4) Back");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    addStock(r);
                    SaveStock(fdStock, r);
                    return true;
                case "2":
                    updateStock(r);
                    SaveStock(fdStock, r);
                    return true;
                case "3":
                    deleteStock(r);
                    SaveStock(fdStock, r);
                    return true;
                case "4":
                    Console.Clear();
                    return false;
                default:
                    return true;
            }
        }
        public static void addStock(Restaurant r)
        {
            Stock stock = new Stock();
            double amountOfSomething = 0;
            Console.Write("Product Name: "); stock.name = Console.ReadLine();
            Console.Write("Unit: "); stock.unit = Console.ReadLine();
            Console.Write("Portion Size: "); stock.portionSize = double.Parse(Console.ReadLine());
            Console.Write(stock.name + " amount in " + stock.unit + ": "); amountOfSomething = double.Parse(Console.ReadLine());
            stock.portionCount = Convert.ToInt32(amountOfSomething / stock.portionSize);
            stock.id = r.stocks.Count + 1;
            r.stocks.Add(stock);
        }
        public static void updateStock(Restaurant r)
        {
            Console.WriteLine("If you dont want to change the value just press ENTER button");
            Console.WriteLine("Enter ID of the product you want to edit: "); int idrl = int.Parse(Console.ReadLine());
            var vStockID = r.stocks.Where(x => x.id == idrl).FirstOrDefault();
            string t1, t2, t3, t4; // not the best solution, want to check if empty or not.. :(
            if (vStockID != null)
            {
                Console.Write("Product Name: "); t1 = Console.ReadLine();
                Console.Write("Unit: "); t2 = Console.ReadLine();
                Console.Write("Portion Size: "); t3 = Console.ReadLine();
                Console.Write(t1 + " amount in " + t2 + ": "); t4 = Console.ReadLine();


                if (!string.IsNullOrEmpty(t1))
                    vStockID.name = t1;
                if (!string.IsNullOrEmpty(t2))
                    vStockID.unit = t2;
                if (!string.IsNullOrEmpty(t3))
                    vStockID.portionSize = double.Parse(t3);
                if (!string.IsNullOrEmpty(t4))
                    if (string.IsNullOrEmpty(t3))
                        vStockID.portionCount = Convert.ToInt32(Double.Parse(t4) / vStockID.portionSize);
                    else
                        vStockID.portionCount = Convert.ToInt32(Double.Parse(t4) / Double.Parse(t3));
            }
            else
            {
                Console.WriteLine("ID (" + idrl + ") doesn't exist");
            }
        }
        public static void deleteStock(Restaurant r)
        {
            Console.Write("Enter ID of the PRODUCT you want to delete: "); int idrl = int.Parse(Console.ReadLine());
            var vStockID = r.stocks.SingleOrDefault(x => x.id == idrl);
            if (vStockID != null)
                r.stocks.Remove(vStockID);
        }
        //Menu functions
        private static bool MenuMenu(Restaurant r)
        {
            Console.Clear();
            PrintMenu(r);
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Add New Menu Items");
            Console.WriteLine("2) Update Menu Items");
            Console.WriteLine("3) Remove Existing Menu Item");
            Console.WriteLine("4) Back");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    addMenu(r);
                    SaveMenu(fdMenu, r);
                    return true;
                case "2":
                    updateMenu(r);
                    SaveMenu(fdMenu, r);
                    return true;
                case "3":
                    deleteMenu(r);
                    SaveMenu(fdMenu, r);
                    return true;
                case "4":
                    Console.Clear();
                    return false;
                default:
                    return true;
            }
        }
        public static void addMenu(Restaurant r)
        {
            PrintStock(r);
            Menu menu = new Menu();
            string input = "-1";
            Console.Write("Name: "); menu.name = Console.ReadLine();
            Console.WriteLine("If you want to stop writing press ENTER button.");
            Console.Write("Products: ");
            while ((input = Console.ReadLine()) != "")
            {
                if (r.stocks.Any(x => x.id == int.Parse(input)))
                { 
                    Console.Write("Products: ");
                    if (!string.IsNullOrEmpty(menu.products))
                    {
                        string[] checkifExist = menu.products.Trim().Split(' ');
                        bool checkif = false;
                        foreach (string x in checkifExist)
                            if (input.Contains(x))
                            {
                                checkif = true;
                                break;
                            }
                        if (!checkif)
                            menu.products += " " + input;
                    }
                    else
                        menu.products += " " + input;
                }
                else
                    Console.WriteLine("Product doesn't exist");
            }
            menu.id = r.menus.Count + 1;
            r.menus.Add(menu);
        }
        public static void updateMenu(Restaurant r)
        {
            PrintStock(r);
            Console.WriteLine("If you dont want to change the value just press ENTER button");
            Console.Write("Enter ID of the MENU you want to edit: "); int idrl = int.Parse(Console.ReadLine());
            var vMenuID = r.menus.Where(x => x.id == idrl).FirstOrDefault();
            string t1, t2, input; // not the best solution, want to check if empty or not.. :(
            if (vMenuID != null)
            {
                Console.Write("Name: "); t1 = Console.ReadLine();
                Console.Write("Do you want to change products (Yes/No)?: "); t2 = Console.ReadLine();

                if (!string.IsNullOrEmpty(t1))
                    vMenuID.name = t1;
                if (t2.ToLower() == "yes")
                {
                    t2 = "";
                    Console.Write("Products: ");
                    while ((input = Console.ReadLine()) != "")
                    {
                        if (r.stocks.Any(x => x.id == int.Parse(input)))
                        {
                            Console.Write("Products: ");
                            if (!string.IsNullOrEmpty(t2))
                            {
                                string[] checkifExist = t2.Trim().Split(' ');
                                bool checkif = false;
                                foreach (string x in checkifExist)
                                    if (input.Contains(x))
                                    {
                                        checkif = true;
                                        break;
                                    }
                                if (!checkif)
                                    t2 += " " + input;
                            }
                            else
                                t2 += " " + input;
                        }
                        else
                            Console.WriteLine("Product doesn't exist");
                    }
                    vMenuID.products = t2;
                }
            }
            else
            {
                Console.WriteLine("ID (" + idrl + ") doesn't exist");
            }
        }
        public static void deleteMenu(Restaurant r)
        {
            Console.WriteLine("Enter ID of the MENU you want to delete: "); int idrl = int.Parse(Console.ReadLine());
            var vMenuID = r.menus.Where(x => x.id == idrl).FirstOrDefault();
            if (vMenuID != null)
                r.menus.Remove(vMenuID);
        }
        //Order functions
        private static bool OrderMenu(Restaurant r)
        {
            Console.Clear();
            PrintOrder(r);
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Add New Customer Order");
            Console.WriteLine("2) Back");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    createOrder(r);
                    SaveStock(fdStock, r);
                    SaveOrder(fdOrder, r);
                    return true;
                case "2":
                    Console.Clear();
                    return false;
                default:
                    return true;
            }
        }
        public static void createOrder(Restaurant r)
        {
            PrintMenu(r);
            Order order = new Order();
            List<Stock> currentStock = r.stocks.ConvertAll(x => new Stock(x));
            string input; bool isDone = false;
            Console.WriteLine("Choose items from MENU list by writing its ID");
            Console.WriteLine("If you want to stop writing press ENTER button.");
            Console.Write("Menu item: ");
            while ((input = Console.ReadLine()) != "")
            {
                Console.Write("Menu item: ");
                var vMenuID = r.menus.Where(x => x.id == int.Parse(input)).FirstOrDefault();
                if (vMenuID != null)
                {
                    int whatHappened = checkIfEnough(vMenuID, currentStock, input);
                    if (whatHappened == 1)
                    {
                        isDone = true;
                        order.menuItems += " " + input;
                    }
                    else if(whatHappened == 0)
                    {
                        isDone = false;
                        order.menuItems += " " + input;
                    }
                    else if (whatHappened == -1)
                    {
                        isDone = false;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("This menu item ID doesn't exist");
                }
            }
            if ((!isDone && input == "") || (isDone && input == ""))
            {
                r.stocks = currentStock;
                order.id = r.orders.Count + 1;
                r.orders.Add(order);
            }
            else
                Console.WriteLine("Some of the menu item ingredients are out of stock!");
        }
        private static int checkIfEnough(Menu vMenuID, List<Stock> stock, string menuItem)
        { // tarkim menu item issirinko 1, tada paziuri ar toks menu item egzistuoja, tada pasiima products ir issplitina po viena tada ji checkinam ar uztenka
            int ifEnough = -1;
                string[] stockRequired = vMenuID.products.Split(' ');
                for (int i = 0; i < stockRequired.Count(); i++)
                {
                    ifEnough = -1;
                    for (int j = 0; j < stock.Count; j++)
                        if (stock[j].id == int.Parse(stockRequired[i]))
                        {
                            if (stock[j].portionCount >= 1)
                            {
                                ifEnough = 1;
                                break;
                            }
                            else
                                break;
                        }
                    if (ifEnough == -1)
                    {
                        Console.WriteLine("Out of stock..");
                        break;
                    }
                }
                if (ifEnough == 1)
                    for (int i = 0; i < stockRequired.Count(); i++)
                    {
                        var vStockID = stock.Where(x => x.id == int.Parse(stockRequired[i])).FirstOrDefault();
                        if(vStockID != null)
                        {
                            vStockID.deductPortionCount();
                            if (vStockID.portionCount == 0)
                                ifEnough = 0;
                        }
                    }
            return ifEnough;
        }
    }
}
