﻿//==============================================================================
//  WARNING!!  This file is overwritten by the Block UI Styler while generating
//  the automation code. Any modifications to this file will be lost after
//  generating the code again.
//
//       Filename:  C:\ug_customization\application\dialogs\angleSet\angleSet.cs
//
//        This file was generated by the NX Block UI Styler
//        Created by: USP
//              Version: NX 7.5
//              Date: 10-19-2013  (Format: mm-dd-yyyy)
//              Time: 12:54 (Format: hh-mm)
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
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NXOpen.Assemblies;
using NXOpen.BlockStyler;
using NXOpen.Positioning;
using algorithm;
using img_gallery;
using ListBox = NXOpen.BlockStyler.ListBox;

/// <summary>
/// Класс набора угла.
/// </summary>
public class AngleSet : DialogProgpam
{
    //class members
    private readonly string _theDialogName;

    private UIBlock _group0;// Block type: Group
    private UIBlock _integer0;// Block type: Integer
    private UIBlock _integer01;// Block type: Integer
    private UIBlock _group;// Block type: Group
    private UIBlock _button0;// Block type: Button
    private UIBlock _group1;// Block type: Group
    private UIBlock _label0;// Block type: Label
    private ListBox _listBox0;// Block type: List Box

    //-----------------------------------------------------

    private readonly Catalog _catalog;

    private int _degrees, _minutes;
    private bool _angleIsObtuse;

    private ImageForm _dialogForm;
    private List<AngleSolution> _orderedList;
    
    /// <summary>
    /// Инициализирует новый экземпляр класса диалога для набора угла для заданного каталога.
    /// </summary>
    /// <param name="catalog">Каталог.</param>
    public AngleSet(Catalog catalog)
    {
        try
        {
            Init();
            _catalog = catalog;
            _theDialogName = Path.Combine(ConfigDlx.FullDlxFolder, ConfigDlx.DlxAngle); 

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
            Logger.WriteError(ex.ToString());
            Message.Show("Block Styler", Message.MessageType.Error, ex);
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
            _integer0 = TheDialog.TopBlock.FindBlock("integer0");
            _integer01 = TheDialog.TopBlock.FindBlock("integer01");
            _group = TheDialog.TopBlock.FindBlock("group");
            _button0 = TheDialog.TopBlock.FindBlock("button0");
            _group1 = TheDialog.TopBlock.FindBlock("group1");
            _label0 = TheDialog.TopBlock.FindBlock("label0");
            _listBox0 = (ListBox)TheDialog.TopBlock.FindBlock("list_box0");
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
            _group1.GetProperties().SetLogical("Expanded", false);
            _integer0.GetProperties().SetInteger("Value", 0);
            _integer01.GetProperties().SetInteger("Value", 0);
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
    private int update_cb( UIBlock block)
    {
        try
        {
            if(block == _integer0)
            {
            //---------Enter your code here-----------
            }
            else if(block == _integer01)
            {
            //---------Enter your code here-----------
            }
            else if(block == _button0)
            {
            //---------Enter your code here-----------
                Logger.WriteLine("Нажат кнопка выбора типа ГОСТа.");
                SetGostImages();
            }
            else if(block == _label0)
            {
            //---------Enter your code here-----------
            }
            else if(block == _listBox0)
            {
            //---------Enter your code here-----------
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
    private int cancel_cb()
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

    private void SetGostImages()
    {
        


        _degrees = _integer0.GetProperties().GetInteger("Value");
        _minutes = _integer01.GetProperties().GetInteger("Value");
        if (AngleIsGood(_degrees, _minutes))
        {
            //List<string> gosts;
            //List<ImageInfo> images = new List<ImageInfo>();

            try
            {
                bool freeElems;
    #if(DEBUG)
                freeElems = true;
    #endif
                Dictionary<String, AngleSolution> solution = AngleSolver.solve(GetAngle(), freeElems, (int)_catalog.CatalogUsp);
                List<AngleSolution> orderedList = AngleSolver.GetOrderedList(solution);
                if (orderedList == null || orderedList.Count == 0)
                {
                    Message.Tst("Нет решений");
                    return;
                }
                _orderedList = AngleSolver.GetOrderedList(solution);
                //Dictionary<Element, byte> smallSolution = orderedList[0].solution.getMainSolution(0);
                //PrintAnswer(orderedList);
                ShowGallery(_orderedList);
            }
            catch (TimeoutException)
            {
                Message.Timeout();
                //goto Exit;
            }

            if (_angleIsObtuse)
            {
                try
                {
                    //gosts = SqlUspBigAngleElems.GetGosts_ObtuseAngle(_catalog);

                    //foreach (string gost in gosts)
                    //{
                    //    Image image = SqlUspElement.GetImage(gost);
                    //    string name = SqlUspElement.GetName(gost);
                    //    ImageInfo info = new ImageInfo(image, name, 1, false);
                    //    images.Add(info);
                    //}
                }
                catch (TimeoutException)
                {
                    Message.Timeout();
                    //goto Exit;
                }
            }
            else
            {
                try
                {
                    //gosts = SqlUspBigAngleElems.GetGosts_AcuteAngle(_catalog);

                    //foreach (string gost in gosts)
                    //{
                    //    Image image = SqlUspElement.GetImage(gost);
                    //    string name = SqlUspElement.GetName(gost);
                    //    ImageInfo info = new ImageInfo(image, name, 1, false);
                    //    images.Add(info);
                    //}
                }
                catch (TimeoutException)
                {
                    Message.Timeout();
                    //goto Exit;
                }
            }
            //ImageForm form = new ImageForm(images, MouseClickEventHandler);
            //form.Text = "Типы элементов для набора угла";
            //form.DrawItems();
            //form.ShowDialog();
        }

    //Exit:

        //SetElements();
    }

    private void ShowGallery(List<AngleSolution> answer)
    {
        List<ImageInfo> images = new List<ImageInfo>();
        int minCount = answer[0].count;
        foreach (AngleSolution solution in answer)
        {
                Image image = SqlUspElement.GetImage(solution.Gost);
                string name = SqlUspElement.GetName(solution.Gost);
                images.Add(new ImageInfo(image, "ГОСТ " + solution.Gost + " " + name, solution.count + 1, solution.count == minCount, solution));

        }
        _dialogForm = new ImageForm(images, MouseClickEventHandler);
        _dialogForm.Text = "Типы элементов для набора угла";
        _dialogForm.DrawItems();
        _dialogForm.ShowDialog();
    }

    private void SetElements(AngleSolution solution)
    {
        Dictionary<Element, byte> eDictionary = solution.solution.getMainSolution(0);

        List<string> list = new List<string>();
        list.Add(solution.baseElement.Obozn);

        foreach (KeyValuePair<Element, byte> keyValuePair in eDictionary)
        {
            for (int i = 0; i < keyValuePair.Value; i++)
            {
                list.Add(keyValuePair.Key.Obozn);
            }
        }
        
        NxFunctions.FreezeDisplay();
        Stack<Component> loadedElements = new Stack<Component>();
        try
        {
            LoadParts(list, loadedElements);
            SetEnable(_button0, false);
        }
        catch (ParamObjectNotFoundExeption ex)
        {
            NxFunctions.Delete(loadedElements);
            string mess = "Деталь " + ex.PartName + " неверно параметризированна!" +
                          Environment.NewLine;
            mess += "Не хватает параметра " + ex.NxObjectName;
            Message.ShowError(mess);
            SetEnable(_button0, true);
        }
        NxFunctions.UnFreezeDisplay();

    }

    private void LoadParts(List<string> list, Stack<Component> loadedElements)
    {
        Logger.WriteLine("Элементы для набора на угол:");
        Logger.WriteLine(list);

        bool notFirst = false;
        UspElement prevElement = null;
        UspElement firstElement = null;
        foreach (string s in list)
        {
            Katalog2005.Algorithm.SpecialFunctions.LoadPart(s, false);
            UspElement element = new UspElement(Katalog2005.Algorithm.SpecialFunctions.LoadedPart);
            loadedElements.Push(element.ElementComponent);

            if (element.IsBiqAngleElement)
            {
                element = new BigAngleElement(Katalog2005.Algorithm.SpecialFunctions.LoadedPart);
            }
            else
            {
                element = new SmallAngleElement(Katalog2005.Algorithm.SpecialFunctions.LoadedPart);
            }

            if (notFirst)
            {
                SmallAngleElement smallAngleElement = new SmallAngleElement(element.ElementComponent);
                prevElement.AttachToMe(smallAngleElement);
            }
            else
            {
                firstElement = element;
            }
            prevElement = element;
            notFirst = true;
        }
        SetAngleConstraint(firstElement, prevElement);
    }

    private void SetAngleConstraint(UspElement first, UspElement last)
    {
        BigAngleElement firstElement = new BigAngleElement(first.ElementComponent);
        SmallAngleElement lastElement = new SmallAngleElement(last.ElementComponent);
        double firstAngle = new Surface(firstElement.BottomFace).GetAngle(new Surface(lastElement.TopFace));
        Message.Tst(firstAngle);
        AlongSlotReverse(first);
        double secondAngle = new Surface(firstElement.BottomFace).GetAngle(new Surface(lastElement.TopFace));
        Message.Tst(secondAngle);
        double difference1 = Math.Abs(GetDecimalAngle() - firstAngle);
        double difference2 = Math.Abs(GetDecimalAngle() - secondAngle);
        Message.Tst(difference1, difference2);
        if (difference1 < difference2)
        {
            Message.Tst("!");
            AlongSlotReverse(first);
        }
    }

    private void AlongSlotReverse(UspElement first)
    {
        bool isFixed = first.ElementComponent.IsFixed;
        if (!isFixed)
        {
            first.Fix();
        }
        ComponentConstraint[] references = first.ElementComponent.GetConstraints();
        foreach (ComponentConstraint componentConstraint in references)
        {
            if (componentConstraint.Name != "ALONG_SLOT") 
                continue;
            componentConstraint.FlipAlignment();
            NxFunctions.Update();
        }
        if (!isFixed)
        {
            first.Unfix();
        }
    }

    private void MouseClickEventHandler(object sender, MouseEventArgs mouseEventArgs)
    {
        PictureBox pictureBox = (PictureBox) sender;
        foreach (ImageInfo imageInfo in _dialogForm.Images)
        {
            if (pictureBox.Image == null)
            {
                if (imageInfo.Image == null)
                {
                    SetElements(imageInfo.Solution);
                    _dialogForm.Close();
                    return;
                }
            }
            else
            {
                if (!imageInfo.Image.Equals(pictureBox.Image))
                    continue;
                SetElements(imageInfo.Solution);
                _dialogForm.Close();
                return;
            }
        }
    }

    private bool AngleIsGood(int degrees, int minutes)
    {
        Logger.WriteLine("Введённый угол = " + degrees + " градусов и " + minutes + " минут.");
        ConvertAngle(ref degrees, ref minutes);
        if (degrees == 0 && minutes == 0)
        {
            const string mess = "Угол не может быть нулевым!";
            Message.ShowError(mess);
            return false;
        }
        if (degrees > 90 ||
            degrees == 90 && minutes > 0)
        {
            _angleIsObtuse = true;
            Message.ShowWarn("Для набора тупого угла рациональнее использовать корпусный элемент для выдерживания прямого угла и последующий набор острого угла!");
            return true;
        }
        if (degrees == 90)
        {
            Message.ShowError("Для набора прямого угла необходимо использовать корпусный элемент!");
            return false;
        }
        _angleIsObtuse = false;
        return true;
    }

    private void ConvertAngle(ref int degrees, ref int minutes)
    {
        if (minutes != 60) 
            return;
        minutes = 0;
        degrees++;
        _integer0.GetProperties().SetInteger("Value", degrees);
        _integer01.GetProperties().SetInteger("Value", minutes);
    }

    private int GetAngle()
    {
        return _degrees * 60 + _minutes;
    }

    private double GetDecimalAngle()
    {
        return _degrees + _minutes/60;
    }

    private void Init()
    {
        Check();
    }

    private void Check()
    {
        ConfigDlx.UnloadDialog(ConfigDlx.DlxAngle);
    }

}
