using System;
using Xunit;
using StudentLib;

namespace StudentLib.Tests
{
    public class StudentTests
    {
        [Theory]
        [InlineData("John", true)]        // Valid name
        [InlineData("j0hn", false)]      // Starts with lowercase
        public void CheckName_ValidatesNameCorrectly(string name, bool expected)
        {
            var student = new Student(name, "Doe", new DateTime(2000, 1, 1), 1, "CS");
            Assert.Equal(expected, student.CheckName());
        }

        [Fact]
        public void TransformSpecialty_RemovesExtraSpacesAndUppercases()
        {
            var student = new Student("John", "Doe", new DateTime(2000, 1, 1), 1, "  computer   science  ");
            Assert.Equal("COMPUTER SCIENCE", student.TransformSpecialty());
        }

        [Fact]
        public void CheckBirthday_ReturnsCorrectAgeForAdult()
        {
            var birthDate = DateTime.Today.AddYears(-20); // 20 years old
            var student = new Student("John", "Doe", birthDate, 1, "CS");
            Assert.Equal(2, student.CheckBirthday()); // 18-22 age group
        }

        [Fact]
        public void Constructor_SetsAllPropertiesCorrectly()
        {
            var student = new Student("John", "Doe", new DateTime(2000, 1, 1), 1, "CS");
            
            Assert.Equal("John", student.Name);
            Assert.Equal("Doe", student.Surname);
            Assert.Equal(new DateTime(2000, 1, 1), student.Birthday);
            Assert.Equal(1, student.Course);
            Assert.Equal("CS", student.Specialty);
        }

        [Fact]
        public void SettingNullName_ThrowsArgumentNullException()
        {
            var student = new Student("John", "Doe", new DateTime(2000, 1, 1), 1, "CS");
            Assert.Throws<ArgumentNullException>(() => student.Name = null);
        }
    }
}