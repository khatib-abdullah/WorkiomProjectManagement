using System;
using System.Collections.Generic;
using System.Text.Json;

namespace WorkiomProjectManagement.ReportEngine;

public class ProjectReportRequest
{
    public Dictionary<string, object?> ExtraProperties { get; set; } = [];

    public T GetProperty<T>(string key, T defaultValue = default!)
    {
        if (!ExtraProperties.TryGetValue(key, out var raw) || raw is null)
        {
            return defaultValue;
        }

        if (raw is T value)
        {
            return value;
        }

        var targetType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

        if (raw is JsonElement element)
        {
            try
            {
                var deserialized = JsonSerializer.Deserialize(element.GetRawText(), targetType);
                return deserialized is T t ? t : defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        try
        {
            return (T)Convert.ChangeType(raw, targetType);
        }
        catch
        {
            return defaultValue;
        }
    }
}
