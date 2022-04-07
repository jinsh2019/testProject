using System;
using Xunit;

namespace TestProject2
{
    public class UnitTest1
    {

        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

        [Theory]
        [InlineData(1, 2)]
        public void TheoryPassingTest(int a, int b)
        {
            Assert.Equal(3, Add(a, b));
        }
        [Theory]
        [InlineData(3, 4)]
        public void TheoryPassingTest2(int a, int b)
        {
            Assert.Equal(3, Add(a, b));
        }

        int Add(int x, int y)
        {
            return x + y;
        }


        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(6)]
        public void MyFirstTheory(int value)
        {
            Assert.True(IsOdd(value));
        }

        bool IsOdd(int value)
        {
            return value % 2 == 1;
        }
    }
}
