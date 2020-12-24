using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Manager.Duomenys
{
    public class Order
    {
        public int id { get; set; }
        public string dateTime { get; set; }
        public string menuItems { get; set; }
        public Order() 
        {
            this.dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public Order(int id = -1, string dateTime = "", string menuItems = "")
        {
            this.id = id;
            this.dateTime = dateTime;
            this.menuItems = menuItems;
        }
        public Order(int id = -1, string menuItems = "")
        {
            this.id = id;
            this.dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.menuItems = menuItems;
        }
        public override string ToString()
        {
            return string.Format("|{0, -5} | {1, 20} | {2, 20}|", id, dateTime, menuItems);
        }
    }
}
