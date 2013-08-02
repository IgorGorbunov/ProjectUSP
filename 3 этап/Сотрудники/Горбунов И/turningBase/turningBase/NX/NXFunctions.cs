using NXOpen;

static class NxFunctions
{
    public static bool GetFace(TaggedObject[] to, UspElement element, out Face face)
    {
        TaggedObject t = to[0];

        Part p = (Part)element.ElementComponent.OwningPart;
        BodyCollection bc = p.Bodies;
        foreach (Body b in bc)
        {
            Face[] fc = b.GetFaces();

            foreach (Face f in fc)
            {
                if (f.Tag != t.Tag) continue;
                face = f;
                return true;
            }
        }

        face = null;
        return false;
    }
}
