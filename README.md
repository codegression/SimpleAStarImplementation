A simple A* search implementation in C#

In Form1.cs, double[,] Cost is the cost matrix where Double.PositiveInfinity refers to a place that cannot be traversed and char[,] States stores the names of places.
Just run the program and the solution will be displayed.

        double[,] Cost = new double[,] { { 1, 3, 3, 3, 3, 1 },
                                         { Double.PositiveInfinity, 1, Double.PositiveInfinity, Double.PositiveInfinity, Double.PositiveInfinity, 3 },
                                         { Double.PositiveInfinity, 1, 3, 3, Double.PositiveInfinity, 1 },
                                         {0, 1, Double.PositiveInfinity, 1, 1, 1}};

        char[,] States = new char[,] { { 'a', 'b', 'c', 'd', 'e', 'G' },
                                       { '-', 'h', '-', '-', '-', 'i' },
                                       { '-', 'j', 'k', 'l', '-', 'm' },
                                       { 'S', 'n', '-', 'p', 'q', 'r' }};
