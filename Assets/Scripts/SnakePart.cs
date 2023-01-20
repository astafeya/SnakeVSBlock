using UnityEngine;

public class SnakePart : MonoBehaviour
{    
    public Rigidbody Rigidbody;
    public bool IsHead;
    public Player Player;
    public GameObject PreviousPart;


    [SerializeField]
    private float _sensitivity/* = 2.25f*/;
    [SerializeField]
    private float _velosity/* = 30f*/;
    [SerializeField]
    private Vector3 _couner/* = new Vector3(8, 0, 2)*/;
    private float _previousMousePosition;

    private void Update()
    {
        if (!IsHead)
        {
            Vector3 newVelosity = PreviousPart.transform.position - transform.position;
            newVelosity.x *= _couner.x;
            newVelosity.z *= _couner.z;
            Rigidbody.velocity = newVelosity * _velosity * Time.deltaTime;
            return;
        }
        Vector3 moving = Vector3.forward * _velosity;
        if (Input.GetMouseButton(0))
        {
            float mouse = Input.mousePosition.x - _previousMousePosition;
            moving.x = mouse * _sensitivity;
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
            Debug.Log("Win");
        }
    }
}
