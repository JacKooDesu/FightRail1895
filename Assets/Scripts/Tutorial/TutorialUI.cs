using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JacDev.Tutorial
{
    public class TutorialUI : MonoBehaviour
    {
        public Text description;
        public Image image;

        public void Setup(string description, Sprite image)
        {
            this.description.text = description;
            this.image.sprite = image;
        }
    }
}