using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public GameObject Player;
    public float followSpeed = 5;
    public Vector3 offset;
    Camera camera;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.2f;
    private void Awake()
    {
        camera = GetComponent<Camera>();
        SceneManager.sceneLoaded += CloseAllSubCamera;
    }
    public void OnDestroy()
    {
        SceneManager.sceneLoaded -= CloseAllSubCamera;
    }
    public void Update()
    {
        
        /*
        Vector3 _moveDir = Player.transform.position - transform.position + offset;
        if (_moveDir.magnitude <= 0.1f) { return; }
        Vector3 _move = _moveDir.normalized * followSpeed * Time.deltaTime;
        transform.Translate(_move);*/
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = Player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

    }

    private void CloseAllSubCamera(Scene _scene, LoadSceneMode loadSceneMode)
    {
        foreach (Camera _camera in FindObjectsOfType<Camera>())
        {
            if (_camera != camera && _camera.transform.parent != camera.transform)
            {
                _camera.enabled = false;
            }
        }
    }
}
