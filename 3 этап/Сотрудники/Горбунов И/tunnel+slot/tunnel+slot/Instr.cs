using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Статический класс со стандартным инструментарием.
/// </summary>
public static class Instr
{
    public static void qSort(int[] A, int low, int high)
    {
        int i = low;
        int j = high;
        int x = A[(low + high) / 2];  // x - опорный элемент посредине между low и high
        do
        {
            while (A[i] < x) ++i;  // поиск элемента для переноса в старшую часть
            while (A[j] > x) --j;  // поиск элемента для переноса в младшую часть
            if (i <= j)
            {
                // обмен элементов местами:
                int temp = A[i];
                A[i] = A[j];
                A[j] = temp;
                // переход к следующим элементам:
                i++; j--;
            }
        } while (i < j);
        if (low < j) qSort(A, low, j);
        if (i < high) qSort(A, i, high);
    }

    /// <summary>
    /// Производит "быструю сортировку" массива из пар Грань - Вещественное число (расстояние до грани).
    /// </summary>
    /// <param name="A">Одномерный массив пар Грань - Вещественное число</param>
    /// <param name="low">Нижняя грань сортировки (по умолчанию - 0).</param>
    /// <param name="high">Верхняя грань сортировки (по умолчанию - длина_массива-1).</param>
    public static void qSortPair(KeyValuePair<NXOpen.Face, double>[] A, int low, int high)
    {
        int i = low;
        int j = high;
        double x = A[(low + high) / 2].Value;  // x - опорный элемент посредине между low и high
        do
        {
            while (A[i].Value < x) ++i;  // поиск элемента для переноса в старшую часть
            while (A[j].Value > x) --j;  // поиск элемента для переноса в младшую часть
            if (i <= j)
            {
                // обмен элементов местами:
                KeyValuePair<NXOpen.Face, double> temp = A[i];
                A[i] = A[j];
                A[j] = temp;
                // переход к следующим элементам:
                i++; j--;
            }
        } while (i < j);
        if (low < j) qSortPair(A, low, j);
        if (i < high) qSortPair(A, i, high);
    }

}

