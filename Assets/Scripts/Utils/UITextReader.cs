using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JacDev.Utils
{
    public class UITextReader : MonoBehaviour
    {
        public float speed = 5;    // 每秒字數
        public Text textUI;     // 使用之UI

        [TextArea(2, 10)]
        public List<string> contents;   // 文字內容
        int currentContent = 0;    // 讀取內容陣列位置

        protected void OnEnable()
        {
            if (Application.isPlaying)
                StartCoroutine(Read());
        }

        IEnumerator Read()
        {
            if (textUI != null)
            {
                string s = contents[currentContent];
                textUI.text = "";
                for (int i = 0; i < s.Length; ++i)
                {
                    textUI.text = textUI.text + s[i];
                    yield return new WaitForSeconds(1f / speed);
                }
            }
        }

        public void NextContent()
        {
            currentContent++;
        }
    }
}

