using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Model;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageModelsController : ControllerBase
    {
        private readonly PaymentDetailContext _context;
        private readonly IWebHostEnvironment _env;

        public ImageModelsController(PaymentDetailContext context, IWebHostEnvironment? env)
        {
            _context = context;
            _env = env;
        }

        #region unuwanted code
        // GET: api/ImageModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageModel>>> GetImages()
        {
            return await _context.Images.ToListAsync();
        }

        // GET: api/ImageModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageModel>> GetImageModel(int id)
        {
            var imageModel = await _context.Images.FindAsync(id);

            if (imageModel == null)
            {
                return NotFound();
            }

            return imageModel;
        }

        // PUT: api/ImageModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImageModel(int id, ImageModel imageModel)
        {
            if (id != imageModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(imageModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ImageModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImageModel>> PostImageModel(ImageModel imageModel)
        {
            _context.Images.Add(imageModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImageModel", new { id = imageModel.Id }, imageModel);
        }

        // DELETE: api/ImageModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImageModel(int id)
        {
            var imageModel = await _context.Images.FindAsync(id);
            if (imageModel == null)
            {
                return NotFound();
            }

            _context.Images.Remove(imageModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImageModelExists(int id)
        {
            return _context.Images.Any(e => e.Id == id);
        }

        //    [HttpPost("upload")]
        //    public async Task<IActionResult> UploadImage(IFormFile image)
        //    {
        //        if (image == null || image.Length == 0)
        //            return BadRequest("Image file is required.");

        //        // Define your upload directory (make sure it's not null)
        //        string uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        //        // Ensure the directory exists
        //        if (!Directory.Exists(uploadDirectory))
        //            Directory.CreateDirectory(uploadDirectory);

        //        // Generate a file name (ensure this is not null)
        //        string fileName = Path.GetFileName(image.FileName);
        //        string fullPath = Path.Combine(uploadDirectory, fileName);

        //        // Save the file
        //        using (var stream = new FileStream(fullPath, FileMode.Create))
        //        {
        //            await image.CopyToAsync(stream);
        //        }

        //        return Ok(new { ImagePath = $"/images/{fileName}" });
        //    }
        #endregion



        #region image upload
        [HttpPost("uploadbytes")]
        public async Task<IActionResult> UploadImage([FromBody] byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
                return BadRequest("No image data received.");

            // Generate unique file name
            string fileName = Guid.NewGuid().ToString() + ".jpg";
            string folderPath = Path.Combine(_env.WebRootPath, "uploads");
            string filePath = Path.Combine(folderPath, fileName);

            // Ensure the uploads folder exists
            Directory.CreateDirectory(folderPath);

            // Save the image to the uploads folder
            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

            // Save file details to database
            var imageModel = new ImageModel
            {
                FileName = fileName,
                FilePath = $"/uploads/{fileName}"
            };

            _context.Images.Add(imageModel);
            await _context.SaveChangesAsync();

            return Ok(new { imageModel.Id, imageModel.FileName, imageModel.FilePath });
        }

        [HttpGet("getbyimage/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
                return NotFound("Image not found.");

            var filePath = Path.Combine(_env.WebRootPath, image.FilePath.TrimStart('/'));
            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found.");

            var imageBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var base64Image = Convert.ToBase64String(imageBytes);

            return Ok(base64Image);
        }
        #endregion
    }
}
