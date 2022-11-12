using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    public GameObject bullet;
    public Camera _camera;
    bool CanvasOpen;
    public GameObject _canvas;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();

        //// Camera object check
        //if (_camera == null)
        //{
        //    Debug.LogError("FreeLook: No camera component was found on this gameobject.");
        //}

        //// Camera spawn
        //if (cameraSpawn == null)
        //{
        //    cameraSpawn = transform.position;
        //}

        //transform.position = cameraSpawn;

        // Turn speed
        turnSpeedH = turnSpeed;
        turnSpeedV = turnSpeed;

        // Cursor
        //CursorSettings();
    }
    Ray r;
    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("µã»÷µ½UI");
            return;
        }
        if (Input.GetMouseButton(0))
        {

            yaw += turnSpeedH * Input.GetAxisRaw("Mouse X");
            pitch -= turnSpeedV * Input.GetAxisRaw("Mouse Y");

            if (pitch > 90)
                pitch = 90;
            if (pitch < -90)
                pitch = -90;
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

            inputH = Input.GetAxisRaw("Horizontal");
            inputV = Input.GetAxisRaw("Vertical");

            Vector3 moveDirection = (transform.forward * inputV + inputH * transform.right).normalized;

            if (Input.GetKey(KeyCode.E))
            {
                moveDirection.y = -1.0f;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                moveDirection.y = 1.0f;
            }

            transform.position = transform.position + moveDirection * movementSpeed * Time.deltaTime;
        }

        if(Input.GetMouseButtonDown(1))
        {
            r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] rh;
            rh = Physics.RaycastAll(r);
            for (int i = 0; i < rh.Length; i++)
            {
                print(rh[i].collider.gameObject);
                RagdollHitbox hitbox = rh[i].collider.GetComponent<RagdollHitbox>();
                if (hitbox != null)
                {
                    hitbox._ragDoll.RagdollActive();
                    hitbox._rig.AddForceAtPosition(r.direction.normalized * 500, rh[i].point);
                    //hitbox._rig.AddExplosionForce(5000, rh[i].point, 10);
                }
            }
        }
    }
    public float movementSpeed = 10.0f;
    public float turnSpeed = 3.0f;
    public Vector3 cameraSpawn;
    public bool cursorVisible = false;
    public bool lockCursor = true;

    float yaw = 180f;
    float pitch = 0.0f;
    float turnSpeedH;
    float turnSpeedV;
    float inputH = 0.0f;
    float inputV = 0.0f;
    bool buttonJumpDown = false;

    //private void OnApplicationFocus(bool focus)
    //{
    //    if (focus)
    //    {
    //        CursorSettings();
    //    }
    //}

    //private void CursorSettings()
    //{
    //    // Cursor
    //    Cursor.visible = cursorVisible;

    //    if (lockCursor)
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }
    //    else
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //    }
    //}
}
