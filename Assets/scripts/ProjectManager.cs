using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectManager : MonoBehaviour
{
    public GameObject guy_prefab;
    public GameObject ammo;
    public Camera camera1;
    public int ammoForce = 500;
    public Text myText;
    public Text textCounter;
    public GameObject InfoWindow;
    public Image ColorPickerIMG;


    int textCounterInt = 0;
    public Color[] ColorPickerColors = { Color.red, Color.green, Color.magenta, Color.blue, Color.yellow };
    int ColorPickerIter = 0;


    // Start is called before the first frame update
    void Start()
    {
        //GameObject guy = Instantiate(guy_prefab);
        //guy.transform.position = new Vector3(3, 3, 4);

        for (int i = 0; i < 10; i++)
        {
            for (int k = 0; k < 10; k++)
            {
                GameObject guy1 = Instantiate(guy_prefab);
                guy1.transform.position = new Vector3(i*2, 5, k * 3);
            }
        }
        //mycube_rot_speed1[] cubes = FindObjectsOfType<mycube_rot_speed1>();
        //Debug.Log("В сцене " + cubes.Length + "кубиков mycube_rot_speed1");
        //for (int i = 0; i < cubes.Length; i++)
        //{
        //    //cubes[i].setScale(2);
        //    cubes[i].transform.position = new Vector3(i*2, 2,-5);
        //}
        myText.text = "sample";
        ColorPickerIMG.color = ColorPickerColors[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = camera1.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10000.0f))
            {
                Vector3 h = hit.point;
                Debug.Log("Click point: " + h);
                GameObject guy2 = Instantiate(guy_prefab);
                guy2.transform.position = h;
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Vector3 dir = camera1.transform.TransformDirection(Vector3.forward);
            GameObject shot = Instantiate(ammo);
            shot.transform.position = camera1.transform.position;
            shot.GetComponent<Rigidbody>().AddForce(dir * ammoForce);
        }
    }


    public void onMyButtonClick()
    {
        Debug.Log("button was pressed");
        textCounterInt += 1;
        textCounter.text = textCounterInt.ToString();
        InfoWindow.SetActive(true);
    }

    public void onOkButtonClick()
    {
        InfoWindow.SetActive(false);
    }

    public void onColorLeftClick()
    {
        ColorPickerIter = ColorPickerIter - 1;
        if (ColorPickerIter < 0) ColorPickerIter = ColorPickerColors.Length-1;
        ColorPickerIMG.color = ColorPickerColors[ColorPickerIter];
    }

    public void onColorRightClick()
    {
        ColorPickerIter = (ColorPickerIter + 1) % ColorPickerColors.Length;
        ColorPickerIMG.color = ColorPickerColors[ColorPickerIter];
    }
}
