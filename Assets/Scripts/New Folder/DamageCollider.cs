using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public float speed;
    public int Damage;
    private void FixedUpdate()
    {
        transform.Rotate(0, speed, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<VrmCharacterControl>() != null)
            collision.collider.GetComponent<VrmCharacterControl>().HP -= Damage;
    }
}
