using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera gameCamera;

    void Update() {
        if (UIManager.ui != null && !UIManager.ui.ScriptFocused()) {
            ButtonMovement();
            MouseMovement();
            ScrollMovement();
        }
    }

    private float scrollCoeff = 2f;
    private void ScrollMovement() {
        gameCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * scrollCoeff;
    }

    private float speedCoeff = 5f;
    private void ButtonMovement() {
        float speed = Time.deltaTime * speedCoeff;
        transform.position += transform.right * Input.GetAxis("Horizontal") * speed;
        transform.position += transform.forward * Input.GetAxis("Vertical") * speed;
    }

    private Vector3 dragOrigin, camOrigin;
    private float dragCoeff = 10;

    private void MouseMovement() {
        if (Input.GetMouseButtonDown(Config.MIDDLE_MOUSE)) {
            dragOrigin = Input.mousePosition;
            camOrigin = transform.position;
            return;
        }
        if (Input.GetMouseButton(Config.MIDDLE_MOUSE)) {
            Vector3 delta = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
            delta = delta * dragCoeff;
            transform.position = camOrigin + transform.right * delta.x + transform.forward * delta.y;
        }
    }
}
