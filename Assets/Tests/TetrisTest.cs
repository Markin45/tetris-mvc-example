using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TetrisTest
    {
        class Cube : IFigure
        {
            public int Width => 2;
            public int Height => 2;
            public bool At(int x, int y) { return true; }
        }

        class CubeFactory : IFigureFactory
        {
            public IFigure RandomFigure() { return new Cube(); }
        }

        static void SetupField(ITetrisModel model, string expected)
        {
            for (int y = 0; y < model.Height; y++) {
                for (int x = 0; x < model.Width; x++)
                    model.SetAt(x, y, expected[y * model.Width + x] == 'x');
            }
        }

        static void AssertField(ITetrisModel model, string expected)
        {
            for (int y = 0; y < model.Height; y++) {
                for (int x = 0; x < model.Width; x++) {
                    bool expect = (expected[y * model.Width + x] == 'x');
                    bool actual = model.At(x, y);
                    Assert.AreEqual(expect, actual);
                }
            }
        }

        [Test]
        public void TetrisTestSimple()
        {
            var model = new TetrisModel(4, 3);
            var controller = new TetrisController(model, new CubeFactory());
            bool result;

            Assert.AreEqual(null, model.Figure);

            result = controller.Step();
            Assert.IsTrue(result);
            Assert.IsFalse(model.Figure == null);
            Assert.AreEqual(1, model.FigureX);
            Assert.AreEqual(1, model.FigureY);

            AssertField(model, "...."+
                               "...."+
                               "....");

            result = controller.Step();
            Assert.IsTrue(result);
            Assert.AreEqual(null, model.Figure);

            AssertField(model, "...."+
                               ".xx."+
                               ".xx.");
        }

        [Test]
        public void TetrisTestMoveLeft()
        {
            var model = new TetrisModel(4, 3);
            var controller = new TetrisController(model, new CubeFactory());
            bool result;

            Assert.AreEqual(null, model.Figure);

            result = controller.Step();
            Assert.IsTrue(result);
            Assert.IsFalse(model.Figure == null);
            Assert.AreEqual(1, model.FigureX);
            Assert.AreEqual(1, model.FigureY);

            AssertField(model, "...."+
                               "...."+
                               "....");

            controller.MoveFigureLeft();
            Assert.IsTrue(result);
            Assert.IsFalse(model.Figure == null);
            Assert.AreEqual(0, model.FigureX);
            Assert.AreEqual(1, model.FigureY);

            result = controller.Step();
            Assert.IsTrue(result);
            Assert.AreEqual(null, model.Figure);

            AssertField(model, "...."+
                               "xx.."+
                               "xx..");
        }

        [Test]
        public void TetrisTestMoveRight()
        {
            var model = new TetrisModel(4, 3);
            var controller = new TetrisController(model, new CubeFactory());
            bool result;

            Assert.AreEqual(null, model.Figure);

            result = controller.Step();
            Assert.IsTrue(result);
            Assert.IsFalse(model.Figure == null);
            Assert.AreEqual(1, model.FigureX);
            Assert.AreEqual(1, model.FigureY);

            AssertField(model, "...."+
                               "...."+
                               "....");

            controller.MoveFigureRight();
            Assert.IsTrue(result);
            Assert.IsFalse(model.Figure == null);
            Assert.AreEqual(2, model.FigureX);
            Assert.AreEqual(1, model.FigureY);

            result = controller.Step();
            Assert.IsTrue(result);
            Assert.AreEqual(null, model.Figure);

            AssertField(model, "...."+
                               "..xx"+
                               "..xx");
        }

        [Test]
        public void TetrisTestEraseLines()
        {
            var model = new TetrisModel(4, 3);
            var controller = new TetrisController(model, new CubeFactory());
            bool result;

            SetupField(model, "...."+
                              "...."+
                              "x..x");

            Assert.AreEqual(null, model.Figure);

            result = controller.Step();
            Assert.IsTrue(result);
            Assert.IsFalse(model.Figure == null);
            Assert.AreEqual(1, model.FigureX);
            Assert.AreEqual(1, model.FigureY);

            AssertField(model, "...."+
                               "...."+
                               "x..x");

            result = controller.Step();
            Assert.IsTrue(result);
            Assert.AreEqual(null, model.Figure);

            AssertField(model, "...."+
                               "...."+
                               ".xx.");
        }

        [Test]
        public void TetrisTestGameOver()
        {
            var model = new TetrisModel(4, 3);
            var controller = new TetrisController(model, new CubeFactory());
            bool result;

            Assert.AreEqual(null, model.Figure);

            result = controller.Step();
            Assert.IsTrue(result);
            Assert.IsFalse(model.Figure == null);
            Assert.AreEqual(1, model.FigureX);
            Assert.AreEqual(1, model.FigureY);

            AssertField(model, "...."+
                               "...."+
                               "....");

            result = controller.Step();
            Assert.IsTrue(result);
            Assert.AreEqual(null, model.Figure);

            AssertField(model, "...."+
                               ".xx."+
                               ".xx.");

            result = controller.Step();
            Assert.IsFalse(result);
        }
    }
}
