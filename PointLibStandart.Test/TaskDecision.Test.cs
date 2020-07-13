using Microsoft.VisualStudio.TestTools.UnitTesting;
using KinematicModeling;
using PointSpase;
using System.Diagnostics;
using System;

namespace PointLibStandart.Test
{
    [TestClass]
    public class UnitTest1
    {
        public Dec      dec;
        public Point    comparison ;
        public Point    pointOut;
        public Dec      decOut;

        [TestInitialize]
        public void Begin()
        {
            //arrange
            dec = new Dec(175, 155, 212, -90, 0, 0);
            comparison = new Point();
            comparison['a'] = 0;
            comparison['b'] = 90;
            comparison['c'] = 0;
            comparison['d'] = 90;
            comparison['e'] = 90;
            comparison['f'] = -90;
            pointOut = TaskDecision.DecToPoint(dec);
            decOut = TaskDecision.PointToDec(pointOut);
        }

        [TestMethod]
        public void TestDecToPointA() => Assert.AreEqual(pointOut.CanA, comparison.CanA);
        
        [TestMethod]
        public void TestDecToPointB()=> Assert.AreEqual(pointOut.CanB, comparison.CanB);
        
        [TestMethod]
        public void TestDecToPointC() => Assert.AreEqual(Math.Round( pointOut.CanC), comparison.CanC);
        
        [TestMethod]
        public void TestDecToPointD() => Assert.AreEqual(Math.Round(pointOut.CanD), comparison.CanD);
        
        [TestMethod]
        public void TestDecToPointE() => Assert.AreEqual(Math.Round(pointOut.CanE), comparison.CanE);
        
        [TestMethod]
        public void TestDecToPointF() => Assert.AreEqual(Math.Round(pointOut.CanF), comparison.CanF);
       
        [TestMethod]
        public void TestPointToDecX() => Assert.AreEqual(Math.Round(decOut.DecX), dec.DecX);
        [TestMethod]
        public void TestPointToDecY() => Assert.AreEqual(Math.Round(decOut.DecY), dec.DecY);

        [TestMethod]
        public void TestPointToDecZ() => Assert.AreEqual(Math.Round(decOut.DecZ), dec.DecZ);

        [TestMethod]
        public void TestPointToDecA() => Assert.AreEqual(Math.Round(decOut.AnglA), dec.AnglA);

        [TestMethod]
        public void TestPointToDecB() => Assert.AreEqual(Math.Round(decOut.AnglB), dec.AnglB);

        [TestMethod]
        public void TestPointToDecC() => Assert.AreEqual(Math.Round(decOut.AnglC), dec.AnglC);



    }
}
