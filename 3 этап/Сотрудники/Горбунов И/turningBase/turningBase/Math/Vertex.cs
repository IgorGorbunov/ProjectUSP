using NXOpen;

/// <summary>
/// Класс математической точки.
/// </summary>
public class Vertex
{
    private readonly Point3d _point;

    /// <summary>
    /// Инициализирует новый экземпляр класса математической точки в координатах (0, 0, 0).
    /// </summary>
    public Vertex() : this(new Point3d())
    {
        
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса математической точки по заданной точке Point3d.
    /// </summary>
    /// <param name="point">Точка Point3d.</param>
    public Vertex(Point3d point)
    {
        _point = point;
    }
    /// <summary>
    /// Возвращает координаты точки для необходимой оси.
    /// </summary>
    /// <param name="axe">Ось.</param>
    /// <returns></returns>
    public double GetCoordinate(CoordinateAxe axe)
    {
        switch (axe.Type)
        {
                case CoordinateConfig.Type.X:
                return _point.X;
                case CoordinateConfig.Type.Y:
                return _point.Y;
                case CoordinateConfig.Type.Z:
                return _point.Z;
        }
        return 0.0;
    }
}

