using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;

    public bool hasDraggedSomething;

    void Start()
    {
        NumberBlocksManager.Instance.CreateManyNumberBlocks(
            new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
        );
    }

    void Awake()
    {
        Instance = this;
    }

    
}
