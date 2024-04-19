using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

    public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
    {
        _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider ?? throw new ArgumentNullException(nameof(fileExtensionContentTypeProvider));
    }

    [HttpGet("{fileId}")]
    public IActionResult GetFile(int fileId)
    {
        var pathToFile = "creating-the-api-and-returning-resources-slides.pdf";
        if (!System.IO.File.Exists(pathToFile))
        {
            return NotFound();
        }
        if (!_fileExtensionContentTypeProvider.TryGetContentType(pathToFile, out var contentType))
        {
            contentType = "application/octet-stream";
        }

        var fileStream = System.IO.File.ReadAllBytes(pathToFile);
        return File(fileStream, "text/plain",Path.GetFileName(pathToFile));
    }
}
