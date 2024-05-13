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
    public int numberSpawnPerTurn = 4;

    public int bonusSpawnPerTurn = 0;

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

    public void RemoveNumberBlockFromList(NumberBlock numberBlock)
    {
        numberBlocks.Remove(numberBlock);
    }

    public void RemoveNumberBlock(NumberBlock numberBlock)
    {
        // Remove the number block from the container
        RemoveNumberBlockFromList(numberBlock);
        numberBlock.RemoveBlock();
        numberBlocksInContainer--;
    }

    public void RemoveAllNumberBlocks()
    {
        // Remove all number blocks from the container, use for loop as it removes the number from the list
        for (int i = numberBlocks.Count - 1; i >= 0; i--)
        {
            RemoveNumberBlock(numberBlocks[i]);
        }

        numberBlocks.Clear();
        numberBlocksInContainer = 0;
        NumberBlockZone.numbers.Clear();
    }

    public void NextTurn()
    {
        RemoveAllNumberBlocks();
        CreateManyNumberBlocks(
            GenerateStartingRandomFairNumbers(numberSpawnPerTurn + bonusSpawnPerTurn)
        );
        bonusSpawnPerTurn = 0;
    }

    public List<int> GenerateStartingRandomFairNumbers(int amount)
    {
        // Generate 9 numbers , 3 numbers has to be low, 3 has to be high the rest can be any number (low is 1-4, high 6-9)
        List<int> numbers = new List<int>();
        for (int i = 0; i < amount / 3; i++)
        {
            numbers.Add(Random.Range(1, 5));
            amount--;
        }
        for (int i = 0; i < amount / 3; i++)
        {
            numbers.Add(Random.Range(6, 10));
            amount--;
        }
        for (int i = 0; i < amount; i++)
        {
            numbers.Add(Random.Range(1, 10));
        }
        //Print the numbers in the log
        string numberString = "";
        foreach (int number in numbers)
        {
            numberString += number + " ";
        }
        Debug.Log("Generated numbers: " + numberString);
        return numbers;
    }

    public int GenerateRandomNumber()
    {
        return Random.Range(1, 10);
    }
}
