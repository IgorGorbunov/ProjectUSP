using System;
using System.IO;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.UF;
using NXOpen.Utilities;

/// <summary>
/// Класс замены компонентов в NX.
/// </summary>
public static class Replacement
{
    public static Component ReplaceByTitle(Component oldComponent, string newComponentTitle)
    {
        string oldName = oldComponent.Name;
        Katalog2005.Algorithm.SpecialFunctions.LoadPart(newComponentTitle, true);
        string uniqueName = DateTime.Now.GetHashCode() + "_" + newComponentTitle;

        Logger.WriteLine("Замена компонента " + oldComponent.Name + " компонентом " + uniqueName);

        ReplaceComponentBuilder rcb = Config.WorkPart.AssemblyManager.CreateReplaceComponentBuilder();
        rcb.ComponentNameType = ReplaceComponentBuilder.ComponentNameOption.AsSpecified;

        rcb.ComponentsToReplace.Add(oldComponent);
        rcb.ComponentName = uniqueName;
        rcb.ReplacementPart = Path.GetTempPath() + Config.TmpFolder + Path.DirectorySeparatorChar +
                    newComponentTitle + Config.PartFileExtension;

        rcb.SetComponentReferenceSetType(ReplaceComponentBuilder.ComponentReferenceSet.Others, "Оставить");
        PartLoadStatus partLoadStatus1 = rcb.RegisterReplacePartLoadStatus();
        rcb.Commit();

        bool alreadyHave = false;
        if (partLoadStatus1.NumberUnloadedParts > 0)
        {
            for (int i = 0; i < partLoadStatus1.NumberUnloadedParts; i++)
            {
                Logger.WriteLine(partLoadStatus1.GetPartName(i), partLoadStatus1.GetStatus(i),
                                 partLoadStatus1.GetStatusDescription(i));

                if (partLoadStatus1.GetStatus(i) == 641014)
                {
                    alreadyHave = true;
                    throw new PartAlreadyLoadedExeption();
                }
            }
        }

        partLoadStatus1.Dispose();
        rcb.Destroy();

        Tag newCompTag = Tag.Null;
        string findName = uniqueName;
        if (alreadyHave)
        {

        }
        else
        {
            findName = uniqueName;
        }

        Config.TheUfSession.Obj.CycleByNameAndType(Config.WorkPart.Tag, findName, UFConstants.UF_component_type, true, ref newCompTag);
        Component newConmponent = (Component)NXObjectManager.Get(newCompTag);
        newConmponent.SetName(uniqueName);
        return newConmponent;
    }
}

