using System;
using System.Collections.Generic;
using System.Text;
using NXOpen;

/// <summary>
/// Класс содержащий отверстие для базирования.
/// </summary>
public class Tunnel
{
    
    Face face;

    /// <summary>
    /// Инициализирует новый экземпляр класса отверстия для базирования для данной грани.
    /// </summary>
    /// <param name="face"></param>
    public Tunnel(Face face)
    {
        this.face = face;
    }
}

