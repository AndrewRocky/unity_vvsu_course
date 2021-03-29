using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breathofthescale : MonoBehaviour
{
    float ScaleState = 0;
    public float Speed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScaleState += Speed;
        float sin = Mathf.Sin(ScaleState);
        float d = Mathf.Abs(sin);
        float t = Mathf.Lerp(1, 2, d);
        transform.localScale = new Vector3(t, t, t);

    }
}
