using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{

    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fruitRigidBody;
    private Collider fruitCollider;

    private void Awake()
    {
        fruitRigidBody.GetComponent<Rigidbody>();
        fruitCollider.GetComponent<Collider>();
    }

    private void Slice(Vector3 direction, Vector3 Position, float force)
    {
        whole.SetActive(false);
        sliced.SetActive(true);

        fruitCollider.enabled = false;

        float angle = Mathf.Atan2(direction.x,direction.y)*Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f,0f,angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidBody.velocity;
            slice.AddForceAtPosition(direction*force,Position,ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BLADE blade = other.GetComponent<BLADE>();
            Slice(blade.direction,blade.transform.position,blade.sliceforce);
        }
    }


}
