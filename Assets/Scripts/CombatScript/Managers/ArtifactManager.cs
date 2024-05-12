using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactManager : MonoBehaviour
{
    // Call all the Artifact that effect the game play from start to finish
    public static ArtifactManager Instance;

    public List<Artifact> artifacts;
    public Transform artifactPosition;
    public GameObject artifactInfo;

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Load all artifacts
        LoadArtifacts();
    }

    public void CreateArtifactInfo()
    {
        foreach (Artifact artifact in artifacts)
        {
            // Create ArtifactInfo in ArtifactPanel
        }
    }

    public void LoadArtifacts()
    {
        // Load all artifacts from Gamemanager
        artifacts = GameManager.instance._playerInventory.artifacts;
    }

    public void OnceStartCombat()
    {
        foreach (Artifact artifact in artifacts)
        {
            if (artifact.effectType == ArtifactEffectType.OnceStartCombat)
            {
                // Call the effect of the artifact
            }
        }
    }

    public void BeforeTurnStart()
    {
        foreach (Artifact artifact in artifacts)
        {
            if (artifact.effectType == ArtifactEffectType.BeforeTurn)
            {
                // Call the effect of the artifact
            }
        }
    }

    public void AfterTurn()
    {
        foreach (Artifact artifact in artifacts)
        {
            if (artifact.effectType == ArtifactEffectType.BeforeTurn)
            {
                // Call the effect of the artifact
            }
        }
    }
}
