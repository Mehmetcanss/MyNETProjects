using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberOfPaths
{


    /// <summary>
    /// the problem ist too big we need to work parallel
    /// </summary>
    public class PathCalculator

    {
        private int _width;
        private int _height;
        private long[,] _matrix;

        public PathCalculator(int rows, int columns)
        {
            this._width = columns;
            this._height = rows;
            this._matrix = new long[rows,columns];

            InitializeMatrix();
        }

        private void InitializeMatrix()
        {

            for(int i = 0; i< _height; i++)
            {
                initRow(i);
            }
        }

        private void initRow(int row)
        {
            if(row == 0)
            {
                for (int i = 0; i < _width; i++) _matrix[row, i] = 1;
            } else
            {
                for(int i = 0; i< _width; i++)
                {
                    if (i == 0) _matrix[row, i] = 1;
                    else
                    {
                        _matrix[row, i] = _matrix[row - 1, i] + _matrix[row, i - 1];
                    }
                }
            }
        }

        public long GetNumberOfPathsToPoint(int row, int column)
        {
            return _matrix[row, column];
        }



        /// <summary>
        /// recursive calculating takes extremely long
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public Task<long>  CalculateNumberOfPaths(int row, int column)

        {
            if (row == 0)
                return Task.FromResult<long>(1);

            if (row == 1)
                return Task.FromResult<long>(column + 1);
            if (row == 2)
            {
                long sum = 0;
                for (int i = column + 1; i > 0; i--) sum += i;
                return Task.FromResult<long>(sum);
            }
            long s = 0;
            for (int j = 1; j <= row; j++)
            {
                for (int i = column; i >= 0;)
                {
                    Task<long> result= CalculateNumberOfPaths(row - j, --i);
                    long res = result.Result;
                    s += res;
                };

            }
            return Task.FromResult<long>(s);

        }

   

    }
}
