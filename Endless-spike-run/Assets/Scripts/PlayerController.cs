using UnityEngine;
using TMPro;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float acceleration = 5f;
    public float jumpForce = 8f;
    public float gravity = -9.81f;
    public float slideSpeed = 10f;
    public float slideDuration = 0.7f;


    [Header("Audio Settings")]
    public AudioClip footstepsClip;
    public AudioClip jumpClip;
    public AudioClip slideClip;
    public float walkStepRate = 0.5f;
    public float runStepRate = 0.3f;

    private CharacterController controller;
    private AudioSource audioSource;
    private Vector3 velocity;
    private bool isSliding = false;
    private float originalHeight;
    public float slideHeight = 1f;

    private float currentSpeed = 0f;
    private float stepTimer = 0f;

    // score and star

    private float starBuff = 10f; 
    public int score = 0;
    
    public TextMeshProUGUI textUI;



    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        originalHeight = controller.height;
    }

    void Update()
    {
        Move();
        HandleFootsteps();

        if (Input.GetButtonDown("Jump") && controller.isGrounded && !isSliding)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && controller.isGrounded && !isSliding)
        {
            StartSlide();
        }
        if (textUI != null)
            textUI.text = "Wynik: " + score.ToString();
        else
            Debug.LogWarning("textUI nie jest przypisane w inspektorze!");
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = isRunning ? runSpeed : walkSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        controller.Move(move * currentSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void HandleFootsteps()
    {
        if (!controller.isGrounded || isSliding)
            return;

        bool isMoving = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).sqrMagnitude > 0.1f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlaySound(footstepsClip);
                stepTimer = isRunning ? runStepRate : walkStepRate;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        PlaySound(jumpClip);
    }

    void StartSlide()
    {
        isSliding = true;
        controller.height = slideHeight;
        PlaySound(slideClip);
        Invoke(nameof(EndSlide), slideDuration);
    }

    void EndSlide()
    {
        isSliding = false;
        controller.height = originalHeight;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
// score and star
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Coin"))
        {
            score++;
            Destroy(other.gameObject);
        }
        
        if (other.CompareTag("Star"))
        {
            score += 10;
            starBuff = 10;
            Destroy(other.gameObject);
        }
    }
}
