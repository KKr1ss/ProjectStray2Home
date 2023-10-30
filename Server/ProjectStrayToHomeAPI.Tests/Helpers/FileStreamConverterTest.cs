using ProjectStrayToHomeAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectStrayToHomeAPI.Tests.Helpers
{
    public class FileStreamConverterTest
    {
        [Fact]
        public void ConvertByteArrayToFileStreamResultTest()
        {
            //Arrange
            var path = "./Assets/Images/image1.jpg";
            byte[] imageBytes = File.ReadAllBytes(path);
            string fileDownloadName = "testName";

            //Act
            var result = FileStreamConverter.ConvertByteArrayToFileStreamResult(imageBytes, fileDownloadName);

            //Assert
            Assert.Equal(fileDownloadName+".jpg", result.FileDownloadName);
        }
    }
}
