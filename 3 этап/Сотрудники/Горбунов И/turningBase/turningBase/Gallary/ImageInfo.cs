using System;
using System.Drawing;
using algorithm;

public class ImageInfo
{
    public Image Image;
    public String Name;
    public int ElementCount;
    public bool Selected;
    public AngleSolution Solution;

    public ImageInfo(Image image, String name, int elementCount, bool selected, AngleSolution solution)
    {
        Image = image;
        Name = name;
        Selected = selected;
        ElementCount = elementCount;
        Solution = solution;
    }
}
