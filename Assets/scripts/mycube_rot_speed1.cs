using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mycube_rot_speed1 : MonoBehaviour
{
    public float RotationSpeed = 0.9f;
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(1, 2, 3);
        Debug.Log("Привет, я кубик.");
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, RotationSpeed, 0);
    }

    public void setScale(int scale)
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
