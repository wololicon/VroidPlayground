using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    public VrmCharacterControl _player;
    public Vector3 CameraOffset;
    float yaw = 0.0f;
    float pitch = 0.0f;
    float turnSpeedH = 1;
    float turnSpeedV = 1;
    float inputH = 0.0f;
    float inputV = 0.0f;
    //public GameObject bullet;
    Camera _camera;
    public CameraState _cameraState;
    public bool CursorLock;
    [Space]
    public float Freelook_MoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponentInChildren<Camera>();

    }
    #region tpsÊÓ½Ç
    void CameraFollowMode()
    {
        _camera.transform.localPosition = CameraOffset;
        CheckCameraBlock();
        transform.position = _player.transform.position + Vector3.up;
        if (CursorLock)
        {
            yaw += turnSpeedH * Input.GetAxisRaw("Mouse X");
            pitch -= turnSpeedV * Input.GetAxisRaw("Mouse Y");

            if (pitch > 70)
                pitch = 70;
            if (pitch < -70)
                pitch = -70;

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
        Vector3 LookPoint = _camera.transform.position + _camera.transform.forward * 100 - CameraOffset;

        _player.SetHeadLookPosition(LookPoint);

        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = (transform.forward * inputV + inputH * transform.right);
        moveDirection.y = 0.0f;
        moveDirection = moveDirection.normalized;
        float InputSpeed = moveDirection.magnitude;
        float speed = _player.RunSpeed * InputSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = _player.SprintSpeed * InputSpeed;
        }
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            speed = _player.WalkSpeed * InputSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Q))
            CameraOffset.x = -CameraOffset.x;
        _player.SetMoveDirection(moveDirection, speed);
    }

    void CheckCameraBlock()
    {
        Vector3 ori, dir;

        ori = _camera.transform.position + _camera.transform.forward * -CameraOffset.z;
        dir = _camera.transform.position - ori;
        Ray ray = new Ray(ori, dir);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, -CameraOffset.z))
        {
            Vector3 offset = CameraOffset;
            offset.z = -hit.distance + 0.02f;
            _camera.transform.localPosition = offset;
        }
        else
        {
            _camera.transform.localPosition = CameraOffset;
        }
    }
    #endregion
    void CameraFreelookMode()
    {
        _camera.transform.localPosition = Vector3.zero;
        if (CursorLock)
        {
            yaw += turnSpeedH * Input.GetAxisRaw("Mouse X");
            pitch -= turnSpeedV * Input.GetAxisRaw("Mouse Y");

            if (pitch > 90)
                pitch = 90;
            if (pitch < -90)
                pitch = -90;

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = (transform.forward * inputV + inputH * transform.right).normalized * Freelook_MoveSpeed;
        transform.position += moveDirection;
    }
    //// Update is called once per frame
    void Update()
    {
        switch (_cameraState)
        {
            case CameraState.Follow:
                CameraFollowMode();
                break;
            case CameraState.Freelook:
                CameraFreelookMode();
                break;

        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SetCursorLock(!CursorLock);
        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //    r = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit[] rh;
        //    rh = Physics.RaycastAll(r);
        //    for (int i = 0; i < rh.Length; i++)
        //    {
        //        print(rh[i].collider.gameObject);
        //        RagdollHitbox hitbox = rh[i].collider.GetComponent<RagdollHitbox>();
        //        if (hitbox != null)
        //        {
        //            hitbox._ragDoll.SetRagdollActive(true);
        //            hitbox._rig.AddForceAtPosition(r.direction.normalized * 500, rh[i].point);
        //            //hitbox._rig.AddExplosionForce(5000, rh[i].point, 10);
        //        }
        //    }
        //}
    }

    public void SetCursorLock(bool value)
    {
        CursorLock = value;
        //Cursor.visible = value;
        if (value)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

}

public enum CameraState
{
    Follow,
    Freelook,
    Lock,
}
