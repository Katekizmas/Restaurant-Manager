using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Manager.Duomenys
{
    public class Stock
    {
        public int id { get; set; }
        public string name { get; set; }
        public int portionCount { get; set; }
        public string unit { get; set; }
        public double portionSize { get; set; }
        public Stock() { }
        public Stock(Stock obj)
        {
            this.id = obj.id;
            this.name = obj.name;
            this.portionCount = obj.portionCount;
            this.unit = obj.unit;
            this.portionSize = obj.portionSize;
        }
        public Stock(int id = -1, string name = "", int portionCount = 0, string unit = "", double portionSize = 0.0)
        {
            this.id = id;
            this.name = name;
            this.portionCount = portionCount;
            this.unit = unit;
            this.portionSize = portionSize;
        }
        public override string ToString()
        {
            return string.Format("|{0, -5} | {1, 15} | {2, 15} | {3, 8} | {4, 13:f}|", id, name, portionCount, unit, portionSize);
        }
        public void deductPortionCount()
        {
            portionCount--;
        }
    }
}
