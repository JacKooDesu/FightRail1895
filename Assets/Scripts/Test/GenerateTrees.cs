using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Entity;

namespace JacDev.Testing
{
    public class GenerateTrees : MonoBehaviour
    {
        [System.Serializable]
        public class MapObject
        {
            public string name;
            public GameObject[] origins;
            public float size = 2f;
            public int max = 20;
            public int min = 5;
            [Range(0f, 1f)]
            public float res = .5f;
        }

        public MapObject stoneSetting;

        public MapObject treeSetting;

        [Header("Map Setting")]
        public float size = 10f;
        public float railWidth = 5f;
        public float spawnpointAvoidSize = 5f;

        List<Rect> objectMapping;

        void Start()
        {
            ButtonGenerate();
        }

        public void ButtonGenerate()
        {
            objectMapping = new List<Rect>();
            objectMapping.Add(new Rect(-size / 2, -railWidth / 2, size, railWidth));

            foreach (Transform t in transform)
            {

                if (t.GetComponent<EntitySpawner>())
                {
                    Rect r =
                        new Rect(
                            t.localPosition.x - spawnpointAvoidSize/2,
                            t.localPosition.z - spawnpointAvoidSize/2,
                            spawnpointAvoidSize,
                            spawnpointAvoidSize / 2 + Mathf.Abs(t.localPosition.z));
                     objectMapping.Add(r);
                     print(r.width + "  " + r.height);
                }
            }
            Generate(treeSetting);
            Generate(stoneSetting);
        }

        void Generate(MapObject mo)
        {
            Transform parent;
            if (transform.Find(mo.name))
            {
                parent = transform.Find(mo.name);
            }
            else
            {
                parent = new GameObject(mo.name).transform;
                parent.SetParent(transform);
            }

            for (int i = parent.childCount - 1; i >= 0; --i)
                Destroy(parent.GetChild(i).gameObject);

            int count = (int)Random.Range(mo.min, (size * size) / (mo.size * mo.size) * mo.res);
            count = (count > mo.max ? mo.max : count);

            for (int i = 0; i < count; ++i)
            {
                float x = Random.Range(-(size / 2) + mo.size, (size / 2) - mo.size);
                float z = Random.Range(-(size / 2) + mo.size, (size / 2) - mo.size);
                Rect rect = new Rect(x + mo.size / 2, z + mo.size / 2, mo.size, mo.size);
                bool overlap = false;
                for (int j = 0; j < objectMapping.Count; ++j)
                {
                    if (rect.Overlaps(objectMapping[j]))
                    {
                        print("overlap");
                        overlap = true;
                        break;
                    }
                }

                if (overlap)
                {
                    continue;
                }
                else
                {
                    objectMapping.Add(rect);
                    GameObject g = Instantiate(mo.origins[Random.Range(0, mo.origins.Length)]);
                    g.transform.position = new Vector3(x, 0, z);
                    g.transform.eulerAngles = new Vector3(0, Random.Range(0, 360f));
                    g.transform.SetParent(parent);
                }
            }
        }
    }

}
