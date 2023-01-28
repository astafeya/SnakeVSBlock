/* (c) Irina Astafeva, 2023 */

using UnityEngine;
using Random = System.Random;

public class LevelCreator : MonoBehaviour
{
    public GameObject Block;
    public GameObject Food;
    public GameObject Finish;
    public GameObject Road;
    public int Length;
    public Game Game;

    [SerializeField]
    private int _startLength;
    [SerializeField]
    private int _endLength;
    private Random _random;
    private Vector3 _blockPositionInaccuracy = new Vector3(-0.2f, 0.34f, -0.25f);
    private Vector3 _foodPositionInaccuracy = new Vector3(0, 0.3f, -0.2f);

    private void Awake()
    {
        int levelIndex = Game.LevelIndex;
        int playerLifes = Game.PlayerLifes;
        _random = new Random(levelIndex);
        Vector3 _finishPosition = new Vector3(0, 0.062f, 0) + Vector3.forward * (_startLength + Length + 1);
        Vector3 _roadPosition = Vector3.back * (10 + playerLifes / 2 + playerLifes % 2);
        Vector3 _roadScale = new Vector3(1, 1, 0) - _roadPosition + (Vector3.forward * (_startLength + Length + _endLength));
        Finish.transform.localPosition = _finishPosition;
        Road.transform.localPosition = _roadPosition;
        Road.transform.localScale = _roadScale;

        bool blockOnPrevRow = false;
        int notUsedBlocks = 0;
        for (int z = 0; z <= Length; z++)
        {
            bool blockOnThisRow = false;
            if (notUsedBlocks >= 5)
            {
                blockOnThisRow = true;
                for (int x = -2; x <= 2; x++)
                {
                    CreateBlock(x, 0, z + _startLength);
                }
                notUsedBlocks -= 5;
            }
            else
            {
                for (int x = -2; x <= 2; x++)
                {
                    if (!IsFilled()) continue;
                    if (IsBlock())
                    {
                        if (blockOnPrevRow)
                        {
                            notUsedBlocks++;
                        }
                        else
                        {
                            CreateBlock(x, 0, z + _startLength);
                            blockOnThisRow = true;
                        }
                    }
                    else
                    {
                        CreateFood(x, 0, z + _startLength);
                    }
                }
            }
            blockOnPrevRow = blockOnThisRow;
        }
    }

    private bool IsFilled()
    {
        return RandomRange(0, 101) > 74;
    }

    private bool IsBlock()
    {
        return RandomRange(0, 101) < 66;
    }

    private int RandomRange(int min, int max)
    {
        int number = _random.Next();
        int length = max - min;
        number %= length;
        return number + min;
    }

    private void CreateBlock(float x, float y, float z)
    {
        GameObject block = Instantiate(Block, transform);
        block.transform.localPosition = new Vector3(x, y, z) + _blockPositionInaccuracy;
        Block bblock = block.GetComponent<Block>();
        bblock.SetCost(RandomRange(1, 51));
    }

    private void CreateFood(float x, float y, float z)
    {
        GameObject food = Instantiate(Food, transform);
        food.transform.localPosition = new Vector3(x, y, z) + _foodPositionInaccuracy;
        Food ffood = food.GetComponent<Food>();
        ffood.SetLifes(RandomRange(1, 6));
    }
}
