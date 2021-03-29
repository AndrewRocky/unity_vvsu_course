using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class DungeonMaster : MonoBehaviour
{

    public Camera camera1;
    public GameObject block_prefab;
    GameObject cur_block;
    int cur_block_id = 0;
    GameObject del_block;
    Color del_block_og_color;
    Boolean is_build_mode = false;
    Boolean is_delete_mode = false;
    UnityEngine.Object[] block_list;
    int block_scale = 1;
    int rotation_pos = 0;
    public Boolean is_grid_enabled = true;
    public float temp_object_transparency = 0.01f;

    //dict that says which relates object to its type
    List<block_info> all_blocks = new List<block_info>(); //for save system


    // Start is called before the first frame update
    void Start()
    {
        block_list = Resources.LoadAll("Blocks", typeof(GameObject));
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() == false) {
            if (is_build_mode)
            {
                RaycastHit hit;
                Ray ray = camera1.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    Vector3 h_real;
                    if (is_grid_enabled)
                    {
                        Vector3 h = hit.point;
                        float x = Round(h.x);
                        float y = Round(h.y + 0.5f*block_scale);
                        float z = Round(h.z);
                        h_real = new Vector3(x, y, z);
                    }
                    else
                    {
                        h_real = hit.point; //make Vector3int h = int(hit.point); for 'grid'
                        h_real.y += 0.5f * block_scale;

                    }
                    //Vector3 h_transformed = h_real;
                    //h_transformed.y += 0.5f * block_scale;
                    if (Input.GetButtonDown("Fire1"))
                    {
                        GameObject real_block = Instantiate(block_prefab);
                        //scaling
                        real_block.transform.localScale = block_prefab.transform.localScale * block_scale;
                        //rotation
                        Vector3 angles = real_block.transform.rotation.eulerAngles;
                        real_block.transform.rotation = Quaternion.Euler(
                                            angles.x,
                                            rotation_pos * 90,
                                            angles.z);
                        //place it on pointer
                        real_block.transform.position = h_real;
                        all_blocks.Add(new block_info(real_block, cur_block_id));
                        Debug.Log("There are " + all_blocks.Count.ToString() + "in all_blocks");
                    }
                    else
                    {
                        //move it to pointer
                        cur_block.transform.position = h_real;
                    }
                }
            }

            else if (is_delete_mode)
            {
                RaycastHit hit;
                Ray ray = camera1.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f, 1))
                {
                    GameObject new_del_block = hit.collider.gameObject;

                    //if object isn't marked for deletion
                    //restore previous marked object
                    //save new object's color and change to transparent red
                    try
                    {
                        if (del_block == null || new_del_block != del_block)
                        {
                            Debug.Log("new_del_block.Equals(del_block)");
                            if (del_block != null)
                            {
                                restore_del_object();
                            }
                            del_block = new_del_block;

                            Renderer rend = del_block.transform.GetComponent<Renderer>();
                            Color meshColor = rend.material.color;
                            del_block_og_color = meshColor; //save original color
                            meshColor = new Color(255, 0, 0, temp_object_transparency);
                            rend.material.color = meshColor;
                        }
                    }
                    catch (NullReferenceException) { };

                    if (Input.GetButtonDown("Fire1"))
                    {
                        for (int i = 0; i < all_blocks.Count; i++)
                        {
                            block_info info = all_blocks[i];
                            if (del_block == info.block)
                            {
                                all_blocks.RemoveAt(i);
                            }
                            //else
                            //{
                            //    Debug.Log("COULDN'T REMOVE INFO FROM BLOCKS REGISTER");
                            //    Debug.Log(all_blocks.ToString());
                            //}
                        }
                        Destroy(del_block);
                        
                        //Destroy(new_del_block);
                    }
                }
                else
                {
                    restore_del_object();
                }
            }
        }
        

    }

    public void on_build_btn_click()
    {
        is_delete_mode = false;
        is_build_mode = true;
        Destroy(cur_block);
        new_temp_block();
    }

    public void on_delete_btn_click()
    {
        is_delete_mode = true;
        is_build_mode = false;
        Destroy(cur_block);
    }

    public void on_block_select(int block_id)
    {
        Destroy(cur_block);
        cur_block_id = block_id; //for save system
        block_prefab = (GameObject)block_list[block_id];
        if (is_build_mode)
        {
            new_temp_block();
        }
    }

    public void on_resize_btn_click(int scale)
    {
        block_scale = scale;
        cur_block.transform.localScale = block_prefab.transform.localScale * block_scale;
    }

    public void on_rotate_btn_click()
    {
        rotation_pos = (rotation_pos + 1) % 4; // can be from 0 to 3; 0 = 0 deg, 1 = 90 deg, 2 = 180 deg, 3 = 270 deg
        Vector3 angles = cur_block.transform.rotation.eulerAngles;
        cur_block.transform.rotation = Quaternion.Euler(
                            angles.x,
                            rotation_pos * 90,
                            angles.z);
    }

    void new_temp_block()
    {
        //create temp object
        cur_block = Instantiate(block_prefab) as GameObject;

        //change temp object's transparency
        Renderer rend = cur_block.transform.GetComponent<Renderer>();
        Color meshColor = rend.material.color;
        meshColor.a = temp_object_transparency;
        rend.material.color = meshColor;

        //change temp object's layer to IgnoreRaycast
        cur_block.layer = 2;

        //block scaling
        cur_block.transform.localScale = block_prefab.transform.localScale * block_scale;

        //block rotation
        Vector3 angles = cur_block.transform.rotation.eulerAngles;
        cur_block.transform.rotation = Quaternion.Euler(
                            angles.x,
                            rotation_pos * 90,
                            angles.z);
    }

    void restore_del_object()
    {
        //Debug.Log("restore_del_object");
        if (del_block == null)
        {
            return;
        }
        Renderer rend = del_block.transform.GetComponent<Renderer>();
        rend.material.color = del_block_og_color;
        del_block = null;
    }

    public static float Round(float d)
    {
        //if (Math.Abs(d) < 0.25f)
        //{
        //    return 0;
        //}

        var absoluteValue = Math.Abs(d);
        var integralPart = (long)absoluteValue;
        var decimalPart = absoluteValue - integralPart;
        var sign = Math.Sign(d);

        float roundedNumber;

        if (decimalPart > 0.75f)
        {
            roundedNumber = integralPart + 1;
        }
        else if (decimalPart < 0.25f)
        {
            roundedNumber = integralPart;
        }
        else
        {
            roundedNumber = integralPart + 0.5f;
        }

        return sign * roundedNumber;
    }


    class block_info
    {
        public GameObject block { get; set; }
        public int block_type { get; set; }
        public block_info(GameObject block, int block_type)
        {
            this.block = block;
            this.block_type = block_type;
        }
    }

    public void on_save_btn_click()
    {
        //rewrite save if already exists
        string save_path = "Saves\\save.sav";
        if (File.Exists(save_path))
        {
            File.WriteAllText(save_path, "");
        }

        //get info about placed blocks from register
        for (int i = 0; i < all_blocks.Count; i++)
        {
            String to_save = "";
            block_info info = all_blocks[i];

            //save type of block
            to_save += info.block_type.ToString() + " ";

            //save xyz coordinates
            Vector3 pos = info.block.transform.localPosition;
            to_save += pos.x + " " + pos.y + " " + pos.z + " ";

            //save xyz in eulerAngles of rotation
            Vector3 rot = info.block.transform.rotation.eulerAngles;
            to_save += rot.x + " " + rot.y + " " + rot.z + " ";

            //save xyz scale
            Vector3 scl = info.block.transform.localScale;
            to_save += scl.x + " " + scl.y + " " + scl.z + " ";

            //save rgb of colors
            Renderer rend = info.block.transform.GetComponent<Renderer>();
            Color col = rend.material.color;
            to_save += col.r + " " + col.g + " " + col.b + " ";

            //add new_line character
            to_save += Environment.NewLine;

            //add block info to file as a single line
            File.AppendAllText(save_path, to_save);

        }
    }
}
