using System.Collections.Generic;

namespace ifizika.ViewModel
{
    public class CategoryViewModel
    {
        public string Error { get; set; }
        public List<CategoryModel> Categories { get; set; } 
    }

    public class CategoryModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
