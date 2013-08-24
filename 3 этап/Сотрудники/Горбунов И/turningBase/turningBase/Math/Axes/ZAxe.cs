/// <summary>
/// Класс математических координат оси Z.
/// </summary>
public sealed class PointZAxe : PointCoordinateAxe
{
    /// <summary>
    /// Инициализирует новый экземпляр класса математических координат оси X 
    /// по координатам (0, 0, 0).
    /// </summary>
    public PointZAxe()
        : this(0.0)
    {
        
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса математических координат оси Z 
    /// по заданным координатам.
    /// </summary>
    /// <param name="value">Координаты оси Z.</param>
    public PointZAxe(double value)
    {
        Value = value;
    }
}

