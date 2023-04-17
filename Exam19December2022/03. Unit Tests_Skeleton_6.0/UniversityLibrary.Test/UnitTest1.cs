namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using System.Text;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            TextBook book = new TextBook("title", "author", "category");

            Assert.AreEqual("title", book.Title);
            Assert.AreEqual("author", book.Author);
            Assert.AreEqual("category", book.Category);
        }

        [Test]
        public void Test2()
        {
            TextBook book = new TextBook("title", "author", "category");

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Book: title - 0");
            sb.AppendLine("Category: category");
            sb.AppendLine("Author: author");

            string expectedResult = sb.ToString().TrimEnd();
            string actualResult = book.ToString();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Test3()
        {
            UniversityLibrary lib = new UniversityLibrary();

            Assert.AreEqual(0, lib.Catalogue.Count);
        }

        [Test]
        public void Test4()
        {
            UniversityLibrary lib = new UniversityLibrary();
            TextBook book = new TextBook("title", "author", "category");

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Book: title - 1");
            sb.AppendLine("Category: category");
            sb.AppendLine("Author: author");

            string actualResult = lib.AddTextBookToLibrary(book);
            string expectedResult = sb.ToString().TrimEnd();

            Assert.AreEqual(1, lib.Catalogue.Count);
            Assert.AreEqual(1, book.InventoryNumber);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Test5()
        {
            UniversityLibrary lib = new UniversityLibrary();

            TextBook textBook = new TextBook("History", "Balabanov", "Humanity");
            TextBook textBook2 = new TextBook("History2", "Balabanov", "Humanity");
            TextBook textBook3 = new TextBook("History3", "Balabanov", "Humanity");

            lib.AddTextBookToLibrary(textBook);
            lib.AddTextBookToLibrary(textBook2);
            lib.AddTextBookToLibrary(textBook3);

            var actualResult = lib.LoanTextBook(2, "George Bush");
            var expectedResult = "History2 loaned to George Bush.";

            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(textBook2.Holder, "George Bush");
        }

        [Test]
        public void Test6()
        {
            UniversityLibrary lib = new UniversityLibrary();

            TextBook textBook = new TextBook("History", "Balabanov", "Humanity");
            TextBook textBook2 = new TextBook("History2", "Balabanov", "Humanity");
            TextBook textBook3 = new TextBook("History3", "Balabanov", "Humanity");

            lib.AddTextBookToLibrary(textBook);
            lib.AddTextBookToLibrary(textBook2);
            lib.AddTextBookToLibrary(textBook3);

            lib.LoanTextBook(2, "George Bush");


            var actualResult = lib.LoanTextBook(2, "George Bush");
            var expectedResult = "George Bush still hasn't returned History2!";

            Assert.AreEqual(expectedResult, actualResult);

        }
        [Test]
        public void Test7()
        {
            UniversityLibrary lib = new UniversityLibrary();

            TextBook textBook = new TextBook("History", "Balabanov", "Humanity");
            TextBook textBook2 = new TextBook("History2", "Balabanov", "Humanity");
            TextBook textBook3 = new TextBook("History3", "Balabanov", "Humanity");

            lib.AddTextBookToLibrary(textBook);
            lib.AddTextBookToLibrary(textBook2);
            lib.AddTextBookToLibrary(textBook3);

            lib.LoanTextBook(2, "George Bush");
            lib.LoanTextBook(1, "Eray Ridvan");

            var firstResult = lib.ReturnTextBook(2);
            var secondResult = lib.ReturnTextBook(1);

            var expectedResultOne = "History2 is returned to the library.";
            var expectedResultTwo = "History is returned to the library.";

            Assert.AreEqual(expectedResultOne, firstResult);
            Assert.AreEqual(expectedResultTwo, secondResult);
            Assert.AreEqual(string.Empty, textBook.Holder);
        }
    }
}