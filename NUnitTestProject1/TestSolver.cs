using GameOfLifeSolver.Models;
using GameOfLifeSolver.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using FluentAssertions.Specialized;
using System.Text.Json;

namespace NUnitTestProject1
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }

        [TestCase("(1,1)", "", 1, "Any live cell with fewer than two live neighbors dies as if by underpopulation (0 neighbors)")]
        [TestCase("(1,1);(1,2)", "", 1, "Any live cell with fewer than two live neighbors dies as if by underpopulation (1 neighbor)")]
        [TestCase("(1,1);(1,2);(2,1)", "(1,1);(1,2);(2,1);(2,2)", 1, "Any live cell with two or three live neighbors lives on to the next generation (2 neigbors)")]
        [TestCase("(1,1);(1,2);(2,1);(2,2)", "(1,1);(1,2);(2,1);(2,2)", 1, "Any live cell with two or three live neighbors lives on to the next generation (3 neigbors)")]
        [TestCase("(0,2);(1,1);(1,2);(2,1);(2,2)", "(0,2);(0,1);(1,3);(2,1);(2,2)", 1, "Any live cell with more than three live neighbors dies, as if by overpopulation")]
        [TestCase("(1,1);(3,1);(2,3)", "(2,2)", 1, "Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction")]
        [TestCase("(1,1);(1,2);(1,3);(2,1);(2,2);(2,3);(3,1);(3,2);(3,3)", "(2,0);(1,1);(3,1);(0,2);(4,2);(1,3);(3,3);(2,4)", 1, "block turns into diamond")]
        public void TestSolveGeneration(string seed, string result, int numGenerations, string reason)
        {
            var seedBoard = seed.FromString();
            var expectedResult = result.FromString();

            var actualResult = SolverService.Solve(seedBoard, numGenerations);

            expectedResult.Should().BeEquivalentTo(actualResult, because: reason);
        }
        [TestCase("(0,0);(2,0);(1,-1);(2,-1);(1,-2)", "(2,0);(0,-1);(2,-1);(1,-2);(2,-2)", 1, "glider moved")]
        [TestCase("(0,0);(2,0);(1,-1);(2,-1);(1,-2)", "(4,-5);(5,-5);(5,-3);(6,-5);(6,-4)",15, "glider moved")]
        public void TestSolveGlider(string seed, string result, int numGenerations, string reason)
        {
            var seedBoard = seed.FromString();
            var expectedResult = result.FromString();

            var actualResult = SolverService.Solve(seedBoard, numGenerations);
            foreach (var element in actualResult)
            {
                Console.WriteLine(element.ToString());
            }
            expectedResult.Should().BeEquivalentTo(actualResult, because: reason);
        }


        [TestCase("(1,1);(1,2)", 1, Description = "1 neighbor")]
        [TestCase("(1,1);(1,2);(2,1)", 2, Description = "2 neighbors")]
        public void FindNeighbors(string seed, int expectedNeighbors)
        {
            var board = seed.FromString();
            int actualNeighbors = SolverService.FindNeighborCount(board.First(), board);

            Assert.AreEqual(expectedNeighbors, actualNeighbors);
        }

        [Test]
        public void UpperLeftFrom1_1()
        {
            var starting = new Cell(1, 1);
            Assert.AreEqual(starting.UpperLeft, new Cell(0, 2));
        }

        [Test]
        public void LowerRightFrom1_1()
        {
            var starting = new Cell(1, 1);
            Assert.AreEqual(starting.LowerRight, new Cell(2, 0));
        }
    }
    public static class SolverExtensions
    {
        public static IEnumerable<Cell> FromString(this string str)
        {
            var board = from cell in str.Split(";")
                        where cell.Contains("(")
                        let numbers = cell.Replace("(", "").Replace(")", "").Split(',')
                        let x = int.Parse(numbers[0])
                        let y = int.Parse(numbers[1])
                        select new Cell(x, y);
            return board;
        }

        public static IEnumerable<Cell> FromTable(this string table)
        {
            table = table.Replace("\t", "").Replace("\n", "");
            var board = new List<Cell>();
            var rows = table.Split('\r', StringSplitOptions.RemoveEmptyEntries).ToList();
            for (int r = 0; r < rows.Count; r++)
            {
                var row = rows[r];
                var cells = row.Split('|').ToList();
                for (int c = 0; c < cells.Count; c++)
                {
                    if (cells[c].Contains("X"))
                        board.Add(new Cell(r, c));
                }
            }
            return board;
        }

        public static string ToTable(this IEnumerable<Cell> board)
        {
            var minX = board.Min(c => c.X);
            var maxX = board.Max(c => c.X);

            var minY = board.Min(c => c.Y);
            var maxY = board.Max(c => c.Y);

            var rows = new List<string>();
            for (int r = minX; r <= maxX; r++)
            {
                var cells = new List<string>();
                for (int c = minY; c <= maxY; c++)
                {
                    var cellValue = " ";
                    if (board.Any(b => b.X == r && b.Y == c))
                        cellValue = "X";
                    cells.Add(cellValue);
                }
                var row = $"|{String.Join('|', cells)}|";
                rows.Add(row);
            }
            var table = String.Join('\r', rows);
            return table;
        }
    }
}