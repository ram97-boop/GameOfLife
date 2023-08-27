﻿using System;

namespace GameOfLife
{
    public class World
    {
        private char[,] worldMatrix = new char[32,32];

        // Constructor with lambda expression.w
        public World() => buildMatrix(worldMatrix);

        private char[,] buildMatrix(char[,] matrix)
        {
            int length = matrix.GetLength(0) - 1;

            matrix[0,0] = '+';
            matrix[0,length] = '+';
            matrix[length,0] = '+';
            matrix[length,length] = '+';

            for (int i=0; i<=length; i++)
            {
                for (int j=0; j<=length; j++)
                {
                    if ((i==0 || i==length) && j!=0 && j!=length)
                    {
                        matrix[i,j] = '-';
                    }
                    else if ((j==0 || j==length) && i!=0 && i!=length)
                    {
                        matrix[i,j] = '|';
                    }
                    else if (i!=0 && i!=length && j!=0 && j!=length)
                    {
                        matrix[i,j] = ' ';
                    }
                }
            }
            return matrix;
        }

        public char[,] getMatrix()
        {
            return this.worldMatrix;
        }

        public static void Main(string[] args)
        {
            World world = new World();
            int length = world.getMatrix().GetLength(0) - 1;

            for (int i=0; i<=length; i++)
            {
                for (int j=0; j<=length; j++)
                {
                    Console.Write(world.getMatrix()[i,j]);
                }
                
                Console.Write('\n');
            }
        }
    }
}