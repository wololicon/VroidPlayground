using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHitbox : MonoBehaviour
{
    public RagDollControl _ragDoll;
    public Rigidbody _rig;
    Joint _joint;
    
    private void Start()
    {
        _ragDoll = GetComponentInParent<RagDollControl>();
        _rig = GetComponent<Rigidbody>();
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.gameObject.tag.Equals("Player"))
    //    {
    //        //if (_joint != null)
    //        //{
    //        //    _joint.massScale = 100;
    //        //    _joint.connectedMassScale = 10;
    //        //}
    //        _ragDoll.RagdollActive();

    //        //collision.collider.gameObject.SetActive(false);
    //        _rig.AddForceAtPosition(collision.impulse*10, collision.GetContact(0).point);
    //    }
    //}

    //private void Update()
    //{
    //    if(!_rig.isKinematic&&_joint!=null)
    //    {
    //        if(_joint.massScale>1)
    //        {
    //            _joint.massScale -= 0.5f;
    //            _joint.connectedMassScale -= 0.5f;
    //        }
    //    }
    //}
}
