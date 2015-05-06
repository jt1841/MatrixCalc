using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix zero = new Matrix(3,4);
            Matrix I = new Matrix(5, 5, 'I');
            Matrix myMatrix = new Matrix(new double[,] {{1,2,3,3.5},{4,5,6,6.5},{5,4,2,1.0}});
            Matrix myColumn = new Matrix(new double[,] { { 1 }, { -1 }, { 1 }, {-1} });
            Matrix myMatrix2 = new Matrix(new double[,] { { -1, -2 }, { -3, -4 } });

            myMatrix.PrintToConsole();
            Calculator.REF(myMatrix).PrintToConsole();

        }






    }
}
