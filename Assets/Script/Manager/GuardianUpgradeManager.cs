using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianUpgradeManager : MonoBehaviour
{
    public GuardianStatus[] GuardianStatuses;
    public void UpgradeGuardian(Guardian guardian)
    {
        if(guardian.Level < GuardianStatuses.Length - 1)
        {
            PlayerManager player = GameManager.Inst.playerManager;
            int cost = GuardianStatuses[guardian.Level + 1].UpgradeCost;

            if (player.CanUseCoin(cost))
            {
                player.UseCoin(cost);
                guardian.Upgrade(GuardianStatuses[guardian.Level + 1]);
            }
        }
    }
}
