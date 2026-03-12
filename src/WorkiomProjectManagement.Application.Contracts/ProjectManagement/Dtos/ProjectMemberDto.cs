using System;
using Volo.Abp.Application.Dtos;

namespace WorkiomProjectManagement.ProjectManagement.Dtos;

public class ProjectMemberDto : EntityDto
{
    public Guid ProjectId { get; set; }

    public Guid UserId { get; set; }
    public string MemberName { get; set; } = null!;
    public string MemberSurname { get; set; } = null!;
    public string MemberUsername { get; set; } = null!;
    public string MemberEmail { get; set; } = null!;
}
