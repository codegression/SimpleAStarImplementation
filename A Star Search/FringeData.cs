using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Star_Search
{
    public class FringeData
    {
        public string Path { set; get; }
        public double Cost { set; get; }

        public bool Popped { set; get; }

        public FringeData(string path, double cost)
        {
            Path = path;
            Cost = cost;
        }
    }
}
