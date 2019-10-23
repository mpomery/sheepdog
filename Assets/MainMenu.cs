using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // TODO: Convert this to a list of objects
    private string[] players = { "", "", "", "" }; // Maximum of 4 players

    private string[] possibleControllers = { "Keyboard", "Joy1", "Joy2", "Joy3", "Joy4", "Joy5", "Joy6", "Joy7", "Joy8" };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var controller in possibleControllers)
        {
            if (Input.GetButtonDown($"{controller} Select"))
            {
                if (Array.Exists(players, player => player == controller))
                {
                    Debug.Log($"{controller} is already in game!");
                }
                else
                {
                    var playerAdded = false;
                    for (var i = 0; i < players.Length; i++)
                    {
                        if (players[i] == "")
                        {
                            players[i] = controller;
                            Debug.Log($"{controller} added to the game!");
                            playerAdded = true;
                            break;
                        }
                    }
                    if (!playerAdded)
                    {
                        Debug.Log($"No free spaces for {controller} :(");
                    }

                }
            }

            if (Input.GetButtonDown($"{controller} Submit"))
            {
                if (Array.Exists(players, player => player == controller))
                {
                    Debug.Log($"{controller} is in game and wants to start!");
                    Players.players = players;
                    SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
                }
                else
                {
                    Debug.Log($"{controller} is not in the game so can't start it.");
                }
            }

            if (Input.GetButtonDown($"{controller} Cancel"))
            {
                if (Array.Exists(players, player => player == controller))
                {
                    for (var i = 0; i < players.Length; i++)
                    {
                        if (players[i] == controller)
                        {
                            players[i] = "";
                            Debug.Log($"{controller} is in game and wants out!");
                            break;
                        }
                    }
                }
            }
        }
    }
}
