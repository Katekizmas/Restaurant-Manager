using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Manager.Duomenys
{
    public class Restaurant
    {
        public List<Stock> stocks;
        public List<Menu> menus;
        public List<Order> orders;
        public Restaurant()
        {
            stocks = new List<Stock>();
            menus = new List<Menu>();
            orders = new List<Order>();
        }
    }
}
