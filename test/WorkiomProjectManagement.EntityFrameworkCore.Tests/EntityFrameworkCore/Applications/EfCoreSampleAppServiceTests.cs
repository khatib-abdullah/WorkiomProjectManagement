using WorkiomProjectManagement.Samples;
using Xunit;

namespace WorkiomProjectManagement.EntityFrameworkCore.Applications;

[Collection(WorkiomProjectManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<WorkiomProjectManagementEntityFrameworkCoreTestModule>
{

}
