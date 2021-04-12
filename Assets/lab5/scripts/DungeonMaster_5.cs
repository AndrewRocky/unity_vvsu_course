using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonMaster_5 : MonoBehaviour
{
    public int kills_count = 0;
    public int health = 100;
    private Text health_counter;
    
    // Start is called before the first frame update
    void Start()
    {
        health_counter = GameObject.Find("health_label").GetComponent<Text>();
        health_counter.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void player_hit()
    {
        health -= 1;
        health_counter.text = health.ToString();
    }
}
