/// <summary>
/// Абстрактный класс математических координат по оси.
/// </summary>
public abstract class PointCoordinateAxe
{
    protected double Value;
    

    public static PointCoordinateAxe[] GetSurfaceAxes(PointCoordinateAxe axe)
    {
        PointXAxe x = new PointXAxe();
        PointYAxe y = new PointYAxe();
        PointZAxe z = new PointZAxe();
        switch (axe.GetType().ToString())
        {
                case x.GetType().ToString():

        }
    }
}
