using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public int Lifes;
    public Rigidbody Rigidbody;
    public TextMeshPro Text;
    public List<SnakePart> Snake;
    public GameObject Parent;
    public GameObject SnakePartPrefab;

    [SerializeField]
    private Vector3 Offset;
    [SerializeField]
    private float _sensitivity/* = 2.25f*/;
    [SerializeField]
    private float _velosity/* = 30f*/;
    private float _previousMousePosition;

    private void Awake()
    {
        gameObject.SetActive(true);
        SetText();
        Snake = new List<SnakePart>();
        Vector3 newPartPosition = new Vector3(0, 0.3f, 0);
        for (int i = 0; i <= Lifes; i++)
        {
            GameObject newPart = Instantiate(SnakePartPrefab, newPartPosition, Quaternion.identity, Parent.transform);
            newPartPosition.z -= 0.5f;
            Snake.Add(newPart.GetComponent<SnakePart>());
            Snake[i].Player = this;
            if (i > 0) Snake[i].PreviousPart = Snake[i - 1].gameObject;
        }
        Snake[0].IsHead = true;
    }
    void Update()
    {
        transform.position = Snake[0].gameObject.transform.position + Offset;
    }

    public void ChangeLifes(int value)
    {
        if (value == 0) return;
        if (value > 0)
        {
            for (int i = 0; i < value; i++)
            {
                AddTail();
                Lifes++;
                SetText();
            }
        } else
        {
            DeleteHead();
            Lifes--;
            if (Lifes >= 0) SetText();
            else
            {
                Debug.Log("Game over!");
                gameObject.SetActive(false);
            }
        }
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
    }

    private void SetText()
    {
        Text.text = Lifes.ToString();
    }
}
