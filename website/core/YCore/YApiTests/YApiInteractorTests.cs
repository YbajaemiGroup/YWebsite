using YApi;

namespace YApiTests
{
    public class Config : IConfigInteractor
    {
        public string GetImagesLocation()
        {
            return "E:\\MyProgs\\ybajaemi\\config\\console_images";
        }

        public string GetToken()
        {
            return "token";
        }
    }

    public class YApiInteractorTests
    {
        [Fact]
        public void DownloadImageTest()
        {
            var interactor = new YApiInteractor(new Config());
            interactor.DownloadImage("img1.png", "E:\\MyProgs\\ybajaemi\\config\\console_images");
            Assert.True(File.Exists("E:\\MyProgs\\ybajaemi\\config\\console_images\\img1.png"));
        }

        [Fact]
        public void DownloadAllImagesTest()
        {
            var interactor = new YApiInteractor(new Config());
            interactor.DownloadAllImagesAsync("E:\\MyProgs\\ybajaemi\\config\\console_images").ToBlockingEnumerable();
            Assert.True(File.Exists("E:\\MyProgs\\ybajaemi\\config\\console_images\\img1.png"));
            Assert.True(File.Exists("E:\\MyProgs\\ybajaemi\\config\\console_images\\img2.png"));
        }
    }
}