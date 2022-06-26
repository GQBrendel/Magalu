using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Drone : MonoBehaviour
{
    rayDrone[] ray;

    public delegate void DeathHandler(Transform t);
    public DeathHandler OnChacterDeath;
    [SerializeField] Transform respawn;

    void Awake()
    {
        ray = GetComponentsInChildren<rayDrone>();
    }

    void FixedUpdate()
    {
        foreach(var obj in ray)
        {
            RayConfig(obj.transform.position);
        }
    }

    void RayConfig(Vector3 p_vec)
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, p_vec);

        RaycastHit2D hit = Physics2D.Linecast(transform.position, p_vec);
        Debug.DrawLine(transform.position, hit.point, Color.white);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                OnChacterDeath?.Invoke(respawn);
            }
        }
    }
}
