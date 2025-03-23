using XAlarm.Center.Contract.Abstractions;
using XAlarm.Center.Contract.Projects;
using XAlarm.Center.Domain.Abstractions;
using XAlarm.Center.Domain.Projects;

namespace XAlarm.Center.Contract.Mappers;

public static class MapperExtensions
{
    public static TDestination MapTo<TDestination>(this object? source)
    {
        switch (source)
        {
            case Entity:
                switch (source)
                {
                    case Project entity:
                        return (TDestination)Convert.ChangeType(ProjectMapper.ToDto(entity), typeof(TDestination));
                }

                break;
            case Dto:
                switch (source)
                {
                    case ProjectDto dto:
                        return (TDestination)Convert.ChangeType(ProjectMapper.ToEntity(dto), typeof(TDestination));
                }

                break;
        }

        return default!;
    }
}