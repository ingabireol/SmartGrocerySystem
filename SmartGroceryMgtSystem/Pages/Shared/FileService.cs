
//using Microsoft.AspNetCore.Hosting;

//namespace SmartGroceryMgtSystem.Pages.Shared
//{
//    public class FileService
//    {
//        private readonly IWebHostEnvironment _webHostEnvironment;
//        public FileService(IWebHostEnvironment webHostEnvironment)
//        {
//            _webHostEnvironment = webHostEnvironment;
//        }
//    }
//    public async Task<string> SaveFile(IFormFile file,string direcotry, string[] allowedExtensions)
//    {
//        var wwwPath = _webHostEnvironment.WebRootPath;
//        var path = Path.Combine(wwwPath, direcotry);
//        if (!Directory.Exists(path))
//        {
//            Directory.CreateDirectory(path);
//        }
//        var extension = Path.GetExtension(file.FileName);
//        if (!allowedExtensions.Contains(extension))
//        {
//            throw new InvalidOperationException($"only {string.Join(",", allowedExtensions)} are allowed");

//        }
//        var newFileName = $"{Guid.NewGuid()}{extension}";
//        var fileWithPath = Path.Combine(path, newFileName);

//        using var fileStream = new FileStream(fileWithPath, FileMode.Create);
//        await file.CopyToAsync(fileStream);
//        return newFileName;

//    }
//    public void DeleteFile(string fileName, string directory)
//    {
//        var fileWithPath = Path.Join(_webHoEnvironment.WebRootPath, directory, fileName);
//        if (!Path.Exists(fileWithPath))
//        {
//            throw new FileNotFoundException($"{fileName} does not exists");
//        }
//        File.Delete(fileWithPath);

//    }

//} 
