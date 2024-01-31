using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst
    {
        get; private set;
    }
    public PlayerManager playerManager;
    public GuardianUpgradeManager guardianUpgradeManager;
    public GuardianBuildManager guardianBuildManager;

    private void Awake()
    {
        if(Inst == null)
        {
            Inst = this;
        }
        else
        {
            Destroy(Inst);
        }
    }

    public void GameDefeat()
    {

    }
    public void EnemyDead(int coin)
    {
        playerManager.Coin += coin;
    }
}
