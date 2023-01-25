using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public int Cost;
    public TextMeshPro[] Texts;
    private Material _material;

    private void Awake()
    {
        _material = gameObject.GetComponent<MeshRenderer>().material;
        SetCost(Cost);
    }

    public void SetCost (int value)
    {
        Cost = value;
        _material.SetFloat("_Cost", Cost);
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
