/// <summary>
/// Класс каталога с информацией о 12ом пазе.
/// </summary>
class Catalog12 : Catalog
{
    public override int Series
    {
        get { return _SERIES; }
    }
    public override double PSlotWidth
    {
        get { return _P_SLOT_WIDTH; }
    }
    public override double PSlotHeight
    {
        get { return _P_SLOT_HEIGHT; }
    }
    public override double SlotWidthA
    {
        get { return _SLOT_WIDTH_A; }
    }
    public override double SlotWidthB
    {
        get { return _SLOT_WIDTH_B; }
    }
    public override double SlotWidthB1
    {
        get { return _SLOT_WIDTH_B1; }
    }
    public override double[] SlotHeight
    {
        get { return _SLOT_HEIGHT; }
    }
    public override double[] SlotHeight1
    {
        get { return _SLOT_HEIGHT1; }
    }
    public override double SlotHeight2
    {
        get { return _SLOT_HEIGHT2; }
    }
    public override double StepWidthTSlot1
    {
        get { return _STEP_WIDTH_T_SLOT1; }
    }


    private const int _SERIES = 3;

    private const double _P_SLOT_WIDTH = 12;
    private const double _P_SLOT_HEIGHT = 3;

    private const double _SLOT_WIDTH_A = 12;
    private const double _SLOT_WIDTH_B = 20;
    private const double _SLOT_WIDTH_B1 = 13;
    private static readonly double[] _SLOT_HEIGHT = { 6, 8, 10 };
    private static readonly double[] _SLOT_HEIGHT1 = { 7, 7.2, 7.5, 8, 9 };
    private const double _SLOT_HEIGHT2 = 4;

    private const double _STEP_WIDTH_T_SLOT1 = (_SLOT_WIDTH_B - _SLOT_WIDTH_A) / 2.0;
}

