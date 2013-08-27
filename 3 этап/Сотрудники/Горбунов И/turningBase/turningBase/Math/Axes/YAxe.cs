/// <summary>
/// Класс математических координат оси Y.
/// </summary>
public sealed class YAxe : CoordinateAxe
{
    /// <summary>
    /// Возвращает тип координатной оси.
    /// </summary>
    public override CoordinateConfig.Type Type
    {
        get { return CoordinateConfig.Type.Y; }
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса математических координат оси X 
    /// по координатам (0, 0, 0).
    /// </summary>
    public YAxe()
        : this(0.0)
    {
        
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса математических координат оси Y 
    /// по заданным координатам.
    /// </summary>
    /// <param name="value">Координаты оси Y.</param>
    public YAxe(double value)
    {
        Value = value;
    }
}

