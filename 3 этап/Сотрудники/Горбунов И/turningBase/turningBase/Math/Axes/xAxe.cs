/// <summary>
/// Класс математических координат оси X.
/// </summary>
public sealed class PointXAxe : PointCoordinateAxe
{
    /// <summary>
    /// Инициализирует новый экземпляр класса математических координат оси X 
    /// по координатам (0, 0, 0).
    /// </summary>
    public PointXAxe() 
        : this(0.0)
    {
        
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса математических координат оси X 
    /// по заданным координатам.
    /// </summary>
    /// <param name="value">Координаты оси X.</param>
    public PointXAxe(double value)
    {
        Value = value;
    }
}
