using System;
using System.Collections.Generic;
using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс содержащий элемент УСП.
/// </summary>
public class UspElement
{
    /// <summary>
    /// Возвращает компонент элемента.
    /// </summary>
    public Component ElementComponent
    {
        get
        {
            return _component;
        }
    }
    /// <summary>
    /// Возвращает список всех нижних плоскостей пазов для данного элемента.
    /// </summary>
    public List<Face> SlotFaces
    {
        get
        {
            return _bottomFaces;
        }
    }
    /// <summary>
    /// Возвращает тело данного элемента.
    /// </summary>
    public Body Body
    {
        get
        {
            return _body;
        }
    }

    readonly Component _component;
    Body _body;

    List<Face> _bottomFaces;

    private const int MagicNumber = 1680; //TODO


    /// <summary>
    /// Инициализирует новый экземпляр класса элемента УСП для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент из сборки NX.</param>
    public UspElement(Component component)
    {
        _component = component;

        SetBody();
    }

    /// <summary>
    /// Проводит поиск и устанавливает нижние плоскости пазов.
    /// </summary>
    public void SetBottomFaces()
    {
        Face[] faces = _body.GetFaces();

        _bottomFaces = new List<Face>();
        for (int j = 0; j < faces.Length; j++)
        {
            try
            {
                Face face = faces[j];

                if (face.Name != null)
                {
                    string[] split = face.Name.Split(Config.FaceNameSplitter);

                    if (split[0] == Config.SlotSymbol && 
                        split[1] == Config.SlotBottomSymbol)
                    {
                        _bottomFaces.Add(face);
                    }
                }
            }
            catch (NXException ex)
            {
                if (ex.ErrorCode != 3520016)
                {
                    UI.GetUI().NXMessageBox.Show("Ошибка!",
                                                 NXMessageBox.DialogType.Error,
                                                 "Ашипка!");
                }
            }
        }

        string mess = "В качестве нижних граней паза выбраны:";
        foreach (Face f in _bottomFaces)
        {
            mess += Environment.NewLine + f;
        }
        mess += Environment.NewLine + "---------------";
        Log.WriteLine(mess);
    }

    //refactor
    Face GetSomeFace()
    {
        Face someFace = null;
        for (int j = 1; j < MagicNumber; j++)
        {
            try
            {
                someFace = (Face)_component.FindObject(
                    "PROTO#.Features|UNPARAMETERIZED_FEATURE(0)|FACE " + j);

                break;
            }
            catch (NXException ex)
            {
                if (ex.ErrorCode != 3520016)
                {
                    UI.GetUI().NXMessageBox.Show("Ошибка!",
                                                 NXMessageBox.DialogType.Error,
                                                 "Ашипка!");
                }
            }
        }
        return someFace;
    }

    void SetBody()
    {
        Face someFace = GetSomeFace();
        _body = someFace.GetBody();
    }
}

