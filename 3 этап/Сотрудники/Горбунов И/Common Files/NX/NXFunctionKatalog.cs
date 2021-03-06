﻿using System;
using System.Collections.Generic;
using System.IO;
using NXOpen;
using NXOpen.Assemblies;

namespace Katalog2005.Algorithm
{
    static class SpecialFunctions

    {
        public static Component UnLoadedPart;

        public static void DefineTypeOfModel(string oboznOfUsp)
        {
            string path = Path.GetTempPath() + Config.TmpFolder + Path.DirectorySeparatorChar;
            string fileName = oboznOfUsp + Config.PartFileExtension;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fullPath = Path.Combine(path, fileName);
            if (!File.Exists(fullPath))
            {
                if (SQLOracle.exist((object)oboznOfUsp, "HD", "MODEL_ATTR"))
                {
                    LoadPartToTemp(oboznOfUsp);
                }
                else if (SQLOracle.exist((object)oboznOfUsp, "HD", "MODEL_ATTR20"))
                {
                    LoadPartToTempSpecDet(oboznOfUsp);
                }
                else
                {
                    const string mess = "Детали нет в каталоге!";
                    Logger.WriteError(mess);
                    Message.Show(mess);
                }
            }

            LoadPartToNx(fullPath);
        }

        /// <summary>
        /// Загрузка стандартной модели в Temp
        /// </summary>   
        /// <returns></returns>
        private static void LoadPartToTemp(string oboznachenie)
        {
            string openPart = SQLOracle.UnloadPartToTEMPFolder(oboznachenie);

            List<string> childComponents = SQLOracle.GetInformationListWithParamQuery("NMF",
                                                                                      "MODEL_STRUCT21",
                                                                                      "PARENT",
                                                                                      (oboznachenie +
                                                                                       ".prt"));

            for (int i = 0; i < childComponents.Count; i++)
            {
                string curname = Path.GetFileNameWithoutExtension(childComponents[i]);
                SQLOracle.UnloadPartToTEMPFolder(curname);
            }

            //if (String.Compare(openPart, "0") != 0)
            //{
            //    if (
            //        string.Compare(
            //            SQLOracle.ParamQuerySelect(
            //                "select nalichi from db_data where obozn = :obozn", "obozn",
            //                oboznachenie), "0") != 0)
            //    {
            //        SpecialFunctions.loadPartToNX(oboznachenie + ".prt");
            //    }
            //    else
            //    {
            //        Message.Show("Данной детали нет в наличии на складе");
            //        //MessageBox.Show("Данной детали нет в наличии на складе");
            //    }
            //}
        }

        /// <summary>
        /// Загрузка не стандартной модели в Temp
        /// </summary>   
        /// <returns></returns>
        private static void LoadPartToTempSpecDet(string oboznachenie)
        {

            string nmf =
                SQLOracle.ParamQuerySelect("SELECT NMF FROM KTC.MODEL_ATTR20 WHERE HD = :HD", "HD",
                                           oboznachenie);


            string openPart = SQLOracle.UnloadOsnasToTEMPFolderFile20(nmf.Trim());


            //if (String.Compare(openPart, "0") != 0)
            //{
            //    if (
            //        string.Compare(
            //            SQLOracle.ParamQuerySelect(
            //                "select nalichi from db_data where obozn = :obozn", "obozn",
            //                oboznachenie), "0") != 0)
            //    {
            //        SpecialFunctions.loadPartToNX(oboznachenie + ".prt");
            //    }
            //    else
            //    {
            //        Message.Show("Данной детали нет в наличии на складе");
            //        //MessageBox.Show("Данной детали нет в наличии на складе");
            //    }

            //}

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

            UnLoadedPart = Config.WorkPart.ComponentAssembly.AddComponent(part1, "MODEL", nmf,
                                                                           basePoint1, orientation1,
                                                                           -1, out partLoadStatus1);
                //}
                //catch (Exception ex)
                //{


            //        if (String.Compare(ex.Message, "File already exists") == 0)
            //        {

            //            Part part1 = (Part)theSession.Parts.FindObject(NMF);

            //            Point3d basePoint1 = new Point3d(Z_coor, Z_coor, Z_coor);



            //            //if (MessageBox.Show("Задать координаты автоматически?", "Сообщение", MessageBoxButtons.YesNo) == DialogResult.No)
            //            //{
            //            //    WinFroms.LoadPartToNX.xyzPRM setNewCoord;
            //            //    setNewCoord = new WinFroms.LoadPartToNX.xyzPRM();
            //            //    setNewCoord.ShowDialog();
            //            //    basePoint1.X = setNewCoord.xCoordPrm;
            //            //    basePoint1.X = setNewCoord.xCoordPrm;
            //            //    basePoint1.X = setNewCoord.xCoordPrm;

            //            //}

            //            Matrix3x3 orientation1;
            //            orientation1.Xx = 1.0;
            //            orientation1.Xy = 0.0;
            //            orientation1.Xz = 0.0;
            //            orientation1.Yx = 0.0;
            //            orientation1.Yy = 1.0;
            //            orientation1.Yz = 0.0;
            //            orientation1.Zx = 0.0;
            //            orientation1.Zy = 0.0;
            //            orientation1.Zz = 1.0;

            //            PartLoadStatus partLoadStatus2;

            //            _unLoadedPart = workPart.ComponentAssembly.AddComponent(part1, "MODEL", NMF, basePoint1, orientation1, -1, out partLoadStatus2);

            //            partLoadStatus2.Dispose();

            //            //								PartLoadStatus partLoadStatus_disp;
            //            //
            //            //								theSession.Parts.SetDisplay(displayPart,false,false,out partLoadStatus_disp);
            //            //
            //            //								partLoadStatus_disp.Dispose();							

            //        }
            //        else
            //        {
            //            //MessageBox.Show(ex.Message, "Ошибка");
            //            Message.Show(ex);
            //        }
                //}

            //    Z_coor = Z_coor + 50;


            //}
        }
    }
}