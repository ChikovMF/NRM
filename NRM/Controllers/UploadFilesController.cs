using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
namespace NRM.Controllers
{
    public class UploadController : Controller
    {
        private AppDbContext _context { get; set; }

        public UploadController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Uploads")]
        public async Task<IActionResult> getFiles(List<IFormFile> files)
        {
            var uploadPath = $"{Directory.GetCurrentDirectory()}/wwwroot/uploads";

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var fileUrls = new List<string>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string filePath = $"{uploadPath}/{file.FileName}";
                    

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    fileUrls.Add(filePath);
                }
            }

            return Ok(new { fileUrls });
        }

       
    }
}





/*
[HttpPost]
        [Route("Uploads")]
        public async  Task<JsonResult> getFiles(IFormFile file)
        {
                
                var uploadPath = $"{Directory.GetCurrentDirectory()}/wwwroot/uploads";
                // создаем папку для хранения файлов
                Directory.CreateDirectory(uploadPath);
                string fullPath = $"{uploadPath}/{file.FileName}";
                Console.WriteLine("text");
                // сохраняем файл в папку uploads
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return new JsonResult(
                    "status : ok"
                );
        }
*/