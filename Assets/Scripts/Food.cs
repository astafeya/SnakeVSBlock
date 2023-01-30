/* (c) Irina Astafeva, 2023 */

using UnityEngine;
using TMPro;

public class Food : MonoBehaviour
{
    public int Lifes { get; private set; }
    public TextMeshPro Text;

    private void Awake()
    {
        SetLifes(5);
    }

    public void SetLifes(int lifes)
    {
        Lifes = lifes;
        Text.text = Lifes.ToString();
    }
}
