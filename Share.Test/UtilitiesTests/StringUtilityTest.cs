using Share.Utilities;
using Xunit;

namespace Share.Test.UtilitiesTests
{
    public static class StringUtilityTest
    {
        [Fact]
        public static void Replace_Should_Return_Empty_String_With_Empty_OriginalValue()
        {
            // arrange
            string originalValue = "";

            // act
            var newValue = StringUtility.Replace(originalValue, new[] { ' ', '-' }, "_");

            // assert
            Assert.Equal(originalValue, newValue);
        }

        [Fact]
        public static void Replace_Should_Return_Empty_String_With_Null_OriginalValue()
        {
            // arrange
            string originalValue = null;

            // act
            var newValue = StringUtility.Replace(originalValue, new[] { ' ', '-' }, "_");

            // assert
            Assert.Equal(string.Empty, newValue);
        }

        [Fact]
        public static void Replace_Should_Return_OriginalString_With_Nothing_Matchs()
        {
            // arrange
            string originalValue = "test";

            // act
            var newValue = StringUtility.Replace(originalValue, new[] { ' ', '-' }, "_");

            // assert
            Assert.Equal(originalValue, newValue);
        }

        [Fact]
        public static void Replace_Should_Return_OriginalString_With_Single_Match()
        {
            // arrange
            string originalValue = "test test";

            // act
            var newValue = StringUtility.Replace(originalValue, new[] { ' ', '-' }, "_");

            // assert
            Assert.Equal(originalValue.Replace(" ","_"), newValue);
        }

        [Fact]
        public static void Replace_Should_Return_OriginalString_With_Multi_Matchs()
        {
            // arrange
            string originalValue = "test test-test";

            // act
            var newValue = StringUtility.Replace(originalValue, new[] { ' ', '-' }, "_");

            // assert
            Assert.Equal(originalValue.Replace(" ", "_").Replace("-", "_"), newValue);
        }
    }
}
