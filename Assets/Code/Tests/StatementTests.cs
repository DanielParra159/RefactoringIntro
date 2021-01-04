using System.Collections.Generic;
using NUnit.Framework;

namespace Code.Tests
{
    public class StatementTests
    {
        private Performance[] _performances;
        private Invoice _invoice;
        private Dictionary<string, Play> _plays;

        [SetUp]
        public void SetUp()
        {
            _performances = new[]
                            {
                                new Performance("hamlet", 55),
                                new Performance("as-like", 35),
                                new Performance("othello", 40),
                            };
            _invoice = new Invoice("BigCo", _performances);
            _plays = new Dictionary<string, Play>
                     {
                         {"hamlet", new Play("Hamlet", "tragedy")},
                         {"as-like", new Play("As You Like It", "comedy")},
                         {"othello", new Play("Othello", "tragedy")},
                     };
        }

        [Test]
        public void Generate_ContainsCustomerName()
        {
            var statement = new Statement();
            var result = statement.Generate(_invoice, _plays);

            Assert.IsTrue(result.Contains("Statement for BigCo"));
        }
        
        [Test]
        public void Generate_ContainsPlaysAmount()
        {
            var statement = new Statement();
            var result = statement.Generate(_invoice, _plays);

            Assert.IsTrue(result.Contains("Hamlet: $650.000 (55 seats)"));
            Assert.IsTrue(result.Contains("As You Like It: $580.000 (35 seats)"));
            Assert.IsTrue(result.Contains("Othello: $500.000 (40 seats)"));
        }
        
        [Test]
        public void Generate_ContainsAmountOwned()
        {
            var statement = new Statement();
            var result = statement.Generate(_invoice, _plays);

            Assert.IsTrue(result.Contains("Amount owed is $1,730.000"));
        }
        
        [Test]
        public void Generate_ContainsEarnedCredits()
        {
            var statement = new Statement();
            var result = statement.Generate(_invoice, _plays);

            Assert.IsTrue(result.Contains("You earned 47 credits"));
        }
    }
}
