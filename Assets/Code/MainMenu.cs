using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // TODO: Convert this to a list of objects
    private string[] possibleControllers = { "Keyboard", "Joy1", "Joy2", "Joy3", "Joy4", "Joy5", "Joy6", "Joy7", "Joy8" };

    // Start is called before the first frame update
    void Start()
    {
        Players.RemoveAllPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var controller in possibleControllers)
        {
            if (Input.GetButtonDown($"{controller} Select"))
            {
                if (Players.IsPlayer(controller))
                {
                    Debug.Log($"{controller} is already in game!");
                }
                else
                {
                    Players.AddPlayer(controller);
                    Debug.Log($"{controller} added to the game!");
                }
            }

            if (Input.GetButtonDown($"{controller} Submit"))
            {
                if (Players.IsPlayer(controller))
                {
                    Debug.Log($"{controller} is in game and wants to start!");
                    SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
                }
                else
                {
                    Debug.Log($"{controller} is not in the game so can't start it.");
                }
            }

            if (Input.GetButtonDown($"{controller} Cancel"))
            {
                Players.RemovePlayer(controller);
                Debug.Log($"{controller} is in game and wants out!");
            }
        }
    }

    private void OnGUI()
    {
        var gui = new GUIStyle();

        gui.normal.textColor = Color.black;

        var players = "";
        var allPlayers = Players.GetAllPlayers();
        for (var i = 0; i < 4; i++)
        {
            var controller = allPlayers[i]?.Controller;
            if (controller == null)
            {
                controller = "No One";
            }
            players += $"{i}: {controller}\r\n";
        }
        GUI.Label(new Rect(100, 100, 500, 500), players, gui);
    }
}
