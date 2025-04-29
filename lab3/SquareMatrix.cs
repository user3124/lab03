using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class SquareMatrix : ICloneable, IComparable<SquareMatrix>
    {
        public int Size { get; }  // Размер матрицы
        public int[,] Matrix { get; }  // Двумерный массив, представляющий матрицу

        // Конструктор с заданным размером и диапазоном случайных чисел
        public SquareMatrix(int size, int minValue = 0, int maxValue = 10)
        {
            if (size <= 0)
                throw new ArgumentException("Размер матрицы должен быть положительным числом.");

            Size = size;
            Matrix = new int[Size, Size];
            RandomMatrix(minValue, maxValue);
        }

        // Сделал генератор чисел статическим, иначе "случайные" матрицы получаются одинаковыми
        private static readonly Random randomGenerator = new Random();

        private void RandomMatrix(int minValue, int maxValue)
        {
            for (int row = 0; row < Size; ++row)
            {
                for (int column = 0; column < Size; ++column)
                {
                    Matrix[row, column] = randomGenerator.Next(minValue, maxValue + 1);
                }
            }
        }

        public static SquareMatrix operator +(SquareMatrix matrixA, SquareMatrix matrixB)
        {
            if (matrixA == null || matrixB == null)
                throw new ArgumentNullException("Обе матрицы должны быть инициализированы.");

            if (matrixA.Size != matrixB.Size)
                throw new MatrixSizeMismatchException();

            SquareMatrix resultMatrix = new SquareMatrix(matrixA.Size, 0, 0);

            for (int row = 0; row < matrixA.Size; ++row)
            {
                for (int column = 0; column < matrixA.Size; ++column)
                {
                    resultMatrix.Matrix[row, column] = matrixA.Matrix[row, column] + matrixB.Matrix[row, column];
                }
            }

            return resultMatrix;
        }

        public static SquareMatrix operator *(SquareMatrix matrixA, SquareMatrix matrixB)
        {
            if (matrixA == null || matrixB == null)
                throw new ArgumentNullException("Обе матрицы должны быть инициализированы.");

            if (matrixA.Size != matrixB.Size)
                throw new MatrixSizeMismatchException("Нельзя перемножить матрицы разного размера.");

            int size = matrixA.Size;
            SquareMatrix resultMatrix = new SquareMatrix(size, 0, 0);

            for (int row = 0; row < size; ++row)
            {
                for (int column = 0; column < size; ++column)
                {
                    int sum = 0;
                    for (int index = 0; index < size; ++index)
                    {
                        sum += matrixA.Matrix[row, index] * matrixB.Matrix[index, column];
                    }
                    resultMatrix.Matrix[row, column] = sum;
                }
            }

            return resultMatrix;
        }

        public int GetSum()
        {
            int sum = 0;
            for (int row = 0; row < Size; ++row)
            {
                for (int column = 0; column < Size; ++column)
                {
                    sum += Matrix[row, column];
                }
            }
            return sum;
        }

        public static bool operator >(SquareMatrix matrixA, SquareMatrix matrixB)
        {
            if (matrixA == null || matrixB == null)
                throw new ArgumentNullException("Матрицы не должны быть null.");

            return matrixA.GetSum() > matrixB.GetSum();
        }

        // => matrixA.CompareTo(matrixB) < 0
        public static bool operator <(SquareMatrix matrixA, SquareMatrix matrixB)
        {
            if (matrixA == null || matrixB == null)
                throw new ArgumentNullException("Матрицы не должны быть null.");

            return matrixA.GetSum() < matrixB.GetSum();
        }

        // return matrixA.CompareTo(matrixB) >= 0;
        public static bool operator >=(SquareMatrix matrixA, SquareMatrix matrixB)
        {
            return !(matrixA < matrixB);
        }

        public static bool operator <=(SquareMatrix matrixA, SquareMatrix matrixB)
        {
            return !(matrixA > matrixB);
        }

        // return Object.Equals(matrixA, matrixB);
        public static bool operator ==(SquareMatrix matrixA, SquareMatrix matrixB)
        {
            if (ReferenceEquals(matrixA, matrixB))
                return true;

            if (matrixA is null || matrixB is null)
                return false;

            if (matrixA.Size != matrixB.Size)
                return false;

            for (int row = 0; row < matrixA.Size; ++row)
            {
                for (int column = 0; column < matrixA.Size; ++column)
                {
                    if (matrixA.Matrix[row, column] != matrixB.Matrix[row, column])
                        return false;
                }
            }

            return true;
        }

        // return !Object.Equals(matrixA, matrixB);
        public static bool operator !=(SquareMatrix matrixA, SquareMatrix matrixB)
        {
            return !(matrixA == matrixB);
        }

        // Метод для красивого вывода матрицы
        public override string ToString()
        {
            string result = "";
            for (int row = 0; row < Size; ++row)
            {
                for (int column = 0; column < Size; ++column)
                {
                    result += $"{Matrix[row, column],5}";
                }
                result += "\n";
            }
            return result;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + Size.GetHashCode();

            for (int row = 0; row < Size; ++row)
            {
                for (int column = 0; column < Size; ++column)
                {
                    hash = hash * 23 + Matrix[row, column].GetHashCode();
                }
            }

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj is SquareMatrix otherMatrix)
                return this == otherMatrix;

            return false;
        }

        public int CompareTo(SquareMatrix otherMatrix)
        {
            if (otherMatrix == null)
                return 1;

            return this.GetSum().CompareTo(otherMatrix.GetSum());
        }

        // Перегрузка оператора true
        public static bool operator true(SquareMatrix matrix)
        {
            if (matrix == null)
                return false;

            return matrix.GetSum() > 0;
        }

        // Перегрузка оператора false
        public static bool operator false(SquareMatrix matrix)
        {
            if (matrix == null)
                return true;

            return matrix.GetSum() <= 0;
        }
        public object ShallowClone()
        {
            // Поверхностное копирование, копируются не сами объекты, а ссылки на них
            // Eсли вложенный объект является ссылочным типом, то изменения в копии отразятся и в оригинале
            // Т.е. оба объекта будут ссылаться на одну и ту же область памяти
            return this.MemberwiseClone();
        }

        public object Clone()
        {
            // Глубокое копирование элементов, создаются новые массивы
            // Каждый объект будет содержать свой отдельный массив
            SquareMatrix clonedMatrix = new SquareMatrix(Size);

            for (int row = 0; row < Size; ++row)
            {
                for (int column = 0; column < Size; ++column)
                {
                    clonedMatrix.Matrix[row, column] = this.Matrix[row, column];
                }
            }

            return clonedMatrix;
        }

        public int Determinant()
        {
            if (Size == 1)
                return Matrix[0, 0];  // Для матрицы 1x1 детерминант равен единственному элементу

            int determinant = 0;

            for (int column = 0; column < Size; ++column)
            {
                determinant += (int)Math.Pow(-1, column) * Matrix[0, column] * Minor(0, column).Determinant();
            }

            return determinant;
        }

        private SquareMatrix Minor(int row, int column)
        {
            SquareMatrix minorMatrix = new SquareMatrix(Size - 1);

            int minorRow = 0;
            for (int i = 0; i < Size; ++i)
            {
                if (i == row) continue;
                int minorColumn = 0;

                for (int j = 0; j < Size; ++j)
                {
                    if (j == column) continue;
                    minorMatrix.Matrix[minorRow, minorColumn] = Matrix[i, j];
                    minorColumn++;
                }

                minorRow++;
            }

            return minorMatrix;
        }

        // Метод для нахождения обратной матрицы
        public SquareMatrix Inverse()
        {
            int det = Determinant();
            if (det == 0)
                throw new InvalidOperationException("Матрица не имеет обратной матрицы, так как её детерминант равен нулю.");

            SquareMatrix adjoint = Adjoint();
            SquareMatrix inverseMatrix = new SquareMatrix(Size);

            for (int row = 0; row < Size; ++row)
            {
                for (int column = 0; column < Size; ++column)
                {
                    inverseMatrix.Matrix[row, column] = adjoint.Matrix[row, column] / det;
                }
            }

            return inverseMatrix;
        }

        private SquareMatrix Adjoint()
        {
            SquareMatrix adjointMatrix = new SquareMatrix(Size);

            for (int row = 0; row < Size; ++row)
            {
                for (int column = 0; column < Size; ++column)
                {
                    adjointMatrix.Matrix[row, column] = (int)Math.Pow(-1, row + column) * Minor(row, column).Determinant();
                }
            }

            return adjointMatrix;
        }

        public static implicit operator string(SquareMatrix matrix)
        {
            if (matrix == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < matrix.Size; ++row)
            {
                for (int column = 0; column < matrix.Size; ++column)
                {
                    sb.Append($"{matrix.Matrix[row, column],5}");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public static explicit operator SquareMatrix(string str)
        {
            var rows = str.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int size = rows.Length;
            SquareMatrix matrix = new SquareMatrix(size);

            for (int index = 0; index < size; ++index)
            {
                var columns = rows[index].Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int jndex = 0; jndex < size; ++jndex)
                {
                    matrix.Matrix[index, jndex] = int.Parse(columns[jndex]);
                }
            }

            return matrix;
        }
    }
}
