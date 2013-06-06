using System;
using System.Collections.Generic;
using System.Text;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.BlockStyler;
using NXOpen.Positioning;

/// <summary>
/// Класс Т-образного и П-образного паза.
/// </summary>
public class Slot
{
    public Component ParentComponent
    {
        get
        {
            return this.slotSet.ParentComponent;
        }
    }

    public Face BottomFace
    {
        get
        {
            return this.slotSet.BottomFace;
        }
    }
    public Face SideFace1
    {
        get
        {
            return sideFace1;
        }
    }
    public Face SideFace2
    {
        get
        {
            return sideFace2;
        }
    }
    public Face TouchFace
    {
        get
        {
            return touchFace;
        }
    }
    public Face TopFace
    {
        get
        {
            return topFace;
        }
    }
    /// <summary>
    /// Возвращает тело элемента, на котором расположен паз.
    /// </summary>
    public Body Body
    {
        get
        {
            return slotSet.Body;
        }
    }
    /// <summary>
    /// Возвращает набор граней и расстояний до них от ВГП, параллельных пазу и находящихся выше него.
    /// </summary>
    public KeyValuePair<Face, double>[] OrtFaces
    {
        get
        {
            if (this.ortFacePairs == null)
            {
                this.findOrtFaces();
            }
            return this.ortFacePairs;
        }
    }
    /// <summary>
    /// Возвращает направление НГП.
    /// </summary>
    public double[] BottomDirection
    {
        get
        {
            return this.bottomDirection;
        }
    }

    Config.SlotType type;

    SlotSet slotSet;

    //Face bottomFace;
    Face sideFace1, sideFace2;
    Face touchFace;
    Face topFace;

    Edge edgeLong1, edgeLong2;
    List<Edge> touchEdges = new List<Edge>();
    Edge touchEdge;

    Straight straight;
    double[] bottomDirection;

    KeyValuePair<Face, double>[] ortFacePairs = null;
    
    /// <summary>
    /// Инициализирует новый экземпляр класса паза.
    /// </summary>
    /// <param name="slotSet">Набор пазов.</param>
    /// <param name="edgeLong1">Первое боковое ребро паза.</param>
    /// <param name="edgeLong2">Второе боковое ребро паза.</param>
    /// <param name="type">Тип паза.</param>
    public Slot(SlotSet slotSet, Edge edgeLong1, Edge edgeLong2, Config.SlotType type)
    {
        this.slotSet = slotSet;
        this.edgeLong1 = edgeLong1;
        this.edgeLong2 = edgeLong2;
        this.type = type;

        this.sideFace1 = this.getNotBottomFace(edgeLong1);
        this.sideFace2 = this.getNotBottomFace(edgeLong2);

        this.setStraitEquation();
    }


    //public void setTouchEdge()
    //{
    //    double length;
    //    double min_len = -1.0;
    //    Edge nearestEdge = null;

    //    foreach (Edge e in this.slotSet.TouchEdges)
    //    {
    //        if (Geom.isEdgePointOnStraight(e, this.straight,
    //                                       out length, this.slotSet.SelectPoint))
    //        {
    //            this.touchEdges.Add(e);
    //            if (min_len == -1.0 || length < min_len)
    //            {
    //                min_len = length;
    //                nearestEdge = e;
    //            }
    //        }
    //    }
    //    this.touchEdge = nearestEdge;
    //}
    public void reverseTouchEdge()
    {
        Edge otherEdge = null;

        foreach (Edge e in this.touchEdges)
        {
            if (e != touchEdge)
            {
                otherEdge = e;
                break;
            }
            
        }
        this.touchEdge = otherEdge;
    }

    public void setTouchFace()
    {
        foreach (Face f in this.touchEdge.GetFaces())
        {
            if (f != this.BottomFace)
            {
                this.touchFace = f;
                break;
            }
        }
    }

    //refactor
    void findOrtFaces()
    {
        double[] direction1 = this.bottomDirection;


        Edge[] pointEdges = topFace.GetEdges();
        Edge pointEdge = pointEdges[0];
        Point3d point, tempPoint;
        pointEdge.GetVertices(out point, out tempPoint);

        Dictionary<Face, double> dictFaces = new Dictionary<Face, double>();

        Face[] faces = this.Body.GetFaces();
        foreach (Face f in faces)
        {
            double[] direction2 = Geom.getDirection(f);

            if (Geom.isEqual(direction1, direction2) && f.SolidFaceType == Face.FaceType.Planar)
            {
                Platan pl = new Platan(f);

                //точка находится "под" необходимыми гранями
                double distance = - pl.getDistanceToPoint(point);
                
                if (distance >= 0 && !dictFaces.ContainsValue(Config.round(distance)))
                {
                    dictFaces.Add(f, Config.round(distance));
                }
            }
        }

        this.setOrtFaces(dictFaces);
    }

    void setOrtFaces(Dictionary<Face, double> dictFaces)
    {
        this.ortFacePairs = new KeyValuePair<Face, double>[dictFaces.Count];
        int i = 0;
        foreach (KeyValuePair<Face, double> pair in dictFaces)
        {
            this.ortFacePairs[i] = pair;
            i++;
        }

        if (this.ortFacePairs.Length > 1)
        {
            Instr.qSortPair(this.ortFacePairs, 0, this.ortFacePairs.Length - 1);            
        }
    }

    /*Dictionary<Edge, double> getNearestEdges(Edge[] edges, Point3d from_point)
    {
        Dictionary<Edge, double> Edges = new Dictionary<Edge, double>();

        foreach (Edge Edge in edges)
        {
            if (Edge.SolidEdgeType == Edge.EdgeType.Linear)
            {
                Point3d FirstPoint, SecondPoint;
                Edge.GetVertices(out FirstPoint, out  SecondPoint);

                Vector Vec1 = new Vector(from_point, FirstPoint);
                Vector Vec2 = new Vector(from_point, SecondPoint);

                double len1 = Vec1.getLength();
                double len2 = Vec2.getLength();

                double min_len;
                if (len1 < len2)
                {
                    min_len = len1;
                }
                else
                {
                    min_len = len2;
                }

                addInDictMinValue(Edges, Edge, min_len);

                //theUI.NXMessageBox.Show("", NXMessageBox.DialogType.Error, min_len + " " + len1 + " " + len2);
            }
        }

        return Edges;
    }*/

    public void highlight()
    {
        edgeLong1.Highlight();
        edgeLong2.Highlight();
    }
    public void unhighlight()
    {
        edgeLong1.Unhighlight();
        edgeLong2.Unhighlight();
    }

    Face getNotBottomFace(Edge slotEdge)
    {
        Face[] faces = slotEdge.GetFaces();
        foreach (Face f in faces)
        {
            if (f != this.BottomFace)
            {
                return f;
            }
        }

        return null;
    }
    void setStraitEquation()
    {
        this.straight = new Straight(this.edgeLong1);
    }


    //refactor slots
    public void findTopFace()
    {
        Face topFace = null;
        Edge topEdge = null;
        Face face;
        Edge edge;
        this.bottomDirection = Geom.getDirection(this.BottomFace);
        double[] direction;

        edge = this.edgeLong1;
        face = this.sideFace1;

        if (this.type == Config.SlotType.Pslot)
        {
            topEdge = this.getNextEdge(face, edge, Config.P_SLOT_HEIGHT);
            topFace = this.getNextFace(topEdge, face);
            direction = Geom.getDirection(topFace);

            if (!Geom.isEqual(this.bottomDirection, direction))
            {
                Config.theUI.NXMessageBox.Show("Error!", NXMessageBox.DialogType.Error, "Печаль с П-образным пазом!");
            }
        }
        else if (this.type == Config.SlotType.Tslot)
        {
            foreach (double slotHeight in Config.T_SLOT_HEIGHT1)
            {
                topEdge = this.getNextEdge(face, edge, slotHeight);

                if (topEdge != null)
                {
                    break;
                }
            }
            topFace = this.getNextFace(topEdge, face);
            edge = topEdge;
            face = topFace;

            topEdge = this.getNextEdge(face, edge, Config.STEP_WIDTH_T_SLOT_1);

            //значит Т-образный паз второго исполнения
            if (topEdge == null)
            {
                
                topEdge = this.getNextEdge(face, edge, Config.STEP_DOWN_WIDTH_T_SLOT_2);
                topFace = this.getNextFace(topEdge, face);
                edge = topEdge;
                face = topFace;
                
                foreach (double d in Config.T_SLOT_HEIGHT3)
                {
                    topEdge = this.getNextEdge(face, edge, d);

                    if (topEdge != null)
                    {
                        break;
                    }
                }
                
                topFace = this.getNextFace(topEdge, face);
                edge = topEdge;
                face = topFace;


                topEdge = this.getNextEdge(face, edge, Config.STEP_UP_WIDTH_T_SLOT_2);
                topFace = this.getNextFace(topEdge, face);
                edge = topEdge;
                face = topFace;

                topEdge = this.getNextEdge(face, edge, Config.T_SLOT_HEIGHT2);
                this.type = Config.SlotType.Tslot2;
            }
            else
            {
                topFace = this.getNextFace(topEdge, face);
                edge = topEdge;
                face = topFace;

                foreach (double d in Config.T_SLOT_HEIGHT)
                {
                    topEdge = this.getNextEdge(face, edge, d);

                    if (topEdge != null)
                    {
                        break;
                    }
                }
                this.type = Config.SlotType.Tslot1;
            }

            topFace = this.getNextFace(topEdge, face);
        }

        direction = Geom.getDirection(topFace);

        if (!Geom.isEqual(this.bottomDirection, direction))
        {
            Config.theUI.NXMessageBox.Show("Error!", NXMessageBox.DialogType.Error, "Печаль с T-образным пазом!");
        }


        this.topFace = topFace;
    }


    Edge getNextEdge(Face face, Edge edge, double distance)
    {
        //поиск верхнего левого ребра
        Edge resultEdge = null;
        Vector vecEtalon = new Vector(edge);
        foreach (Edge e in face.GetEdges())
        {
            if (e != edge)
            {
                Vector vecTmp = new Vector(e);
                if (vecEtalon.IsParallel(vecTmp))
                {
                    Straight edgeStraight = new Straight(e);
                    Point3d heightStart = vecEtalon.Start;
                    Point3d pointOnStraight = Geom.getIntersectionPointStraight(heightStart, edgeStraight);

                    Vector vecHeight = new Vector(heightStart, pointOnStraight);

                    if (Config.round(vecHeight.Length) == distance)
                    {
                        resultEdge = e;
                        break;
                    }
                }
            }
        }

        return resultEdge;
    }

    Face getNextFace(Edge edge, Face face)
    {
        //поиск верхней горизонтальной поверхности
        Face resultFace = null;
        Face[] faces = edge.GetFaces();
        foreach (Face f in faces)
        {
            if (f != face)
            {
                resultFace = f;
                break;
            }
        }

        if (resultFace == null)
        {
            Config.theUI.NXMessageBox.Show("Error!", NXMessageBox.DialogType.Error, "Верхняя горизонтальная поверхность не найдена!");
        }

        return resultFace;
    }

    /*bool isTypeTwo(Face face, Edge edge)
    {
        Vector vecEtalon = new Vector(edge);
        Edge[] edges = face.GetEdges();
        foreach (Edge e in edges)
        {
            if (e != edge)
            {
                Vector vecTmp = new Vector(e);

                if (vecEtalon.isParallel(vecTmp))
                {
                    double[,] edgeEquation = Geom.getStraitEquation(e);
                    Point3d heightStart = vecEtalon.start;
                    Point3d pointOnStraight = Geom.getIntersectionPointStraight(heightStart, edgeEquation);
                    Vector vecHeight = new Vector(heightStart, pointOnStraight);


                    if (Config.doub(vecHeight.getLength()) == Config.STEP_DOWN_WIDTH_T_SLOT_2)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }*/

}

