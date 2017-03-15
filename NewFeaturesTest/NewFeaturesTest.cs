using System;
using Xunit;

namespace NewFeaturesTest
{
    public class NewFeaturesTest
	{
		[Fact]
		public void TestOutVariables()
		{
			// old way
			int num;
			Int32.TryParse("15", out num);
			Assert.Equal(num, 15);

			// C# 7 way
			Int32.TryParse("15", out int number);
			Assert.Equal(number, 15);
		}


		[Fact]
		public void TestPattern_Is()
		{
			object one = 1;

			Assert.True(one is int number && number == 1);
		}

		[Fact]
		public void TestTupleTypes()
		{
			var host = GetHostInfo();

			// traditional unamed tuple still works
			Assert.Equal(host.Item1, "Robert");
			Assert.Equal(host.Item2, "Wang");
			Assert.Equal(host.Item3, 1998);

			// named tuple is more readable
			Assert.Equal(host.firstName, "Robert");
			Assert.Equal(host.lastName, "Wang");
			Assert.Equal(host.birthYear, 1998);


			// deconstructing assignment
			(var firstName, var lastName, var birthYear) = GetHostInfo();
			Assert.Equal(firstName, "Robert");
			Assert.Equal(lastName, "Wang");
			Assert.Equal(birthYear, 1998);
		}

		private static (string firstName, string lastName, int birthYear) GetHostInfo()
		{
			return ("Robert", "Wang", 1998);
		}
	}
}
