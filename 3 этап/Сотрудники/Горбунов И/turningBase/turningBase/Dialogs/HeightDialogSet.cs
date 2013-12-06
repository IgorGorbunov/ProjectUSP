﻿//==============================================================================
//  WARNING!!  This file is overwritten by the Block UI Styler while generating
//  the automation code. Any modifications to this file will be lost after
//  generating the code again.
//
//       Filename:  C:\ug_customization\application\dialogs\heightSet\heightSet.cs
//
//        This file was generated by the NX Block UI Styler
//        Created by: USP
//              Version: NX 7.5
//              Date: 10-08-2013  (Format: mm-dd-yyyy)
//              Time: 10:41 (Format: hh-mm)
//
//==============================================================================

//==============================================================================
//  Purpose:  This TEMPLATE file contains C# source to guide you in the
//  construction of your Block application dialog. The generation of your
//  dialog file (.dlx extension) is the first step towards dialog construction
//  within NX.  You must now create a NX Open application that
//  utilizes this file (.dlx).
//
//  The information in this file provides you with the following:
//
//  1.  Help on how to load and display your Block UI Styler dialog in NX
//      using APIs provided in NXOpen.BlockStyler namespace
//  2.  The empty callback methods (stubs) associated with your dialog items
//      have also been placed in this file. These empty methods have been
//      created simply to start you along with your coding requirements.
//      The method name, argument list and possible return values have already
//      been provided for you.
//==============================================================================

//------------------------------------------------------------------------------
//These imports are needed for the following template code
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.BlockStyler;
using NXOpen.UF;
using algorithm;

/// <summary>
/// Класс диалога для набора высоты.
/// </summary>
public sealed class HeightDialogSet : DialogProgpam
{
    /// <summary>
    /// Высота введенная пользователем.
    /// </summary>
    public static double UserHeight;

    public static bool BoltAdded;

    public static IEnumerable<UspElement> BoltFixElements;

    //class members
    private readonly string _theDialogName;

    private UIBlock _group0;// Block type: Group
    private UIBlock _group01;// Block type: Group
    private UIBlock _selection0;// Block type: Selection
    private UIBlock _selection01;// Block type: Selection
    private UIBlock _button0;// Block type: Selection
    private UIBlock _double0;// Block type: Selection
    private UIBlock _toggle0;// Block type: Selection

    private Face _face1, _face2;
    private bool _face1Selected, _face2Selected, _heightSetted;

    private Catalog _catalog;
    private double _height;
    private const double _RESERVE_HEIGHT = 10;

    private HeightElement _firstElement;


    /// <summary>
    /// Инициализирует новый экземпляр класса диалога для набора высоты для заданного каталога.
    /// </summary>
    public HeightDialogSet()
    {
        try
        {
            Init();
            _theDialogName = Path.Combine(ConfigDlx.FullDlxFolder, ConfigDlx.DlxHeight);

            TheDialog = Config.TheUi.CreateDialog(_theDialogName);
            TheDialog.AddApplyHandler(apply_cb);
            TheDialog.AddOkHandler(ok_cb);
            TheDialog.AddUpdateHandler(update_cb);
            TheDialog.AddCancelHandler(cancel_cb);
            TheDialog.AddInitializeHandler(initialize_cb);
            TheDialog.AddFocusNotifyHandler(focusNotify_cb);
            TheDialog.AddKeyboardFocusNotifyHandler(keyboardFocusNotify_cb);
            TheDialog.AddDialogShownHandler(dialogShown_cb);
        }
        catch (TimeoutException)
        {
            //---- Enter your exception handling code here -----
            //const string mess = "Нет соединения с БД!";
            //Logger.WriteError(mess, ex);
            //Message.Show(mess);
            throw;
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            string mess = "Ошибка в конструкторе " + GetType().Name;
            Logger.WriteError(mess, ex);
            Message.Show(mess);
            throw;
        }
    }

    
    //------------------------------------------------------------------------------
    // Following method cleanup any housekeeping chores that may be needed.
    // This method is automatically called by NX.
    //------------------------------------------------------------------------------
    public static int UnloadLibrary(string arg)
    {
        try
        {
            //---- Enter your code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Message.ShowError(ex);
        }
        return 0;
    }

    //------------------------------------------------------------------------------
    //---------------------Block UI Styler Callback Functions--------------------------
    //------------------------------------------------------------------------------
    
    //------------------------------------------------------------------------------
    //Callback Name: initialize_cb
    //------------------------------------------------------------------------------
    private void initialize_cb()
    {
        try
        {
            _group0 = TheDialog.TopBlock.FindBlock("group0");
            _group01 = TheDialog.TopBlock.FindBlock("group01");
            _selection0 = TheDialog.TopBlock.FindBlock("selection0");
            _selection01 = TheDialog.TopBlock.FindBlock("selection01");
            _button0 = TheDialog.TopBlock.FindBlock("button0");
            _double0 = TheDialog.TopBlock.FindBlock("double0");
            _toggle0 = TheDialog.TopBlock.FindBlock("toggle0");
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Logger.WriteError(ex.ToString());
            Message.Show("Block Styler", Message.MessageType.Error, ex);
        }
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: dialogShown_cb
    //This callback is executed just before the dialog launch. Thus any value set 
    //here will take precedence and dialog will be launched showing that value. 
    //------------------------------------------------------------------------------
    private void dialogShown_cb()
    {
        try
        {
            //---- Enter your callback code here -----
            Selection.MaskTriple[] mask = new Selection.MaskTriple[1];
            mask[0].Type = UFConstants.UF_solid_type;
            mask[0].Subtype = UFConstants.UF_all_subtype;
            mask[0].SolidBodySubtype = UFConstants.UF_UI_SEL_FEATURE_PLANAR_FACE;
            _selection0.GetProperties().SetSelectionFilter("SelectionFilter", Selection.SelectionAction.ClearAndEnableSpecific, mask);
            _selection01.GetProperties().SetSelectionFilter("SelectionFilter", Selection.SelectionAction.ClearAndEnableSpecific, mask);
            SetEnable(_button0, false);
            SetEnable(_toggle0, true);
            _double0.GetProperties().SetDouble("Value", 0);
            _toggle0.GetProperties().SetLogical("Value", true);
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Logger.WriteError(ex.ToString());
            Message.Show("Block Styler", Message.MessageType.Error, ex);
        }
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: apply_cb
    //------------------------------------------------------------------------------
    private int apply_cb()
    {
        int errorCode = 0;
        try
        {
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            errorCode = 1;
            Logger.WriteError(ex.ToString());
            Message.Show("Block Styler", Message.MessageType.Error, ex);
        }
        return errorCode;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: update_cb
    //------------------------------------------------------------------------------
    private int update_cb(UIBlock block)
    {
        try
        {
            if(block == _selection0)
            {
            //---------Enter your code here-----------
                Logger.WriteLine("Активирована кнопка выбора первой грани.");
                TaggedObject[] taggedObjects = block.GetProperties().GetTaggedObjectVector("SelectedObjects");
                if (taggedObjects.Length > 0)
                {
                    _face1 = (Face) taggedObjects[0];
                    _face1Selected = true;
                    DoMagic();
                }
                else
                {
                    _face1Selected = false;
                }
            }
            else if(block == _selection01)
            {
            //---------Enter your code here-----------
                Logger.WriteLine("Активирована кнопка выбора второй грани.");
                TaggedObject[] taggedObjects = block.GetProperties().GetTaggedObjectVector("SelectedObjects");
                if (taggedObjects.Length > 0)
                {
                    _face2 = (Face)taggedObjects[0];
                    _face2Selected = true;
                    DoMagic();
                }
                else
                {
                    _face2Selected = false;
                }
            }
            else if (block == _button0)
            {
                SetBolt();
            }
            else if (block == _double0)
            {
                _height = _double0.GetProperties().GetDouble("Value");
                _heightSetted = true;
                DoMagic();
            }
            else if (block == _toggle0)
            {
                bool isToggle = block.GetProperties().GetLogical("Value");
                if (isToggle)
                {
                    SetEnable(_double0, true);
                    SetEnable(_group0, false);
                    UnSelectObjects(_selection0);
                    UnSelectObjects(_selection01);
                }
                else
                {
                    SetEnable(_double0, false);
                    SetEnable(_group0, true);
                    _double0.GetProperties().SetDouble("Value", 0.0);
                }
            }
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Logger.WriteError(ex.ToString());
            Message.Show("Block Styler", Message.MessageType.Error, ex);
        }
        return 0;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: ok_cb
    //------------------------------------------------------------------------------
    private int ok_cb()
    {
        int errorCode;
        try
        {
            errorCode = apply_cb();
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            errorCode = 1;
            Logger.WriteError(ex.ToString());
            Message.Show("Block Styler", Message.MessageType.Error, ex);
        }
        return errorCode;
    }

    //------------------------------------------------------------------------------
    //Callback Name: cancel_cb
    //------------------------------------------------------------------------------
    private static int cancel_cb()
    {
        try
        {
            //---- Enter your callback code here -----
            Logger.WriteLine("Нажата кнопка ОТМЕНА.");
            BoltAdded = false;
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Config.TheUi.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
            Logger.WriteError(ex.ToString());
        }
        return 0;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: focusNotify_cb
    //This callback is executed when any block (except the ones which receive keyboard entry such as Integer block) receives focus.
    //------------------------------------------------------------------------------
    private void focusNotify_cb(UIBlock block, bool focus)
    {
        try
        {
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Logger.WriteError(ex.ToString());
            Message.Show("Block Styler", Message.MessageType.Error, ex);
        }
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: keyboardFocusNotify_cb
    //This callback is executed when block which can receive keyboard entry, receives the focus.
    //------------------------------------------------------------------------------
    private void keyboardFocusNotify_cb(UIBlock block, bool focus)
    {
        try
        {
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Logger.WriteError(ex.ToString());
            Message.Show("Block Styler", Message.MessageType.Error, ex);
        }
    }
    
    //----------------------------------------------------------------------------------

    //private void SetBolt(UIBlock block)
    //{
    //    try
    //    {
    //        Point3d point = GetPoint(block);
    //        UspElement element = NxFunctions.GetUnsuppressElement(point);
    //        Logger.WriteLine("Точка находится на компоненте ", element.ElementComponent.Name);
    //        Slot slot = element.GetNearestSlot(point);
    //        double maxSlotHeight = Instr.Max(_catalog.SlotHeight1);
    //        double needHeight = maxSlotHeight + _height + _RESERVE_HEIGHT;
    //        double maxLen = SqlUspElement.GetMaxLenSlotFixture(_catalog);
    //        if (maxLen < needHeight)
    //        {
    //            UnSelectObjects(block);
    //            const string mess = "Подходящих по длине болтов не было найдено!";
    //            Logger.WriteLine(mess);
    //            Message.ShowError(mess);
    //            return;
    //        }
    //        string boltTitle = SqlUspElement.GetTitleMinLengthFixture(needHeight, _catalog);
    //        Katalog2005.Algorithm.SpecialFunctions.LoadPart(boltTitle, false);
    //        SlotTBolt bolt = new SlotTBolt(Katalog2005.Algorithm.SpecialFunctions.LoadedPart);
    //        bolt.SetInSlot(slot);
    //        bolt.SetInTunnel(_firstElement.HoleFace);
    //    }
    //    catch (TimeoutException)
    //    {
    //        Message.Timeout();
    //        throw;
    //    }
    //}

    protected override bool ShowCatalog()
    {
        CatalogForm catalogForm = new CatalogForm();
        catalogForm.ShowDialog();
        if (catalogForm.SelectedCatalog == null)
        {
            return false;
        }
        _catalog = catalogForm.SelectedCatalog;
        return true;
    }

    private void SetBolt()
    {
        BoltFixElements = FixElements();
        SetBoltInSlot setBolt = new SetBoltInSlot(_catalog, _height, _RESERVE_HEIGHT,
                                                          _firstElement);
        setBolt.Show();
        Unfix(BoltFixElements);
    }

    //private Point3d GetPoint(UIBlock block)
    //{
    //    Logger.WriteLine("Активирована кнопка постановки точки.");
    //    Point3d point = block.GetProperties().GetPoint("Point");
    //    Logger.WriteLine("Координаты точки", point);
    //    return point;
    //}

    private void DoMagic()
    {
        if ((!_face1Selected || !_face2Selected) && !_heightSetted) 
            return;

        if (!_heightSetted)
        {
            Surface surface1 = new Surface(_face1);
            Surface surface2 = new Surface(_face2);
            if (!surface1.IsParallel(surface2))
            {
                const string mess = "Выбранные грани не параллельны!";
                Logger.WriteLine(mess);
                Message.ShowError(mess);
                return;
            }

            _height = Config.Round(Math.Abs(GetHeight()));
        }
        
        if (Config.Round(_height) == 0.0)
        {
            const string mess = "Расстояние между гранями равно нулю!";
            Logger.WriteLine(mess);
            Message.ShowError(mess);
            return;
        }

        try
        {
            double maxLen = SqlUspElement.GetMaxLenSlotFixture(_catalog);
            if (maxLen >= _height)
            {
                SetHeihgtElems(_height, ElementType.HeightBySquare, false, _catalog);
            }
            else
            {
                Message.ShowError("Высота для набора слишком большая!",
                                  "Подходящий П-образный болт не найден!");
                Unselect();
            }
        }
        catch (BadQueryExeption)
        {
            Message.ShowError("Болты не найдены!");
            throw;
        }
        catch (TimeoutException)
        {
            Message.Timeout();
            throw;
        }
    }

    private void SetHeihgtElems(double height, ElementType elementType, bool ignoreInStock, Catalog catalog)
    {

        Solution solution = new SelectionAlgorihtm(
            DatabaseUtils.loadFromDb(elementType, ignoreInStock, (int)catalog.CatalogUsp),//учитываем колво на складе
            1000).solve(height, ignoreInStock); //учитываем колво на складе
 
        if (solution.mainAnswer == -1)
        {
            ExactHeightForm form = new ExactHeightForm(height, solution.lowerBound,
                                                       solution.upperBound);
            form.ShowDialog();

            if (UserHeight == -1)
            {
                Unselect();
            }
            else
            {
                SetElems(new SelectionAlgorihtm(
                    DatabaseUtils.loadFromDb(elementType, ignoreInStock, (int)catalog.CatalogUsp),
                    1000).solve(UserHeight, ignoreInStock));
            }
        }
        else
        {
            SetElems(solution);
        }
        if (!_heightSetted)
        {
            SetEnable(_group01, true);
            SetEnable(_button0, true);
        }
        SetEnable(_double0, false);
        SetEnable(_selection0, false);
        SetEnable(_selection01, false);
    }

    private void SetElems(Solution solution)
    {
        Dictionary<Element, byte> eDictionary = solution.getMainSolution(0);
        List<string> list = new List<string>();
        foreach (KeyValuePair<Element, byte> keyValuePair in eDictionary)
        {
            for (int i = 0; i < keyValuePair.Value; i++)
            {
                list.Add(keyValuePair.Key.Obozn);
            }
        }
        
        LoadParts(list);
    }

    private double GetHeight()
    {
        Surface surface1 = new Surface(_face1);
        Surface surface2 = new Surface(_face2);
        double height = surface1.GetDistance(surface2);

        Logger.WriteLine("Расстояние между гранями = " + height);
        return height;
    }

    private void LoadParts(List<string> partList)
    {
        NxFunctions.FreezeDisplay();
        IEnumerable<UspElement> fixElements = null;
        if (!_heightSetted)
        {
            fixElements = FixElements();
        }
        Stack<Component> loadedElements = new Stack<Component>();
        try
        {
            int i = 0;
            HeightElement[] elements = new HeightElement[partList.Count];
            foreach (string s in partList)
            {
                Katalog2005.Algorithm.SpecialFunctions.LoadPart(s, false);
                Component component = Katalog2005.Algorithm.SpecialFunctions.LoadedPart;
                loadedElements.Push(component);
                elements[i] = new HeightElement(component);

                if (i > 0)
                {
                    elements[i].SetOn(elements[i - 1]);
                }
                else
                {
                    _firstElement = elements[i];
                    if (!_heightSetted)
                    {
                        Touch touch = new Touch();
                        touch.Create(_face1.OwningComponent, _face1, component,
                                     elements[i].BottomFace);
                        NxFunctions.Update();
                    }
                }

                if (!_heightSetted && i == elements.Length - 1)
                {
                    Touch touch = new Touch();
                    touch.Create(_face2.OwningComponent, _face2, component, elements[i].TopFace);
                    NxFunctions.Update();
                }
                i++;
            }
        }
        catch (ParamObjectNotFoundExeption e)
        {
            NxFunctions.Delete(loadedElements);
            string mess = "Деталь '" + e.Element.Title + "' неверно параметризированна!" +
                          Environment.NewLine;
            mess += "Не найден параметр '" + e.NxObjectName + "'.";
            Message.ShowError(mess);
        }

        Unfix(fixElements);
        SetEnable(_toggle0, false);
        NxFunctions.UnFreezeDisplay();
    }

    private IEnumerable<UspElement> FixElements()
    {
        UspElement element1 = null;
        bool firstElementIsFixed = _face1.OwningComponent.IsFixed;
        if (!firstElementIsFixed)
        {
            element1 = new UspElement(_face1.OwningComponent);
            element1.Fix();
        }

        UspElement element2 = null;
        bool secondElementIsFixed = _face2.OwningComponent.IsFixed;
        if (!secondElementIsFixed)
        {
            element2 = new UspElement(_face2.OwningComponent);
            element2.Fix();
        }
        NxFunctions.Update();
        UspElement[] array = {element1, element2};
        return array;
    }

    public static void Unfix(IEnumerable<UspElement> uspElements)
    {
        if (uspElements == null)
            return;
        foreach (UspElement uspElement in uspElements)
        {
            if (uspElement != null)
            {
                uspElement.Unfix();
            }
        }
        NxFunctions.Update();
    }

    private void Init()
    {
        Check();
    }

    private void Check()
    {
        ConfigDlx.UnloadDialog(ConfigDlx.DlxHeight);
    }

    private void Unselect()
    {
        UnSelectObjects(_selection0);
        UnSelectObjects(_selection01);
        SetEnable(_group01, false);
        _face1Selected = false;
        _face2Selected = false;
        _selection0.Focus();
    }
}
