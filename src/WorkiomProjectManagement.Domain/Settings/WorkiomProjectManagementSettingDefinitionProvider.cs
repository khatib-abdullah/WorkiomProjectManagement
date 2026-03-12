using Volo.Abp.Settings;

namespace WorkiomProjectManagement.Settings;

public class WorkiomProjectManagementSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(WorkiomProjectManagementSettings.MySetting1));
    }
}
