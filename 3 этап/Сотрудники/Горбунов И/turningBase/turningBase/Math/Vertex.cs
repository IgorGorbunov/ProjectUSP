using NXOpen;

/// <summary>
/// Класс математической точки.
/// </summary>
public class Vertex
{
    private readonly PointCoordinateAxe[] _coordinateAxes = new PointCoordinateAxe[3];

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
        _coordinateAxes[0] = new PointXAxe(point.X);
        _coordinateAxes[1] = new PointYAxe(point.Y);
        _coordinateAxes[2] = new PointZAxe(point.Z);
    }

}

