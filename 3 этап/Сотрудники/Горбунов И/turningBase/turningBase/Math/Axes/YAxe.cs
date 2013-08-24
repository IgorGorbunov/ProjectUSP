/// <summary>
/// Класс математических координат оси Y.
/// </summary>
public sealed class PointYAxe : PointCoordinateAxe
{
    /// <summary>
    /// Инициализирует новый экземпляр класса математических координат оси X 
    /// по координатам (0, 0, 0).
    /// </summary>
    public PointYAxe()
        : this(0.0)
    {
        
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса математических координат оси Y 
    /// по заданным координатам.
    /// </summary>
    /// <param name="value">Координаты оси Y.</param>
    public PointYAxe(double value)
    {
        Value = value;
    }
}

