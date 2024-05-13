using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactManager : MonoBehaviour
{
    // Call all the Artifact that effect the game play from start to finish
    public static ArtifactManager Instance;

    public List<Artifact> artifacts;
    public List<GameObject> artifactInPanel; //There is 9 position
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
        //Get sprite renderer from artifactInpanel and set to artifact.artifactsprite
        for (int i = 0; i < Math.Min(artifactInPanel.Count, artifacts.Count); i++)
        {
            artifactInPanel[i].GetComponent<SpriteRenderer>().sprite = artifacts[i].artifactSprite;
        }
    }

    public void OnceStartCombat()
    {
        foreach (Artifact artifact in artifacts)
        {
            if (artifact.effectType == ArtifactEffectType.OnceStartCombat)
            {
                // Call the effect of the artifact
                ArtifactEffectManager.Instance.ActivateEffect(artifact);
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
                ArtifactEffectManager.Instance.ActivateEffect(artifact);
            }
        }
    }

    public DamageInfo AfterTurn(DamageInfo damageInfo)
    {
        foreach (Artifact artifact in artifacts)
        {
            if (artifact.effectType == ArtifactEffectType.BeforeTurn)
            {
                // Call the effect of the artifact
                damageInfo = ArtifactEffectManager.Instance.ActivateEffect(artifact, damageInfo);
            }
        }
        return damageInfo;
    }
}
