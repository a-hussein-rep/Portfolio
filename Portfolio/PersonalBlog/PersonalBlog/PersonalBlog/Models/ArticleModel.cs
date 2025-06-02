using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalBlog.Models;

public class ArticleModel
{
    public int Id { get; set; }

    public string Title { get; set; }

    [DisplayName("Article")]
    public string Content { get; set; }

    public string Author { get; set; }

    [DisplayName("Created At")]
    public DateTime CreatedAt { get; set; }

    public string? Image { get; set; }


    [NotMapped]
    [DisplayName("Upload Image")]
    public IFormFile ImageFile { get; set; }
}
