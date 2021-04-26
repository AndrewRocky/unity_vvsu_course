using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class menu_manager : MonoBehaviour
{
    GameObject settings_panel;
    InputField enemies_counter_object;
    public int enemies_count = 10;
    settings settings_script;
    // Start is called before the first frame update
    void Start()
    {
        settings_script = GameObject.Find("persist_settings").GetComponent<settings>();
        settings_script.enemies_count = enemies_count;
        enemies_counter_object = GameObject.Find("enemies_input").GetComponent<InputField>();
        settings_panel = GameObject.Find("settings");
        settings_panel.SetActive(false);

    }

    public void onStartButton()
    {
        SceneManager.LoadScene("lab5_scene");
    }

    public void onSettingsButton()
    {
        settings_panel.SetActive(true);
    }

    public void onSettingsCloseButton()
    {
        settings_panel.SetActive(false);
    }

    public void onEnemiesCounterEdit()
    {
        enemies_count = Int32.Parse(enemies_counter_object.text);
        settings_script.enemies_count = enemies_count;
        Debug.Log(enemies_count);
    }
}
