/// <summary>
/// Класс математических координат оси Z.
/// </summary>
public sealed class ZAxe : CoordinateAxe
{
    public override CoordinateConfig.Type Type
    {
        get { return CoordinateConfig.Type.Z; }
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса математических координат оси X 
    /// по координатам (0, 0, 0).
    /// </summary>
    public ZAxe()
        : this(0.0)
    {
        
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса математических координат оси Z 
    /// по заданным координатам.
    /// </summary>
    /// <param name="value">Координаты оси Z.</param>
    public ZAxe(double value)
    {
        Value = value;
    }
}

