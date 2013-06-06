using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

// Igor
static class Element
{
    static public bool addElemIsCanceled = false;

    static int minProcToAdd = 10;
    static int minToAdd = 2;

    public static string titleToAdd = "";

    /// <summary>
    /// Возвращает true при наличии свободных элементов в БД и текущей сборке, возращает кол-во элементов
    /// </summary>
    /// <param name="title">Обозначение элемента</param>
    /// <param name="count">Кол-во элементов</param>
    /// <returns></returns>
    public static bool isFree(string title, out int count)
    {
        count = 0;

        int nInDB = 0;
        if (isFreeInDB(title, out nInDB))
        {
            count = nInDB;

            int nAll = _ELEMENTS.getAllN(title);
            if (nAll >= count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Возвращает true при наличии свободных элементов в БД, возращает кол-во элементов
    /// </summary>
    /// <param name="title">Обозначение элемента</param>
    /// <param name="count">Кол-во элементов</param>
    /// <returns></returns>
    public static bool isFreeInDB(string title, out int count)
    {
        count = _ELEMENTS.getFreeN(title);
        if (count > 0)
        {
            return true;           
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Возвращает кол-во элементов
    /// </summary>
    /// <param name="title">Обозначение элемента</param>
    /// <returns></returns>
    public static int getAllN(string title)
    {
        return _ELEMENTS.getAllN(title);
    }


    /// <summary>
    /// Проверяет возможность использования данного элемента, возвращает false, если никакой елемент не был выбран
    /// </summary>
    /// <param name="inTitle">Обозначение проверяемого элемента</param>
    /// <param name="outTitle">Обозначение выбранного элемента</param>
    /// <param name="nElsInProject">Кол-во элементов в сборке</param>
    /// <returns></returns>
    public static bool checkFree(string inTitle, out string outTitle, int nElsInProject)
    {
        int nFreeElems = 0;
        if (isFree(inTitle, out nFreeElems) && (nFreeElems > nElsInProject))
        {
            nFreeElems -= nElsInProject;
            int nAllElems = getAllN(inTitle);

            if ((nFreeElems > minToAdd) && (nAllElems / nFreeElems <= minProcToAdd))
            {
                outTitle = inTitle;
                return true;
            }
            else
            {
                if (launchUsabilityForm(inTitle, nFreeElems, nAllElems, nElsInProject, out outTitle))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            int nAllElems = getAllN(inTitle);
            nFreeElems -= nElsInProject;

            if(launchUsabilityForm(inTitle, nFreeElems, nAllElems, nElsInProject, out outTitle))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    /// <summary>
    /// Запускает форму используемости элемента в сборках, возвращает false, если форма была отменена, true, если элемент был выбран
    /// </summary>
    /// <param name="title">Обозначение нехватающего элемента</param>
    /// <param name="nFreeElems">Кол-во свободных нехватающих элементов</param>
    /// <param name="nAllElems">Кол-во нехватающих элементов (всего)</param>
    /// <param name="outTitle">Обозначение выбранного элемента</param>
    /// <param name="nInProject">Кол-во элементов в тек. сборке</param>
    /// <returns></returns>
    static bool launchUsabilityForm(string inTitle, int nFreeElems, int nAllElems, int nInProject, out string outTitle)
    {
        Form fElUsab = new FElementsUsability(inTitle, nFreeElems, nAllElems, nInProject);
        fElUsab.ShowDialog();
        fElUsab.Close();

        outTitle = "";
        if (addElemIsCanceled)
        {
            return false;
        }
        else
        {
            outTitle = titleToAdd;
            return true;
        }
    }
}

