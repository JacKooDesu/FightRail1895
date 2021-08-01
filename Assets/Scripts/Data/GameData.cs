using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Data
{
    public class GameData : MonoBehaviour
    {
        GameState state;
        public GameState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }


    }
}
