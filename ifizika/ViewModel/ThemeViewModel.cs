using System.Collections.Generic;

namespace ifizika.ViewModel
{
    public class ThemeViewModel
    {
        public string Error { get; set; }
        public List<ThemeModel> Themes { get; set; } 
    }

    public class ThemeModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
