using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace NewFeatures
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

			// note the scope of number is within Assert.True(). 
			Assert.True(one is int number && number == 1);
		}

		[Fact]
		public void TestPattern_SwitchCase()
		{
			var robertTheNerd = "RobertTheNerd";
			var list = new ArrayList
			{
				100,
				5,
				robertTheNerd,
				new List<Boolean>() { true, true, true },
				DateTime.Now,
				null
			};

			var nullFound = false;
			foreach (var item in list)
			{
				switch(item)
				{
					case string name:
						Assert.Equal(name, robertTheNerd);
						break;
					case int val when val > 10:
						Assert.Equal(val, 100);
						break;
					case int val when val <= 10:
						Assert.Equal(val, 5);
						break;
					case IEnumerable<Boolean> booleans:
						foreach (var b in booleans)
							Assert.True(b);
						break;
					case DateTime past:
						Assert.True((DateTime.Now - past).Hours < 1);	// very loose
						break;
					default:		// default will always be evaluated last, even though "null" cased is stated below it.
						break;
					case null:
						nullFound = true;
						break;
				}
			}
			Assert.True(nullFound);
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
			return ("Robert", "Wang", 1998);	// not real!
		}


		[Fact]
		public void TestRefReturn()
		{
			int[] numbers = { 1, 3, 5, 7 };

			// ref return but non-ref local
			var first = GetFirst(numbers);
			Assert.Equal(first, 1);
			first = 10;
			Assert.Equal(numbers[0], 1);

			// ref return and ref local
			ref var refFirst = ref GetFirst(numbers);
			Assert.Equal(refFirst, 1);
			refFirst = 10;
			Assert.Equal(numbers[0], refFirst);
		}
		private static ref int GetFirst(int[] input)
		{
			return ref input[0];
		}
	}
}
