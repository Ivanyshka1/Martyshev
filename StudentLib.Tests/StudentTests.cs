using System;
using Xunit;
using StudentLib;

namespace StudentLib.Tests
{
    public class StudentTests
    {
        [Theory]
        [InlineData("John", true)]        // Valid name
        [InlineData("J0hn", true)]       // Valid name with one digit
        [InlineData("john", false)]      // Starts with lowercase
        [InlineData("Jo", true)]         // Minimum length
        [InlineData("Johndoe1234567", true)] // Maximum length
        [InlineData("J", false)]         // Too short
        [InlineData("Johndoe12345678", false)] // Too long
        [InlineData("J0h1", false)]      // Two digits
        [InlineData("Jöhn", false)]      // Invalid characters
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
        [InlineData("2050-01-01", -1)]   // Future date
        [InlineData("2010-01-01", 0)]    // Under 16
        [InlineData("2008-06-15", 1)]    // 16-17 years old
        [InlineData("2005-06-15", 2)]    // 18-22 years old
        [InlineData("2003-01-01", 2)]    // Exactly 22
        [InlineData("2002-12-31", 3)]    // Over 22
        public void CheckBirthday_ReturnsCorrectAgeGroup(string birthday, int expected)
        {
            // Arrange
            var student = new Student("John", "Doe", DateTime.Parse(birthday), 1, "Computer Science");
            
            // Act
            int result = student.CheckBirthday();
            
            // Assert
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