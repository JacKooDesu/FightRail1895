using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    public bool hideAtStart;
    public int defaultObject = 0;

    private void Start()
    {
        if (hideAtStart)
            Switch(defaultObject);

    }

    public void Switch(int index)
    {
        foreach (GameObject g in objects)
        {
            if (g != null)
                g.SetActive(false);
        }

        objects[index].SetActive(true);
    }
}
