using System;

namespace Contracts.Class
{
    public class ClassesDto : BaseClass
    {
        public Guid IdClass { get; set; }
        public string Name { get; set; }
        public int ClassNumber { get; set; }
    }
}
