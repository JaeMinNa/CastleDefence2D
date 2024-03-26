using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobyButtonController : MonoBehaviour
{
    public void BattleSceneButton()
    {
        GameManager.I.ScenesManager.SceneMove("BattleScene0");
    }
}
