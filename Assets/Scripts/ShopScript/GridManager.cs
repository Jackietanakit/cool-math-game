using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    GameObject ShopGridParent;

    public List<ArtifactGrid> ArtifactsInShop;

    [SerializeField]
    GameObject artifacts;

    void AddArtifactToGrid(Artifact artifact)
    {
        GameObject artifactGrid = Instantiate(artifacts, ShopGridParent.transform);
        artifactGrid.GetComponent<ArtifactGrid>().Initialize(artifact);
        ArtifactsInShop.Add(artifactGrid.GetComponent<ArtifactGrid>());
    }
}
