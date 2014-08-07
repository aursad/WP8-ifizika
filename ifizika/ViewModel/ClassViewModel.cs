using System.Collections.Generic;

namespace ifizika.ViewModel
{
    public class ClassViewModel
    {
        public string Error { get; set; }
        public List<ClassModel> Classes { get; set; } 
    }

    public class ClassModel
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public string Name { get; set; }
    }
}
