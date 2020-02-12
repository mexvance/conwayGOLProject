using GameOfLifeSolver.Models;
using GameOfLifeSolver.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace NUnitTestProject1
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSolveGeneration()
        {
            var solvedBoard = SolverService.SolveGeneration(new List<Cell>());
            Assert.AreEqual(solvedBoard, "{}");
        }
    }
}