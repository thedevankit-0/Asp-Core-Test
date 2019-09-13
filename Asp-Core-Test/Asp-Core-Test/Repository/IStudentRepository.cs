using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Asp_Core_Test.Models
{
    #region "Srudent interface"
    public interface IStudentRepository
    {
        //To list student.
        IEnumerable<Student> GetStudentList();

        //To get specific student by id.
        Student GetStudent(Guid? Id);

        //To delete specific student by id.
        Student DeleteStudent(Guid Id);

        //To save student deatils.
        Student SaveStudent(Student student);

        List<Student> SearchStudent(String search);

        //To list of subjects.
        List<SelectListItem> GetSubjectList();
    }
    #endregion
}