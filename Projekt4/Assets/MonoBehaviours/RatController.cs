using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RatController : MonoBehaviour
{
    public bool isGrounded;
    public float slopeLimit = 45f;

    [SerializeField]
    private Rigidbody rb;

    public Dictionary<int, Collision> collisions = new Dictionary<int, Collision>();
    private Vector3 accumulatedMotion;
    private Vector3 accumulatedForce;
    public void Move(Vector3 motion)
    {
        accumulatedMotion += motion;
    }
    public void AddForce(Vector3 force)
    {
        accumulatedForce += force;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + accumulatedMotion);
        rb.AddForce(accumulatedForce);
        accumulatedMotion = Vector3.zero;
        accumulatedForce = Vector3.zero;


        foreach (KeyValuePair<int, Collision> keyValuePair in collisions)
        {
            int length = keyValuePair.Value.contactCount;
            ContactPoint[] contacts = new ContactPoint[length];
            keyValuePair.Value.GetContacts(contacts);

            for (int contactIndex = 0; contactIndex < length; contactIndex++)
            {
                //Debug.Log($"{{Collision #{keyValuePair.Key}; Contact #{contactIndex}; normal: {contacts[contactIndex].normal} }}");
                if (Vector3.Angle(transform.up, contacts[contactIndex].normal) <= slopeLimit)
                {

                    isGrounded = true;
                    return;
                }
            }
        }
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisions.Add(collision.collider.GetInstanceID(), collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        collisions[collision.collider.GetInstanceID()] = collision;
    }

    private void OnCollisionExit(Collision collision)
    {
        collisions.Remove(collision.collider.GetInstanceID());
    }
}
