using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс кондукторных втулок.
/// </summary>
public class JigSleeve : UspElement
{
    /// <summary>
    /// Возвращает грань для центрирования втулки в кондукторной планке.
    /// </summary>
    public Face SleeveFace
    {
        get
        {
            if (_cyllindricalFace == null)
            {
                SetSleeveFace();
            }
            return _cyllindricalFace;
        }
    }

    private Face _cyllindricalFace;

    protected JigSleeve(Component component) : base(component)
    {
        
    }

    /// <summary>
    /// Устанавливает цилиндрическую грань для установки в кондукторную планку, использовать после Replacement.
    /// </summary>
    public void SetSleeveFace()
    {
        _cyllindricalFace = GetFace(Config.SleeveFaceName);
    }

}

