using Hostele.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Hostele.Controllers;

public class FileController: Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public ActionResult Index(IFormFile? postedFile, String? cipher, int key)
    {
        if (postedFile == null || postedFile.Length <= 0) return View();

        var filePath = Path.GetTempFileName();
        
        if (!User.IsInRole("Admin"))
        {
            if (!String.IsNullOrEmpty(cipher))
            {
                if (!Cipher.Decode(cipher, key))
                {
                    ViewBag.ErrorMessage = "Cipher is incorrect";
                    return View();
                }
                
                using (var stream = System.IO.File.Create(filePath))
                {
                    postedFile.CopyTo(stream);
                }
                ViewBag.Message = "File uploaded successfully.";
                return View();
            }
            
            ViewBag.ErrorMessage = "Can't upload file";
            return View();
        }

        using (var stream = System.IO.File.Create(filePath))
        {
            postedFile.CopyTo(stream);
        }
        ViewBag.Message = "File uploaded successfully.";
        return View();
    }
}