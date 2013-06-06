using System;
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
            CheckIntersection(false);
            if (_result == SimpleInterference.Result.NoInterference)
            {
                _si.Reset();
                return false;
            }
            _si.Reset();
            return true;
        }
    }

    /// <summary>
    /// Возвращает true, если существует касание.
    /// </summary>
    public bool TouchExists
    {
        get
        {
            CheckIntersection(true);
            if (_result == SimpleInterference.Result.OnlyEdgesOrFacesInterfere)
            {
                _si.Reset();
                return true;
            }
            _si.Reset();
            return false;
        }
    }

    readonly SimpleInterference _si;
    SimpleInterference.Result _result;
    readonly Body _element1;
    readonly Body _element2;

    /// <summary>
    /// Инициализирует новый экземпляр класса для проверки пересечения.
    /// </summary>
    /// <param name="body1">Первое тело.</param>
    /// <param name="body2">Второе тело.</param>
    public ElementIntersection(Body body1, Body body2)
    {
        _si = Config.WorkPart.AnalysisManager.CreateSimpleInterferenceObject();

        _element1 = body1;
        _element2 = body2;
    }

    void CheckIntersection(bool touch)
    {
        if (touch)
        {
            _si.InterferenceType = SimpleInterference.InterferenceMethod.InterferenceSolid;
        }
        else
        {
            _si.InterferenceType = SimpleInterference.InterferenceMethod.InterferingFaces;
            _si.FaceInterferenceType = SimpleInterference.FaceInterferenceMethod.FirstPairOnly;
        }

        _si.FirstBody.Value = _element1;
        _si.SecondBody.Value = _element2;

        _result = _si.PerformCheck();
        Log.WriteLine("Произведена проверка на пересечение элементов. Результат:" +
                            Environment.NewLine + _result);
    }
}

