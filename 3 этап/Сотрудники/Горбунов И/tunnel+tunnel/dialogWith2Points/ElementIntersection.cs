using System;
using System.Collections.Generic;
using System.Text;
using NXOpen;
using NXOpen.GeometricAnalysis;

/// <summary>
/// Класс для проверки пересечений тел.
/// </summary>
public class ElementIntersection
{

    /// <summary>
    /// Возвращает true, если пересечение существует.
    /// </summary>
    public bool InterferenseExists
    {
        get
        {
            this.checkIntersection(false);
            if (this.result == SimpleInterference.Result.NoInterference)
            {
                SI.Reset();
                return false;
            }
            else
            {
                SI.Reset();
                return true;
            }
        }
    }

    /// <summary>
    /// Возвращает true, если существует касание.
    /// </summary>
    public bool TouchExists
    {
        get
        {
            this.checkIntersection(true);
            if (this.result == SimpleInterference.Result.OnlyEdgesOrFacesInterfere)
            {
                SI.Reset();
                return true;
            }
            else
            {
                SI.Reset();
                return false;
            }
        }
    }

    SimpleInterference SI;
    SimpleInterference.Result result;
    Body element1, element2;

    /// <summary>
    /// Инициализирует новый экземпляр класса для проверки пересечения.
    /// </summary>
    /// <param name="body1">Первое тело.</param>
    /// <param name="body2">Второе тело.</param>
    public ElementIntersection(Body body1, Body body2)
    {
        SI = Config.workPart.AnalysisManager.CreateSimpleInterferenceObject();

        this.element1 = body1;
        this.element2 = body2;
    }

    void checkIntersection(bool touch)
    {
        if (touch)
        {
            SI.InterferenceType = SimpleInterference.InterferenceMethod.InterferenceSolid;
        }
        else
        {
            SI.InterferenceType = SimpleInterference.InterferenceMethod.InterferingFaces;
            SI.FaceInterferenceType = SimpleInterference.FaceInterferenceMethod.FirstPairOnly;
        }

        SI.FirstBody.Value = this.element1;
        SI.SecondBody.Value = this.element2;

        this.result = SI.PerformCheck();
        Log.writeLine("Произведена проверка на пересечение элементов. Результат:" +
                            Environment.NewLine + result.ToString());
    }
}

