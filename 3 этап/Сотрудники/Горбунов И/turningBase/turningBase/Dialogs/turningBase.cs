﻿//==============================================================================
//  WARNING!!  This file is overwritten by the Block UI Styler while generating
//  the automation code. Any modifications to this file will be lost after
//  generating the code again.
//
//       Filename:  C:\ug_customization\application\dialogs\turningBase\turningBase.cs
//
//        This file was generated by the NX Block UI Styler
//        Created by: USP
//              Version: NX 7.5
//              Date: 07-26-2013  (Format: mm-dd-yyyy)
//              Time: 15:48 (Format: hh-mm)
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
using NXOpen.Utilities;

//------------------------------------------------------------------------------
//Represents Block Styler application class
//------------------------------------------------------------------------------
public sealed class TurningBase : DialogProgpam
{
    //class members
    private readonly string _theDialogName;

// ReSharper disable NotAccessedField.Local
    private UIBlock _group0;// Block type: Group
// ReSharper restore NotAccessedField.Local
    private UIBlock _faceSelect0;// Block type: Face Collector
    private UIBlock _enum0;// Block type: Enumeration
    private UIBlock _group1;// Block type: Group
    private UIBlock _direction0;
    private UIBlock _selection0;
    private UIBlock _double0;
    private UIBlock _linearDim0;// Block type: Linear Dim
    private UIBlock _group;// Block type: Group
    private UIBlock _button0;// Block type: Button
    private UIBlock _button01;// Block type: Button
    private UIBlock _label0;// Block type: Label

    private Point3d _centrePoint;
    private Point3d _basePoint;
    private double[] _direct;

    private bool _faceSelected;
    private bool _partIsLoaded;
    private bool _isFixed = true;
    private bool _isFirstReplace = true;

    private Platan _projectPlatan;
    private readonly List<Point3d> _projectList = new List<Point3d>();

    private Catalog _catalog;
    private string _slotType;
    private double _radius;
    private KeyValuePair<string, double>[] _bases;

    private Face _turningFace;
    private UspElement _turningElement;

    private Face _baseFace;
    private UspElement _baseElement;
    private string _newTitle;

    private Vector _workpeaceBaseVector;
    private Point3d _oldPointMovement;

    private TouchAxe _touchAxe;


    //------------------------------------------------------------------------------
    //Constructor for NX Styler class
    //------------------------------------------------------------------------------
    public TurningBase()
    {
        try
        {
            _theDialogName = AppDomain.CurrentDomain.BaseDirectory +
                Config.DlxFolder + Path.DirectorySeparatorChar + Config.DlxTurningBase;

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
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Message.Show(ex);
            throw;
        }
    }
    //------------------------------- DIALOG LAUNCHING ---------------------------------
    //
    //    Before invoking this application one needs to open any part/empty part in NX
    //    because of the behavior of the blocks.
    //
    //    Make sure the dlx file is in one of the following locations:
    //        1.) From where NX session is launched
    //        2.) $UGII_USER_DIR/application
    //        3.) For released applications, using UGII_CUSTOM_DIRECTORY_FILE is highly
    //            recommended. This variable is set to a full directory path to a file 
    //            containing a list of root directories for all custom applications.
    //            e.g., UGII_CUSTOM_DIRECTORY_FILE=$UGII_ROOT_DIR\menus\custom_dirs.dat
    //
    //    You can create the dialog using one of the following way:
    //
    //    1. Journal Replay
    //
    //        1) Replay this file through Tool->Journal->Play Menu.
    //
    //    2. USER EXIT
    //
    //        1) Create the Shared Library -- Refer "Block UI Styler programmer's guide"
    //        2) Invoke the Shared Library through File->Execute->NX Open menu.
    //
    //------------------------------------------------------------------------------


    
    //------------------------------------------------------------------------------
    // Following method cleanup any housekeeping chores that may be needed.
    // This method is automatically called by NX.
    //------------------------------------------------------------------------------
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Global
    public static int UnloadLibrary(string arg)
// ReSharper restore UnusedParameter.Global
// ReSharper restore UnusedMember.Global
    {
        try
        {
            //---- Enter your code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Message.Show(ex);
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
            _faceSelect0 = TheDialog.TopBlock.FindBlock("face_select0");
            _enum0 = TheDialog.TopBlock.FindBlock("enum0");
            _group1 = TheDialog.TopBlock.FindBlock("group1");
            _direction0 = TheDialog.TopBlock.FindBlock("direction0");
            _linearDim0 = TheDialog.TopBlock.FindBlock("linear_dim0");
            _group = TheDialog.TopBlock.FindBlock("group");
            _button0 = TheDialog.TopBlock.FindBlock("button0");
            _button01 = TheDialog.TopBlock.FindBlock("button01");
            _label0 = TheDialog.TopBlock.FindBlock("label0");
            _selection0 = TheDialog.TopBlock.FindBlock("selection0");
            _double0 = TheDialog.TopBlock.FindBlock("double0");

            
            _catalog = new Catalog8();
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Message.Show(ex);
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
            mask[0].SolidBodySubtype = UFConstants.UF_UI_SEL_FEATURE_CYLINDRICAL_FACE;
            _selection0.GetProperties().SetSelectionFilter("SelectionFilter", Selection.SelectionAction.ClearAndEnableSpecific, mask);

            PropertyList propertyList = _double0.GetProperties();
            propertyList.SetDouble("Value", 0.0);
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Message.Show(ex);
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
            CleanUp();
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            errorCode = 1;
            Message.Show(ex);
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
            if (block == _enum0)
            {
                //---------Enter your code here-----------
                Logger.WriteLine("Нажат выбор типа паза.");
                UpdateLoad(block);
            }
            if(block == _faceSelect0)
            {
            //---------Enter your code here-----------
            }
            else if (block == _direction0)
            {
                //---------Enter your code here-----------
                Logger.WriteLine("Нажат реверс.");
                _touchAxe.Reverse();
                NxFunctions.Update();
            }
            else if (block == _selection0)
            {
                //---------Enter your code here-----------
                Logger.WriteLine("Нажат выбор грани.");
                SetFirstFace(block);
                UpdateLoad(block);
            }
            else if (block == _linearDim0)
            {
                //---------Enter your code here-----------
                PropertyList propertyList = _linearDim0.GetProperties();
                double doub = propertyList.GetDouble("Value");
                Point3d newPoint = _workpeaceBaseVector.GetPoint(doub);
                Vector newVector = new Vector(_oldPointMovement, newPoint);
                Movement.MoveByDirection(_baseElement.ElementComponent, newVector);
                _oldPointMovement = newPoint;
            }
            else if (block == _double0)
            {
                //---------Enter your code here-----------
                PropertyList propertyList = _double0.GetProperties();
                double doub = propertyList.GetDouble("Value");
                Point3d newPoint = _workpeaceBaseVector.GetPoint(doub);
                Vector newVector = new Vector(_oldPointMovement, newPoint);
                Movement.MoveByDirection(_baseElement.ElementComponent, newVector);
                _oldPointMovement = newPoint;
            }
            else if (block == _button0)
            {
                //---------Enter your code here-----------
                Logger.WriteLine("Нажата кнопка меньшего диаметра.");
                _newTitle = GetTitle(SetNewPartDiametr(block));
                UpdateLoad(block);
            }
            else if (block == _button01)
            {
                //---------Enter your code here-----------
                Logger.WriteLine("Нажата кнопка большего диаметра.");
                _newTitle = GetTitle(SetNewPartDiametr(block));
                UpdateLoad(block);
            }
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Logger.WriteError(ex);
            Message.Show(ex);
        }
        return 0;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: ok_cb
    //------------------------------------------------------------------------------
    private int ok_cb()
    {
// ReSharper disable RedundantAssignment
        int errorCode = 0;
// ReSharper restore RedundantAssignment
        try
        {
            errorCode = apply_cb();
            CleanUp();
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            errorCode = 1;
            Message.Show(ex);
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
            CleanUp();
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Message.Show(ex);
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
            Message.Show(ex);
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
            Message.Show(ex);
        }
    }

    void SetBaseType()
    {
        PropertyList propertyList = _enum0.GetProperties();
        _slotType = propertyList.GetEnumAsString("Value");
        Logger.WriteLine("Тип паза - " + _slotType);
    }

    void SetFirstFace(UIBlock block)
    {
        if (SetFace(block, out _centrePoint, out _direct))
        {
            _faceSelected = true;

            Point3d point2 = new Point3d();
            point2.X = _centrePoint.X + _direct[0];
            point2.Y = _centrePoint.Y + _direct[1];
            point2.Z = _centrePoint.Z + _direct[2];

            Straight straight = new Straight(_centrePoint, point2);
            _projectPlatan = new Platan(_centrePoint, straight);
        }
        else
        {
            _faceSelected = false;
        }

    }
    bool SetFace(UIBlock block, out Point3d point3D, out double[] direction)
    {
        point3D = new Point3d();
        direction = new double[3];

        PropertyList propertyList = block.GetProperties();
        TaggedObject[] to = propertyList.GetTaggedObjectVector("SelectedObjects");

        //если не деселект
        if (to.Length > 0)
        {
            TaggedObject face = to[0];

            int type;
            double voidDouble;
            double[] dir = new double[3];
            double[] box = new double[6];
            double[] point = new double[3];

            Config.TheUfSession.Modl.AskFaceData(face.Tag, out type, point, dir, box, out voidDouble, out voidDouble, out type);

            //цилиндрическая грань
            if (type == 16)
            {
                point3D.X = point[0];
                point3D.Y = point[1];
                point3D.Z = point[2];
                direction = dir;

                Logger.WriteLine("Точка обр. грани - " + point3D);
                _turningFace = (Face)face;

                if (_turningFace.IsOccurrence)
                {
                    _turningElement = new UspElement(_turningFace.OwningComponent);
                    _isFixed = _turningElement.ElementComponent.IsFixed;
                    if (!_isFixed)
                    {
                        _turningElement.Fix();
                    }
                }
                else
                {
                    Logger.WriteLine("Это прототип!!!!");
                }
                 
                return true;
            }
            const string message = "Грань не цилиндрическая! Выберите другую грань!";
            Logger.WriteWarning(message + Environment.NewLine + "Выбрана грань - " + face);
            Message.Show(message);
            UnSelectObjects(block);
            return false;
        }
        Logger.WriteLine("Деселект грани");
        return false;
    }

    static void UnSelectObjects(UIBlock block)
    {
        PropertyList propList = block.GetProperties();
        propList.SetTaggedObjectVector("SelectedObjects", new TaggedObject[0]);
    }

    static void SetEnable(UIBlock block, bool enable)
    {
        PropertyList propList = block.GetProperties();
        propList.SetLogical("Enable", enable);
    }

    void SetPoints()
    {
        PartCollection partCollection = Config.TheSession.Parts;

        Logger.WriteLine("Пишем точки");
        foreach (Part part in partCollection)
        {
            Tag[] occurences;
            Config.TheUfSession.Assem.AskOccsOfPart(Config.WorkPart.Tag, part.Tag, out occurences);

            foreach (Tag tag in occurences)
            {
                Component component = (Component)NXObjectManager.Get(tag);
                if (component.IsBlanked) continue;

                UspElement element = new UspElement(component);

                double[] minCorner = new double[3];
                double[] distances = new double[3];
                double[,] directions = new double[3, 3];
                Config.TheUfSession.Modl.AskBoundingBoxExact(element.Body.Tag, Tag.Null, minCorner, directions, distances);

                AddToProjectList(new Point3d(minCorner[0], minCorner[1], minCorner[2]));
                AddToProjectList(new Point3d(minCorner[0] + distances[0], minCorner[1], minCorner[2]));
                AddToProjectList(new Point3d(minCorner[0], minCorner[1] + distances[1], minCorner[2]));
                AddToProjectList(new Point3d(minCorner[0], minCorner[1], minCorner[2] + distances[2]));

                AddToProjectList(new Point3d(minCorner[0] + distances[0], minCorner[1] + distances[1], minCorner[2]));
                AddToProjectList(new Point3d(minCorner[0] + distances[0], minCorner[1], minCorner[2] + distances[2]));
                AddToProjectList(new Point3d(minCorner[0], minCorner[1] + distances[1], minCorner[2] + distances[2]));
                AddToProjectList(new Point3d(minCorner[0] + distances[0], minCorner[1] + distances[1], minCorner[2] + distances[2]));
            }
        }
    }

    void AddToProjectList(Point3d point)
    {
        Point3d projectPoint = _projectPlatan.GetProection(point);
        bool alreadyHave = false;
        foreach (Point3d point3D in _projectList)
        {
            if (!Geom.IsEqual(projectPoint, point3D)) continue;

            alreadyHave = true;
            break;
        }
        if (alreadyHave) return;

        _projectList.Add(projectPoint);
        Logger.WriteLine(projectPoint);
    }

    double FindMaxLen()
    {
        double maxLen = double.MinValue;
        Point3d centreProj = _projectPlatan.GetProection(_centrePoint);
        foreach (Point3d point in _projectList)
        {
            double len = (new Vector(centreProj, point)).Length;
            if (len > maxLen)
            {
                maxLen = len;
            }
        }
        Logger.WriteLine("Минимальный радиус выгружаемой базы = " + maxLen);
        return maxLen;
    }

    KeyValuePair<string, double>[] GetAllBases()
    {
        Dictionary<string, string> bases = new Dictionary<string, string>();
        switch (_slotType)
        {
            case "Крестообразное":
                bases = SqlUspElement.GetTitleLengthRoundCrossPlates(_catalog);
                break;
            case "Радиально-поперечное":
                bases = SqlUspElement.GetTitleLengthRoundRadialPlates(_catalog);
                break;
        }

        KeyValuePair<string, double>[] correctNumBases = new KeyValuePair<string, double>[bases.Count];
        int i = 0;
        foreach (KeyValuePair<string, string> keyValuePair in bases)
        {
            double value = Int32.Parse(keyValuePair.Value);
            correctNumBases[i] = new KeyValuePair<string, double>(keyValuePair.Key, value);
            i++;
        }
        if (correctNumBases.Length > 1)
        {
            Instr.QSortPairs(correctNumBases, 0, correctNumBases.Length - 1);
        }

        return correctNumBases;
    }

    double[] GetThreeBases(KeyValuePair<string, double>[] bases,
                                                 out string title)
    {
        title = null;
        double prevD = 0.0, d = 0.0, nextD = 0.0;

        //если не нашли нужную - выгрузим самую большую
        if (bases.Length > 0)
        {
            d = bases[bases.Length - 1].Value;
            title = bases[bases.Length - 1].Key;
        }
        if (bases.Length > 1)
        {
            prevD = bases[bases.Length - 2].Value;
        }
        
        for (int j = 0; j < bases.Length; j++)
        {
            if (bases[j].Value < _radius * 2) continue;

            if (j > 0)
            {
                prevD = bases[j-1].Value;
            }

            title = bases[j].Key;
            d = bases[j].Value;

            if (j < bases.Length - 1)
            {
                nextD = bases[j + 1].Value;
            }

            break;
        }
        double[] arr = { prevD, d, nextD };
        return arr;
    }

    void UpdateLoad(UIBlock block)
    {
        if (!_faceSelected) return;

        string title;
        double[] threeBases;

        if (_partIsLoaded)
        {
            if (block == _enum0)
            {
                SetBaseType();
                _bases = GetAllBases();
            }

            if (block.Type == "Button")
            {
                _newTitle = GetTitle(SetNewPartDiametr(block));
            }
            else
            {
                _newTitle = GetTitle(SetNewPartDiametr(_label0));
            }
            
#if(!DEBUG)
            NxFunctions.FreezeDisplay();
#endif
            ReplaceComponent(_newTitle);

            _baseFace = _baseElement.GetFace(Config.BaseHoleName);

            PropertyList propertyList = _double0.GetProperties();
            double doub = propertyList.GetDouble("Value");
            Point3d newPoint = _workpeaceBaseVector.GetPoint(doub);
            Vector newVector = new Vector(_oldPointMovement, newPoint);
            Movement.MoveByDirection(_baseElement.ElementComponent, newVector);
            _oldPointMovement = newPoint;
#if(!DEBUG)
            NxFunctions.UnFreezeDisplay();
#endif

            threeBases = GetThreeBases(_bases, out title);
        }
        else
        {
            SetEnable(_group1, true);
            SetEnable(_group, true);

            SetPoints();
            _radius = FindMaxLen();

            SetBaseType();
            _bases = GetAllBases();
            threeBases = GetThreeBases(_bases, out title);
            
            if (title != null)
            {
                NxFunctions.FreezeDisplay();
                Katalog2005.Algorithm.SpecialFunctions.LoadPart(title, false);
                SetConstraints();
                NxFunctions.UnFreezeDisplay();
                _partIsLoaded = true;
            }
            else
            {
                _partIsLoaded = false;
                Message.Show("Подходящая база не найдена!");
            }
        }

        SetPrevBttnText(threeBases[0]);
        SetLabelDiametr(threeBases[1]);
        SetNextBttnText(threeBases[2]);

    }

    void SetLabelDiametr(double diametr)
    {
        PropertyList propertyList = _label0.GetProperties();
        if (diametr == 0.0)
        {
            propertyList.SetString("Label", "Текущий диаметр");
        }
        else
        {
            propertyList.SetString("Label", "Текущий диаметр - " + diametr + " мм");
        }
        
    }

    void SetPrevBttnText(double diametr)
    {
        if (diametr == 0.0)
        {
            SetBttnText(_button0, "Меньший диаметр");
            SetEnable(_button0, false);
        }
        else
        {
            SetBttnText(_button0, "Меньший диаметр - " + diametr + " мм");
            SetEnable(_button0, true);
        }
    }

    void SetNextBttnText(double diametr)
    {
        if (diametr == 0.0)
        {
            SetBttnText(_button01, "Больший диаметр");
            SetEnable(_button01, false);
        }
        else
        {
            SetBttnText(_button01, "Больший диаметр - " + diametr + " мм");
            SetEnable(_button01, true);
        }
    }

    static void SetBttnText(UIBlock block, string text)
    {
        PropertyList propertyList = block.GetProperties();
        propertyList.SetString("Label", text);
    }

    void SetConstraints()
    {
        _baseElement = new UspElement(Katalog2005.Algorithm.SpecialFunctions.LoadedPart);
        _baseFace = _baseElement.GetFace(Config.BaseHoleName);

        _touchAxe = new TouchAxe();
        _touchAxe.Create(_turningElement.ElementComponent, _turningFace,
                         _baseElement.ElementComponent, _baseFace);
        Config.TheUfSession.Modl.Update();

        SetBasePoint();
    }

    void SetBasePoint()
    {
        int type;
        double voidDouble;
        double[] dir = new double[3];
        double[] box = new double[6];
        double[] point = new double[3];

        Config.TheUfSession.Modl.AskFaceData(_baseFace.Tag, out type, point, dir, box, out voidDouble, out voidDouble, out type);

        _basePoint.X = point[0];
        _basePoint.Y = point[1];
        _basePoint.Z = point[2];

        _oldPointMovement = _basePoint;
        _workpeaceBaseVector = new Vector(_centrePoint, _basePoint);
    }

    void ReplaceComponent(string newTitleComponent)
    {
        Component oldBase = _baseElement.ElementComponent;
        Katalog2005.Algorithm.SpecialFunctions.LoadPart(newTitleComponent, true);
        string uniqueName = newTitleComponent + "__" + DateTime.Now.GetHashCode();

        Logger.WriteLine("Замена компонента " + oldBase.Name + " компонентом " + uniqueName);
        
        ReplaceComponentBuilder rcb = Config.WorkPart.AssemblyManager.CreateReplaceComponentBuilder();
        rcb.ComponentNameType = ReplaceComponentBuilder.ComponentNameOption.AsSpecified;

        rcb.ComponentsToReplace.Add(oldBase);
        rcb.ComponentName = uniqueName;
        rcb.ReplacementPart = Path.GetTempPath() + Config.TmpFolder + Path.DirectorySeparatorChar +
                    newTitleComponent + Config.PartFileExtension;
        

        rcb.SetComponentReferenceSetType(ReplaceComponentBuilder.ComponentReferenceSet.Others, "Оставить");
        PartLoadStatus partLoadStatus1 = rcb.RegisterReplacePartLoadStatus();
        rcb.Commit();

        if (partLoadStatus1.NumberUnloadedParts > 0)
        {
            for (int i = 0; i < partLoadStatus1.NumberUnloadedParts; i++)
            {
                Logger.WriteLine(partLoadStatus1.GetPartName(i), partLoadStatus1.GetStatus(i),
                                 partLoadStatus1.GetStatusDescription(i));
            }
        }

        partLoadStatus1.Dispose();
        rcb.Destroy();

        Tag newCompTag = Tag.Null;
        string findName;
        if (_isFirstReplace)
        {
            findName = uniqueName;
            _isFirstReplace = false;
        }
        else
        {
            findName = oldBase.Name;
        }
        Config.TheUfSession.Obj.CycleByNameAndType(Config.WorkPart.Tag, findName, UFConstants.UF_component_type, true, ref newCompTag);
        Logger.WriteLine("Тэг и имя добавленного компонента - " + newCompTag + "/" + findName);
        oldBase = (Component)NXObjectManager.Get(newCompTag);
        oldBase.SetName(findName);

        _baseElement = new UspElement(oldBase);
    }

    string GetTitle(double diametr)
    {
        for (int i = 0; i < _bases.Length; i++)
        {
            if (_bases[i].Value == diametr)
            {
                return _bases[i].Key;
            }
        }
        return "0";
    }

    double SetNewPartDiametr(UIBlock block)
    {
        PropertyList propertyList = block.GetProperties();
        string label = propertyList.GetString("Label");
        string[] split = label.Split(' ');
        string diametr = split[split.Length - 2];
        _radius = Double.Parse(diametr) / 2;
        return _radius * 2;
    }

    void CleanUp()
    {
        if (!_isFixed)
        {
            _turningElement.Unfix();
        }
    }
}
