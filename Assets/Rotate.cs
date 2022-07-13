using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotate : MonoBehaviour
{
    public Transform Player;
    float RotateAngle;
    public float TimeSpeed;

    float mouseX;
    float mouseY;

    public float mouseSensity = 100f;

    float xRotation;

    /*----------*/

    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //RotatePlayer();
        Rotate2();


    }

    void RotatePlayer()
    {
        RotateAngle = Input.GetAxis("Mouse X") * TimeSpeed * Time.deltaTime;
        RotateAngle = Mathf.Clamp(RotateAngle, 0, 180);

        
        Player.localRotation = Quaternion.AngleAxis(RotateAngle, Vector3.up);
    }

    void Rotate2()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensity * Time.deltaTime;

        xRotation -= mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(0, -xRotation * 8, 0);
        Player.Rotate(Vector3.up * mouseY);
    }

    void Rotate3()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (groundPlane.Raycast(camRay, out rayLength))
        {
            Vector3 pointtoLook = camRay.GetPoint(rayLength);

            Debug.DrawLine(camRay.origin, pointtoLook, Color.blue);

            transform.LookAt(new Vector3(pointtoLook.x, transform.position.y, pointtoLook.z));
        }
    }
}
