using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    class Calculator
    {
        public static Matrix Abs(Matrix M)
        {
            for (int i = 0; i < M.NumberOfRows(); i++)
            {
                for (int j = 0; j < M.NumberOfColumns(); j++)
                {
                    M[i, j] = Math.Abs(M[i, j]);
                }
            }

            return M;
        }

        public static Matrix Power(Matrix M, int degree)
        {

            for (int i = 1; i < degree; i++)
            {
                M *= M;
            }

            return M;
        }

        public static Matrix Transpose(Matrix M)
        {
            Matrix MT = new Matrix(M.NumberOfColumns(), M.NumberOfRows());

            for (int i = 0; i < M.NumberOfColumns(); i++)
            {
                for (int j = 0; j < M.NumberOfRows(); j++)
                {
                    MT[i, j] = M[j, i];
                }
            }

            return MT;

        }

        public static Matrix Concatenate(Matrix M1, Matrix M2, int dimension)
        {
            if (dimension == 0 && M1.NumberOfColumns() == M2.NumberOfColumns())
            {
                Matrix M3 = new Matrix(M1.NumberOfRows() + M2.NumberOfRows(), M1.NumberOfColumns());


                for (int i = 0; i < M1.NumberOfRows(); i++)
                {
                    for (int j = 0; j < M1.NumberOfColumns(); j++)
                    {
                        M3[i, j] = M1[i, j];
                    }
                }

                for (int i = 0; i < M2.NumberOfRows(); i++)
                {
                    for (int j = 0; j < M2.NumberOfColumns(); j++)
                    {
                        M3[i + M1.NumberOfRows(), j] = M2[i, j];
                    }
                }

                return M3;
            }
            else if (dimension == 1)
            {
                Matrix M3 = new Matrix(M1.NumberOfRows(), M1.NumberOfColumns() + M2.NumberOfColumns());

                for (int i = 0; i < M1.NumberOfRows(); i++)
                {
                    for (int j = 0; j < M1.NumberOfColumns(); j++)
                    {
                        M3[i, j] = M1[i, j];
                    }
                }

                for (int i = 0; i < M2.NumberOfRows(); i++)
                {
                    for (int j = 0; j < M2.NumberOfColumns(); j++)
                    {
                        M3[i, j + M1.NumberOfColumns()] = M2[i, j];
                    }
                }

                return M3;
            }
            else
            {
                throw new System.ArgumentException("dimension(s) not valid");
            }
        }

        public static Matrix DeleteRow(Matrix M, int rowNum)
        {
            Matrix newM = new Matrix(M.NumberOfRows() - 1, M.NumberOfColumns());
            int skip = 0;

            for (int i = 0; i < newM.NumberOfRows(); i++)
            {
                if( i == rowNum)
                {
                    skip = 1;
                }

                for (int j = 0; j < newM.NumberOfColumns(); j++)
                {
                    newM[i, j] = M[i + skip, j];
                }
            }

            return newM;
        }

        public static Matrix DeleteColumn(Matrix M, int colNum)
        {
            Matrix newM = new Matrix(M.NumberOfRows(), M.NumberOfColumns() - 1);
            int skip = 0;

            for (int j = 0; j < newM.NumberOfColumns(); j++)
            {
                if (j == colNum)
                {
                    skip = 1;
                }

                for (int i = 0; i < newM.NumberOfRows(); i++)
                {
                    newM[i, j] = M[i, j + skip];
                }
            }

            return newM;
        }
       
        public static Matrix Replace(Matrix M, Matrix replacement, int rowIndex, int colIndex)
        {
            if(M.NumberOfColumns() - colIndex - replacement.NumberOfColumns() < 0 || M.NumberOfRows() - rowIndex - replacement.NumberOfRows() < 0)
            {
                throw new System.Exception("replacement matrix does not fit in current matrix");
            }
            else
            {
                Matrix newM = new Matrix(M.NumberOfRows(), M.NumberOfColumns());

                for (int i = 0; i < M.NumberOfRows(); i++)
                {
                    for (int j = 0; j < M.NumberOfColumns(); j++)
                    {
                        if(i >= rowIndex && j >= colIndex && i < rowIndex + replacement.NumberOfRows() && j < colIndex + replacement.NumberOfColumns())
                        {
                            newM[i, j] = replacement[i - rowIndex, j - colIndex]; 
                        }
                        else
                        {
                            newM[i, j] = M[i, j];
                        }
                    }
                }

                return newM;
            }
        }
        public static double Determinant(Matrix M)
        {
            if (M.NumberOfRows() != M.NumberOfColumns())
            {
                throw new System.ArrayTypeMismatchException("Matrix is not square");
            }
            else
            {
                double det = 0;

                if( M.NumberOfColumns() == 2)
                {
                    return (M[0, 0] * M[1, 1] - M[0, 1] * M[1, 0]);
                }
                else
                {
                    int sign = -1;
                    for (int i = 0; i < M.NumberOfColumns(); i++)
                    {
                        sign *= -1;
                        det += sign * (double) M[0,i] * Determinant(DeleteColumn(DeleteRow(M,0),i)); 
                    }
                    return det;
                }
            }
        }

        public static Matrix CofactorMatrix(Matrix M)
        {
            if(M.NumberOfColumns() != M.NumberOfRows())
            {
                throw new System.Exception("matrix must be sqaure");
            }
            else
            {
                Matrix cofactorMatrix = new Matrix(M.NumberOfRows(), M.NumberOfColumns());
                int sign = -1;

                for (int i = 0; i < cofactorMatrix.NumberOfRows(); i++)
                {
                    for (int j = 0; j < cofactorMatrix.NumberOfColumns(); j++)
                    {
                        sign *= -1;
                        cofactorMatrix[i, j] = sign * Determinant(DeleteColumn(DeleteRow(M, i), j));
                    }
                }

                return cofactorMatrix;
            }
        }

        public static Matrix Adjugate(Matrix M)
        {
            return Transpose(CofactorMatrix(M));
        }

        public static Matrix Inverse(Matrix M)
        {
            return 1/Determinant(M) * Adjugate(M);
        }

        public static Matrix REF(Matrix M)
        {
                for (int j = 0; j < M.NumberOfColumns() - 1; j++)
                {
                    for (int i = 0; i < M.NumberOfRows() - 1; i++)
                    {
                        i = i + j;
                        if (i < M.NumberOfRows() - 1)
                        {
                            Matrix newRow = M.SubMatrix(j, 0, j, M.NumberOfColumns() - 1) * (double)(M[i + 1, j] / M[j, j]) - M.SubMatrix(i + 1, 0, i + 1, M.NumberOfColumns() - 1);
                            M = Replace(M, newRow, i + 1, 0);
                        }
                    }
                }

                double scale = 1;
                
                for (int i = 0; i < M.NumberOfRows(); i++)
                {
                    for (int j = 0; j < M.NumberOfColumns(); j++)
                    {
                        if (i == j) 
                        {   
                            scale = (M[i,j] != 0) ? M[i, j] : 1; 
                        }

                        M[i, j] = M[i, j] / scale;
                    }
                }

                    return M;
        }
    }
}
