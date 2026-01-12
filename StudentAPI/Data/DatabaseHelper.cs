using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.IO;
using StudentAPI.Models;

namespace StudentAPI.Data
{
    public class DatabaseHelper
    {
        private string connectionString = "Data Source=students.db;";

        public DatabaseHelper()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Students (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Email TEXT NOT NULL,
                        Course TEXT NOT NULL,
                        Age INTEGER NOT NULL
                    )";

                using (var cmd = new SqliteCommand(createTableQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // CREATE
        public bool AddStudent(Student student)
        {
            try
            {
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Students (Name, Email, Course, Age) VALUES (@name, @email, @course, @age)";
                    
                    using (var cmd = new SqliteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", student.Name);
                        cmd.Parameters.AddWithValue("@email", student.Email);
                        cmd.Parameters.AddWithValue("@course", student.Course);
                        cmd.Parameters.AddWithValue("@age", student.Age);
                        
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // READ ALL
        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            try
            {
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Students";

                    using (var cmd = new SqliteCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Course = reader["Course"].ToString(),
                                Age = Convert.ToInt32(reader["Age"])
                            });
                        }
                    }
                }
            }
            catch { }

            return students;
        }

        // READ BY ID
        public Student GetStudentById(int id)
        {
            try
            {
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Students WHERE Id = @id";

                    using (var cmd = new SqliteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Student
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Course = reader["Course"].ToString(),
                                    Age = Convert.ToInt32(reader["Age"])
                                };
                            }
                        }
                    }
                }
            }
            catch { }

            return null;
        }

        // UPDATE
        public bool UpdateStudent(Student student)
        {
            try
            {
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE Students 
                                   SET Name = @name, Email = @email, Course = @course, Age = @age 
                                   WHERE Id = @id";

                    using (var cmd = new SqliteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", student.Id);
                        cmd.Parameters.AddWithValue("@name", student.Name);
                        cmd.Parameters.AddWithValue("@email", student.Email);
                        cmd.Parameters.AddWithValue("@course", student.Course);
                        cmd.Parameters.AddWithValue("@age", student.Age);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        // DELETE
        public bool DeleteStudent(int id)
        {
            try
            {
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Students WHERE Id = @id";

                    using (var cmd = new SqliteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}