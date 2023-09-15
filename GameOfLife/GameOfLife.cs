using System;

namespace GameOfLife
{
    public class World
    {
        private char[,] worldMatrix = new char[32,32];

        // Constructor with lambda expression.w
        public World() => buildMatrix(worldMatrix);

        public World(char[,] matrix)
        {
            this.worldMatrix = matrix;
        }

        public void nextGeneration()
        {
            int length = this.worldMatrix.GetLength(0) - 1;

            for (int i=1; i<length; i++)
            {
                for (int j=1; j<length; j++)
                {
                    int liveNeighbours = getLiveNeighbours(i, j);
                }
            }
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
            this.worldMatrix[4,4] = '\u2588';
            this.worldMatrix[5,5] = '\u2588';
            this.worldMatrix[6,3] = '\u2588';
            this.worldMatrix[6,4] = '\u2588';
            this.worldMatrix[6,5] = '\u2588';
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

        private int[,] getLiveNeighbours(int x, int j)
        {
            
        }

        public static void Main(string[] args)
        {
            World world = new World();
            world.createGlider();
            Console.Clear();
            int positionX = Console.CursorLeft;
            int positionY = Console.CursorTop;
            Console.SetCursorPosition(positionX, positionY);
            world.printWorld();
            Console.SetCursorPosition(positionX, positionY);
            world.printWorld();
            // Console.WriteLine($"Cursor position is: {positionX}, {positionY}.");
        }
    }
}