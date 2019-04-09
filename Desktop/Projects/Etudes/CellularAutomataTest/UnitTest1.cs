using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace CellularAutomataTest
{
    class Game
    {
        private int width;
        private int height;
        public Game(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public bool[,] NextGen(bool[,] mat)
        {
            bool[,] newMat = new bool[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int count = Neighbours(mat, i, j);
                    if (count > 3 || count < 2) newMat[i, j] = false;
                    else if (count == 3) newMat[i, j] = true;
                    else if (count == 2 && mat[i, j]) newMat[i, j] = true;
                }
            }
            return newMat;
        }

        public int Neighbours(bool[,] mat, int i, int j)
        {
            int count = 0;
            if (i - 1 >= 0 && j - 1 >= 0 && mat[i - 1, j - 1]) count++;
            if (i - 1 >= 0 && mat[i - 1, j]) count++;
            if (i - 1 >= 0 && j + 1 < width && mat[i - 1, j + 1]) count++;
            if (j - 1 >= 0 && mat[i, j - 1]) count++;
            if (j + 1 < width && mat[i, j + 1]) count++;
            if (i + 1 < height && j - 1 >= 0 && mat[i + 1, j - 1]) count++;
            if (i + 1 < height && mat[i + 1, j]) count++;
            if (i + 1 < height && j + 1 < width && mat[i + 1, j + 1]) count++;

            return count;
        }
    }

    [TestClass]
    public class GameOfLifeTest
    {
        [TestMethod]
        public void Generation_1_Test()
        {
            bool[,] mat = new bool[3, 3];
            mat[1, 1] = mat[2, 2] = true;
            Game game = new Game(3, 3);
            bool[,] answer = new bool[3, 3];
            answer[1, 2] = answer[2, 1] = true;
            bool[,] next = game.NextGen(mat);
            Assert.IsTrue(IsEqual(answer, next));
        }

        [TestMethod]
        public void Generation_2_Test()
        {
            bool[,] mat = new bool[3, 3];
            mat[1, 0] = mat[1, 1] = mat[2, 1] = mat[2, 2] = true;
            Game game = new Game(3, 3);
            bool[,] answer = new bool[3, 3];
            answer[1, 0] = answer[1, 1] = 
            answer[2, 1] = answer[2, 2] = 
            answer[2, 0] = answer[1, 2] = true;
            bool[,] next = game.NextGen(mat);
            Assert.IsTrue(IsEqual(answer, next));
        }

        [TestMethod]
        public void Generation_3_Test()
        {
            
        }

        [TestMethod]
        public void Generation_4_Test()
        {
            
        }

        public bool IsEqual(bool[,] mat1, bool[,] mat2)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (mat1[i, j] != mat2[i, j]) return false;
                }
            }

            return true;
        }
    }
}
