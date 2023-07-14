using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SierraLibraryProject
{
    public class ImageHelper
    {
        public static string SaveUploadedFile(string path, HttpPostedFileBase file1)
        {
            //如果沒有上傳檔案或檔案室空的，就不處理，傳回string empty
            if (file1 == null || file1.ContentLength == 0) return string.Empty;
            //取得上傳檔案的附檔名
            string ext = System.IO.Path.GetExtension(file1.FileName);
            //如果附檔名不在允許的範圍哩，表示上傳不合理的檔案類型，就不處哩，傳回string.empty
            string[] allowedExts = new string[] { ".jpg", ".jpeg", ".png", ".tif" };
            if (allowedExts.Contains(ext.ToLower()) == false) return string.Empty;
            //生成一個不會重複的檔名
            string newFileName = Guid.NewGuid().ToString("N") + ext;
            string fullName = System.IO.Path.Combine(path, newFileName);
            //將上傳檔案存放到指定位置
            file1.SaveAs(fullName);
            //傳回存放的檔名
            return newFileName;

        }
    }
}
