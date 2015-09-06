namespace RecipesHeaven.Web.Infrastructure.ImageProcessing
{
    public static class ImageConfiguration
    {
        public static int Width { get { return 800; } }

        public static int Height { get { return 300; } }


        public static string UploadedImagesPath { get { return "~/Content/UserImages/"; } }
    }
}