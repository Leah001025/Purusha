using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static int GetHashWithString(string path)
    {
        return path.GetHashCode();
    }
    public static int GetHashWithTag(GameObject path)
    {
        return path.tag.GetHashCode();
    }
    public static int GetLayerWithTag(GameObject path)
    {
        return path.layer.GetHashCode();
    }
}
