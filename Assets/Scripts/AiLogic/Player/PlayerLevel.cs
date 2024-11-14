using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private PlayerLevelInformation _playerLevelInfo;
    public PlayerLevelInformation GetPlayerLevelInformation() => _playerLevelInfo;
    [SerializeField] private int _level;
    public int GetLevel() => _level;

    public void SetLevel(int level)
    {
        
    }
}
