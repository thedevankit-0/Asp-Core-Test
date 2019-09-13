using Asp_Core_Test.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcFirstApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asp_Core_Test.Repository
{
    public class StudentRepository : IStudentRepository
    {
        //Variable declarations
        private readonly AppDBContext context;

        #region "StudentRepository() Constructor"
        //StudentRepository() constructor
        public StudentRepository(AppDBContext context) => this.context = context;
        #endregion

        #region "GetStudentList() lists all students"
        //GetStudentList() listing the all students from student table.
        public IEnumerable<Student> GetStudentList() => context.Student.Include(e => e.phdSubject).ToList();
        #endregion

        #region "GetStudent() Method returns Students Details"
        //GetStudent() Method returns the particular students details
        public Student GetStudent(Guid? Id) => context.Student.Include(e => e.phdSubject).SingleOrDefault(e => e.Id == Id);
        #endregion

        #region "DeleteStudent() Deletes Student Record"
        //DeleteStudent() method deletes the student's record from student table
        public Student DeleteStudent(Guid Id)
        {
            Student student = context.Student.Find(Id);
            if (student != null)
            {
                context.Student.Remove(student);
                context.SaveChanges();
            }
            return student;
        }
        #endregion

        #region "SaveStudent() method Adds/Edits student record"
        //SaveStudent() method add/edit student's record
        public Student SaveStudent(Student student)
        {
            if (student != null)
            {
                if (student.Id == Guid.Empty)
                    context.Student.Add(student);
                else
                {
                    Student saveStudent = context.Student.FirstOrDefault(e => e.Id == student.Id);
                    saveStudent.FirstName = student.FirstName;
                    saveStudent.LastName = student.LastName;
                    saveStudent.Address = student.Address;
                    saveStudent.Email = student.Email;
                    saveStudent.Contact = student.Contact;
                    saveStudent.Gender = student.Gender;
                    saveStudent.Year = student.Year;
                    saveStudent.PhdSubjectId = student.PhdSubjectId;
                }
                context.SaveChanges();
            }

            return student;
        }
        #endregion

        #region "GetSubjectList() Method"
        //GetSubjectList() method lists the all phd-subjects from the PhdSubject table
        public List<SelectListItem> GetSubjectList()
        {
            return context.PhdSubject.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
        }
        #endregion

        #region "Search student"
        public List<Student> SearchStudent(String search)
        {
            return context.Student.Where(s => s.FirstName.Contains(search) || s.LastName.Contains(search)).ToList();
        }
        #endregion
    }
}
