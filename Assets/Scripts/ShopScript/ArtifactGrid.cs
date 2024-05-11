using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactGrid : MonoBehaviour
{
    ScriptableObject ArtifactInfo;
    public SpriteRenderer SpriteRenderer;
    public Artifact Artifact;

    public void Initialize(Artifact ArtifactInfo)
    {
        this.ArtifactInfo = ArtifactInfo;
        SetArtifact(ArtifactInfo.ArtifactName);
    }

    public void SetArtifact(string ArtifactName)
    {
        SpriteRenderer.sprite = Resources.Load<Sprite>("Artifacts/" + ArtifactName);
    }

    public void BuyArtifact()
    {
        RewardManager.Instance.AddArtifact(ArtifactInfo as Artifact);
    }
}
