using System;
using System.IO;

namespace FileObject
{
    public class FileObject
    {
        private string _fullPath;

        public string FileName { get { return Path.GetFileName(_fullPath); } }

        public FileObject(string path)
        {
            _fullPath = path;
        }

        public void MoveTo(string path, bool overwrite = false)
        {
            if (overwrite && File.Exists(path))
                File.Delete(path);

            File.Move(_fullPath, path);
            _fullPath = path;
        }

        public void MoveToDirectory(string folderPath, Func<string, string> rename = null, bool overwrite = false)
        {
            var path = Path.Combine(folderPath, rename != null ? rename(FileName) : FileName);
            if (overwrite && File.Exists(path))
                File.Delete(path);

            File.Move(_fullPath, path);
            _fullPath = path;
        }

        public FileObject CopyTo(string path, bool overwrite = false)
        {
            File.Copy(_fullPath, path, overwrite);
            return path.ToFileObject();
        }

        public FileObject CopyToDirectory(string folderPath, Func<string, string> rename = null, bool overwrite = false)
        {
            var path = Path.Combine(folderPath, rename != null ? rename(FileName) : FileName);
            File.Copy(_fullPath, path, overwrite);
            return path.ToFileObject();
        }

        public void Rename(Func<string, string> rename)
        {
            var folderPath = Path.GetDirectoryName(_fullPath);
            var path = Path.Combine(folderPath, rename != null ? rename(FileName) : FileName);
            File.Move(_fullPath, path);
            _fullPath = path;
        }

        public void ChangeExtension(Func<string, string> changeExtension)
        {
            var extension = Path.GetExtension(_fullPath);
            var path = Path.ChangeExtension(_fullPath, changeExtension(extension));
            File.Move(_fullPath, path);
            _fullPath = path;
        }

        public void Delete()
        {
            File.Delete(_fullPath);
        }

        public bool Exists() => File.Exists(_fullPath);
        public void Rename(string fileName) => Rename(_ => fileName);
        public void ChangeExtension(string extension) => ChangeExtension(_ => extension);
    }

    public static class FileObjectExtensionMethods
    {
        public static FileObject ToFileObject(this string path) => new FileObject(path);
    }
}
