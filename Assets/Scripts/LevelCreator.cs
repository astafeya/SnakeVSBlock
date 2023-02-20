/* (c) Irina Astafeva, 2023 */

using UnityEngine;
using Random = System.Random;
using System.Collections.Generic;

public class LevelCreator : MonoBehaviour
{
    public GameObject Block;
    public GameObject Food;
    public GameObject Finish;
    public GameObject Road;
    public int Length;
    public Game Game;
    public Player Player;

    private int _startLength = 10;
    private int _endLength = 15;
    private Random _random;
    private Vector3 _blockPositionInaccuracy = new Vector3(-0.2f, 0.34f, -0.25f);
    private Vector3 _foodPositionInaccuracy = new Vector3(0, 0.3f, -0.2f);
    private bool _blockOnPrevRow;
    private bool _blockOnThisRow;
    private int _notUsedBlocks;
    private List<GameObject> _levelObjects;
    private float offset = 7;

    private void Awake()
    {
        _random = new Random(Game.LevelIndex);
        _levelObjects = new List<GameObject>();
        SetLevelLength();
        CreateFoodAndBlocks();
    }

    private void Update()
    {
        if (Game.CurrentState != Constants.State.Playing
            || Player.Snake.Count <= 0
            || _levelObjects.Count <= 0) return;
        if (_levelObjects[0] == null)
        {
            _levelObjects.RemoveAt(0);
            return;
        }
        float playerHeadPosZ = Player.Snake[0].transform.position.z;
        float z = _levelObjects[0].transform.position.z;
        if (z + offset < playerHeadPosZ)
        {
            GameObject objectForHide = _levelObjects[0];
            _levelObjects.RemoveAt(0);
            Destroy(objectForHide);
        }
    }

    private int RandomRange(int min, int max)
    {
        int number = _random.Next();
        int length = max - min;
        number %= length;
        return number + min;
    }

    private void SetLevelLength()
    {
        int playerLifes = Game.PlayerLifes;
        Vector3 _finishPosition = new Vector3(0, 0.062f, 0) + Vector3.forward * (_startLength + Length + 1);
        Vector3 _roadPosition = Vector3.back * (10 + playerLifes / 2 + playerLifes % 2);
        Vector3 _roadScale = new Vector3(1, 1, 0) - _roadPosition + (Vector3.forward * (_startLength + Length + _endLength));
        Finish.transform.localPosition = _finishPosition;
        Road.transform.localPosition = _roadPosition;
        Road.transform.localScale = _roadScale;
    }

    private void CreateFoodAndBlocks()
    {
        _blockOnPrevRow = false;
        _notUsedBlocks = 0;
        for (int z = 0; z <= Length; z++)
        {
            _blockOnThisRow = false;
            if (_notUsedBlocks >= 5)
            {
                CreateRowOfBlocks(z);
            }
            else
            {
                CreateMixedRow(z);
            }
            _blockOnPrevRow = _blockOnThisRow;
        }
    }

    private bool IsFilled()
    {
        return RandomRange(0, 101) > 66;
    }

    private bool IsBlock()
    {
        return RandomRange(0, 101) < 66;
    }

    private void CreateRowOfBlocks(float z)
    {
        _blockOnThisRow = true;
        for (int x = -2; x <= 2; x++)
        {
            CreateBlock(x, 0, z);
        }
        _notUsedBlocks -= 5;
    }

    private void CreateMixedRow(float z)
    {
        for (int x = -2; x <= 2; x++)
        {
            if (!IsFilled()) continue;
            if (IsBlock())
            {
                CreateBlock(x, 0, z);
            }
            else
            {
                CreateFood(x, 0, z);
            }
        }
    }

    private void CreateBlock(float x, float y, float z)
    {
        if (_blockOnPrevRow)
        {
            _notUsedBlocks++;
            return;
        }
        GameObject block = Instantiate(Block, transform);
        block.transform.localPosition = new Vector3(x, y, z + _startLength) + _blockPositionInaccuracy;
        Block bblock = block.GetComponent<Block>();
        bblock.SetCost(RandomRange(1, 51));
        bblock.Parent = gameObject;
        _blockOnThisRow = true;
        _levelObjects.Add(block);
    }

    private void CreateFood(float x, float y, float z)
    {
        GameObject food = Instantiate(Food, transform);
        food.transform.localPosition = new Vector3(x, y, z + _startLength) + _foodPositionInaccuracy;
        Food ffood = food.GetComponent<Food>();
        ffood.SetLifes(RandomRange(1, 6));
        _levelObjects.Add(food);
    }
}
