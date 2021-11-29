using UnityEngine;
using System.Collections;
using System.Linq;

public static class TransformExtensions
{
    public static Transform FindRecursively(this Transform transform, string name)
    {
		if (transform.name == name)
			return transform;

        Transform[] children = transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in children)
        {
            if (t.name == name)
                return t;
        }

        return null;
    }

    public static Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }

    public static Transform[] GetAllChildren(this Transform transform)
    {
        return transform.GetComponentsInChildren<Transform>().Skip(1).ToArray();
    }

    public static Transform[] GetAllChildren(this Transform transform, bool includeInactive)
    {
        return transform.GetComponentsInChildren<Transform>(includeInactive).Skip(1).ToArray();
    }

    public static Transform FindChildRecursive (this Transform transform, string name)
    {        
        foreach (Transform child in transform)
        {
            if (child.name == name)
                return child;

            Transform grandChild = FindChildRecursive(child, name);

            if (grandChild != null)
                return grandChild;
        }

        return null;
    }

    public static void DestroyChildren (this Transform transform)
    {
        foreach (Transform child in transform)
        {
            if (child != transform)
                Object.Destroy(child.gameObject);
        }
    }

    public static void Reset(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
}
