using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public Transform playerCamera;
    public string inputDevice;

    // Camera angle and rotation is relative to games axis, not player
    private float cameraElevationAngle = 10; // 10 to 45 degrees
    private float cameraRotation = 180; // 0 - 360 degrees
    private readonly float cameraDistance = 40; // How far away from the player the camera should sit

    private float playerAngle = 0;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player Initialized");
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        for (int action = (int)KeyCode.Backspace; action <= (int)KeyCode.Joystick8Button19; action++)
        {
            if (Input.GetKeyDown((KeyCode)action) && ((KeyCode)action).ToString().Contains("Joystick"))
            {
                var controllerNumber = ((KeyCode)action).ToString().Substring(8, 2);
                if (controllerNumber.EndsWith("B"))
                {
                    controllerNumber = controllerNumber.Substring(0, 1);
                }
                Debug.Log("This is Joystick Number " + controllerNumber);
            }
        }

        // Move the camera
        var verticalCameraInput = Input.GetAxis($"{inputDevice} Camera X");
        var horizontalCameraInput = Input.GetAxis($"{inputDevice} Camera Y");

        cameraRotation = (cameraRotation - verticalCameraInput) % 360;
        cameraElevationAngle += horizontalCameraInput;
        if (cameraElevationAngle > 45)
        {
            cameraElevationAngle = 45;
        }
        else if (cameraElevationAngle < 10)
        {
            cameraElevationAngle = 10;
        }

        //Debug.Log($"cameraRotation: {cameraRotation}"); // Rotate around the Y Axis
        //Debug.Log($"cameraElevationAngle: {cameraElevationAngle}");

        // Place the camera relative to the player
        var cameraOffset = new Vector3(0, 0, cameraDistance);
        cameraOffset = Quaternion.Euler(-cameraElevationAngle, cameraRotation, 0f) * cameraOffset;
        playerCamera.position = transform.position + cameraOffset;
        playerCamera.rotation = Quaternion.Euler(cameraElevationAngle, cameraRotation + 180, 0f);


        // Calculate input amount
        var verticalInput = Input.GetAxis($"{inputDevice} Vertical");
        var horizontalInput = Input.GetAxis($"{inputDevice} Horizontal");
        var inputSpeed = Mathf.Sqrt(Mathf.Pow(verticalInput, 2) + Mathf.Pow(horizontalInput, 2));
        // Cap input speed to 1 to prevent keyboard from going faster
        if (inputSpeed > 1)
        {
            inputSpeed = 1;
        }
        
        // Input Threshold
        if (inputSpeed > 0.3)
        {
            // Calucate the movement direction input angle
            var theta = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;
            playerAngle = theta + cameraRotation;
            //Debug.Log($"theta: {theta}");
            //Debug.Log($"playerAngle: {playerAngle}");

            // Look Towards that angle
            var step = 1000 * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(90, playerAngle, 0), step);

            rb.velocity = Quaternion.Euler(0, playerAngle + 90, 0) * new Vector3(inputSpeed * 10, 0, 0);
        }

    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {

        //rb.AddForce(new Vector3(horizontalInput, 0.0f, verticalInput) * moveSpeed * Time.deltaTime);

        //rb.velocity = new Vector3(verticalInput * moveSpeed * Time.deltaTime, 0, horizontalInput * moveSpeed * Time.deltaTime);
    }
}
