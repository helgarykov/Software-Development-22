using NUnit.Framework;
using Collatz;

namespace Collatz.Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [TestCase(8, 20, 20, 4)]
    [TestCase(3, 20, 20, 8)]
    [TestCase(9, 20, 53, 20)]
    [TestCase(1,1,1,1)]
    [TestCase(0,20, 20, 0)]

    public void CollatzReturnsCorrectly(int n, int maxLen, int maxSize, int expect_len)
    {
        Assert.AreEqual(BasicFunctions.Collatz(n, maxLen, maxSize), expect_len);
    }
    
    [Test]
    [TestCase(9, 20, 52, 19)]
    [TestCase(9, 10, 53, 19)]
    public void CollatzFailsOutOfBounds(int n, int maxLen, int maxSize, int expect_len)
    {
        Assert.AreNotEqual(BasicFunctions.Collatz(n, maxLen, maxSize), expect_len);
    }
    
    [Test]
    [TestCase(-9, 20, 53)]
    public void CollatzNeverRuns(int n, int maxLen, int maxSize)
    {
        Assert.LessOrEqual(BasicFunctions.Collatz(n, maxLen, maxSize),0);
    }
    
    [Test]
    [TestCase(3, 20, 8)]
    [TestCase(9, 20, 53)]
    [TestCase(1,1,1)]
    [TestCase(0,20, 20)]
    public void CollatzAndCollatzRecReturnEqual(int n, int maxLen, int maxSize)
    {
        int collatzResult = BasicFunctions.Collatz(n, maxLen, maxSize);
        int collatzRecResult = BasicFunctions.CollatzRec(n, maxLen, maxSize);
        
        Assert.AreEqual(collatzResult,collatzRecResult);
    }
    
    
    
}