using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PickableItem : MonoBehaviour
{
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Drag(GameObject pickablePos, float approachSpeed)
    {
        _rb.useGravity = false;
        _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        var direction = pickablePos.transform.position - transform.position;
        _rb.velocity = direction * approachSpeed;
    }

    public void Drop()
    {
        _rb.useGravity = true;
        _rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        _rb.velocity = Vector3.zero;
    }
}
