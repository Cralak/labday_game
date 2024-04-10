using UnityEngine;

public class Pixelle : MonoBehaviour
{
    GameObject player;
    bool hasCollided;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        transform.rotation = Quaternion.Euler(-90.0f, transform.eulerAngles.y + 90.0f, 90.0f);

        if (hasCollided && Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(transform.position.x, transform.position.z)) > 2.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 10.0f * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, -1.28f, transform.position.z);
        }
    }

    void OnTriggerEnter()
    {
        hasCollided = true;
    }
}
