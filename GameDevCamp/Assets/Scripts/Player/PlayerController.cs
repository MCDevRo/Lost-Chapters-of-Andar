using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float maxVelocity = 10f;
    [SerializeField] private Transform moveDirectionReference;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform fireballSpawnPoint;
    private CharacterController characterController;

    public float fireballSpeed = 20f;
    public float fireballDuration = 5f;
    public float fireballRange = 50f;

    private bool isFiring = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            // Move the player based on input
            Vector3 moveDirection = moveDirectionReference.forward * vertical;
            moveDirection.Normalize();

            if (characterController.velocity.magnitude < maxVelocity)
            {
                characterController.SimpleMove(moveDirection * moveSpeed);
            }

            // Set the "Speed" parameter based on the player's velocity
            animator.SetFloat("Speed", characterController.velocity.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }

        // Rotate the player based on input
        animator.SetFloat("Turn", horizontal);

        // Project the mouse position onto a plane
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance = 0f;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 targetPosition = ray.GetPoint(distance);
            Vector3 targetDirection = targetPosition - transform.position;
            targetDirection.y = 0f;

            if (targetDirection != Vector3.zero)
            {
                Quaternion targetRotation2 = Quaternion.LookRotation(targetDirection);
                transform.rotation = targetRotation2;
            }
        }
    }
}