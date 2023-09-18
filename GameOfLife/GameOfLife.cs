﻿using System;
using System.Runtime.CompilerServices;

namespace GameOfLife
{
    public class World
    {
        private char liveCellIndicator = '\u2588';
        private char[,] worldMatrix = new char[32,32];

        // Constructor with lambda expression.w
        public World() => buildMatrix(worldMatrix);

        public World(char[,] matrix)
        {
            this.worldMatrix = matrix;
        }

        public void nextGeneration()
        {
            char[,] newWorldMatrix = buildMatrix(new char[32,32]);
            int length = this.worldMatrix.GetLength(0) - 1;

            for (int i=1; i<length; i++)
            {
                for (int j=1; j<length; j++)
                {
                    char currentCell = this.worldMatrix[i,j];
                    int liveNeighbours = getLiveNeighbours(i, j);
                    if
                    (
                        currentCell == this.liveCellIndicator
                        && (liveNeighbours<2 || liveNeighbours>3)
                    )
                    {
                        updateNewWorldCell(newWorldMatrix, i, j, ' ');
                    }

                    else if
                    (
                        currentCell == ' '
                        && liveNeighbours == 3
                    )
                    {
                        updateNewWorldCell(newWorldMatrix, i, j, this.liveCellIndicator);
                    }

                    else
                    {
                        updateNewWorldCell(newWorldMatrix, i, j, currentCell);
                    }
                }
            }

            this.worldMatrix = newWorldMatrix;
        }


        public char[,] getMatrix()
        {
            return this.worldMatrix;
        }

        public void printWorld()
        {
            int length = this.worldMatrix.GetLength(0) - 1;

            for (int i=0; i<=length; i++)
            {
                for (int j=0; j<=length; j++)
                {
                    Console.Write(this.worldMatrix[i,j]);
                }
                
                Console.Write('\n');
            }

        }

        public void createGlider()
        {
            this.worldMatrix[4,4] = this.liveCellIndicator;
            this.worldMatrix[5,5] = this.liveCellIndicator;
            this.worldMatrix[6,3] = this.liveCellIndicator;
            this.worldMatrix[6,4] = this.liveCellIndicator;
            this.worldMatrix[6,5] = this.liveCellIndicator;
        }

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

        private int getLiveNeighbours(int x, int y)
        {
            int[,] relativeNeighbourCoords =
            {
                {-1,-1},{-1,0},{-1,1},{0,-1},{0,1},{1,-1},{1,0},{1,1}
            };

            int liveNeighbours = 0;
            for (int i=0; i<relativeNeighbourCoords.GetLength(0); i++)
            {
                int deltaX = x + relativeNeighbourCoords[i,0];
                int deltaY = y + relativeNeighbourCoords[i,1];
                char neighbour = this.worldMatrix[deltaX, deltaY];

                if (neighbour == this.liveCellIndicator)
                {
                    liveNeighbours++;
                }
            }

            return liveNeighbours;
        }
        
        private void updateNewWorldCell(char[,] newWorldMatrix, int x, int y, char status)
        {
            newWorldMatrix[x,y] = status;
        }

        public static void Main(string[] args)
        {
            World world = new World();
            world.createGlider();

            Console.Clear();
            int positionX = Console.CursorLeft;
            int positionY = Console.CursorTop;

            for (int i=0; i<1000; i++)
            {
                Console.SetCursorPosition(positionX, positionY);
                world.printWorld();
                world.nextGeneration();
                Thread.Sleep(80);
            }
        }
    }
}