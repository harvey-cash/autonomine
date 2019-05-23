using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    const float SPEED_COEFF = 5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Time.deltaTime * SPEED_COEFF;
        transform.position += transform.right * Input.GetAxis("Horizontal") * speed;
        transform.position += transform.forward * Input.GetAxis("Vertical") * speed;
    }
}
