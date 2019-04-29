using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reply.UpLoad.Data;
using Reply.UpLoad.Models;

namespace Reply.UpLoad.Controllers
{
	//[Produces("application/json")]
	//[Route("api/images")]
	public class UpLoadImageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public UpLoadImageController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: UpLoadImage
        public async Task<IActionResult> Index()
        {
            return View(await _context.UpLoadImages.ToListAsync());
        }

        // GET: UpLoadImage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.UpLoadImages.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: UpLoadImage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UpLoadImage/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] UpLoadImageVM upLoadImageVM)
        {
            if (ModelState.IsValid)
            {
                var imageUpLoad = new UpLoadImage
                {
                    Name = upLoadImageVM.Name,
                    Description = upLoadImageVM.Description
                };

                using(var memoryStream = new MemoryStream())
                {
                    await upLoadImageVM.Image.CopyToAsync(memoryStream);
                    imageUpLoad.Image = memoryStream.ToArray();
                }
                _context.Add(imageUpLoad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(upLoadImageVM);
        }

        // GET: UpLoadImage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upLoadImage = await _context.UpLoadImages.SingleOrDefaultAsync(m => m.Id == id);
            if (upLoadImage == null)
            {
                return NotFound();
            }

            var vm = new UpLoadImageVM
            {
                Name = upLoadImage.Name,
                Description = upLoadImage.Description
            };

            return View(vm);
        }

        // POST: UpLoadImage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] UpLoadImageVM upLoadImageVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here
                    //var upLoadImage = await _context.UpLoadImages.SingleOrDefaultAsync(m => m.Id == id);
                    var upLoadImage = await _context.UpLoadImages.SingleOrDefaultAsync(m => m.Id == id);
                    //var filePath = Path.Combine(_environment.ContentRootPath, @"Uploads", upLoadImageVM.Image.FileName);

                    //using (var stream = new FileStream(filePath, FileMode.Create))
                    //{
                    //    await upLoadImageVM.Image.CopyToAsync(stream);
                    //}


                    upLoadImage.Name = upLoadImageVM.Name;
                    upLoadImage.Description = upLoadImageVM.Description;

                    using (var memoryStream = new MemoryStream())
                    {
                        await upLoadImageVM.Image.CopyToAsync(memoryStream);
                        upLoadImage.Image = memoryStream.ToArray();
                    }

                    _context.Update(upLoadImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(upLoadImageVM);
        }

        // GET: UpLoadImage/Delete/5
        public async Task<IActionResult>  Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upLoadImage = await _context.UpLoadImages
                .SingleOrDefaultAsync(m => m.Id == id);
            if (upLoadImage == null)
            {
                return NotFound();
            }

            return View(upLoadImage);
        }

        // POST: UpLoadImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                var upLoadImage = await _context.UpLoadImages.SingleOrDefaultAsync(m => m.Id == id);
                _context.UpLoadImages.Remove(upLoadImage);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
            return _context.UpLoadImages.Any(e => e.Id == id);
        }
    }
}