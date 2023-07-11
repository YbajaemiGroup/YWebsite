using YApi;

namespace YApiTests
{
    public class YApiInteractorTests
    {
        [Fact]
        public void DownloadImageTest()
        {
            var interactor = new YApiInteractor("token");
            interactor.DownloadImage("img1.png", "E:\\MyProgs\\ybajaemi\\config\\console_images");
            Assert.True(File.Exists("E:\\MyProgs\\ybajaemi\\config\\console_images\\img1.png"));
        }

        [Fact]
        public void DownloadAllImagesTest()
        {
            var interactor = new YApiInteractor("token");
            interactor.DownloadAllImagesAsync("E:\\MyProgs\\ybajaemi\\config\\console_images").Wait();
            Assert.True(File.Exists("E:\\MyProgs\\ybajaemi\\config\\console_images\\img1.png"));
            Assert.True(File.Exists("E:\\MyProgs\\ybajaemi\\config\\console_images\\img2.png"));
        }
    }
}