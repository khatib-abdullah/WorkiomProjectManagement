using WorkiomProjectManagement.Samples;
using Xunit;

namespace WorkiomProjectManagement.EntityFrameworkCore.Domains;

[Collection(WorkiomProjectManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<WorkiomProjectManagementEntityFrameworkCoreTestModule>
{

}
