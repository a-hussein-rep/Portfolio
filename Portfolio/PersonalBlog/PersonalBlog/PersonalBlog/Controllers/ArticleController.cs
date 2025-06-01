using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PersonalBlog.Database;
using PersonalBlog.Models;

namespace PersonalBlog.Controllers;

public class ArticleController : Controller
{
    private readonly BlogDbContext dbContext;

    public ArticleController(BlogDbContext context)
    {
        dbContext = context;
    }

    public async Task<IActionResult> Index()
    {
        return RedirectToAction("Index", "Home");
    }

    // GET: Article/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var articleModel = await dbContext.Articles
            .FirstOrDefaultAsync(m => m.Id == id);
        if (articleModel == null)
        {
            return NotFound();
        }

        return View(articleModel);
    }

    // GET: Article/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Article/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ArticleModel articleModel)
    {
        if (ModelState.IsValid)
        {
            if(articleModel.ImageFile.Length > 0)
            {
                using var memoryStream = new MemoryStream();

                if(memoryStream.Length > 2097152) // Check if the file size is less than 2MB
                {
                    ModelState.AddModelError("ImageFile", "Image size must be less than 2MB.");
                    return View(articleModel);
                }

                await articleModel.ImageFile.CopyToAsync(memoryStream);
                articleModel.Image = Convert.ToBase64String(memoryStream.ToArray());
            }
            else
            {
                articleModel.Image = null; // Handle the case where no image is uploaded
            }

            dbContext.Add(articleModel);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(articleModel);
    }

    // GET: Article/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var articleModel = await dbContext.Articles.FindAsync(id);
        if (articleModel == null)
        {
            return NotFound();
        }
        return View(articleModel);
    }

    // POST: Article/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ArticleModel articleModel)
    {
        if (id != articleModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                if (articleModel.ImageFile.Length > 0)
                {
                    using var memoryStream = new MemoryStream();

                    if (memoryStream.Length > 2097152) // Check if the file size is less than 2MB
                    {
                        ModelState.AddModelError("ImageFile", "Image size must be less than 2MB.");
                        return View(articleModel);
                    }

                    await articleModel.ImageFile.CopyToAsync(memoryStream);
                    articleModel.Image = Convert.ToBase64String(memoryStream.ToArray());
                }

                dbContext.Update(articleModel);
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleModelExists(articleModel.Id))
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
        return View(articleModel);
    }

    // GET: Article/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var articleModel = await dbContext.Articles
            .FirstOrDefaultAsync(m => m.Id == id);
        if (articleModel == null)
        {
            return NotFound();
        }

        return View(articleModel);
    }

    // POST: Article/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var articleModel = await dbContext.Articles.FindAsync(id);
        if (articleModel != null)
        {
            dbContext.Articles.Remove(articleModel);
        }

        await dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ArticleModelExists(int id)
    {
        return dbContext.Articles.Any(e => e.Id == id);
    }
}
