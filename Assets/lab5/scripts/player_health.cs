using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_health : MonoBehaviour
{
    private DungeonMaster_5 event_system;
    // Start is called before the first frame update
    void Start()
    {
        event_system = GameObject.Find("DungeonMaster").GetComponent<DungeonMaster_5>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy_bullet"))
        {
            event_system.player_hit();
        }
    }
}
