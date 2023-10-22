using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Web;

    public class FileController : Controller
    {
    [HttpGet]
    public IActionResult DownloadFile()
    {
        return View();
    }

        [HttpPost]
        public IActionResult DownloadFile(string firstName, string lastName, string fileName)
        {
            string eFileName = HttpUtility.UrlEncode(fileName);
            string contentOfFile = $"Ім'я: {firstName}\nПрізвище: {lastName}";
            byte[] bytes = Encoding.UTF8.GetBytes(contentOfFile);

            Response.Headers.Add("Content-Disposition", $"attachment; filename={eFileName}.txt");
            Response.ContentType = "text/plain";

            return File(bytes, "text/plain");
        }
    }
