using assessment_platform_developer.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace assessment_platform_developer.Tests
{
	[TestClass]
	public class PostalZipValidatorTests
	{
		[DataTestMethod]
        [DataRow("Canada", "V0G 1M0", true)]
        [DataRow("Canada", "Giraffe", false)]
        [DataRow("Canada", "T3T 2T2", true)]
        [DataRow("Canada", "90210", false)]
        [DataRow("Canada", "90210-1122", false)]
        [DataRow("United States", "90210", true)]
        [DataRow("United States", "90210-1122", true)]
        [DataRow("United States", "Caserole", false)]
        [DataRow("United States", "V0G 1M0", false)]
        [DataRow("United States", "T3T 2T2", false)]
        [DataRow("France", "T3T 2T2", false)]
        [DataRow("England", "90210", false)]
        [DataRow("Australia", "Giraffe", false)]
        [DataRow("Zimbabwe", "90210-1122", false)]
        [DataRow("Chile", "V0G 1M0", false)]
        public void ValidatorTest(string country, string code, bool result)
		{
			ZipPostalValidator validator = new ZipPostalValidator();

			bool testResult = validator.IsCorrectPostalForCountry(code, country);

			Assert.AreEqual(result, testResult, country +" with "+code+" should have been "+result.ToString());
		}
	}
}
