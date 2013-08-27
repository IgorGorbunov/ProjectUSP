/// <summary>
/// Класс математических координат оси X.
/// </summary>
public sealed class XAxe : CoordinateAxe
{
    /// <summary>
    /// Возвращает тип оси.
    /// </summary>
    public override CoordinateConfig.Type Type
    {
        get { return CoordinateConfig.Type.X; }
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса математических координат оси X 
    /// по координатам (0, 0, 0).
    /// </summary>
    public XAxe() 
        : this(0.0)
    {
        
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса математических координат оси X 
    /// по заданным координатам.
    /// </summary>
    /// <param name="value">Координаты оси X.</param>
    public XAxe(double value)
    {
        Value = value;
    }
}
