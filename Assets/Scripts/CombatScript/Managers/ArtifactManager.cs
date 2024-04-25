using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactManager : MonoBehaviour
{
    public static ArtifactManager Instance;

    public List<Artifact> artifacts;

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Load all artifacts
        LoadArtifacts();
    }

    public void LoadArtifacts()
    {
        // Load all artifacts from Gamemanager
        GameManager.instance._playerInventory.artifacts = artifacts;
    }
}
