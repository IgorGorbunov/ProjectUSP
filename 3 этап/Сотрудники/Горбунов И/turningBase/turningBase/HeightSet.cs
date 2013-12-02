using System.Collections.Generic;
using NXOpen;
using NXOpen.Assemblies;
using algorithm;

public static class HeightSet
{
    private static Face _lowFace;
    private static Face _highFace;

    public static bool SetHeight(Face face1, Face face2, double height, ElementType elementType, bool ignoreInStock, Catalog catalog)
    {
        _lowFace = face1;
        _highFace = face2;
        return SetHeihgtElems(height, elementType, ignoreInStock, catalog);
    }

    private static bool SetHeihgtElems(double height, ElementType elementType, bool ignoreInStock, Catalog catalog)
    {

        Solution solution = new SelectionAlgorihtm(
            DatabaseUtils.loadFromDb(elementType, ignoreInStock, (int)catalog.CatalogUsp),//учитываем колво на складе
            1000).solve(height, ignoreInStock); //учитываем колво на складе

        if (solution.mainAnswer == -1)
        {
            ExactHeightForm form = new ExactHeightForm(height, solution.lowerBound,
                                                       solution.upperBound);
            form.ShowDialog();

            if (HeightDialogSet.UserHeight == -1)
            {
                return false;
            }
            SetElems(new SelectionAlgorihtm(
                         DatabaseUtils.loadFromDb(elementType, ignoreInStock, (int)catalog.CatalogUsp),
                         1000).solve(HeightDialogSet.UserHeight, ignoreInStock));
        }
        else
        {
            SetElems(solution);
        }
        return true;
    }

    private static void SetElems(Solution solution)
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

    private static void LoadParts(List<string> partList)
    {
        IEnumerable<UspElement> fixElements = NxFunctions.FixElements(_lowFace.OwningComponent,
                                                                      _highFace.OwningComponent);

        int i = 0;
        HeightElement[] elements = new HeightElement[partList.Count];
        foreach (string s in partList)
        {
            Katalog2005.Algorithm.SpecialFunctions.LoadPart(s, false);
            Component component = Katalog2005.Algorithm.SpecialFunctions.LoadedPart;
            elements[i] = new HeightElement(component);

            if (i > 0)
            {
                elements[i].SetOn(elements[i - 1]);
            }
            else
            {
                Touch touch = new Touch();
                touch.Create(_lowFace, elements[i].BottomFace);
                NxFunctions.Update();
            }

            if (i == elements.Length - 1)
            {
                Touch touch = new Touch();
                touch.Create(_highFace, elements[i].TopFace);
                NxFunctions.Update();
            }
            i++;
        }

        NxFunctions.Unfix(fixElements);
    }
}

