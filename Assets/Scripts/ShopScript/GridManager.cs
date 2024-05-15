using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField]
    GameObject ShopGridParent;

    public List<ArtifactGrid> ArtifactsInGrid;

    [SerializeField]
    ArtifactGrid artifacts;

    void Awake()
    {
        Instance = this;
    }

    public void SwitchToBuyArtifact()
    {
        ShopManager.Instance.IsInBuyShop = true;
        RemoveAllArtifactsFromGrid();
        AddArtifactToGrid(ShopManager.Instance.ArtifactsInBuyShop);
    }

    public void SwitchToSellArtifact()
    {
        ShopManager.Instance.IsInBuyShop = false;
        RemoveAllArtifactsFromGrid();
        ShopManager.Instance.SetArtifactsInSellShop();
        AddArtifactToGrid(ShopManager.Instance.ArtifactsInSellShop);
    }

    public void AddArtifactToGrid(Artifact artifact)
    {
        ArtifactGrid artifactGrid = Instantiate(artifacts, ShopGridParent.transform);
        artifactGrid.Initialize(artifact);
        ArtifactsInGrid.Add(artifactGrid.GetComponent<ArtifactGrid>());
    }

    public void AddArtifactToGrid(List<Artifact> artifacts)
    {
        foreach (Artifact artifact in artifacts)
        {
            AddArtifactToGrid(artifact);
        }
    }

    public void RemoveArtifactFromGrid(Artifact artifact)
    {
        foreach (ArtifactGrid artifactGrid in ArtifactsInGrid)
        {
            if (artifactGrid.ArtifactInfo == artifact)
            {
                ArtifactsInGrid.Remove(artifactGrid);
                if (ShopManager.Instance.IsInBuyShop)
                {
                    ShopManager.Instance.ArtifactsInBuyShop.Remove(artifactGrid.ArtifactInfo);
                }
                else
                {
                    ShopManager.Instance.ArtifactsInSellShop.Remove(artifactGrid.ArtifactInfo);
                }
                Destroy(artifactGrid.gameObject);
                break;
            }
        }
    }

    public void RemoveAllArtifactsFromGrid()
    {
        foreach (ArtifactGrid artifactGrid in ArtifactsInGrid)
        {
            Destroy(artifactGrid.gameObject);
        }
        ArtifactsInGrid.Clear();
    }
}
