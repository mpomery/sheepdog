using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInstantiator : MonoBehaviour
{
    public Texture guiTexture;
    public string[] controllers = { "Keyboard", "Keyboard", "Keyboard" };

    // Start is called before the first frame update
    // This is used to create all of the player objects in the scene and their cameras
    void Start()
    {
        var playerCount = controllers.Length;

        var playerScreenHeight = 1.0f;
        var playerScreenWidth = 1.0f;

        if (playerCount > 1)
        {
            playerScreenHeight = 0.5f;
        }

        if (playerCount > 2)
        {
            playerScreenWidth = 0.5f;
        }


        for (var i = 0; i < playerCount; i++)
        {
            var controller = controllers[i];
            Debug.Log($"Creating Player For {controller}");
            var playerObject = Resources.Load("Player/Dog") as GameObject;
            Debug.Log($"Loaded {playerObject}");
            var player = Instantiate(playerObject);

            Debug.Log($"Creating Camera For {controller}");
            var cameraObject = Resources.Load("Player/Camera") as GameObject;
            Debug.Log($"Loaded {cameraObject}");
            var camera = Instantiate(cameraObject);

            var playerCamera = camera.GetComponent<Camera>();
            var playerscript = player.GetComponent<PlayerMovement>();

            var startX = i * playerScreenWidth;
            var startY = 1 - playerScreenHeight;
            if (startX >= 1)
            {
                startX -= 1;
                startY -= playerScreenHeight;
            }
            playerCamera.rect = new Rect(startX, startY, playerScreenWidth, playerScreenHeight);

            playerscript.playerCamera = playerCamera;
            playerscript.inputDevice = controller;
        }
    }

    // Update is called once per frame
    void OnGUI()
    {
        var playerCount = controllers.Length;

        Debug.Log(GUI.color);

        if (playerCount > 1)
        {
            // Draw Horizintal Line
            GUI.Box(new Rect(0, Screen.height / 2f, Screen.width, 2), guiTexture);
            //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "This is a box");
        }

        if (playerCount > 2)
        {
            // Draw Vertical Line
            GUI.Box(new Rect(Screen.width / 2f, 0, 2, Screen.height), guiTexture);
        }
    }
}
