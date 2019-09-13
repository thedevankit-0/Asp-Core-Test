using Asp_Core_Test.Models;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;
using System.DrawingCore;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using PdfSharpCore.Drawing;
using PdfDocument = PdfSharpCore.Pdf.PdfDocument;

namespace Asp_Core_Test.Controllers
{
    #region "Student controller"
    [Route("Student")]
    public class StudentController : Controller
    {
        //Variable Declaration
        private readonly IStudentRepository _studentRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        #region "StudentController Constructor"       
        //StudentController Constructor
        public StudentController(IStudentRepository studentRepository, IHostingEnvironment hostingEnvironment)
        {
            _studentRepository = studentRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region "List student deatls."
        //List all student and show search result
        [Route("/")]
        [Route("Index")]
        public IActionResult Index(string search = null)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var found = _studentRepository.SearchStudent(search);
                return View(found);
            }
            return View(_studentRepository.GetStudentList());
        }
        #endregion

        #region "To save student details."
        //Get specific student detail, if availabe else return empty.
        [Route("Save/{id?}")]
        [HttpGet]
        public IActionResult Save(Guid? id)
        {
            var newStudent = _studentRepository.GetStudent(id);

            ViewBag.PhdSubjectId = _studentRepository.GetSubjectList();

            if (newStudent == null)
                newStudent = new Student();

            return View(newStudent);
        }

        //Post student details.
        [Route("Save/{id?}")]
        [HttpPost]
        public IActionResult Save(Student student)
        {
            ViewBag.PhdSubjectId = _studentRepository.GetSubjectList();

            if (ModelState.IsValid)
                return RedirectToAction("index", _studentRepository.SaveStudent(student));

            return View(student);
        }
        #endregion

        #region "Show student details"
        //Accept id of student and show details.
        [Route("Details/{id?}")]
        [HttpGet]
        public IActionResult Details(Guid id) => View(_studentRepository.GetStudent(id));
        #endregion

        #region "Delete student details"
        //Accept id of student and show details.
        [Route("Delete/{id?}")]
        [HttpGet]
        public IActionResult Delete(Guid? id) => View(_studentRepository.GetStudent(id));

        //Delete student detail.
        [Route("Delete/{id?}")]
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _studentRepository.DeleteStudent(id);
            return RedirectToAction("index");
        }
        #endregion

        #region "Generate QRCode of student details"
        [Route("QRCode/{id?}")]
        [HttpGet]
        public ActionResult QRCode(Guid id)
        {
            StringBuilder sb = new StringBuilder(" ", 500);

            Student studentDetails = _studentRepository.GetStudent(id);

            sb.Append("Id " + studentDetails.Id);
            sb.Append("\nName " + studentDetails.FirstName + studentDetails.LastName);
            sb.Append("\nSubject " + studentDetails.PhdSubjectId);
            sb.Append("\nContsct " + studentDetails.Contact);
            sb.Append("\nEmail " + studentDetails.Email);

            string studDetails = sb.ToString();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(studDetails, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            // Save generated QR image file.
            qrCodeImage.Save("D:\\Ankitkumar-Singh\\Asp-Core-Mvc\\test finally done\\Asp-Core-Test\\Asp-Core-Test\\wwwroot\\images\\qrcode.png");

            return View(studentDetails);
        }
        #endregion

        #region "Export student details to PDF"
        [Route("ExportToPDF")]
        [HttpGet]
        public FileResult ExportToPDF(Guid id)
        {
            const string facename = "Cambria";

            PdfDocument document = new PdfDocument();
            PdfSharpCore.Pdf.PdfPage page = document.AddPage();

            document.Info.Title = "Student Information";
            document.Info.Author = "Ankitkumar Singh";
            document.Info.Subject = "Student Card";
            document.Info.Keywords = "student-card";

            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont(facename, 20, XFontStyle.BoldItalic);

            Student studentDetails = _studentRepository.GetStudent(id);

            XFont fontRegular = new XFont(facename, 14, XFontStyle.Regular);
            XFont fontBold = new XFont(facename, 14, XFontStyle.Bold);

            gfx.DrawString("Student Card", font, XBrushes.Black, new XRect(0, 40, page.Width, page.Height), XStringFormats.TopCenter);
            gfx.DrawString("Student Id : " + studentDetails.Id, fontRegular, XBrushes.Black, 200, 140);
            gfx.DrawString("Name : " + studentDetails.FirstName + " " + studentDetails.LastName, fontBold, XBrushes.Black, 200, 165);
            gfx.DrawString("Contact : " + studentDetails.Contact, fontBold, XBrushes.Black, 200, 190);
            gfx.DrawString("Subject Id : " + studentDetails.PhdSubjectId, fontBold, XBrushes.Black, 200, 215);
            gfx.DrawString("Subject Name : " + studentDetails.phdSubject.Name, fontBold, XBrushes.Black, 200, 240);

            XImage image = XImage.FromFile("D:\\Ankitkumar-Singh\\Asp-Core-Mvc\\test finally done\\Asp-Core-Test\\Asp-Core-Test\\wwwroot\\images\\qrcode.png");
            gfx.DrawImage(image, 20, 120, 150, 150);

            const string filename = "UserIdentityCard.pdf";
            document.Save(filename);
            string ReportURL = "UserIdentityCard.pdf";
            byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
            return File(FileBytes, "application/pdf");

        }
        #endregion
    }
    #endregion
}