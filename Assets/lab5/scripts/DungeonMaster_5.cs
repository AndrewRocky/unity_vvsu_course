using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DungeonMaster_5 : MonoBehaviour
{
    public int kills_count = 0;
    public int health = 100;
    private Text health_counter;
    settings settings_script;
    public GameObject enemy;
    GameObject menu_panel;

    // Start is called before the first frame update
    void Start()
    {
        health_counter = GameObject.Find("health_label").GetComponent<Text>();
        health_counter.text = health.ToString();
        settings_script = GameObject.Find("persist_settings").GetComponent<settings>();
        menu_panel = GameObject.Find("main_menu");
        menu_panel.SetActive(false);
        spawn_enemies(settings_script.enemies_count);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            menu_switch();
        }
    }

    public void player_hit()
    {
        health -= 1;
        health_counter.text = health.ToString();
    }

    void spawn_enemies(int count)
    {
        int column_size = 5;
        int rows;
        int last_row;
        if (count > column_size)
        {
            rows = count / column_size;
            last_row = count % rows;
        }
        else
        {
            rows = 0;
            last_row = count;
        }

        for (int i = 0; i < rows; i++)
        {
            for (int k=0; k < 5; k++)
            {
                GameObject new_enemy = Instantiate(enemy);
                new_enemy.transform.position = new Vector3(i * 3, 0.5f, k * 3);
            }
            for (int k = 0; k < last_row; k++)
            {
                GameObject new_enemy = Instantiate(enemy);
                new_enemy.transform.position = new Vector3(i * 3, 0.5f, k * 3);
            }
        }
    }

    public void return_to_menu()
    {
        Destroy(settings_script.gameObject);
        SceneManager.LoadScene("main_menu");
    }

    void menu_switch()
    {
        if (menu_panel.activeInHierarchy == true)
        {
            menu_panel.SetActive(false);
        }
        else
        {
            menu_panel.SetActive(true);
        }
    }
}
