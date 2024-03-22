using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBlocksManager : MonoBehaviour
{
    public static NumberBlocksManager Instance;
    public RectTransform NumberBlockContainer; //Parent of all number blocks

    public GameObject AnswerContainer; //Will spawn the answer number blocks

    public NumberBlock NumberBlockPrefab;

    public int numberBlocksInContainer = 0;

    public int maxNumberBlockInContainer = 10;
    public List<NumberBlock> numberBlocks = new List<NumberBlock>();

    void Awake()
    {
        Instance = this;
    }

    void AddNumberBlock(NumberBlock numberBlock)
    {
        numberBlocks.Add(numberBlock);
    }

    //using container to set the position of the number block
    public void CreateNumberBlockAtContainer(int number)
    {
        // instantiate the number to be a child of the container
        NumberBlock numberBlock = Instantiate(NumberBlockPrefab, NumberBlockContainer, true);
        PutNumberBlockIntoContainer(numberBlock);
        numberBlock.Initialize(number);
        numberBlocks.Add(numberBlock);
    }

    public void CreateManyNumberBlocks(int[] numbers)
    {
        foreach (int number in numbers)
        {
            CreateNumberBlockAtContainer(number);
        }
    }

    public void PutNumberBlockIntoContainer(NumberBlock numberBlock)
    {
        numberBlock.transform.SetParent(NumberBlockContainer, true);
        numberBlock.transform.localPosition = new Vector2((0.055f - numberBlock.RectPosition.anchoredPosition.x) + numberBlocksInContainer * 0.09f, 0.15f);
        numberBlocksInContainer++;
    }

    public void RemoveNumberBlockFromContainer(NumberBlock numberBlock)
    {
        numberBlocksInContainer--;
    }

}
