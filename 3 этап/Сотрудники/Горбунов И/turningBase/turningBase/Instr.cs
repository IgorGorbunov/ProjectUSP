using System.Collections.Generic;
using System.Drawing;
using System.IO;
using NXOpen;

/// <summary>
/// Статический класс со стандартным инструментарием.
/// </summary>
public static class Instr
{
    public static void QSort(int[] a, int low, int high)
    {
        int i = low;
        int j = high;
        int x = a[(low + high) / 2];  // x - опорный элемент посредине между low и high
        do
        {
            while (a[i] < x) ++i;  // поиск элемента для переноса в старшую часть
            while (a[j] > x) --j;  // поиск элемента для переноса в младшую часть
            if (i <= j)
            {
                // обмен элементов местами:
                int temp = a[i];
                a[i] = a[j];
                a[j] = temp;
                // переход к следующим элементам:
                i++; j--;
            }
        } while (i < j);
        if (low < j) QSort(a, low, j);
        if (i < high) QSort(a, i, high);
    }

    /// <summary>
    /// Производит "быструю сортировку" массива из пар Грань - Вещественное число (расстояние до грани).
    /// </summary>
    /// <param name="a">Одномерный массив пар Грань - Вещественное число</param>
    /// <param name="low">Нижняя грань сортировки (по умолчанию - 0).</param>
    /// <param name="high">Верхняя грань сортировки (по умолчанию - длина_массива-1).</param>
    public static void QSortPairs<TKey>(KeyValuePair<TKey, double>[] a, int low, int high)
    {
        int i = low;
        int j = high;
        double x = a[(low + high) / 2].Value;  // x - опорный элемент посредине между low и high
        do
        {
            while (a[i].Value < x) ++i;  // поиск элемента для переноса в старшую часть
            while (a[j].Value > x) --j;  // поиск элемента для переноса в младшую часть
            if (i <= j)
            {
                // обмен элементов местами:
                KeyValuePair<TKey, double> temp = a[i];
                a[i] = a[j];
                a[j] = temp;
                // переход к следующим элементам:
                i++; j--;
            }
        } while (i < j);
        if (low < j) QSortPairs(a, low, j);
        if (i < high) QSortPairs(a, i, high);
    }

    /// <summary>
    /// Добавляет точку в уникальный список.
    /// </summary>
    /// <param name="list">Список.</param>
    /// <param name="point">Точка.</param>
    public static void AddUnicToList(List<Vertex> list, Point3d point)
    {
        bool alreadyHave = false;
        foreach (Vertex vertex in list)
        {
            if (!Geom.IsEqual(point, vertex.Point)) continue;

            alreadyHave = true;
            break;
        }
        if (alreadyHave) return;

        list.Add(new Vertex(point));
        //NxFunctions.SetAsterix(point);
    }

    /// <summary>
    /// Возвращает экземпляр класса Image из массива байт.
    /// </summary>
    /// <param name="bytes">Массив байт.</param>
    /// <returns></returns>
    public static Image GetImage(byte[] bytes)
    {
        return Image.FromStream(new MemoryStream(bytes));
    }
}

