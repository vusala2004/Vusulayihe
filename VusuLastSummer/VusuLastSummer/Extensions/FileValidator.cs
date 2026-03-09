using VusuLastSummer.Enums;
namespace VusuLastSummer.Extensions
{
   

public static class FileValidator
    {
        public static bool CheckFileType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }

        public static bool CheckFileSize(this IFormFile file, FileSize fileSize, int size)
        {
            return fileSize switch
            {
                FileSize.KB => file.Length <= size * 1024L,
                FileSize.MB => file.Length <= size * 1024L * 1024L,
                FileSize.GB => file.Length <= size * 1024L * 1024L * 1024L,
                _ => false
            };
        }

        public async static Task<string> CreateFileAsync(this IFormFile file, params string[] roots)
        {
            // Path.GetFileName təhlükəsizlik üçün yaxşıdır
            string fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";

            // for dövrünə ehtiyac yoxdur, Path.Combine massivi birbaşa qəbul edir
            string path = Path.Combine(roots);
            path = Path.Combine(path, fileName);

            using (FileStream fileStream = new(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return fileName;
        }

        public static void DeleteFile(this string fileName, params string[] roots)
        {
            string path = Path.Combine(roots);
            path = Path.Combine(path, fileName);

            // ŞƏRT: Əgər fayl doğrudan da mövcuddursa, sil! (Yoxsa Exception verər)
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}

