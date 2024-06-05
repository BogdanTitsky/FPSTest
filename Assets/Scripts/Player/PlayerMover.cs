using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private Rigidbody childRigidbody; 
    
    private bool isGrounded;


    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        Vector3 velocity = move * currentSpeed;
        velocity.y = childRigidbody.velocity.y;

        // Переміщуємо весь батьківський об'єкт
        childRigidbody.velocity = velocity;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            childRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground")) 
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))  
        {
            isGrounded = false;
        }
    }
}