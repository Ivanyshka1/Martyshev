using System;
using System.Text;
using System.Text.RegularExpressions;

namespace StudentLib
{
    public class Student
    {
        private string _name;
        private string _surname;
        private DateTime _birthday;
        private short _course;
        private string _specialty;

        public Student(string name, string surname, DateTime birthday, short course, string specialty)
        {
            Name = name;
            Surname = surname;
            Birthday = birthday;
            Course = course;
            Specialty = specialty;
        }

        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Surname
        {
            get => _surname;
            set => _surname = value ?? throw new ArgumentNullException(nameof(value));
        }

        public DateTime Birthday
        {
            get => _birthday;
            set => _birthday = value;
        }

        public short Course
        {
            get => _course;
            set => _course = value;
        }

        public string Specialty
        {
            get => _specialty;
            set => _specialty = value ?? throw new ArgumentNullException(nameof(value));
        }

        public bool CheckName()
        {
            // Проверка длины имени
            if (Name.Length < 2 || Name.Length > 15)
                return false;

            // Проверка, что имя начинается с заглавной буквы
            if (!char.IsUpper(Name[0]))
                return false;

            // Проверка допустимых символов и количества цифр
            int digitCount = 0;
            foreach (char c in Name)
            {
                if (char.IsDigit(c))
                {
                    digitCount++;
                    if (digitCount > 1)
                        return false;
                }
                else if (!(c >= 'A' && c <= 'Z') && !(c >= 'a' && c <= 'z'))
                {
                    return false;
                }
            }

            return true;
        }

        public string TransformSpecialty()
        {
            // Удаление ведущих и ведомых пробелов
            string transformed = Specialty.Trim();
            
            // Удаление лишних пробелов между словами
            transformed = Regex.Replace(transformed, @"\s+", " ");
            
            // Приведение к верхнему регистру
            transformed = transformed.ToUpper();
            
            // Сохранение измененного значения в поле
            Specialty = transformed;
            
            return Specialty;
        }

        public int CheckBirthday()
        {
            DateTime today = DateTime.Today;
            if (Birthday > today)
                return -1;

            int age = today.Year - Birthday.Year;
            if (Birthday.Date > today.AddYears(-age))
                age--;

            if (age < 16)
                return 0;
            else if (age >= 16 && age < 18)
                return 1;
            else if (age >= 18 && age <= 22)
                return 2;
            else
                return 3;
        }
    }
}