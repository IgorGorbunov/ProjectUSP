using System;
using System.Collections.Generic;
using System.Text;
using NXOpen.GeometricAnalysis;

/// <summary>
/// Класс для проверки пересечений элементов УСП.
/// </summary>
public class ElementIntersection
{
    /// <summary>
    /// Возвращает true, если пересечение существует.
    /// </summary>
    public bool interferenseExists = true;

    SimpleInterference.Result result;

    /// <summary>
    /// Инициализирует новый экземпляр класса для проверки пересечения.
    /// </summary>
    /// <param name="element1">Первый элемент УСП.</param>
    /// <param name="element2">Второй элемент УСП.</param>
    public ElementIntersection(UspElement element1, UspElement element2)
    {
        this.checkIntersection(element1, element2);

        if (this.result == SimpleInterference.Result.NoInterference)
        {
            interferenseExists = false;
        }
    }

    void checkIntersection(UspElement element1, UspElement element2)
    {
        SimpleInterference SI = Config.workPart.AnalysisManager.CreateSimpleInterferenceObject();

        SI.InterferenceType = SimpleInterference.InterferenceMethod.InterferingFaces;
        SI.FaceInterferenceType = SimpleInterference.FaceInterferenceMethod.AllPairs;

        SI.FirstBody.Value = element1.Body;
        SI.SecondBody.Value = element2.Body;

        this.result = SI.PerformCheck();
        Log.writeLine("Произведена проверка на пересечение элементов. Результат:" +
                            Environment.NewLine + result.ToString());
    }
}

