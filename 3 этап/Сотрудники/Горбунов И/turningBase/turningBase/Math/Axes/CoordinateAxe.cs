/// <summary>
/// Абстрактный класс математических координат по оси.
/// </summary>
public abstract class CoordinateAxe
{
    protected double Value;

    public virtual CoordinateConfig.Type Type
    {
        get { return CoordinateConfig.Type.Null; }
    }
}
