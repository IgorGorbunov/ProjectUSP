/// <summary>
/// Класс настроек координат системы.
/// </summary>
public static class CoordinateConfig
{
    public enum Type
    {
        Null = 0,
        X = 1,
        Y = 2,
        Z = 23
    }

    public static CoordinateAxe[] GetSurfaceAxes(CoordinateAxe axe)
    {
        CoordinateAxe[] axes = new CoordinateAxe[2];
        switch (axe.Type)
        {
            case Type.X:
                axes[0] = new YAxe();
                axes[1] = new ZAxe();
                break;
            case Type.Y:
                axes[0] = new XAxe();
                axes[1] = new ZAxe();
                break;
            case Type.Z:
                axes[0] = new XAxe();
                axes[1] = new YAxe();
                break;
            default:
                axes[0] = null;
                axes[1] = null;
                break;
        }
        return axes;
    }
}