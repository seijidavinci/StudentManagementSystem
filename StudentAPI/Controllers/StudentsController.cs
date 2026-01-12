using Microsoft.AspNetCore.Mvc;
using StudentAPI.Data;
using StudentAPI.Models;
using System.Collections.Generic;

namespace StudentAPI.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly DatabaseHelper _db;

        public StudentsController()
        {
            _db = new DatabaseHelper();
        }

        // GET: api/students
        [HttpGet]
        public ActionResult<List<Student>> GetAllStudents()
        {
            return Ok(_db.GetAllStudents());
        }

        // GET: api/students/5
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = _db.GetStudentById(id);
            if (student == null)
                return NotFound();
            
            return Ok(student);
        }

        // POST: api/students
        [HttpPost]
        public ActionResult<Student> CreateStudent([FromBody] Student student)
        {
            if (_db.AddStudent(student))
                return Ok(new { message = "Student created successfully" });
            
            return BadRequest(new { message = "Failed to create student" });
        }

        // PUT: api/students/5
        [HttpPut("{id}")]
        public ActionResult UpdateStudent(int id, [FromBody] Student student)
        {
            student.Id = id;
            if (_db.UpdateStudent(student))
                return Ok(new { message = "Student updated successfully" });
            
            return BadRequest(new { message = "Failed to update student" });
        }

        // DELETE: api/students/5
        [HttpDelete("{id}")]
        public ActionResult DeleteStudent(int id)
        {
            if (_db.DeleteStudent(id))
                return Ok(new { message = "Student deleted successfully" });
            
            return BadRequest(new { message = "Failed to delete student" });
        }
    }
}