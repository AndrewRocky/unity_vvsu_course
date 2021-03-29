using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color_change : MonoBehaviour
{
    public Color color1;
    public Color color2;
    public float ColorSpeed;
    float ColorState = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float r, g, b, r1, r2, g1, g2, b1, b2;
        r1 = color1.r;
        g1 = color1.g;
        b1 = color1.b;
        r2 = color2.r;
        g2 = color2.g;
        b2 = color2.b;

        ColorState += ColorSpeed;
        float sin = Mathf.Sin(ColorState);
        float d = Mathf.Abs(sin);
        r = Mathf.Lerp(r1, r2, d);
        g = Mathf.Lerp(g1, g2, d);
        b = Mathf.Lerp(b1, b2, d);

        Color cur_color = new Color(r, g, b);
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.material.color = cur_color;
    }
}
