using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Yijian.ECIS.RecipeSplit.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(1, 1);
        }


        [TestMethod()]
        public void SplitTest_bid()
        {
            var now = new DateTime(2021, 12, 8, 11, 12, 13);
            RecipeSplitor splitor = new RecipeSplitor();

            var input1 = new
            {
                prescribeFrequency =
                    new Contracts.PrescribeFrequency()
                    {
                        Frequency = "bid",
                        Times = 2,
                        Unit = "1D",
                        ExecuteDayTime = "08:00,16:00",
                        SubmitTime = now,
                        StartTime = now,
                        EndTime = now.AddDays(3).Date
                    },
                conversionTime = now
            };

            var basedate = now.Date;
            var ts1 = new TimeSpan(08, 00, 00);
            var ts2 = new TimeSpan(16, 00, 00);
            var output1 = new DateTime[] {
                basedate.AddDays(0).Add(ts2),
                basedate.AddDays(1).Add(ts1),
                basedate.AddDays(1).Add(ts2),
                basedate.AddDays(2).Add(ts1),
                basedate.AddDays(2).Add(ts2)
            };

            var result = splitor.Split(input1.prescribeFrequency, input1.conversionTime).ToArray();

            Assert.AreEqual(result.Length, output1.Length);

            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreEqual(result[i], output1[i]);
            }

        }


    }
}
