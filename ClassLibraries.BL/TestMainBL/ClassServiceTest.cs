using MainBL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MainEntity.Models.Class;

namespace TestMainBL
{
    
    
    /// <summary>
    ///This is a test class for ClassServiceTest and is intended
    ///to contain all ClassServiceTest Unit Tests
    ///</summary>
	[TestClass()]
	public class ClassServiceTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		/// <summary>
		///A test for GetClassesWithProductsForCategorySearch
		///</summary>
		[TestMethod()]
		public void GetClassesWithProductsForCategorySearchTest()
		{
			ClassService target = new ClassService(GlobalConstants.CONNECTION_NAME); // TODO: Initialize to an appropriate value
			long from_root_id = GlobalConstants.GLOBAL_ROOT_ID; // TODO: Initialize to an appropriate value
			long portal_id = GlobalConstants.TEST_PORTAL_ID; // TODO: Initialize to an appropriate value
			string category_name = "Shabbat"; // TODO: Initialize to an appropriate value
			int start_row_index = 0; // TODO: Initialize to an appropriate value
			int max_rows_count = 10; // TODO: Initialize to an appropriate value
			string[] search_words = new string[] { "Shabbat", "Yom Kippur" }; // TODO: Initialize to an appropriate value
			string search_code = "AB 678"; // TODO: Initialize to an appropriate value
			string word_in_title = "Rabbi"; // TODO: Initialize to an appropriate value
			long[] class_ids = { 10, 20 }; // TODO: Initialize to an appropriate value

			//var data = target.SearchClasses(from_root_id, portal_id, category_name, start_row_index, max_rows_count, search_words, search_code, word_in_title, class_ids);
			var cnt = target.SearchClassesCnt(from_root_id, portal_id, category_name, search_words, search_code, word_in_title, class_ids);

			cnt = cnt + 1;


		}
	}
}
