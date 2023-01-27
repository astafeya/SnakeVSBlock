/* (c) Irina Astafeva, 2023 */

using UnityEngine;
using TMPro;

public class Food : MonoBehaviour
{
    public int Lifes;
    public TextMeshPro Text;

    private void Awake()
    {
        Text.text = Lifes.ToString();
    }
}
