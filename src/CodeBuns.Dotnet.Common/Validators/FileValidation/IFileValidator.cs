using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBuns.Dotnet.Common.Validators
{
    public interface IFileValidator
    {
        bool IsValidFileType(string fileName, List<string> allowedExtensions);
        bool IsImageFile(string fileName);
        bool IsPhpFile(string fileName);
    }
}
