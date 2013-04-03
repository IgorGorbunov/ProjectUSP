using System;
using System.Collections.Generic;
using System.Text;
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
            return this.component;
        }
    }
    /// <summary>
    /// Возвращает список всех нижних плоскостей пазов для данного элемента.
    /// </summary>
    public List<Face> SlotFaces
    {
        get
        {
            return this.bottomFaces;
        }
    }


    Component component;
    Body body;

    List<Face> bottomFaces;

    int magic_number = 1680;//TODO


    /// <summary>
    /// Инициализирует новый экземпляр класса элемента УСП для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент из сборки NX.</param>
    public UspElement(Component component)
    {
        this.component = component;

        this.setBottomFace();
    }

    //refactor
    void setBottomFace()
    {
        Face someFace = null;
        for (int j = 1; j < magic_number; j++)
        {
            try
            {
                someFace = (Face)this.component.FindObject(
                    "PROTO#.Features|UNPARAMETERIZED_FEATURE(0)|FACE " + j);
                break;
            }
            catch (NXException Ex)
            {
                if (Ex.ErrorCode == 3520016) //No object found exeption
                {

                }
                else
                {
                    UI.GetUI().NXMessageBox.Show("Ошибка!", 
                                                 NXMessageBox.DialogType.Error, 
                                                 "Ашипка!");
                }
            }
        }
        this.body = someFace.GetBody();
        Face[] faces = this.body.GetFaces();

        this.bottomFaces = new List<Face>();
        for (int j = 0; j < faces.Length; j++)
        {
            try
            {
                Face face = faces[j];

                if (face.Name != null)
                {
                    string[] split = face.Name.Split(Config.FACE_NAME_SPLITTER);

                    if (split[0] == Config.SLOT_SYMBOL && 
                        split[1] == Config.SLOT_BOTTOM_SYMBOL)
                    {
                        this.bottomFaces.Add(face);
                    }
                }
            }
            catch (NXException Ex)
            {
                if (Ex.ErrorCode == 3520016) //No object found exeption
                {

                }
                else
                {
                    UI.GetUI().NXMessageBox.Show("Ошибка!", 
                                                 NXMessageBox.DialogType.Error, 
                                                 "Ашипка!");
                }
            }
        }
    }
}

