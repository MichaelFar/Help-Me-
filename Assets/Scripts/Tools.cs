using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools
{
    // Start is called before the first frame update
    public static List<GameObject> GetChildrenOfObject(GameObject parent)
    {
        List<GameObject> children = new List<GameObject>();
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            children.Add(parent.transform.GetChild(i).gameObject);
        }
        return children;
    }
}
