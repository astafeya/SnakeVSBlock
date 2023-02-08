/* (c) Irina Astafeva, 2023 */

using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public TextMeshPro Text;
    public List<SnakePart> Snake;
    public GameObject Parent;
    public GameObject SnakePartPrefab;
    public Game Game;
    public float Sensitivity { get; private set; }
    public float Velosity { get; private set; }
    public Vector3 Couner { get; private set; }

    private int _lifes;
    [SerializeField]
    private Vector3 Offset;
    [SerializeField]
    private float _sensitivity/* = 2.25f*/;
    [SerializeField]
    private float _velosity/* = 60f*/;
    [SerializeField]
    private Vector3 _couner/* = new Vector3(8, 0, 2)*/;

    private void Awake()
    {
        Sensitivity = _sensitivity;
        Velosity = _velosity;
        Couner = _couner;
        gameObject.SetActive(true);
        _lifes = Game.PlayerLifes;
        SetText();
        Snake = new List<SnakePart>();
        Vector3 newPartPosition = new Vector3(0, 0.3f, 0);
        for (int i = 0; i <= _lifes; i++)
        {
            GameObject newPart = Instantiate(SnakePartPrefab, newPartPosition, Quaternion.identity, Parent.transform);
            newPartPosition.z -= 0.5f;
            Snake.Add(newPart.GetComponent<SnakePart>());
            Snake[i].Player = this;
            if (i > 0) Snake[i].PreviousPart = Snake[i - 1].gameObject;
        }
        Snake[0].IsHead = true;
        UpdatePositions();
    }
    void Update()
    {
        Vector3 snakeHeadPosition = Snake[0].gameObject.transform.position;
        transform.position = snakeHeadPosition + Offset;
        Game.SetProgress(snakeHeadPosition.z);
    }

    public void ChangeLifes(int value)
    {
        if (value == 0) return;
        _lifes += value;
        Game.ChangeLifes(value);
        if (value > 0)
        {
            for (int i = 0; i < value; i++)
            {
                AddTail();
                SetText();
            }
            Game.OnPlayerEat();
        } else
        {
            DeleteHead();
            if (_lifes >= 0) SetText();
            else
            {
                Lose();
                gameObject.SetActive(false);
                return;
            }
        }
        UpdatePositions();
    }

    private void AddTail()
    {
        Vector3 newPartPosition = Snake[Snake.Count - 1].transform.position;
        newPartPosition.z -= 0.5f;

        GameObject newPart = Instantiate(SnakePartPrefab, newPartPosition, Quaternion.identity, Parent.transform);
        Snake.Add(newPart.GetComponent<SnakePart>());
        Snake[Snake.Count - 1].Player = this;
        Snake[Snake.Count - 1].PreviousPart = Snake[Snake.Count - 2].gameObject;
    }

    private void DeleteHead()
    {

        SnakePart oldHead = Snake[0];
        Snake.RemoveAt(0);
        if (Snake.Count > 0)
        { 
        Snake[0].IsHead = true;
        Snake[0].PreviousPart = null;
        }
        Destroy(oldHead.gameObject);
        Game.OnPlayerPopHead();
    }

    private void SetText()
    {
        Text.text = _lifes.ToString();
    }

    public void Win()
    {
        Game.OnPlayerWin();
    }

    public void Lose()
    {
        Game.OnPlayerDied();
    }

    private void UpdatePositions()
    {
        int count = Snake.Count;
        if (count > 1)
        {
            for (int i = 0; i < count; i++)
            {
                float position = ((float) i) / (count - 1);
                Snake[i].SetPosition(position);
            }
        }
        else Snake[0].SetPosition(0);
    }
}
