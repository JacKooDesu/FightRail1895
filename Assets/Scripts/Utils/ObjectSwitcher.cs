using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    public bool hideAtStart;
    public int defaultIndex = 0;
    int currentIndex = 0;

    public System.Action<int> OnSwitch;

    private void Start()
    {
        if (hideAtStart)
            Switch(defaultIndex);

    }

    public void Switch(int index)
    {
        for (int i = 0; i < objects.Count; ++i)
        {
            if (objects[i] != null)
                objects[i].SetActive(i == index);
        }
        currentIndex = index;

        if (OnSwitch != null)
            OnSwitch.Invoke(currentIndex);
    }

    public void Next()
    {
        if (currentIndex < objects.Count - 1)
            Switch(currentIndex + 1);
    }

    public void Back()
    {
        if (currentIndex > 0)
            Switch(currentIndex - 1);
    }
}
