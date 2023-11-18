using Microsoft.AspNetCore.Mvc;

namespace ProjectStrayToHomeAPI.Helpers
{
    public static class FileStreamConverter
    {
        public static FileStreamResult ConvertByteArrayToFileStreamResult(byte[] bytes, string fileDownloadName)
        {
            var stream = new MemoryStream(bytes, 0, bytes.Length);
            var result = new FileStreamResult(stream, "image/jpeg")
            {
                FileDownloadName = fileDownloadName + ".jpg"
            };
            return result;
        }
    }
}
