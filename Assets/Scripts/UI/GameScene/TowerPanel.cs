using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JacDev.Utils.UISlicker;
using JacDev.GameSystem;

namespace JacDev.UI.GameScene
{
    public class TowerPanel : MonoBehaviour
    {
        PositionSlick positionSlick;

        private void Start()
        {
            positionSlick = GetComponent<PositionSlick>();
            // InputHandler.Singleton.placingTowerEvent.onBegin += () => positionSlick.Slick("hide");
            // InputHandler.Singleton.placingTowerEvent.onEnd += () => positionSlick.SlickBack();
        }

    }

}
