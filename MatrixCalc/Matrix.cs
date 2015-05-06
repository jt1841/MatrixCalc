using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    class Matrix
    {
        public double[,] value
        {
            get;
            private set;
        }
        
        public Matrix(int rows, int cols)
        {
            value = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for(int j = 0; j < cols; j++)
                {
                    value[i, j] = 0.0;
                }
            }
        }

        public Matrix(int rows,int cols, char option)
        {
           
            value = new double[rows, cols];

            if (option.Equals('I') || option.Equals('i'))
            {
                //Initialize array as the identity
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (i == j)
                        {
                            value[i, j] = 1.0;
                        }
                        else
                        {
                            value[i, j] = 0.0;
                        }
                    }
                }
            }
        }

        public Matrix(double[,] M)
        {
            this.value = M;
        }

        public int NumberOfRows()
        {
            return this.value.GetLength(0);
        }

        public int NumberOfColumns()
        {
           return this.value.GetLength(1);
        }

        public void PrintToConsole()
        {

            StringBuilder printedMatrix = new StringBuilder();

            printedMatrix.Append("\n");

            //Determine longest size of the double in each column. Used for pretty print
            int[] columnPadding = new int[value.GetLength(1)];
            for (int i = 0; i < value.GetLength(0); i++)
            {
                for(int j = 0; j < value.GetLength(1); j++)
                {
                    if (value[i,j].ToString().Length > columnPadding[j])
                    {
                        columnPadding[j] = value[i, j].ToString().Length;
                    }
                }
            }



            for (int i = 0; i < value.GetLength(0); i++)
            {
                for(int j = 0; j < value.GetLength(1); j++)
                {
                    if(j == 0)
                    {
                        printedMatrix.Append("| ");
                    }

                    printedMatrix.Append(value[i, j]);

                    for (int k = 0; k <= (columnPadding[j] - value[i,j].ToString().Length); k++)
                    {
                        printedMatrix.Append(" ");
                    }
                    
                    if(j == value.GetLength(1) - 1)
                    {
                        printedMatrix.Append("|");
                        printedMatrix.Append("\n");
                    }
                }                
            }

            Console.WriteLine(printedMatrix.ToString());
        }

        public static Matrix operator *(Matrix M1, Matrix M2)
        {
            Matrix M3 = new Matrix(M1.value.GetLength(0), M2.value.GetLength(1));

            if (M1.NumberOfColumns() != M2.NumberOfRows())
            {
                throw new System.ArrayTypeMismatchException("Invalid matrix multiplication A.getCols() must equal B.getRows()");
            }
            else
            {
                for (int i = 0; i < M3.value.GetLength(0); i++)
                {
                    for (int j = 0; j < M3.value.GetLength(1); j++)
                    {

                        double indexValue = 0.0;

                        for (int k = 0; k < M1.value.GetLength(1); k++)
                        {
                            indexValue += M1.value[i, k] * M2.value[k, j];
                        }

                        M3.value[i, j] = indexValue;
                    }
                }

                return M3;
            }

        }

        public static Matrix operator *(Matrix M, double k)
        {
            for (int i = 0; i < M.NumberOfRows(); i++)
            {
                for (int j = 0; j < M.NumberOfColumns(); j++)
                {
                    M.value[i, j] = M.value[i, j] * k;
                }
            }

            return M;
        }

        public static Matrix operator *(double k, Matrix M)
        {
            for (int i = 0; i < M.NumberOfRows(); i++)
            {
                for (int j = 0; j < M.NumberOfColumns(); j++)
                {
                    M.value[i, j] = M.value[i, j] * k;
                }
            }

            return M;
        }

        public static Matrix operator /(Matrix M, double k)
        {
            for (int i = 0; i < M.NumberOfRows(); i++)
            {
                for (int j = 0; j < M.NumberOfColumns(); j++)
                {
                    M.value[i, j] = M.value[i, j] / k;
                }
            }

            return M;
        }

        public static Matrix operator /(double k, Matrix M)
        {
            for (int i = 0; i < M.NumberOfRows(); i++)
            {
                for (int j = 0; j < M.NumberOfColumns(); j++)
                {
                    M.value[i, j] = M.value[i, j] / k;
                }
            }

            return M;
        }

        public static Matrix operator +(Matrix M1, Matrix M2)
        {
            if (M1.NumberOfColumns() != M2.NumberOfColumns() || M1.NumberOfRows() != M2.NumberOfRows())
            {
                throw new System.ArgumentNullException("rows and cols must be equal for matrix addition");
            }
            else
            {
                Matrix M3 = new Matrix(M1.NumberOfRows(), M1.NumberOfColumns());

                for (int i = 0; i < M1.NumberOfRows(); i++)
                {
                    for (int j = 0; j < M1.NumberOfColumns(); j++)
                    {
                        M3.value[i, j] = M1.value[i, j] + M2.value[i, j];
                    }
                }

                return M3;

            }
        }

        public static Matrix operator -(Matrix M1, Matrix M2)
        {
            if (M1.NumberOfColumns() != M2.NumberOfColumns() || M1.NumberOfRows() != M2.NumberOfRows())
            {
                throw new System.ArgumentNullException("rows and cols must be equal for matrix addition");
            }
            else
            {
                Matrix M3 = new Matrix(M1.NumberOfRows(), M1.NumberOfColumns());

                for (int i = 0; i < M1.NumberOfRows(); i++)
                {
                    for (int j = 0; j < M1.NumberOfColumns(); j++)
                    {
                        M3.value[i, j] = M1.value[i, j] - M2.value[i, j];
                    }
                }

                return M3;

            }
        }

        public static Matrix operator -(Matrix M1)
        {

                Matrix M3 = new Matrix(M1.NumberOfRows(), M1.NumberOfColumns());

                for (int i = 0; i < M1.NumberOfRows(); i++)
                {
                    for (int j = 0; j < M1.NumberOfColumns(); j++)
                    {
                        M3.value[i, j] = -M1.value[i, j];
                    }
                }

                return M3;

        }

        public double this[int i, int j]
        {
            get
            {
                return this.value[i, j];
            }
            set
            {
                this.value[i, j] = value;
            }
        }

        public Matrix SubMatrix(int rowStart, int colStart)
        {

            if (rowStart > this.NumberOfRows() || colStart > this.NumberOfColumns())
            {
                throw new System.ArgumentOutOfRangeException("Index out of bounds");
            }
            else
            {
                Matrix subMatrix = new Matrix(this.NumberOfRows() - rowStart, this.NumberOfColumns() - colStart);

                for (int i = 0; i < this.NumberOfRows() - rowStart; i++)
                {
                    for (int j = 0; j < this.NumberOfColumns() - colStart; j++)
                    {
                        subMatrix[i, j] = this[rowStart + i, colStart + j];
                    }
                }

                return subMatrix;
            }
        }

        public Matrix SubMatrix(int rowStart, int colStart, int rowEnd, int colEnd)
        {
            if (rowStart > this.NumberOfRows() || colStart > this.NumberOfColumns() || rowEnd > this.NumberOfRows() || colEnd > this.NumberOfColumns())
            {
                throw new System.ArgumentOutOfRangeException("Index out of bounds");
            }
            else
            {
                Matrix subMatrix = new Matrix((rowEnd - rowStart) + 1, (colEnd - colStart) + 1);

                for (int i = 0; i < subMatrix.NumberOfRows(); i++)
                {
                    for (int j = 0; j < subMatrix.NumberOfColumns(); j++)
                    {
                        subMatrix[i, j] = this[rowStart + i, colStart + j];
                    }
                }

                return subMatrix;
            }
        }

        public static explicit operator double(Matrix M)
        {
            if (M.NumberOfColumns() != 1 || M.NumberOfRows() != 1)
            {
                throw new System.Exception("Cannot convert non scalar Matrix value to type double");
            }
            else
            {
                return M.value[0, 0];
            }
        }

        public static explicit operator double[,](Matrix M)
        {
                return M.value;
        }
    }

}
