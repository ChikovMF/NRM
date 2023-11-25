using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
namespace NRM.Controllers
{
    public class UploadController : ControllerBase
    {
        private AppDbContext _context { get; set; }

        public UploadController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Uploads")]
        [DisableRequestSizeLimit,RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue, ValueLengthLimit = int.MaxValue)]
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
        [RequestSizeLimit(int.MaxValue)]
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
                        var buffer = new byte[8192]; // размер буффера.
                        int bytesRead;
                        
                        using (var inputStream = file.OpenReadStream())
                        {
                            while ((bytesRead = await inputStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                            {
                                await fileStream.WriteAsync(buffer, 0, bytesRead);
                            }
                        }
                    }

                    fileUrls.Add(filePath);
                }
            }

            return Ok(new { fileUrls });
        }

        */

        