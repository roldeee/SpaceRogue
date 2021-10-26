using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;
    private Vector3 finalOffset;

    public bool lookAtPlayer = false;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        finalOffset = offset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Time.timeScale > 0f) {
            Rotate();
            transform.position = Vector3.Lerp(transform.position, (player.transform.position + finalOffset), .5f);
            transform.LookAt(player.transform.position);
        }
    }

    void Rotate()
    {
        finalOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 4f, Vector3.up) * finalOffset;
    }
}