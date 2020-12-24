using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Manager.Duomenys
{
    public class Menu
    {
        public int id { get; set; }
        public string name { get; set; }
        public string products { get; set; }
        public Menu() { }
        public Menu(int id = -1, string name = "", string products = "")
        {
            this.id = id;
            this.name = name;
            this.products = products;
        }
        public override string ToString()
        {
            return string.Format("|{0, -5} | {1, 30} | {2, 15}|", id, name, products);
        }
    }
}
