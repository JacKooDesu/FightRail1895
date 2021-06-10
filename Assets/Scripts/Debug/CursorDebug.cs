using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Debug
{
    public class CursorDebug : MonoBehaviour
    {
#if UNITY_EDITOR
        public Texture2D cursor;
        public Vector2 offset;

        void Start()
        {
            Cursor.SetCursor(cursor,offset, CursorMode.ForceSoftware);
        }
#endif
    }
}

