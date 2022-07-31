using UnityEngine;
using System;

public class FallowCamera : MonoBehaviour
{
    GameObject playerTag;

    public GameObject player;
    public Vector3 offset = new Vector3(0, 0, 0);

    public Transform target;
    public float cameraTurnSpeed;

    void Start()
    {
        offset = transform.position - player.transform.position;

        playerTag = GameObject.FindGameObjectWithTag("Player");
        cameraTurnSpeed = 1f/4;
    }

    void Update()
    {
        Vector3 cameraPos = playerTag.transform.position;
        cameraPos.z = -10;
        cameraPos.y += 3;
        transform.position = cameraPos;
        CameraTurn();

    }

    public void CameraTurn()
    {
         transform.rotation = Quaternion.Slerp(transform.rotation, playerTag.transform.rotation, cameraTurnSpeed * Time.fixedDeltaTime);

    }
}