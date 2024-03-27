using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationManager : MonoBehaviour
{
    //spawns operators for the calculation similar to numberblock


    public static CalculationManager Instance;

    void Awake()
    {
        Instance = this;
    }
}
