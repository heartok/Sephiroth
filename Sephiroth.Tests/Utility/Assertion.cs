using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sephiroth.Tests.Utility
{
    public static class Assertion
    {
        public static void Inconclusive() => Assert.Inconclusive("Inconclusive");
    }
}
