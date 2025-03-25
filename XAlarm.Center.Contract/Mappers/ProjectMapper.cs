using Riok.Mapperly.Abstractions;
using XAlarm.Center.Contract.Projects;
using XAlarm.Center.Domain.Projects;

namespace XAlarm.Center.Contract.Mappers;

[Mapper]
public partial class ProjectMapper
{
    public static partial ProjectDto ToDto(Project entity);
    public static partial Project ToEntity(ProjectDto dto);
}