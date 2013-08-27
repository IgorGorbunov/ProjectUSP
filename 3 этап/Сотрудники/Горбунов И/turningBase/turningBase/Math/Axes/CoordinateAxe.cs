/// <summary>
/// Абстрактный класс математических координат по оси.
/// </summary>
public abstract class CoordinateAxe
{
    protected double Value;

    /// <summary>
    /// Возвращает тип оси.
    /// </summary>
    public virtual CoordinateConfig.Type Type
    {
        get { return CoordinateConfig.Type.Null; }
    }

    /// <summary>
    /// Возвращает строку, которая представляет текущий объект.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Type.ToString("F");
    }
}
