
#region TRIMESTER 2 Movement Script
/*REFERENCE
    https://moodle-sae-au.axis.navitas.com/course/view.php?id=6287&section=18#tabs-tree-start
*/
#endregion
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Jumping")]
    [Tooltip("Instant jumps on key press. Charged builds strength while holding jump.")]
    public JumpStyle jumpStyle = JumpStyle.Instant;

    [Tooltip("If true, a jump will happen on the next FixedUpdate.")]
    [SerializeField]
    private bool performJump = false;

    [Tooltip("How many seconds the jump key has been held.")]
    [SerializeField]
    private float jumpHeldTime = 0f;

    [Tooltip("The longest the jump key can be held.")]
    [SerializeField]
    private float jumpHeldTimeMax = 1f;

    [Tooltip("Maximum vertical jump velocity.")]
    [SerializeField]
    private float jumpStrength = 6f;

    [Tooltip("Minimum vertical velocity for a charged jump.")]
    [SerializeField]
    private float jumpStrengthMin = 2f;

    [Tooltip("The jump velocity that will be applied.")]
    [SerializeField]
    private float jumpCharge = 0f;

    [Header("Walking")]
    [Tooltip("Maximum walking speed.")]
    public float walkSpeed = 4f;

    [Tooltip("How quickly the player reaches walking speed while grounded.")]
    [SerializeField]
    private float groundAcceleration = 30f;

    [Tooltip("How much movement control the player has while airborne.")]
    [SerializeField]
    private float airAcceleration = 3f;

    [Header("Sensors")]
    [Tooltip("Radius of the sphere used to detect ground beneath the player.")]
    public float groundSensorRadius = 0.2f;

    [Tooltip("Vertical offset of the ground sensor.")]
    public float groundSensorOffset = -0.5f;

    [Tooltip("True while the player is standing on something.")]
    public bool onGround = false;

    [Header("Rotation")]
    [Tooltip("Rotation around the Y axis.")]
    [SerializeField]
    private float yaw = 0f;

    [Tooltip("Rotation around the X axis.")]
    [SerializeField]
    private float pitch = 0f;

    [Tooltip("Horizontal mouse look speed.")]
    [SerializeField]
    private float yawSpeed = 10f;

    [Tooltip("Vertical mouse look speed.")]
    [SerializeField]
    private float pitchSpeed = 10f;

    [Tooltip("Camera attached to the player.")]
    public Transform childCamera;

    [Tooltip("Minimum camera pitch.")]
    [SerializeField]
    private float pitchMinimum = -80f;

    [Tooltip("Maximum camera pitch.")]
    [SerializeField]
    private float pitchMaximum = 80f;

    [Header("Debug")]
    [Tooltip("Current desired local movement velocity.")]
    [SerializeField]
    private Vector3 desiredVelocity = Vector3.zero;

    public enum JumpStyle
    {
        Instant,
        Charged,
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        Debug.Log("Script Attached");
    }

    private void Update()
    {
        HandleJumpInput();
        HandleMouseLook();
        HandleMovementInput();
    }

    private void HandleJumpInput()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        switch (jumpStyle)
        {
            case JumpStyle.Instant:

                if (onGround && Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    Jump(jumpStrength);
                }

                break;

            case JumpStyle.Charged:

                if (Keyboard.current.spaceKey.isPressed)
                {
                    jumpHeldTime += Time.deltaTime;
                    jumpHeldTime = Mathf.Clamp(jumpHeldTime, 0f, jumpHeldTimeMax);
                }

                if (Keyboard.current.spaceKey.wasReleasedThisFrame)
                {
                    if (onGround)
                    {
                        float chargePercentage =
                            jumpHeldTimeMax > 0f ? jumpHeldTime / jumpHeldTimeMax : 1f;

                        float chargedStrength = chargePercentage * jumpStrength;

                        Jump(Mathf.Max(chargedStrength, jumpStrengthMin));
                    }

                    jumpHeldTime = 0f;
                }

                break;
        }
    }

    private void HandleMouseLook()
    {
        if (Mouse.current == null)
        {
            return;
        }

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        yaw += mouseDelta.x * yawSpeed * Time.deltaTime;

        pitch -= mouseDelta.y * pitchSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, pitchMinimum, pitchMaximum);

        transform.rotation = Quaternion.Euler(0f, yaw, 0f);

        if (childCamera != null)
        {
            childCamera.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }
    }

    private void HandleMovementInput()
    {
        if (Keyboard.current == null)
        {
            desiredVelocity = Vector3.zero;
            return;
        }

        float horizontal =
            (Keyboard.current.aKey.isPressed ? -1f : 0f)
            + (Keyboard.current.dKey.isPressed ? 1f : 0f);

        float vertical =
            (Keyboard.current.sKey.isPressed ? -1f : 0f)
            + (Keyboard.current.wKey.isPressed ? 1f : 0f);

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical);

        // Prevents diagonal movement from being faster.
        inputDirection = Vector3.ClampMagnitude(inputDirection, 1f);

        desiredVelocity = inputDirection * walkSpeed;
    }

    public void Jump(float strength)
    {
        performJump = true;
        jumpCharge = strength;
    }

    private void FixedUpdate()
    {
        CheckGround();
        ApplyMovement();
        ApplyJump();
    }

    private void CheckGround()
    {
        onGround = false;

        Vector3 sensorPosition = transform.position + Vector3.up * groundSensorOffset;

        Collider[] hits = Physics.OverlapSphere(
            sensorPosition,
            groundSensorRadius,
            Physics.AllLayers,
            QueryTriggerInteraction.Ignore
        );

        foreach (Collider hit in hits)
        {
            if (hit.gameObject != gameObject)
            {
                onGround = true;
                break;
            }
        }
    }

    private void ApplyMovement()
    {
        // Convert local movement input into world-space movement.
        Vector3 targetVelocity =
            transform.forward * desiredVelocity.z + transform.right * desiredVelocity.x;

        Vector3 currentVelocity = rb.linearVelocity;

        Vector3 currentHorizontalVelocity = new Vector3(currentVelocity.x, 0f, currentVelocity.z);

        Vector3 velocityDifference = targetVelocity - currentHorizontalVelocity;

        float acceleration = onGround ? groundAcceleration : airAcceleration;

        Vector3 velocityChange = Vector3.ClampMagnitude(
            velocityDifference,
            acceleration * Time.fixedDeltaTime
        );

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    private void ApplyJump()
    {
        if (!performJump)
        {
            return;
        }

        Vector3 velocity = rb.linearVelocity;
        velocity.y = jumpCharge;
        rb.linearVelocity = velocity;

        performJump = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 sensorPosition = transform.position + Vector3.up * groundSensorOffset;

        Gizmos.DrawWireSphere(sensorPosition, groundSensorRadius);
    }
}
