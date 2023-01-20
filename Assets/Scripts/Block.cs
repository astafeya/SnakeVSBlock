using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public int Cost;
    public TextMeshPro[] Texts;

    private void Awake()
    {
        SetCost(Cost);
    }

    public void SetCost (int value)
    {
        Cost = value;
        for (int i = 0; i < Texts.Length; i++)
        {
            Texts[i].text = Cost.ToString();
        }
        if (Cost == 0)
        {
            Destroy(gameObject);
        }
    }
}
