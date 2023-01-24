/* (c) Irina Astafeva, 2023 */

using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public Transform PlayerTransform;

    [SerializeField]
    private Vector3 Offset;

    void Update()
    {
        float x = Offset.x;
        float y = PlayerTransform.position.y + Offset.y;
        float z = PlayerTransform.position.z + Offset.z;
        transform.position = new Vector3(x, y, z);
    }
}
