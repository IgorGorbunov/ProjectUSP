/// <summary>
/// Абстрактный класс с информацией о каталогах
/// </summary>
public abstract class Catalog
{
    /// <summary>
    /// Возвращает серию каталога.
    /// </summary>
    protected virtual int Series
    {
        get { return 0; }
    }
    /// <summary>
    /// Возвращает тип каталога.
    /// </summary>
    public virtual SqlTabUspData.CatalogUsp CatalogUsp
    {
        get { return 0; }
    }
    /// <summary>
    /// Возвращает диаметр базового отверстия для каталога.
    /// </summary>
    public virtual double Diametr
    {
        get { return 0; }
    }
    /// <summary>
    /// Возвращает ширину шляпки Т-образного болта.
    /// </summary>
    public virtual double SlotBoltWidth
    {
        get { return 0; }
    }
    /// <summary>
    /// Возвращает длина шляпки Т-образного болта.
    /// </summary>
    public virtual double SlotBoltLength
    {
        get { return 0; }
    }
    public virtual double SlotBoltLendthTolerance
    {
        get { return 10; }
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
    protected virtual double PSlotHeight
    {
        get { return 0; }
    }
    /// <summary>
    /// Возвращает ширину a Т-образного паза.
    /// </summary>
    protected virtual double SlotWidthA
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
    protected virtual double SlotWidthB1
    {
        get { return 0; }
    }
    /// <summary>
    /// Возвращает высоту h Т-образного паза.
    /// </summary>
    protected virtual double[] SlotHeight
    {
        get { return null; }
    }
    /// <summary>
    /// Возвращает высоту h1 Т-образного паза.
    /// </summary>
    protected virtual double[] SlotHeight1
    {
        get { return null; }
    }
    /// <summary>
    /// Возвращает высоту h2 Т-образного паза.
    /// </summary>
    protected virtual double SlotHeight2
    {
        get { return 0; }
    }


    protected virtual double StepWidthTSlot1
    {
        get { return 0; }
    }



    public static bool operator ==(Catalog one, Catalog other)
    {
        if (ReferenceEquals(one, null) && ReferenceEquals(other, null))
        {
            return true;
        }
        return !ReferenceEquals(one, null) && one.Equals((object)other);
    }

    public static bool operator !=(Catalog one, Catalog other)
    {
        return !(one == other);
    }

    #region Equality members

    private bool Equals(Catalog other)
    {
        return Series == other.Series;
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <returns>
    /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
    /// </returns>
    /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param>
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Catalog)obj);
    }

    /// <summary>
    /// Serves as a hash function for a particular type. <see cref="M:System.Object.GetHashCode"/> is suitable for use in hashing algorithms and data structures like a hash table.
    /// </summary>
    /// <returns>
    /// A hash code for the current <see cref="T:System.Object"/>.
    /// </returns>
    public override int GetHashCode()
    {
        return Series.GetHashCode();
    }

    #endregion

    /// <summary>
    /// Возвращает строку, которая представляет текущий объект.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return "Серия - " + Series;
    }

    
}

