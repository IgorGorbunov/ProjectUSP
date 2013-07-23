/// <summary>
/// Абстрактный класс с информацией о каталогах
/// </summary>
public abstract class Catalog
{
    /// <summary>
    /// Возвращает серию каталога.
    /// </summary>
    public virtual int Series
    {
        get { return 0; }
    }
    /// <summary>
    /// Возвращает ширину П-образного паза.
    /// </summary>
    public virtual double PSlotWidth
    {
        get { return 0; }
    }
    /// <summary>
    /// Возвращает высоту П-образного паза.
    /// </summary>
    public virtual double PSlotHeight
    {
        get { return 0; }
    }
    /// <summary>
    /// Возвращает ширину a Т-образного паза.
    /// </summary>
    public virtual double SlotWidthA
    {
        get { return 0; }
    }
    /// <summary>
    /// Возвращает ширину b Т-образного паза.
    /// </summary>
    public virtual double SlotWidthB
    {
        get { return 0; }
    }
    /// <summary>
    /// Возвращает ширину b1 Т-образного паза.
    /// </summary>
    public virtual double SlotWidthB1
    {
        get { return 0; }
    }
    /// <summary>
    /// Возвращает высоту h Т-образного паза.
    /// </summary>
    public virtual double[] SlotHeight
    {
        get { return null; }
    }
    /// <summary>
    /// Возвращает высоту h1 Т-образного паза.
    /// </summary>
    public virtual double[] SlotHeight1
    {
        get { return null; }
    }
    /// <summary>
    /// Возвращает высоту h2 Т-образного паза.
    /// </summary>
    public virtual double SlotHeight2
    {
        get { return 0; }
    }


    public virtual double StepWidthTSlot1
    {
        get { return 0; }
    }

    
}

