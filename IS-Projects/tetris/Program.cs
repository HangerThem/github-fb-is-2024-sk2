using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace tul_001
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(40, 40);
            Console.CursorVisible = false;

            TetrisGame game = new();
            game.Start();
        }
    }

    class TetrisGame
    {
        private const int Width = 10;
        private const int Height = 20;
        private readonly int[,] grid = new int[Height, Width];
        private readonly List<int[,]> shapes =
        [
            new int[,] { { 1, 1, 1, 1 } }, // I
            new int[,] { { 1, 1 }, { 1, 1 } }, // O
            new int[,] { { 0, 1, 0 }, { 1, 1, 1 } }, // T
            new int[,] { { 1, 1, 0 }, { 0, 1, 1 } }, // S
            new int[,] { { 0, 1, 1 }, { 1, 1, 0 } }, // Z
            new int[,] { { 1, 0, 0 }, { 1, 1, 1 } }, // L
            new int[,] { { 0, 0, 1 }, { 1, 1, 1 } } // J
        ];
        private List<int> terminosBag = [];
        private int currentShapeIndex;
        private int heldShapeIndex = -1;
        private bool canHold = true;
        private int currentX, currentY;
        private readonly Random random = new();
        private readonly ConcurrentQueue<ConsoleKey> keyQueue = new();
        private int score = 0;
        private int level = 1;
        private int linesCleared = 0;
        private int combo = 0;
        private bool backToBack = false;

        public void Start()
        {
            Thread keyListenerThread = new(KeyListener);
            keyListenerThread.Start();

            while (true)
            {
                if (terminosBag.Count == 0)
                {
                    FillTerminosBag();
                }
                SpawnShape();
                while (MoveShapeDown())
                {
                    ProcessKeyPresses();
                    Draw();
                    Thread.Sleep(500);
                }
                MergeShape();
                int lines = ClearLines();
                UpdateScore(lines);
                canHold = true;
                if (IsGameOver())
                {
                    Console.Clear();
                    Console.WriteLine("Game Over!");
                    break;
                }
            }
        }

        private void KeyListener()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    keyQueue.Enqueue(keyInfo.Key);
                }
            }
        }

        private void ProcessKeyPresses()
        {
            while (keyQueue.TryDequeue(out ConsoleKey key))
            {
                if (key == ConsoleKey.LeftArrow)
                {
                    MoveShapeLeft();
                }
                else if (key == ConsoleKey.RightArrow)
                {
                    MoveShapeRight();
                }
                else if (key == ConsoleKey.Enter)
                {
                    DropShape();
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    RotateShape();
                }
                else if (key == ConsoleKey.C)
                {
                    HoldShape();
                }
            }
        }

        private void SpawnShape()
        {
            currentShapeIndex = terminosBag[0];
            terminosBag.RemoveAt(0);
            currentX = Width / 2 - shapes[currentShapeIndex].GetLength(1) / 2;
            currentY = 0;
        }

        private bool MoveShapeDown()
        {
            if (CanMove(currentX, currentY + 1))
            {
                currentY++;
                return true;
            }
            return false;
        }

        private void MoveShapeLeft()
        {
            if (CanMove(currentX - 1, currentY))
            {
                currentX--;
            }
        }

        private void MoveShapeRight()
        {
            if (CanMove(currentX + 1, currentY))
            {
                currentX++;
            }
        }

        private void DropShape()
        {
            int dropDistance = 0;
            while (CanMove(currentX, currentY + 1))
            {
                currentY++;
                dropDistance++;
            }
            score += dropDistance * 2; // Hard drop
        }

        private void RotateShape()
        {
            int[,] shape = shapes[currentShapeIndex];
            int[,] newShape = new int[shape.GetLength(1), shape.GetLength(0)];
            for (int y = 0; y < shape.GetLength(0); y++)
            {
                for (int x = 0; x < shape.GetLength(1); x++)
                {
                    newShape[x, shape.GetLength(0) - 1 - y] = shape[y, x];
                }
            }
            if (CanMove(currentX, currentY, newShape))
            {
                shapes[currentShapeIndex] = newShape;
            }
        }

        private void HoldShape()
        {
            if (!canHold) return;

            if (heldShapeIndex == -1)
            {
                heldShapeIndex = currentShapeIndex;
                SpawnShape();
            }
            else
            {
                (heldShapeIndex, currentShapeIndex) = (currentShapeIndex, heldShapeIndex);
                currentX = Width / 2 - shapes[currentShapeIndex].GetLength(1) / 2;
                currentY = 0;
            }
            canHold = false;
        }

        private bool CanMove(int newX, int newY)
        {
            int[,] shape = shapes[currentShapeIndex];
            for (int y = 0; y < shape.GetLength(0); y++)
            {
                for (int x = 0; x < shape.GetLength(1); x++)
                {
                    if (shape[y, x] != 0)
                    {
                        int newGridX = newX + x;
                        int newGridY = newY + y;
                        if (newGridX < 0 || newGridX >= Width || newGridY < 0 || newGridY >= Height || grid[newGridY, newGridX] != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool CanMove(int newX, int newY, int[,] newShape)
        {
            for (int y = 0; y < newShape.GetLength(0); y++)
            {
                for (int x = 0; x < newShape.GetLength(1); x++)
                {
                    if (newShape[y, x] != 0)
                    {
                        int newGridX = newX + x;
                        int newGridY = newY + y;
                        if (newGridX < 0 || newGridX >= Width || newGridY < 0 || newGridY >= Height || grid[newGridY, newGridX] != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void MergeShape()
        {
            int[,] shape = shapes[currentShapeIndex];
            for (int y = 0; y < shape.GetLength(0); y++)
            {
                for (int x = 0; x < shape.GetLength(1); x++)
                {
                    if (shape[y, x] != 0)
                    {
                        grid[currentY + y, currentX + x] = shape[y, x];
                    }
                }
            }
        }

        private void FillTerminosBag()
        {
            terminosBag = Enumerable.Range(0, shapes.Count).OrderBy(x => random.Next()).ToList();
        }

        private int ClearLines()
        {
            int linesCleared = 0;
            for (int y = 0; y < Height; y++)
            {
                bool fullLine = true;
                for (int x = 0; x < Width; x++)
                {
                    if (grid[y, x] == 0)
                    {
                        fullLine = false;
                        break;
                    }
                }
                if (fullLine)
                {
                    linesCleared++;
                    for (int yy = y; yy > 0; yy--)
                    {
                        for (int xx = 0; xx < Width; xx++)
                        {
                            grid[yy, xx] = grid[yy - 1, xx];
                        }
                    }
                    for (int xx = 0; xx < Width; xx++)
                    {
                        grid[0, xx] = 0;
                    }
                }
            }
            return linesCleared;
        }

        private void UpdateScore(int lines)
        {
            int baseScore = 0;
            switch (lines)
            {
                case 1:
                    baseScore = 100;
                    break;
                case 2:
                    baseScore = 300;
                    break;
                case 3:
                    baseScore = 500;
                    break;
                case 4:
                    baseScore = 800;
                    break;
            }

            if (lines > 0)
            {
                combo++;
                score += baseScore * level;
                score += 50 * combo * level;
                if (backToBack && lines == 4)
                {
                    score += (int)(baseScore * 0.5 * level);
                }
                backToBack = lines == 4;
            }
            else
            {
                combo = 0;
            }

            linesCleared += lines;
            level = linesCleared / 10 + 1;
        }

        private bool IsGameOver()
        {
            for (int x = 0; x < Width; x++)
            {
                if (grid[0, x] != 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void Draw()
        {
            Console.Clear();
            DrawGrid();
            DrawGhostShape();
            DrawCurrentShape();
            DrawHeldShape();
            DrawScore();
        }

        private void DrawGrid()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (grid[y, x] != 0)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }

        private void DrawCurrentShape()
        {
            int[,] shape = shapes[currentShapeIndex];
            for (int y = 0; y < shape.GetLength(0); y++)
            {
                for (int x = 0; x < shape.GetLength(1); x++)
                {
                    if (shape[y, x] != 0)
                    {
                        Console.SetCursorPosition(currentX + x, currentY + y);
                        Console.Write("#");
                    }
                }
            }
        }

        private void DrawGhostShape()
        {
            int ghostY = currentY;
            while (CanMove(currentX, ghostY + 1))
            {
                ghostY++;
            }

            int[,] shape = shapes[currentShapeIndex];
            for (int y = 0; y < shape.GetLength(0); y++)
            {
                for (int x = 0; x < shape.GetLength(1); x++)
                {
                    if (shape[y, x] != 0)
                    {
                        Console.SetCursorPosition(currentX + x, ghostY + y);
                        Console.Write("+");
                    }
                }
            }
        }

        private void DrawHeldShape()
        {
            if (heldShapeIndex == -1) return;

            int[,] shape = shapes[heldShapeIndex];
            Console.SetCursorPosition(Width + 2, 0);
            Console.WriteLine("Held Shape:");
            for (int y = 0; y < shape.GetLength(0); y++)
            {
                Console.SetCursorPosition(Width + 2, y + 1);
                for (int x = 0; x < shape.GetLength(1); x++)
                {
                    if (shape[y, x] != 0)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }

        private void DrawScore()
        {
            Console.SetCursorPosition(Width + 2, 10);
            Console.WriteLine($"Score: {score}");
            Console.SetCursorPosition(Width + 2, 11);
            Console.WriteLine($"Level: {level}");
            Console.SetCursorPosition(Width + 2, 12);
            Console.WriteLine($"Lines: {linesCleared}");
        }
    }
}