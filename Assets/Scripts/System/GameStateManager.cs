using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState{
        Title,      // 標題
        IdleMovie,  // 首頁閒置動畫
        LoadGame,   // 讀取場景
        Eyecatch,   // 過場動畫
        OnRail,     // 開火車
        Trade,      // 到站
        Result      // 結算
    }

    public static GameState currentState;

    static public void SetCurrentState(GameState gs){
        currentState = gs;
    }
}
