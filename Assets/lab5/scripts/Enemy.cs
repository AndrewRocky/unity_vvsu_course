using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject bullet_prefab;
    public float bullet_speed;
    private GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.hasPath == false)
        {
            Vector3 target_point = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            agent.SetDestination(target_point);

            shoot_at_player();
        }
    }

    private void shoot_at_player()
    {
        transform.LookAt(player.transform);
        Vector3 dir = transform.TransformDirection(Vector3.forward);
        GameObject cur_bullet = Instantiate(bullet_prefab);
        cur_bullet.transform.position = transform.position + transform.forward * 0.5f;
        cur_bullet.GetComponent<Rigidbody>().AddForce(dir * bullet_speed);
    }
}
