/* (c) Irina Astafeva, 2023 */

using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public int Cost { get; private set; }
    public TextMeshPro[] Texts;
    public GameObject BlockDestroy;
    public GameObject Parent;

    private Material _material;
    private bool _destroyedByPlayer;

    private void Awake()
    {
        _material = gameObject.GetComponent<MeshRenderer>().material;
        _destroyedByPlayer = false;
        SetCost(50);
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
            _destroyedByPlayer = true;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (!_destroyedByPlayer) return;
        GameObject blockDestroy = Instantiate(BlockDestroy, transform.position, Quaternion.identity, Parent.transform);
        ParticleSystem particleSystem = blockDestroy.GetComponent<ParticleSystem>();
        particleSystem.Play();
    }
}
