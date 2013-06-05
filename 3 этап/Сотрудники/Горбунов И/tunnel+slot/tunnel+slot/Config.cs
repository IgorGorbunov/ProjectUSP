using System;
using System.Collections.Generic;
using System.Text;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.UF;

/// <summary>
/// Класс настроек системы.
/// </summary>
public static class Config
{
    public static Session theSession = Session.GetSession();
    public static UI theUI = UI.GetUI();
    public static UFSession theUFSession = UFSession.GetUFSession();
    public static Part workPart= Config.theSession.Parts.Work;

    public const int N_POINTS_IN_EDGE = 2;

    public const int NUMBER_OF_NEAREST_EDGES = 6;

    public static double P_SLOT_WIDTH = 12;
    public static double P_SLOT_HEIGHT = 3;

    public static double T_SLOT_A = 12;
    public static double T_SLOT_WIDTH = 20;
    public static double T_SLOT_B1 = 13;
    public static double[] T_SLOT_HEIGHT = { 6, 8, 10 };
    public static double[] T_SLOT_HEIGHT1 = { 4, 4.2, 7, 7.2, 7.5, 8, 9 };
    public static double T_SLOT_HEIGHT2 = 4;
    public static double[] T_SLOT_HEIGHT3 = { 2, 4, 6 };

    public static double STEP_WIDTH_T_SLOT_1 = (T_SLOT_WIDTH - T_SLOT_A) / 2.0;
    public static double STEP_DOWN_WIDTH_T_SLOT_2 = (T_SLOT_WIDTH - T_SLOT_B1) / 2.0;
    public static double STEP_UP_WIDTH_T_SLOT_2 = (T_SLOT_B1 - T_SLOT_A) / 2.0;

    public enum SlotType
    {
        Pslot,
        Tslot,
        Tslot1,
        Tslot2
    };

    public static char[] FACE_NAME_SPLITTER = { '_' };
    public const string SLOT_SYMBOL = "SLOT";
    public const string SLOT_BOTTOM_SYMBOL = "BOTTOM";

    //снизил с 4 на 3 из-за невозможности пазирования (почему-то) коротких пазов на главной
    //плоскости с длинными на боковых у плит
    //снизил с 3 на 2 из-за невозможности пазирования (почему-то) длинных пазов - неточно
    //вычислялась ширина паза
    const int PRECISION = 5;

    /// <summary>
    /// Папка с формами для диалогов.
    /// </summary>
    public static string dlxFolder = @"dialogs\";

    /// <summary>
    /// Имя файла с формой базирования элементов по отверстиям.
    /// </summary>
    public static string dlxTunnelTunnel = @"tunnel+tunnel.dlx";
    /// <summary>
    /// Имя файла с формой для базирования отверстие-паз.
    /// </summary>
    public static string dlxTunnelSlot = @"tunnel+slot.dlx";

    //------------------------ Methods ------------------------------------------------------------

    public static Component findCompByBodyTag(Tag tag)
    {
        Component[] comps = workPart.ComponentAssembly.RootComponent.GetChildren();

        foreach (Component comp in comps)
	    {
            if (comp.Tag == tag)
            {
                return comp;
            }
	    }

        return null;
    }

    public static double round(double d)
    {
        return Math.Round(d, PRECISION);
    }

    public static SlotType getSlotType(double slotWidth)
    {
        if (round(slotWidth) == T_SLOT_WIDTH)
        {
            return SlotType.Tslot;
        }
        else
        {
            return SlotType.Pslot;
        }
    }

    /// <summary>
    /// Заморозить экран.
    /// </summary>
    public static void freezeDisplay()
    {
        Config.theUFSession.Disp.SetDisplay(UFConstants.UF_DISP_SUPPRESS_DISPLAY);
    }
    /// <summary>
    /// Разморозить экран.
    /// </summary>
    public static void unFreezeDisplay()
    {
        int displayCode;
        Config.theUFSession.Disp.AskDisplay(out displayCode);

        Config.theUFSession.Disp.SetDisplay(UFConstants.UF_DISP_UNSUPPRESS_DISPLAY);

        if (displayCode == UFConstants.UF_DISP_SUPPRESS_DISPLAY)
        {
            Config.theUFSession.Disp.RegenerateDisplay();
        }
    }
}

