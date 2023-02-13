/* (c) Irina Astafeva, 2023 */

using UnityEngine;

public class SnakePart : MonoBehaviour
{    
    public Rigidbody Rigidbody;
    public bool IsHead;
    public Player Player;
    public GameObject PreviousPart;

    private float _previousMousePosition;
    private Material _material;
    public float _position;

    private void Awake()
    {
        _position = 0;
        _material = gameObject.GetComponent<MeshRenderer>().material;
        _material.SetFloat("_Position", _position);
    }

    private void Update()
    {
        if (!IsHead)
        {
            Vector3 newVelosity = PreviousPart.transform.position - transform.position;
            newVelosity.x *= Player.Couner.x;
            newVelosity.z *= Player.Couner.z;
            Rigidbody.velocity = newVelosity * Player.Velosity * Time.deltaTime;
            return;
        }
        Vector3 moving = Vector3.forward * Player.Velosity;
        if (Input.GetMouseButton(0))
        {
            float mouse = Input.mousePosition.x - _previousMousePosition;
            moving.x = mouse * Player.Sensitivity;
        }
        Rigidbody.velocity = moving * Time.deltaTime;
        _previousMousePosition = Input.mousePosition.x;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsHead) return;
        GameObject otherGO = collision.gameObject;
        Food food = otherGO.GetComponent<Food>();
        if (food)
        {
            Player.ChangeLifes(food.Lifes);
            Destroy(otherGO);
            return;
        }
        Block block = otherGO.GetComponent<Block>();
        if (block)
        {
            Vector3 normal = -collision.contacts[0].normal.normalized;
            float dot = Vector3.Dot(normal, Vector3.forward);
            Debug.Log(dot);
            if (dot < 0.7) return;
            Player.ChangeLifes(-1);
            block.SetCost(block.Cost - 1);
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsHead) return;
        GameObject otherGO = other.gameObject;
        Finish finish = otherGO.GetComponent<Finish>();
        if (finish)
        {
            Player.Win();
        }
    }

    public void SetPosition(float position)
    {
        _position = position;
        _material.SetFloat("_Position", _position);
    }
}
