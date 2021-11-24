using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Event
{
    [CreateAssetMenu(fileName = "Event", menuName = "JacDev/Event/Create Event", order = 0)]
    public class EventSetting : ScriptableObject
    {
        [Header("事件名稱")]
        public string eventName;
        [Header("敘述"),TextArea(3, 10)] public string eventDescription;
        public string eventCommand;
    }
}