using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RatController : MonoBehaviour
{
    public bool isGrounded;
    public float slopeLimit = 45f;

    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private new Collider collider;
    
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
    }
    private void OnCollisionStay(Collision collision)
    {
        int length = collision.contactCount;
        ContactPoint[] contacts = new ContactPoint[length];
        collision.GetContacts(contacts);
        for (int i = 0; i < length; i++)
        {
            //Debug.Log(contacts[i].normal);
            if(Vector3.Angle(contacts[i].normal, Vector3.up) <= slopeLimit)
            {
                isGrounded = true;
                return;
            }
        }
        isGrounded = false;
    }

}
