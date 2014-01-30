using NXOpen.Assemblies;

/// <summary>
/// Общий класс УСП элементов.
/// </summary>
public class UadElement
{
    /// <summary>
    /// Возвращает компонент элемента.
    /// </summary>
    public Component ElementComponent
    {
        get
        {
            return _component;
        }
    }
    /// <summary>
    /// Возвращает обозначение элемента.
    /// </summary>
    public string Title
    {
        get
        {
            return _title;
        }
    }

    private readonly Component _component;
    private string _title;

    public UadElement(Component component)
    {
        _component = component;
        SetTitle();
    }

    private void SetTitle()
    {
        string componentName = ElementComponent.Name;
        string[] split = componentName.Split('_');
        string nameWithEx = split[split.Length - 1];
        split = nameWithEx.Split('.');
        string[] nameWithRevision = split[0].Split(' ');
        _title = nameWithRevision[0];
    }
}

