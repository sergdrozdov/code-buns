using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CodeBuns.Dotnet.Common.Validators
{
    public class FileTypeValidator : IFileValidator
    {
        public bool IsValidFileType(string fileName, List<string> allowedExtensions)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;

            foreach (var ext in allowedExtensions)
            {
                switch (ext.ToLowerInvariant())
                {
                    case ".jpg":
                    case ".jpeg":
                    case ".png":
                    case ".gif":
                    case ".bmp":
                    case ".tiff":
                    case ".webp":
                        break;
                    default:
                        return false;
                }
            }

            string fileExtension = Path.GetExtension(fileName).ToLowerInvariant();

            return allowedExtensions.Any(ext => ext.ToLowerInvariant() == fileExtension);
        }

        public bool IsImageFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            try
            {
                var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
                string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp" };

                if (!validExtensions.Contains(fileExtension))
                    return false;

                if (File.Exists(fileName))
                {
                    using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new BinaryReader(fileStream))
                        {
                            // Read the first 12 bytes of the file to check for magic numbers
                            byte[] header = reader.ReadBytes(12);

                            // Check for common image file signatures
                            if (IsJpeg(header) || IsPng(header) || IsGif(header) || IsBmp(header) || IsTiff(header) || IsWebp(header))
                            {
                                try
                                {
                                    using (var image = Image.FromFile(fileName))
                                    {
                                        return true;
                                    }
                                }
                                catch (OutOfMemoryException)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Checking magic numbers for JPEG
        private bool IsJpeg(byte[] header)
        {
            return header.Length >= 3 && header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF;
        }

        // Checking magic numbers for PNG
        private bool IsPng(byte[] header)
        {
            return header.Length >= 8 &&
                   header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47 &&
                   header[4] == 0x0D && header[5] == 0x0A && header[6] == 0x1A && header[7] == 0x0A;
        }

        // Checking magic numbers for GIF
        private bool IsGif(byte[] header)
        {
            return header.Length >= 6 &&
                   header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46 && // "GIF"
                   (header[3] == 0x38 && (header[4] == 0x37 || header[4] == 0x39) && header[5] == 0x61); // "87a" or "89a"
        }

        // Checking magic numbers for BMP
        private bool IsBmp(byte[] header)
        {
            return header.Length >= 2 && header[0] == 0x42 && header[1] == 0x4D; // "BM"
        }

        // Checking magic numbers for TIFF
        private bool IsTiff(byte[] header)
        {
            return header.Length >= 4 &&
                   ((header[0] == 0x49 && header[1] == 0x49 && header[2] == 0x2A && header[3] == 0x00) || // Little-endian
                    (header[0] == 0x4D && header[1] == 0x4D && header[2] == 0x00 && header[3] == 0x2A));  // Big-endian
        }

        // Checking magic numbers for WebP
        private bool IsWebp(byte[] header)
        {
            return header.Length >= 12 &&
                   header[0] == 0x52 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x46 && // "RIFF"
                   header[8] == 0x57 && header[9] == 0x45 && header[10] == 0x42 && header[11] == 0x50; // "WEBP"
        }

        public bool IsPhpFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            try
            {
                string fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
                if (fileExtension != ".php")
                    return false;

                if (File.Exists(fileName))
                {
                    var fileContent = File.ReadAllText(fileName);

                    string[] phpIndicators = new[]
                    {
                        "<?php", "=>", "function", "class", "echo", "$", "namespace", "use", "require", "include"
                    };

                    return phpIndicators.Any(indicator => fileContent.Contains(indicator));
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
