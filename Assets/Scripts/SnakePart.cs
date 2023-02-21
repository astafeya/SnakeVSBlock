/* (c) Irina Astafeva, 2023 */

using System;
using UnityEngine;

public class SnakePart : MonoBehaviour
{    
    public Rigidbody Rigidbody;
    public bool IsHead;
    public Player Player;
    public GameObject PreviousPart;
    public GameObject SnakePartDestroy;

    private float _previousMousePosition;
    private Material _material;
    private float _position;
    private bool _destroyedByBlock;

    private void Awake()
    {
        _position = 0;
        _material = gameObject.GetComponent<MeshRenderer>().material;
        _material.SetFloat("_Position", _position);
        _destroyedByBlock = false;
    }

    private void Update()
    {
        if (!IsHead)
        {
            Vector3 newVelosity = PreviousPart.transform.position - transform.position;
            newVelosity.x *= Player.Couner.x;
            newVelosity.z *= Player.Couner.z;
            if (Math.Abs(PreviousPart.transform.position.x - transform.position.x) > 0.5f)
            {
                newVelosity.z *= 3;
            }
            Rigidbody.velocity = newVelosity * Player.Velosity * Time.deltaTime;
            Rigidbody.drag = Player.Drag;
            return;
        }
        Vector3 moving = Vector3.forward * Player.Velosity;
        if (Input.GetMouseButton(0))
        {
            float mouse = Input.mousePosition.x - _previousMousePosition;
            moving.x = mouse * Player.Sensitivity;
        }
        Rigidbody.velocity = moving * Time.deltaTime;
        Rigidbody.drag = Player.Drag;
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
            finish.PlayConfetty();
            Player.Win();
        }
    }

    public void SetPosition(float position)
    {
        _position = position;
        _material.SetFloat("_Position", _position);
    }

    public void SetDestroyed(bool value)
    {
        _destroyedByBlock = value;
    }

    private void OnDestroy()
    {
        if (!_destroyedByBlock) return;
        GameObject snakePartDestroy = Instantiate(SnakePartDestroy, transform.position, Quaternion.identity, Player.Parent.transform);
        ParticleSystem particleSystem = snakePartDestroy.GetComponent<ParticleSystem>();
        particleSystem.Play();
    }
}
