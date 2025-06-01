using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalBlog.Models;

public class ArticleModel
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public string Author { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Image { get; set; }


    [NotMapped]
    public IFormFile ImageFile { get; set; }
}
