using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class FileUpload1
    {
        public string ErrorMessage { get; set; }
        public decimal filesize { get; set; }
        public string UploadUserFile(IFormFile file)
        { try
            {
                var supportedTypes = new[] { "txt", "doc", "docx", "pdf", "xls", "xlsx" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1); 

                if (!supportedTypes.Contains(fileExt)) {
                    ErrorMessage = "Extension Filet nuk eshte VALID - Lejohen vetem WORD/PDF/EXCEL/TXT File";
                    return ErrorMessage;
                }
                else if (file.Length > (filesize * 1024))  {
                    ErrorMessage = "File i lejuar eshte deri: " + filesize + "KB";
                    return ErrorMessage;
                }
                else {
                    ErrorMessage = "File eshte upload-u me sukses";
                    return ErrorMessage;
                }
            }
            catch (Exception ex) {
                ErrorMessage = "File i upload nuk duhet te jet i Zbrazt or Contact Admin";
                return ErrorMessage;
            }
        }
    }
}
