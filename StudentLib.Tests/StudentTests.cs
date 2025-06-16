using System;
using Xunit;
using StudentLib;

namespace StudentLib.Tests
{
    public class StudentTests
    {
        [Theory]
        [InlineData("John", true)]        // Корректное имя
        [InlineData("J0hn", true)]       // Корректное имя с одной цифрой
        [InlineData("john", false)]       // Начинается с маленькой буквы
        [InlineData("Jo", true)]          // Минимальная длина
        [InlineData("Johndoe1234567", true)] // Максимальная длина
        [InlineData("J", false)]          // Слишком короткое
        [InlineData("Johndoe12345678", false)] // Слишком длинное
        [InlineData("J0h1", false)]       // Две цифры
        [InlineData("Jöhn", false)]       // Недопустимые символы
        public void CheckName_ValidatesNameCorrectly(string name, bool expected)
        {
            // Arrange
            var student = new Student(name, "Doe", new DateTime(2000, 1, 1), 1, "Computer Science");
            
            // Act
            bool result = student.CheckName();
            
            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("  computer   science  ", "COMPUTER SCIENCE")]
        [InlineData("Software Engineering", "SOFTWARE ENGINEERING")]
        [InlineData("  data  science ", "DATA SCIENCE")]
        [InlineData("PHYSICS", "PHYSICS")]
        [InlineData("  math ", "MATH")]
        public void TransformSpecialty_TransformsCorrectly(string input, string expected)
        {
            // Arrange
            var student = new Student("John", "Doe", new DateTime(2000, 1, 1), 1, input);
            
            // Act
            string result = student.TransformSpecialty();
            
            // Assert
            Assert.Equal(expected, result);
            Assert.Equal(expected, student.Specialty);
        }

        [Theory]
        [InlineData("2050-01-01", -1)]   // Дата в будущем
        [InlineData("2010-01-01", 0)]    // Меньше 16 лет
        [InlineData("2008-06-15", 1)]    // 16-17 лет (предполагаем, что сегодня 2025-06-16)
        [InlineData("2005-06-15", 2)]    // 18-22 года
        [InlineData("2003-01-01", 2)]    // 22 года
        [InlineData("2002-12-31", 3)]    // Больше 22 лет
        public void CheckBirthday_ReturnsCorrectAgeGroup(string birthday, int expected)
        {
            // Arrange
            var student = new Student("John", "Doe", DateTime.Parse(birthday), 1, "Computer Science");
            
            // Act
            int result = student.CheckBirthday();
            
            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            // Arrange & Act
            var student = new Student("John", "Doe", new DateTime(2000, 1, 1), 1, "Computer Science");
            
            // Assert
            Assert.Equal("John", student.Name);
            Assert.Equal("Doe", student.Surname);
            Assert.Equal(new DateTime(2000, 1, 1), student.Birthday);
            Assert.Equal(1, student.Course);
            Assert.Equal("Computer Science", student.Specialty);
        }

        [Fact]
        public void Properties_ThrowArgumentNullException_WhenNull()
        {
            // Arrange
            var student = new Student("John", "Doe", new DateTime(2000, 1, 1), 1, "CS");
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => student.Name = null);
            Assert.Throws<ArgumentNullException>(() => student.Surname = null);
            Assert.Throws<ArgumentNullException>(() => student.Specialty = null);
        }
    }
}   