using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] float speed = 20.0f;
    [SerializeField] float minDist = 1f;
    [SerializeField] Transform target;

    // Use this for initialization
    void Start()
    {
        // If no target is specified, assume the player
        if (target == null)
        {
            if (GameObject.FindWithTag("Player") != null)
            {
                target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If no target is assigned, do nothing
        if (target == null)
        {
            return;
        }

        // Face the target
        transform.LookAt(target);
        transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        // Get the distance between the chaser and the target
        float distance = Vector3.Distance(transform.position, target.position);

        // Move towards the target at the specified speed as long as the distance is greater than the minimum distance
        if (distance > minDist)
            transform.position += transform.forward * speed * Time.deltaTime;
    }
}
