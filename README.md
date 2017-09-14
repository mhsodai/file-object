# file-object
Object-oriented library, for easy file move, copy, rename and delete

# Examples
If you move "C:\foo\hello.txt" to "C:\bar\"
```
var srcFile = @"C:\foo\hello.txt";
var dstDir = @"C:\bar\";

// use System.IO.File
File.Move(srcFile, Path.Combine(dstDir, Path.GetFileName(srcFile)));

// use this library
srcFile.ToFileObject()
  .MoveToDirectory(dstDir);
```

If you copy "C:\foo\hello.txt" to "D:\bar\",
but first copied "hello._txt" then rename "hello.txt"

```
var srcFile = @"C:\foo\hello.txt";
var dstDir = @"C:\bar\";

// use System.IO.File
var srcFileName = Path.GetFileName(srcFile);
var tempFileName = Path.ChangeExtension(srcFileName, "._txt");
File.Copy(srcFile, Path.Combine(dstDir, tempFileName));
File.Move(Path.Combine(dstDir, tempFileName), Path.Combine(dstDir, srcFileName));

// use this library
srcFile.ToFileObject()
  .CopyToDirectory(dstDir, rename: fileName => Path.ChangeExtension(fileName, "_txt"))
  .ChangeExtension("txt");
```