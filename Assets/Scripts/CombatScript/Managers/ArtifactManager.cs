using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactManager : MonoBehaviour
{
    // Call all the Artifact that effect the game play from start to finish
    public static ArtifactManager Instance;
    public GameObject artifactPrefab;
    public List<Artifact> artifacts;
    public List<GameObject> artifactInPanel;
    public GridLayoutGroup artifactpanelgrid;

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
        for (int i = 0; i < artifacts.Count; i++)
        {
            //Initiate the game object with sprite renderer
            GameObject artifact = Instantiate(artifactPrefab, artifactpanelgrid.transform);
            //Get sprite renderer in the Gameobject and set it to the artifact sprite
            artifact.GetComponent<SpriteRenderer>().sprite = artifacts[i].artifactSprite;
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
            if (artifact.effectType == ArtifactEffectType.AfterTurn)
            {
                // Call the effect of the artifact
                damageInfo = ArtifactEffectManager.Instance.ActivateEffect(artifact, damageInfo);
            }
        }
        return damageInfo;
    }
}
