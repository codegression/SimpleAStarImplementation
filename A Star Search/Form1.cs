using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A_Star_Search
{
    public partial class Form1 : Form
    {

        double[,] Cost = new double[,] { { 1, 3, 3, 3, 3, 1 },
                                         { Double.PositiveInfinity, 1, Double.PositiveInfinity, Double.PositiveInfinity, Double.PositiveInfinity, 3 },
                                         { Double.PositiveInfinity, 1, 3, 3, Double.PositiveInfinity, 1 },
                                         {0, 1, Double.PositiveInfinity, 1, 1, 1}};

        char[,] States = new char[,] { { 'a', 'b', 'c', 'd', 'e', 'G' },
                                       { '-', 'h', '-', '-', '-', 'i' },
                                       { '-', 'j', 'k', 'l', '-', 'm' },
                                       { 'S', 'n', '-', 'p', 'q', 'r' }};


      

        List<FringeData> fringe = new List<FringeData>();
        public Form1()
        {
            InitializeComponent();
            fringe.Add(new A_Star_Search.FringeData("S", GetCostPath("S")));

            while (true)
            {
                FringeData item = fringe.Where(a => a.Popped == false).OrderBy(x => x.Cost).First();
                item.Popped = true;

                if (item.Path.EndsWith("G"))
                {
                    MessageBox.Show("Solution = " + item.Path);
                    break;
                }

                char[] children = GetChildren(item.Path);

                foreach (char c in children)
                {
                    string newpath = item.Path + c;
                    FringeData child = new FringeData(newpath, GetCostPath(newpath));
                    fringe.Add(child);
                }
            }

            foreach (FringeData item in fringe)
            {
                textBox1.Text += item.Path + " (f+g = " + Math.Round(item.Cost, 2).ToString() + ") ";
                if (item.Popped)
                {
                    textBox1.Text += " (popped)";
                }
                textBox1.Text += Environment.NewLine;
            }
        }


        private char[] GetChildren(string path)
        {
            List<char> children = new List<char>();

            char Last_state = path[path.Length - 1];
            char Previous_state = ' ';

            if (path.Length >= 2)
            {
                Previous_state = path[path.Length - 2];
            }

            Tuple<int, int> xy = GetXY(Last_state);
            int x = xy.Item1;
            int y = xy.Item2;

            if (x>=1)
            {
                if (!double.IsPositiveInfinity(Cost[x - 1, y]))
                {
                    char c = States[x - 1, y];
                    if (Previous_state != c)
                    {
                        children.Add(c);
                    }                 
                }
            }
            if (y >= 1)
            {
                if (!double.IsPositiveInfinity(Cost[x, y-1]))
                {
                    char c = States[x, y - 1];
                    if (Previous_state != c)
                    {
                        children.Add(c);
                    }
                }
            }

            if (x < States.GetLength(0) - 1)
            {
                if (!double.IsPositiveInfinity(Cost[x + 1, y]))
                {
                    char c = States[x + 1, y];
                    if (Previous_state != c)
                    {
                        children.Add(c);
                    }
                }
            }
            if (y < States.GetLength(1) - 1)
            {
                if (!double.IsPositiveInfinity(Cost[x, y+1]))
                {
                    char c = States[x, y+1];
                    if (Previous_state != c)
                    {
                        children.Add(c);
                    }
                }
            }

            char[] children_array = children.ToArray();
            Array.Sort(children_array);
            return children_array;
        }


        private double GetCostPath(string path)
        {
            double total_cost = 0;
            char last_state = ' ';

            foreach (char c in path)
            {
                Tuple<int, int> xy = GetXY(c);
                int x = xy.Item1;
                int y = xy.Item2;
                total_cost += Cost[x, y];
                last_state = c;
            }

            Tuple<int, int> xy_last = GetXY(last_state);

            return total_cost; //+ Heuristic(xy_last.Item1, xy_last.Item2);
        }

        Tuple<int, int> GetXY(char x)
        {
            for (int i = 0; i < States.GetLength(0); i++)
            {
                for (int j=0; j<States.GetLength(1); j++)
                {
                    if (States[i,j]==x)
                    {
                        return new Tuple<int, int>(i, j);
                    }
                }
            }
            return new Tuple<int, int>(-1, -1);
        }
        double Heuristic(int x, int y)
        {
            Tuple<int, int> xy = GetXY('G');
            int goal_x = xy.Item1;
            int goal_y = xy.Item2;

            double x_distance = Math.Abs(goal_x - x);
            double y_distance = Math.Abs(goal_y - y);

            //return Math.Sqrt(x_distance * x_distance + y_distance * y_distance);
            return x_distance + y_distance;
        }
    }
}
