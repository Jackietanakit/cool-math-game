using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationManager : MonoBehaviour
{
    //spawns operators for the calculation similar to numberblock
    public GameObject OperatorPrefab;

    public Transform OperatorContainer;

    public static CalculationManager Instance;

    void Awake()
    {
        Instance = this;
    }
}
