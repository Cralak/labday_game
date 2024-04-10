using UnityEngine;

public class WrongWay : MonoBehaviour
{
    [SerializeField] GameObject monster;
    [SerializeField] GameObject player;

    bool isTouching = false;
    float initialRotationY;
    float currentY;

    void Start()
    {
        initialRotationY = monster.transform.rotation.eulerAngles.y;
        currentY = monster.transform.position.y;
    }

    void Update()
    {
        if (isTouching)
        {
            monster.transform.LookAt(player.transform.position);
            monster.transform.rotation = Quaternion.Euler(0, initialRotationY, 0);

            if (Vector3.Distance(monster.transform.position, player.transform.position) > 1.5f)
            {
                Vector3 targetPosition = player.transform.position - Vector3.up * (player.transform.position.y - currentY);
                monster.transform.position = Vector3.MoveTowards(monster.transform.position, targetPosition, Time.deltaTime * 20f);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        isTouching = true;
    }
}
