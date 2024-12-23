using Microsoft.Data.SqlClient;
using System.Data;

namespace StudentDataAccessLayer
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }

        public byte Gender { get; set; }
        public string Nationality { get; set; }
        public StudentDTO(int ID, string Name, int Age, int Grade, byte Gender, string Nationality)
        {
            this.Id = ID;
            this.Name = Name;
            this.Age = Age;
            this.Grade = Grade;
            this.Nationality = Nationality;
            this.Gender = Gender;
        }
    }
    public class StudentData
    {
        static string _ConnectionString = "Server=.;Database=Students;User Id=sa;Password=sa123456;Encrypt=False;" +
            "TrustServerCertificate=True;Connection Timeout=30;";

        public static List<StudentDTO> GetAllStudents()
        {
            var Students = new List<StudentDTO>();
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllStudents", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Students.Add(new StudentDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade")),
                                reader.GetByte(reader.GetOrdinal("Gender")),
                                reader.GetString(reader.GetOrdinal("Nationality"))


                            ));
                        }
                    }

                }
                return Students;
            }
        }

        public static List<StudentDTO> GetPassedStudents()
        {

            List<StudentDTO> Students = new List<StudentDTO>();
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetPassedStudents", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Students.Add(new StudentDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade")),
                                reader.GetByte(reader.GetOrdinal("Gender")),
                                reader.GetString(reader.GetOrdinal("Nationality"))
                            ));
                        }
                    }

                }

                return Students;
            }

        }

        public static double GetAverage()
        {
            double average = 0;
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAverageGrade", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null && double.TryParse(result.ToString(), out double Average))
                    {
                        return Average;
                    }
                    else
                        average = 0;
                }

            }
            return average;
        }

        public static StudentDTO GetStudentByID(int ID)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetStudentById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentId", ID);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new StudentDTO
                                (
                                   reader.GetInt32(reader.GetOrdinal("Id")),
                                   reader.GetString(reader.GetOrdinal("Name")),
                                   reader.GetInt32(reader.GetOrdinal("Age")),
                                   reader.GetInt32(reader.GetOrdinal("Grade")),
                                    reader.GetByte(reader.GetOrdinal("Gender")),
                                    reader.GetString(reader.GetOrdinal("Nationality"))
                                );
                        }
                        else
                            return null;
                    }
                }
            }
        }

        public static int AddNewStudent(StudentDTO newStudent)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddStudent", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", newStudent.Name);
                    command.Parameters.AddWithValue("@Age", newStudent.Age);
                    command.Parameters.AddWithValue("@Grade", newStudent.Grade);
                    command.Parameters.AddWithValue("@Gender", newStudent.Gender);
                    command.Parameters.AddWithValue("@Nationality", newStudent.Nationality);
                    var outputIdParam = new SqlParameter("@NewStudentId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)outputIdParam.Value;
                }
            }

        }

        public static bool UpdateStudent(StudentDTO updateStudent)
        {

            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateStudent", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentID", updateStudent.Id);
                    command.Parameters.AddWithValue("@Name", updateStudent.Name);
                    command.Parameters.AddWithValue("@Age", updateStudent.Age);
                    command.Parameters.AddWithValue("@Grade", updateStudent.Grade);
                    command.Parameters.AddWithValue("@Gender", updateStudent.Gender);
                    command.Parameters.AddWithValue("@Nationality", updateStudent.Nationality);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }
        public static bool DeleteStudent(int StudentID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(_ConnectionString);

            string query = @"Delete Students 
                                where Id = @StudentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@StudentID", StudentID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }
    }

}


