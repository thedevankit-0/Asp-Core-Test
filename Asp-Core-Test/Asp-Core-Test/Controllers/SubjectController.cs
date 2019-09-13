using Asp_Core_Test.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Asp_Core_Test.Controllers
{
    #region "Subject controller with attribute routing"
    [Route("Subject")]
    public class SubjectController : Controller
    {
        //Variable declaration
        private IPhdSubjectRepository _phdRepository;

        #region "SubjectController Constructor"
        //Constructor to implement IPhdSubjectRepository Interface
        public SubjectController(IPhdSubjectRepository phdRepository) => _phdRepository = phdRepository;
        #endregion

        #region "To list all subject."
        [Route("Index")]
        public IActionResult Index() => View(_phdRepository.GetSubjectList());
        #endregion

        #region "To save subject details to database."
        //Get specific subject detail, if availabe else return empty.
        [Route("Save/{id?}")]
        [HttpGet]
        public IActionResult Save(Guid? id)
        {
            var newSubject = _phdRepository.GetSubject(id);

            if (newSubject == null)
                newSubject = new PhdSubject();

            return View(newSubject);
        }

        //Save student details.
        [Route("Save/{id?}")]
        [HttpPost]
        public IActionResult Save(PhdSubject phdSubject)
        {
            if (ModelState.IsValid)
                return RedirectToAction("index", _phdRepository.SaveSubject(phdSubject));

            return View(phdSubject);
        }
        #endregion

        #region "To get subject details by id."
        //Get details of subject by given id. 
        [Route("Details/{id?}")]
        [HttpGet]
        public IActionResult Details(Guid id) => View(_phdRepository.GetSubject(id));
        #endregion

        #region "Delete subject"
        //Delete the subject from database.
        [Route("Delete/{id?}")]
        [HttpGet]
        public IActionResult Delete(Guid? id) => View(_phdRepository.GetSubject(id));

        [Route("Delete/{id?}")]
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _phdRepository.DeleteSubject(id);
            return RedirectToAction("index");
        }
        #endregion
    }
    #endregion
}