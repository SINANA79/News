namespace NewsProject.Framework.Extensions;

public static class PathExtension
{
    #region default images

    public static string DefaultAvatar = "/img/defaults/avatar.jpg";

    #endregion

    #region uploader

    public static string UploadImage = "/img/upload/";
    public static string UploadImageServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/upload/");

    #endregion

    #region newses

    public static string NewsImage = "/content/images/news/origin/";

    public static string NewsImageServer =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/News/origin/");

    public static string NewsThumbnailImageServer = "/content/images/news/thumb/";

    public static string NewsThumbnailImageImageServer =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/news/thumb/");

    #endregion
}
