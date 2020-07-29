using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirUp : MonoBehaviour
{
    private void OnTriggerStay(Collider collider)
    {
        if (collider.attachedRigidbody != null)
        {
            collider.attachedRigidbody.velocity = new Vector3(
                collider.attachedRigidbody.velocity.x,
                1f,
                collider.attachedRigidbody.velocity.z
                );
        }
    }
}
