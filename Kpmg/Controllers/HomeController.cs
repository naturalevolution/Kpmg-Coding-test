using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Kpmg.Datas.Business;
using Kpmg.Datas.Factories;
using Kpmg.Messages;
using Kpmg.Web.Common.Extensions;

namespace Kpmg.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = Common.Application_Name;
            return View();
        }

        public ActionResult Upload()
        {
            ViewBag.Title = Common.Upload_Title;
            ViewBag.Message = Common.Upload_Message;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            ViewBag.Title = Common.Upload_Title;
            var response = new DataTransactionResponse();
            if (ModelState.IsValid && file != null && file.IsValidForUpload())
            {
                var dataTransaction = new DataTransaction();
                response = dataTransaction.Execute(SaveFile(file));
                ViewBag.Message = InformationMessage.Success_File_Uploaded;
            }
            else
            {
                ViewBag.Message = InformationMessage.Error_File_Is_Invalid;
            }
            return View(response);
        }

        public ActionResult ShowAll()
        {
            ViewBag.Title = Common.ShowAll_Title;
            return View(Repositories.Informations.FindAll());
        }

        private string SaveFile(HttpPostedFileBase file)
        {
            var extension = Path.GetExtension(file.FileName);
            var renamedImage = Server.MapPath("~/Content/Uploads/" + Guid.NewGuid().ToString("N") + extension);
            file.SaveAs(renamedImage);
            return renamedImage;
        }
    }
}