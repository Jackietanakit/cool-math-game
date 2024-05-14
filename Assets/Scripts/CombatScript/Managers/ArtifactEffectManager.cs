using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactEffectManager : MonoBehaviour
{
    public static ArtifactEffectManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void ActivateEffect(Artifact artifact)
    {
        Debug.Log("Activating effect: " + artifact.ArtifactName);
        //Switch case using artifact name
        switch (artifact.ArtifactName)
        {
            case "Big Pocket":
                Big_Pocket();
                break;
            case "Lucky 7":
                Lucky7();
                break;
            case "Life Support":
                Life_Support();
                break;
            default:
                break;
        }
    }

    public DamageInfo ActivateEffect(Artifact artifact, DamageInfo damageInfo)
    {
        Debug.Log("Activating effect: " + artifact.ArtifactName);
        switch (artifact.ArtifactName)
        {
            case "Perfect Blade":
                return Perfect_Blade(damageInfo);
            default:
                return damageInfo;
        }
    }

    void Big_Pocket()
    {
        //Start with an extra Number Block, add 1 to numberSpawnPerTurn
        NumberBlocksManager.Instance.numberSpawnPerTurn += 1;
    }

    void Lucky7()
    {
        //Convert all number blocks with value 1, 2 or 3 to 7\
        foreach (NumberBlock numberBlock in NumberBlocksManager.Instance.numberBlocks)
        {
            if (numberBlock.number == 1 || numberBlock.number == 2 || numberBlock.number == 3)
            {
                numberBlock.ChangeNumber(7);
            }
        }
    }

    void Life_Support()
    {
        //Change the first enemy health to 24
        CombatManager.Instance.GetNearestEnemies(1)[0].SetHealth(24);
    }

    DamageInfo Perfect_Blade(DamageInfo damageInfo)
    {
        //Piece through 1 more enemy if perfect
        //Check if perfect
        if (damageInfo.isPerfect)
        {
            damageInfo.piercing += 1;
        }

        return damageInfo;
    }
}
