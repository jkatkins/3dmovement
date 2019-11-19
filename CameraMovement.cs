using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{


    public float sensitivity = 100f;
    public Transform player;
    public float xRotation = 0f;
    public Quaternion playerRotation;
    public P1Movement parent;

    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent.transform;
        parent = this.transform.parent.gameObject.GetComponentInParent<P1Movement>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float Xmove = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float Ymove = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= Ymove;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * Xmove);
        playerRotation = player.rotation;
        parent.forwardY = transform.forward.y;
    }


}
