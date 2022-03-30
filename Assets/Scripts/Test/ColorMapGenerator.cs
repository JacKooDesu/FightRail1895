using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMapGenerator : MonoBehaviour
{
    public enum InitType
    {
        Random,
        Diagram
    }

    public InitType initType;

    public GameObject cube;
    public Color32 startColor, endColor;
    public int colorCount;

    public Vector2Int size;
    public int gridSize;

    Vector2Int[] diagramPoints;
    int regionAmount;

    void Start()
    {
        SetupCube();
        switch (initType)
        {
            case InitType.Random:
                InitMap();
                return;

            case InitType.Diagram:
                InitByDiagram();
                return;
        }
    }

    void SetupCube()
    {
        cube.transform.localScale = new Vector3(size.x, 1, size.y);
    }

    [ContextMenu("生成")]
    void InitMap()
    {
        Color32[] colors = new Color32[colorCount];
        for (int i = 0; i < colorCount; ++i)
        {
            float t = (float)i / (float)(colorCount - 1);
            colors[i] = Color.Lerp(startColor, endColor, t);
        }

        Texture2D texture = new Texture2D(size.x, size.y);
        texture.filterMode = FilterMode.Point;

        for (int y = 0; y < size.y; y += gridSize)
        {
            for (int x = 0; x < size.x; x += gridSize)
            {
                var color = colors[Random.Range(0, colorCount)];
                var cArray = new Color32[gridSize * gridSize];

                for (int c = 0; c < cArray.Length; ++c)
                    cArray[c] = color;

                Vector2Int fixedGrid = new Vector2Int(gridSize, gridSize);
                if (fixedGrid.x + x > size.x)
                    fixedGrid.x = size.x - x;
                if (fixedGrid.y + y > size.y)
                    fixedGrid.y = size.y - y;

                texture.SetPixels32(x, y, fixedGrid.x, fixedGrid.y, cArray);
            }
        }
        cube.GetComponent<Renderer>().sharedMaterials[0].SetTexture("_BaseMap", texture);

        texture.Apply();
    }

    [ContextMenu("Diagram生成")]
    void InitByDiagram()
    {
        InitDiagram();
        ApplyRegions();
    }

    void InitDiagram()
    {
        regionAmount = size.x * size.y / gridSize;
        diagramPoints = new Vector2Int[regionAmount];
        for (int i = 0; i < regionAmount; ++i)
        {
            bool availiable = false;
            Vector2Int p;
            do
            {
                p = new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));
                availiable = true;
                for (int j = 0; j < i; ++j)
                {
                    if (p == diagramPoints[j])
                    {
                        availiable = false;
                        break;
                    }
                }
            } while (!availiable);

            diagramPoints[i] = p;
        }
    }

    void ApplyRegions()
    {
        Color32[] colors = new Color32[regionAmount];
        for (int i = 0; i < regionAmount; ++i)
        {
            float t = (float)i / (float)(regionAmount - 1);
            colors[i] = Color.Lerp(startColor, endColor, t);
        }

        Texture2D texture = new Texture2D(size.x, size.y);
        texture.filterMode = FilterMode.Point;

        for (int i = 0; i < size.x * size.y; ++i)
        {
            float minDst = float.MaxValue;
            int j = 0;
            foreach (var p in diagramPoints)
            {
                Vector2Int current = new Vector2Int(i / size.x, i % size.y);
                if (Vector2Int.Distance(current, p) < minDst)
                {
                    minDst = Vector2Int.Distance(current, p);
                    texture.SetPixel(current.x, current.y, colors[j]);
                }
                ++j;
            }
        }

        cube.GetComponent<Renderer>().sharedMaterials[0].SetTexture("_BaseMap", texture);

        texture.Apply();
    }
}
