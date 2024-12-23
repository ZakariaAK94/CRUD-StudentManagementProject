using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using StudentAPIBusinessLayer;
using StudentDataAccessLayer;


[Route("api/[controller]")]
[ApiController]
public class StudentsAPIController : ControllerBase
{
    [HttpGet("AllStudents", Name = "GetAllStudent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<Student>> GetAllStudent()
    {
        var _Students = Student.GetAllStudents();
        if (_Students.Count == 0)
            return NotFound("List empty");
        return Ok(_Students);
    }

    [HttpGet("PassedStudents", Name = "GetPasedStudent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<Student>> GetPassedStudents()
    {
        var _Students = Student.GetPassedStudents();
        if (_Students.Count == 0) { return NotFound("No Students Passed"); }
        return Ok(_Students);
    }


    [HttpGet("Average", Name = "GetAverage")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<double> GetAverage()
    {
        var _Students = Student.GetAllStudents();
        if (_Students.Count == 0)
        {
            NotFound("Not Found");
        }

        return Ok(Student.Average());

    }

    [HttpGet("{ID}", Name = "GetStudentByID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Student> GetStudent(int ID)
    {
        if (ID < 0) { return BadRequest($"Not accept ID {ID}"); }
        Student student = Student.Find(ID);
        if (student == null)
        {
            return NotFound("Not found");
        }
        StudentDTO SDTO = student.SDTO;
        return Ok(SDTO);

    }

    [HttpPost(Name = "AddStudent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Student> AddStudent(StudentDTO newStudentDTO)
    {
        if (newStudentDTO == null || string.IsNullOrEmpty(newStudentDTO.Name) || newStudentDTO.Age <= 0 || newStudentDTO.Grade <= 0)
            return BadRequest("Invalid Student Data");
        Student student = new Student(new StudentDTO(newStudentDTO.Id, newStudentDTO.Name, newStudentDTO.Age, newStudentDTO.Grade,
                                                    newStudentDTO.Gender, newStudentDTO.Nationality));
        student.Save();
        newStudentDTO.Id = student.Id;
        return CreatedAtRoute("GetStudentByID", new { ID = newStudentDTO.Id }, newStudentDTO);
    }

    /*
    CreatedAtRoute(string routeName, object routeValues, object value);
    When you call CreatedAtRoute, you pass in:
    1 The name of the route that should be used to retrieve the newly created resource.
    2 The route parameters needed to construct the URL.
    3 The newly created resource (or part of it) as the response body.
    The framework automatically builds the URL using the provided route and parameters and adds it to the Location header of 
    the response.
   */

    [HttpPut("{ID}", Name = "UpdateStudent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult UpdateStudent(int ID, StudentDTO updateStudentDTO)
    {
        if (ID <= 0 || updateStudentDTO == null || string.IsNullOrEmpty(updateStudentDTO.Name) || updateStudentDTO.Age < 0 ||
            updateStudentDTO.Grade < 0)
        {
            return BadRequest("Invalid Data");
        }
        Student student = Student.Find(ID);
        if (student == null)
        {
            return NotFound("Student doesn't exist in the DB");
        }
        student.Name = updateStudentDTO.Name;
        student.Age = updateStudentDTO.Age;
        student.Grade = updateStudentDTO.Grade;
        student.Gender = updateStudentDTO.Gender;
        student.Nationality = updateStudentDTO.Nationality;
        if (student.Save())
            return Ok(student.SDTO);
        else
            return StatusCode(500, new { message = "Error updating Student" });

    }

    [HttpDelete("{ID}", Name = "DeleteStudent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteStudent(int ID)
    {
        if (ID <= 0)
        {
            return BadRequest("Id is invalid");
        }
        if (Student.DeleteStudent(ID))
        {
            return Ok($"Student with ID{ID} wad deleted successfully");
        }
        else
        {
            return NotFound("Deletion was failed");
        }
    }

}
