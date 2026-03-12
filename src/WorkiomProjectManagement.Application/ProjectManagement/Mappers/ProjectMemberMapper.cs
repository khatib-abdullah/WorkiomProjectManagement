using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using WorkiomProjectManagement.ProjectManagement.Dtos;

namespace WorkiomProjectManagement.ProjectManagement;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ProjectMemberMapper : MapperBase<ProjectMember, ProjectMemberDto>
{
    [MapProperty(nameof(ProjectMember.User.Name), nameof(ProjectMemberDto.MemberName))]
    [MapProperty(nameof(ProjectMember.User.Surname), nameof(ProjectMemberDto.MemberSurname))]
    [MapProperty(nameof(ProjectMember.User.Email), nameof(ProjectMemberDto.MemberEmail))]
    [MapProperty(nameof(ProjectMember.User.UserName), nameof(ProjectMemberDto.MemberUsername))]
    public override partial ProjectMemberDto Map(ProjectMember source);

    [MapProperty(nameof(ProjectMember.User.Name), nameof(ProjectMemberDto.MemberName))]
    [MapProperty(nameof(ProjectMember.User.Surname), nameof(ProjectMemberDto.MemberSurname))]
    [MapProperty(nameof(ProjectMember.User.Email), nameof(ProjectMemberDto.MemberEmail))]
    [MapProperty(nameof(ProjectMember.User.UserName), nameof(ProjectMemberDto.MemberUsername))]
    public override partial void Map(ProjectMember source, ProjectMemberDto destination);
}
