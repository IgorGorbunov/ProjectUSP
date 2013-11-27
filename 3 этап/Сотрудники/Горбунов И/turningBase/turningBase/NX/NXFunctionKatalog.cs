using System;
using System.Collections.Generic;
using System.IO;
using NXOpen;
using NXOpen.Assemblies;

namespace Katalog2005.Algorithm
{
    static partial class SpecialFunctions

    {
        public static Component LoadedPart;


        /// <summary>
        /// Выгрузка детали.
        /// </summary>
        /// <param name="oboznOfUsp">Обозначение детали УСП.</param>
        /// <param name="onlyToDisk">Да, если требуется выгрузить только на диск.</param>
        public static void LoadPart(string oboznOfUsp, bool onlyToDisk)
        {
            LoadedPart = null;

            string path = Path.GetTempPath() + Config.TmpFolder + Path.DirectorySeparatorChar;
            string fileName = oboznOfUsp + Config.PartFileExtension;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fullPath = Path.Combine(path, fileName);
            
            if (!File.Exists(fullPath))
            {
                if (SqlOracle1.exist((object)oboznOfUsp, "HD", "MODEL_ATTR"))
                {
                    LoadPartToTemp(oboznOfUsp);
                }
                else if (SqlOracle1.exist((object)oboznOfUsp, "HD", "MODEL_ATTR20"))
                {
                    LoadPartToTempSpecDet(oboznOfUsp);
                }
                else
                {
                    string mess = "Модель детали '" + oboznOfUsp + "' не загружена в каталог!";
                    Message.ShowError(mess);
                }
            }

            if (!onlyToDisk)
            {
                LoadPartToNx(fullPath);
            }

        }

        /// <summary>
        /// Загрузка стандартной модели в Temp
        /// </summary>   
        /// <returns></returns>
        private static void LoadPartToTemp(string oboznachenie)
        {
            SqlOracle1.UnloadPartToTEMPFolder(oboznachenie);

            List<string> childComponents = SqlOracle1.GetInformationListWithParamQuery("NMF",
                                                                                      "MODEL_STRUCT21",
                                                                                      "PARENT",
                                                                                      (oboznachenie +
                                                                                       ".prt"));

            for (int i = 0; i < childComponents.Count; i++)
            {
                string curname = Path.GetFileNameWithoutExtension(childComponents[i]);
                SqlOracle1.UnloadPartToTEMPFolder(curname);
            }
        }

        /// <summary>
        /// Загрузка не стандартной модели в Temp
        /// </summary>   
        /// <returns></returns>
        private static void LoadPartToTempSpecDet(string oboznachenie)
        {
            Message.ShowError("Закомментрировано!");
            //string nmf =
            //    SqlOracle1.ParamQuerySelect("SELECT NMF FROM MODEL_ATTR20 WHERE HD = :HD", "HD",
            //                               oboznachenie);


            //string openPart = SqlOracle1.UnloadOsnasToTEMPFolderFile20(nmf.Trim());
        }

        /// <summary>
        /// Загрузка модели в NX
        /// </summary>   
        /// <returns></returns>
        private static void LoadPartToNx(string nmf)
        {
            PartLoadStatus partLoadStatus1;
            Part part1 = null;
            try
            {
                BasePart basePart1 = Config.TheSession.Parts.OpenBase(nmf, out partLoadStatus1);
                if (partLoadStatus1.NumberUnloadedParts > 0)
                {
                    for (int i = 0; i < partLoadStatus1.NumberUnloadedParts; i++)
                    {
                        Logger.WriteLine(partLoadStatus1.GetStatusDescription(i));
                    }
                }
                else
                {
                    Logger.WriteLine("Деталь " + nmf + " загружена в NX!");
                }
                part1 = (Part)basePart1;
            }
            catch (Exception ex)
            {
                if (String.CompareOrdinal(ex.Message, "File already exists") == 0)
                {
                    part1 = (Part)Config.TheSession.Parts.FindObject(nmf);
                }
                else
                {
                    Logger.WriteError(ex.ToString());
                    Message.Show("Ошибка в загрузке детали!");
                }
                
            }

            Point3d basePoint1 = new Point3d(0, 0, 0);

            Matrix3x3 orientation1;
            orientation1.Xx = 1.0;
            orientation1.Xy = 0.0;
            orientation1.Xz = 0.0;

            orientation1.Yx = 0.0;
            orientation1.Yy = 1.0;
            orientation1.Yz = 0.0;

            orientation1.Zx = 0.0;
            orientation1.Zy = 0.0;
            orientation1.Zz = 1.0;

            LoadedPart = Config.WorkPart.ComponentAssembly.AddComponent(part1, "MODEL", nmf,
                                                                           basePoint1, orientation1,
                                                                           -1, out partLoadStatus1);

        }
    }
}