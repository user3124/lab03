using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class MatrixSizeMismatchException : Exception
    {
        public MatrixSizeMismatchException()
            : base("Размеры матриц не совпадают. Сложение невозможно.") { }

        public MatrixSizeMismatchException(string message)
            : base(message) { }

        public MatrixSizeMismatchException(string message, Exception inner)
            : base(message, inner) { }
    }
}
