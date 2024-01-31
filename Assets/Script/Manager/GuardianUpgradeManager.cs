using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianUpgradeManager : MonoBehaviour
{
    public GuardianStatus[] GuardianStatuses;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpgradeGuardian(Guardian guardian)
    {
        if(guardian.Level < GuardianStatuses.Length - 1)
        {
            guardian.Upgrade(GuardianStatuses[guardian.Level + 1]);
        }
    }
}
