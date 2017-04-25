using UnityEngine;

/// <summary>
/// Example class showing player movement relative to a third-person camera.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class MoveCube : MonoBehaviour
{
    #region Private fields

    // Cached reference to the rigidbody component (required)
    Rigidbody _rigidBody;

    #endregion

    #region Public fields

    [Tooltip("The speed of movement for the cube")]
    public float speed = 20f;

    [Tooltip("The camera that will provide the source forward and right vectors used to calculate player movement")]
    public Camera cam;

    #endregion

    #region Unity messages

    /// <summary>
    /// Awaken this instance!
    /// </summary>
    void Awake()
    {
        // Cache the rigidbody component.
        _rigidBody = GetComponent<Rigidbody>();

        // Check to see if the cam has been set
        if (cam == null)
        {
            // Grab the main camera in the scene
            cam = Camera.main;
        }
    }

    /// <summary>
    /// Tick the script.
    /// </summary>
	void Update ()
    {
        // Keep a local copy of the camera's right vector to save typing later on
        Vector3 right = cam.transform.right;

        // Calculate the forward vector for the player based on the world up and the camera's
        // right vector
        Vector3 forward = Vector3.Cross(right, Vector3.up);
        
        // Calculate the forces to be applied to the player based on player input, the speed,
        // the forward/right vectors
        Vector3 movement = Vector3.zero;
        movement += right * (Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        movement += forward * (Input.GetAxis("Vertical") * speed * Time.deltaTime);
        
        // Apply the forces to the player's rigidbody component
        _rigidBody.AddForce(movement, ForceMode.VelocityChange);
	}

    #endregion
}
