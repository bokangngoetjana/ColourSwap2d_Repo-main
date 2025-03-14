using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public float speed = 1.1f;

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        if(transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }
}
