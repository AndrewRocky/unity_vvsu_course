using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class HitScript : MonoBehaviour
{
    public Text kill_counter;
    //public GameObject event_object;
    private DungeonMaster_5 event_system;
    // Start is called before the first frame update
    void Start()
    {
        event_system = GameObject.Find("DungeonMaster").GetComponent<DungeonMaster_5>();
        //event_system = event_object.GetComponent<DungeonMaster_5>();
        kill_counter = GameObject.Find("kills_label").GetComponent<Text>();
        kill_counter.text = event_system.kills_count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet") || collision.gameObject.CompareTag("player_box"))
        {
            Destroy(gameObject);
            int kill_count = event_system.kills_count + 1;
            kill_counter.text = kill_count.ToString();
            event_system.kills_count += 1;
        }
    }
}
