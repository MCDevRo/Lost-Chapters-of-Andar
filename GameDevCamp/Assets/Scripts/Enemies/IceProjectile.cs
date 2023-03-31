using UnityEngine;

public class IceProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifetime = 3f;

    public GameObject hitFX;

    private Vector3 direction;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TopDownPlayerController playerController = other.GetComponent<TopDownPlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(damage);
                FindObjectOfType<AudioManager>().Play("IceHit");
            }

            Destroy(gameObject);
        }
        if (other.gameObject != null)
        {
            FindObjectOfType<AudioManager>().Play("IceHit");
            Instantiate(hitFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
