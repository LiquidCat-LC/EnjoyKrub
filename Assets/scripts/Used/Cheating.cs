using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheating : MonoBehaviour
{
    public PlayerManager _player;
    public SceneRunning _Loading;

    void Start()
    {
        _Loading = SceneRunning.instance;
        _player = PlayerManager.Instance;
    }

    public void CheatSuccess()
    {
        _player.TotalCostumer_Success = 100;
        _player.TotalCostumer_Fail = 0;
        _Loading.LoadSceneWithLoading("Endday");
    }

     public void CheatFail()
    {
        _player.TotalCostumer_Success = 0;
        _player.TotalCostumer_Fail = 100;
        _Loading.LoadSceneWithLoading("Endday");
    }
}
