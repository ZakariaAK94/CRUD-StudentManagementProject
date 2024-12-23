using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using StudentDataAccessLayer;

namespace StudentAPIBusinessLayer
{
    public class Student
    {
        public enum enMode { AddNew = 0, Update = 1 };

        public enMode Mode = enMode.AddNew;
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }

        public byte Gender { get; set; }
        public string Nationality { get; set; }

        public StudentDTO SDTO
        {
            get { return new StudentDTO(this.Id, this.Name, this.Age, this.Grade, this.Gender, this.Nationality); }
        }

        public Student(StudentDTO SDTO, enMode cMode = enMode.AddNew)
        {
            this.Id = SDTO.Id;
            this.Name = SDTO.Name;
            this.Age = SDTO.Age;
            this.Grade = SDTO.Grade;
            this.Nationality = SDTO.Nationality;
            this.Gender = SDTO.Gender;
            Mode = cMode;
        }

        private bool _AddNewStudent()
        {
            this.Id = StudentData.AddNewStudent(SDTO);
            return (this.Id != -1);
        }

        private bool _UpdateStudent()
        {

            return StudentDataAccessLayer.StudentData.UpdateStudent(SDTO);

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewStudent())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateStudent();

            }

            return false;
        }

        public static List<StudentDTO> GetAllStudents()
        {
            return StudentDataAccessLayer.StudentData.GetAllStudents();

        }

        public static List<StudentDTO> GetPassedStudents()
        {
            return StudentDataAccessLayer.StudentData.GetPassedStudents();

        }

        public static bool DeleteStudent(int ID)
        {
            return StudentDataAccessLayer.StudentData.DeleteStudent(ID);
        }

        public static double Average()
        {
            return StudentDataAccessLayer.StudentData.GetAverage();
        }

        public static Student Find(int ID)
        {
            StudentDTO SDTO = StudentData.GetStudentByID(ID);
            if (SDTO == null)
            {
                return null;
            }

            return new Student(SDTO, enMode.Update);
        }

    }
}
