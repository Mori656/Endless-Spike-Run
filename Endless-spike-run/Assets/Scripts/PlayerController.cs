using UnityEngine;
using TMPro;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    [Header("Camera")]
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float acceleration = 5f;
    public float jumpForce = 8f;
    public float gravity = -9.81f;
    public float crouchHeight = 1f;
    public float defaultHeight = 2f;
    public float crouchSpeed = 3f;
    public float slideSpeed = 10f;
    public float slideDuration = 0.7f;

    [Header("Audio Settings")]
    public AudioClip footstepsClip;
    public AudioClip jumpClip;
    public AudioClip slideClip;
    public float walkStepRate = 0.5f;
    public float runStepRate = 0.3f;

    [Header("UI")]
    public TextMeshProUGUI textUI;
    public TextMeshProUGUI buffUI;

    private CharacterController controller;
    private AudioSource audioSource;

    private Vector3 velocity;
    private bool isSliding = false;
    private float originalHeight;
    private float currentSpeed = 0f;
    private float stepTimer = 0f;

    private float rotationX = 0f;

    // Score and Buff
    public int score = 0;
    private float starBuff = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        originalHeight = controller.height;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleLook();
        HandleMovement();
        HandleFootsteps();
        HandleJump();
        HandleSlide();
        HandleScoreUI();
        HandleStarBuff();
    }

    private void HandleLook()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = isRunning ? runSpeed : walkSpeed;

        // Kucanie
        if (Input.GetKey(KeyCode.R) && controller.isGrounded && !isSliding)
        {
            controller.height = crouchHeight;
            targetSpeed = crouchSpeed;
        }
        else if (!isSliding)
        {
            controller.height = defaultHeight;
        }

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Grawitacja
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void HandleFootsteps()
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

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && controller.isGrounded && !isSliding)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            PlaySound(jumpClip);
        }
    }

    private void HandleSlide()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && controller.isGrounded && !isSliding)
        {
            isSliding = true;
            controller.height = crouchHeight;

            if (slideClip != null)
            {
                audioSource.clip = slideClip;
                audioSource.loop = false;
                audioSource.Play();
            }

            Invoke(nameof(EndSlide), slideDuration);
        }
    }

    private void EndSlide()
    {
        isSliding = false;
        controller.height = defaultHeight;

        if (audioSource.clip == slideClip && audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
    }

    private void HandleScoreUI()
    {
        if (textUI != null)
            textUI.text = "Wynik: " + score.ToString();
        else
            Debug.LogWarning("textUI nie jest przypisane w inspektorze!");

        if (buffUI != null && starBuff > 0)
            buffUI.text = "StarBuff: " + (int)starBuff;
        else {
            Debug.LogWarning("buffUI nie jest przypisane w inspektorze!");
            buffUI.text = "";
        }
    }

    private void HandleStarBuff()
    {
        if (starBuff >= 1f)
        {
            starBuff -= Time.deltaTime;
        }
        else
        {
            starBuff = 0;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            score++;
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Star"))
        {
            score += 10;
            starBuff = 10;
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Envoirment") && starBuff > 0)
        {
            other.gameObject.SetActive(false);
        }
    }
}
