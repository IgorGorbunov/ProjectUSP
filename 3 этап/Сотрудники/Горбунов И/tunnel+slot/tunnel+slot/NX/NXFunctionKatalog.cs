using System;
using System.Collections.Generic;
using NXOpen;

namespace Katalog2005.Algorithm
{
    partial class SpecialFunctions
    {
        static private float Z_coor;

        static public Session theSession;

        static public Part workPart;

        static public Part displayPart, Part_Specification;

        /// <summary>
        /// Инициализация объектов UG
        /// </summary>   
        /// <returns></returns>
        public static void initUGData()
        {
            Z_coor = 0;

            theSession = Session.GetSession();


            workPart = theSession.Parts.Work;

            displayPart = theSession.Parts.Display;

            Part_Specification = theSession.Parts.Display;
        }

        public void defineTypeOfModel(string oboznOfUsp)
        {
            if (SQLOracle.exist(oboznOfUsp, "HD", "MODEL_ATTR"))
            {
                loadPartToTemp(oboznOfUsp);
            }
            else if (SQLOracle.exist(oboznOfUsp, "HD", "MODEL_ATTR20"))
            {
                loadPartToTempSpecDet(oboznOfUsp);
            }
        }
        /// <summary>
        /// Загрузка стандартной модели в Temp
        /// </summary>   
        /// <returns></returns>
        public void loadPartToTemp(string oboznachenie)
        {
            string curname;

            string openPart = SQLOracle.UnloadPartToTEMPFolder(oboznachenie);

            List<string> childComponents = SQLOracle.GetInformationListWithParamQuery("NMF", "MODEL_STRUCT21", "PARENT", (oboznachenie + ".prt"));

            for (int i = 0; i < childComponents.Count; i++)
            {
                curname = System.IO.Path.GetFileNameWithoutExtension(childComponents[i]);
                SQLOracle.UnloadPartToTEMPFolder(curname);
            }

            if (String.Compare(openPart, "0") != 0)
            {
                if (string.Compare(SQLOracle.ParamQuerySelect("select nalichi from db_data where obozn = :obozn", "obozn", oboznachenie), "0") != 0)
                {
                    SpecialFunctions.loadPartToNX(oboznachenie + ".prt");
                }
                else
                {
                    Message.Show("Данной детали нет в наличии на складе");
                    //MessageBox.Show("Данной детали нет в наличии на складе");
                }
            }
        }

        /// <summary>
        /// Загрузка не стандартной модели в Temp
        /// </summary>   
        /// <returns></returns>
        public void loadPartToTempSpecDet(string oboznachenie)
        {

            string NMF = SQLOracle.ParamQuerySelect("SELECT NMF FROM KTC.MODEL_ATTR20 WHERE HD = :HD", "HD", oboznachenie);

            string openPart = SQLOracle.UnloadOsnasToTEMPFolderFile20(NMF.Trim());


            if (String.Compare(openPart, "0") != 0)
            {
                if (string.Compare(SQLOracle.ParamQuerySelect("select nalichi from db_data where obozn = :obozn", "obozn", oboznachenie), "0") != 0)
                {
                    SpecialFunctions.loadPartToNX(oboznachenie + ".prt");
                }
                else
                {
                    Message.Show("Данной детали нет в наличии на складе");
                    //MessageBox.Show("Данной детали нет в наличии на складе");
                }

            }

        }

        /// <summary>
        /// Загрузка модели в NX
        /// </summary>   
        /// <returns></returns>
        public static void loadPartToNX(string NMF)
        {

            try
            {

                BasePart basePart1;

                PartLoadStatus partLoadStatus1;

                basePart1 = theSession.Parts.OpenBase((System.IO.Path.GetTempPath() + "UGH\\" + NMF), out partLoadStatus1);

                Part part1 = (Part)basePart1;

                partLoadStatus1.Dispose();


                Point3d basePoint1 = new Point3d(Z_coor, Z_coor, Z_coor);

                //if (MessageBox.Show("Задать координаты автоматически?", "Сообщение", MessageBoxButtons.YesNo) == DialogResult.No)
                //{
                //    WinFroms.LoadPartToNX.xyzPRM setNewCoord;
                //    setNewCoord = new WinFroms.LoadPartToNX.xyzPRM();
                //    setNewCoord.ShowDialog();
                //    basePoint1.X = setNewCoord.xCoordPrm;
                //    basePoint1.X = setNewCoord.xCoordPrm;
                //    basePoint1.X = setNewCoord.xCoordPrm;


                //}

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

                PartLoadStatus partLoadStatus2;

                NXOpen.Assemblies.Component component1;

                component1 = workPart.ComponentAssembly.AddComponent(part1, "MODEL", NMF, basePoint1, orientation1, -1, out partLoadStatus2);

                partLoadStatus2.Dispose();

            }
            catch (Exception ex)
            {

                if (String.Compare(ex.Message, "File already exists") == 0)
                {

                    Part part1 = (Part)theSession.Parts.FindObject(NMF);

                    Point3d basePoint1 = new Point3d(Z_coor, Z_coor, Z_coor);



                    //if (MessageBox.Show("Задать координаты автоматически?", "Сообщение", MessageBoxButtons.YesNo) == DialogResult.No)
                    //{
                    //    WinFroms.LoadPartToNX.xyzPRM setNewCoord;
                    //    setNewCoord = new WinFroms.LoadPartToNX.xyzPRM();
                    //    setNewCoord.ShowDialog();
                    //    basePoint1.X = setNewCoord.xCoordPrm;
                    //    basePoint1.X = setNewCoord.xCoordPrm;
                    //    basePoint1.X = setNewCoord.xCoordPrm;

                    //}

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

                    PartLoadStatus partLoadStatus2;

                    NXOpen.Assemblies.Component component1;


                    component1 = workPart.ComponentAssembly.AddComponent(part1, "MODEL", NMF, basePoint1, orientation1, -1, out partLoadStatus2);

                    partLoadStatus2.Dispose();

                    //								PartLoadStatus partLoadStatus_disp;
                    //
                    //								theSession.Parts.SetDisplay(displayPart,false,false,out partLoadStatus_disp);
                    //
                    //								partLoadStatus_disp.Dispose();							

                }
                else
                {
                    //MessageBox.Show(ex.Message, "Ошибка");
                    Message.Show(ex);
                }
            }

            Z_coor = Z_coor + 50;


        }
    }
}