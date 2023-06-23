using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCore.Data.OS;

namespace YCoreTests
{
    public class ImagesOperatorTest
    {
        private ImagesOperator imagesOperator;

        public ImagesOperatorTest()
        {
            imagesOperator = new ImagesOperator("E:\\MyProgs\\ex");
        }

        [Fact]
        public void GetImageTest()
        {
            byte[]? imageData = imagesOperator.GetImage("1.png");
            Assert.NotNull(imageData);
        }

        [Fact]
        public void SaveImageTest()
        {
            byte[]? imageData = imagesOperator.GetImage("1.png");
            Assert.NotNull(imageData);
            Assert.NotNull(imageData);
            Assert.True(imagesOperator.SaveImage("2.png", imageData));
        }

        [Fact]
        public void DeleteImageTest()
        {
            byte[]? imageData = imagesOperator.GetImage("1.png");
            Assert.NotNull(imageData);
            imagesOperator.SaveImage("3.png", imageData);
            Assert.True(imagesOperator.DeleteImage("3.png"));
            Assert.False(File.Exists("E:\\MyProgs\\ex\\3.png"));
        }
    }
}
