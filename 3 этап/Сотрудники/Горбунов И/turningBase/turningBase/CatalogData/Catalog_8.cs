/// <summary>
/// Класс каталога с информацией о 8ом пазе.
/// </summary>
sealed class Catalog8 : Catalog
{
    protected override int Series
    {
        get { return _SERIES; }
    }
    public override int Slot
    {
        get
        {
            return 8;
        }
    }
    public override SqlTabUspData.CatalogUsp CatalogUsp
    {
        get { return SqlTabUspData.CatalogUsp.Slot8; }
    }
    public override double Diametr
    {
        get { return _DIAMETR; }
    }
    public override double SlotBoltWidth
    {
        get { return _SLOT_BOLT_WIDTH; }
    }
    public override double SlotBoltLength
    {
        get { return _SLOT_BOLT_LENGTH; }
    }
    public override double PSlotWidth
    {
        get { return _P_SLOT_WIDTH; }
    }
    protected override double PSlotHeight
    {
        get { return _P_SLOT_HEIGHT; }
    }
    protected override double SlotWidthA
    {
        get { return _SLOT_WIDTH_A; }
    }
    public override double SlotWidthB
    {
        get { return _SLOT_WIDTH_B; }
    }
    protected override double[] SlotHeight
    {
        get { return _SLOT_HEIGHT; }
    }
    protected override double[] SlotHeight1
    {
        get { return _SLOT_HEIGHT1; }
    }
    protected override double StepWidthTSlot1
    {
        get { return _STEP_WIDTH_T_SLOT1; }
    }


    private const int _SERIES = 2;
    private const double _DIAMETR = 8;

    private const double _SLOT_BOLT_WIDTH = 12;
    private const double _SLOT_BOLT_LENGTH = 20;

    private const double _P_SLOT_WIDTH = 8;
    private const double _P_SLOT_HEIGHT = 2.5;

    private const double _SLOT_WIDTH_A = 8;
    private const double _SLOT_WIDTH_B = 13;
    private static readonly double[] _SLOT_HEIGHT = { 5, 6 };
    private static readonly double[] _SLOT_HEIGHT1 = { 4, 4.2 };

    private const double _STEP_WIDTH_T_SLOT1 = (_SLOT_WIDTH_B - _SLOT_WIDTH_A) / 2.0;
}
