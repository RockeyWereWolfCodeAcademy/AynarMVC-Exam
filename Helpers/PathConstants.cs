namespace AynarMVC_Exam.Helpers
{
    public static class PathConstants
    {
        public static string RootPath { get; set; }
        public static string TeamImagePath => Path.Combine("savedimages", "team");
    }
}
