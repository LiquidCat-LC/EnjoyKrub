using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Button checkButton;
    public PlayerManager _player;

    public void Start()
    {
        _player = FindObjectOfType<PlayerManager>();

        if (checkButton != null)
        {
            checkButton.onClick.AddListener(checkRest);
        }

    }

    public void checkRest()
    {
        _player.checkResult(_player.userCurrent);
    }

}
