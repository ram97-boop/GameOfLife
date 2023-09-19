using System;
using System.Runtime.CompilerServices;

namespace GameOfLife
{
    public class World
    {
        private char liveCellIndicator = '\u2588';
        private int rows = 32;
        private int cols = 32;
        private char[,] worldMatrix;

        public World()
        {
            worldMatrix = new char[rows, cols];
            buildMatrix(worldMatrix);
        }

        public World(char[,] matrix)
        {
            this.rows = matrix.GetLength(0);
            this.cols = matrix.GetLength(1);
            this.worldMatrix = matrix;
        }

        public World(int rows, int cols)
        {
            this.rows = rows + 2;
            this.cols = cols + 2;
            this.worldMatrix = new char[this.rows, this.cols];
            buildMatrix(worldMatrix); 
        }

        public void spreadRandomLivesInWorld()
        {
            Random rand = new Random();
            for (int i=1; i<this.worldMatrix.GetLength(0); i++)
            {
                for (int j=1; j<this.worldMatrix.GetLength(1); j++)
                {
                    if (rand.Next(0, 2) == 1)
                    {
                        this.worldMatrix[i,j] = this.liveCellIndicator;
                    }
                }
            }
        }

        public void nextGeneration()
        {
            char[,] newWorldMatrix = buildMatrix(new char[this.worldMatrix.GetLength(0), this.worldMatrix.GetLength(1)]);
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
            this.worldMatrix[1,2] = this.liveCellIndicator;
            this.worldMatrix[2,3] = this.liveCellIndicator;
            this.worldMatrix[3,1] = this.liveCellIndicator;
            this.worldMatrix[3,2] = this.liveCellIndicator;
            this.worldMatrix[3,3] = this.liveCellIndicator;
        }

        private char[,] buildMatrix(char[,] matrix)
        {
            int lastRowIndex = matrix.GetLength(0) - 1;
            int lastColIndex = matrix.GetLength(1) - 1;

            matrix[0,0] = '+';
            matrix[0,lastColIndex] = '+';
            matrix[lastRowIndex,0] = '+';
            matrix[lastRowIndex,lastColIndex] = '+';

            for (int i=0; i<=lastRowIndex; i++)
            {
                for (int j=0; j<=lastColIndex; j++)
                {
                    if ((i==0 || i==lastRowIndex) && j!=0 && j!=lastColIndex)
                    {
                        matrix[i,j] = '-';
                    }
                    else if ((j==0 || j==lastColIndex) && i!=0 && i!=lastRowIndex)
                    {
                        matrix[i,j] = '|';
                    }
                    else if (i!=0 && i!=lastRowIndex && j!=0 && j!=lastColIndex)
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
                // modulo and one-liner if-statements are for making the world cyclical.
                int neighbourX = (x + relativeNeighbourCoords[i,0]) % (this.rows - 2);
                int neighbourY = (y + relativeNeighbourCoords[i,1]) % (this.cols - 2);
                neighbourX = neighbourX != 0 ? neighbourX : (this.rows - 2);
                neighbourY = neighbourY != 0 ? neighbourY : (this.cols - 2);
                
                char neighbour = this.worldMatrix[neighbourX, neighbourY];

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
            World world = new World(10,10);
            // world.spreadRandomLivesInWorld();
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