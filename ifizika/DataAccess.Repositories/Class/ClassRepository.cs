using System;
using System.Collections.Generic;
using Contracts.Class;

namespace DataAccess.Repositories.Class
{
    public interface IClassRepository
    {
        IEnumerable<ClassesDto> Get();
        ClassesDto GetClass(Guid idClass);
    }

    public class ClassRepository : IClassRepository
    {
        public IEnumerable<ClassesDto> Get()
        {
            var classDtoList = new List<ClassesDto>
            {
                new ClassesDto
                {
                    ClassNumber = 12,
                    Error = false,
                    IdClass = Guid.NewGuid(),
                    Name = "12 klase"
                },
                new ClassesDto
                {
                    ClassNumber = 11,
                    Error = false,
                    IdClass = Guid.NewGuid(),
                    Name = "11 klase"
                },
                new ClassesDto
                {
                    ClassNumber = 10,
                    Error = false,
                    IdClass = Guid.NewGuid(),
                    Name = "10 klase"
                },
                new ClassesDto
                {
                    ClassNumber = 9,
                    Error = false,
                    IdClass = Guid.NewGuid(),
                    Name = "9 klase"
                }
            };
            return classDtoList;
        }

        public ClassesDto GetClass(Guid idClass)
        {
            var classesDto = new ClassesDto
            {
                ClassNumber = 12,
                Error = false,
                IdClass = Guid.NewGuid(),
                Name = "12 klase"
            };
            return classesDto;
        }
    }
}
