namespace Adarsh_Studio.App_Code
{
    public class FileManager
    {
        internal IFormFile  FileObject { get; set; }
        internal string FileName { get; set; }
        internal string FileExtension {  get; set; }
        internal string FolderName {  get; set; }
        internal float FileSizeInKB { get; set; }
        private string Result;
        internal string[] AllowedExtensions { get; set; }

        internal float MaxAllowedFileSizeInKB { get; set; }
        public FileManager()
        {
            FileObject = null;
            FolderName = "Uploads";
            FileName = Path.GetRandomFileName();
            AllowedExtensions = new string[] { ".JPG", ".PNG", ".JPEG", ".MP3", ".MP4",".JFIF",".DOC",".TXT",".PDF",".DOCX",".XLS",".GIG" };
            MaxAllowedFileSizeInKB = 500;
            Result =string.Empty;
         }

        private string ValidateMyFile()
        {
            if (FileObject != null)
            {
                FileExtension=FileObject.FileName.Substring(FileObject.FileName.LastIndexOf('.'));
                FileName = FileObject.FileName.Substring(0, FileObject.FileName.LastIndexOf(".") - 1) + "_" + FileName + FileExtension;
                //validate file type
                foreach (string ext in AllowedExtensions)
                {
                    if (ext.ToUpper().Equals(FileExtension.ToUpper()) == true)
                    {
                        Result = "SUCCESS";
                        break;
                    }
                }
                if (Result != "SUCCESS")
                {
                    Result = "Invalid file type. Only " + AllowedExtensions.ToString() + "are allowed.";
                }
                else
                {
                    //validate file size
                    FileSizeInKB = FileObject.Length / 1024;
                    if (FileSizeInKB <= MaxAllowedFileSizeInKB)
                    {
                        Result = "SUCCESS";
                    }

                    else
                    {
                        Result = "File Size is too large. Max allowed file Size is: " + MaxAllowedFileSizeInKB + "KB";
                    }
                }
           }
              else
                Result = "Please choose a file.";
            return Result;
        }


        internal string UploadMyFile()
        {
            Result = ValidateMyFile();
            try
            {
                if(Result.Equals("SUCCESS")==true)
                {
                    string FolderPath = "wwwroot/" + FolderName;
                    if (!Directory.Exists(FolderPath)) Directory.CreateDirectory(FolderPath);
                    FileStream fs = new FileStream(FolderPath + "/" + FileName, FileMode.Create);
                    FileObject.CopyTo(fs);
                    fs.Close();
                    Result = "SUCCESS";
                }
            }
            catch(Exception ex)
            {
                Result = "FAIL";
                Console.WriteLine("Error in File Uploading: " + ex.Message);
            
            }
            return Result;
        }
    }
}
