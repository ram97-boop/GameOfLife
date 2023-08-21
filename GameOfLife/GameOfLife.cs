using System;

namespace GameOfLife
{
    public class World
    {
        public char[,] buildMatrix()
        {
            char[,] matrix = new char[32,32];
            int length = matrix.Length - 1;
            matrix[0,0] = '+';
            matrix[0,length] = '+';
            matrix[length,0] = '+';
            matrix[length,length] = '+';
            for (int i=0; i<=length; i++)
            {

            }
            return matrix;
        }
    }
}