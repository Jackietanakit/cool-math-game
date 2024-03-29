using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBlocksManager : MonoBehaviour
{
    public static NumberBlocksManager Instance;

    public GameObject AnswerContainer; //Will spawn the answer number blocks

    public NumberBlockZone NumberBlockZone;

    public AnswerZone AnswerZone;

    public NumberBlock NumberBlockPrefab;

    public int numberBlocksInContainer = 0;

    public int maxNumberBlockInContainer = 10;
    public List<NumberBlock> numberBlocks = new List<NumberBlock>();

    void Awake()
    {
        Instance = this;
    }

    //using container to set the position of the number block
    public void CreateNumberBlockAtContainer(int number)
    {
        // instantiate the number to be a child of the container
        NumberBlock numberBlock = Instantiate(NumberBlockPrefab, NumberBlockZone.transform, true);
        NumberBlockZone.AddBlockToZone(numberBlock);
        numberBlock.Initialize(number, NumberBlockZone);
        numberBlock.SetOriginalPosition();
        numberBlocks.Add(numberBlock);
    }

    public void CreateManyNumberBlocks(List<int> numbers)
    {
        foreach (int number in numbers)
        {
            CreateNumberBlockAtContainer(number);
        }
    }

    public void CreateAnswerNumberBlock(int number)
    {
        if (AnswerZone.numbers.Count == 1)
        {
            return;
        }
        Debug.Log("Creating answer number block");
        NumberBlock numberBlock = Instantiate(NumberBlockPrefab, AnswerContainer.transform, true);
        AnswerZone.AddBlockToZone(numberBlock);
        numberBlock.Initialize(number, AnswerZone);
        numberBlock.SetOriginalPosition();
        numberBlocks.Add(numberBlock);
    }
}
