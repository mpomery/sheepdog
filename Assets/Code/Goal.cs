using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private static int goalRadius = 10;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(2 * goalRadius, 1, 2 * goalRadius);
        var sheep = FindObjectsOfType(typeof(SheepAI)) as SheepAI[];
    }

    // Update is called once per frame
    void Update()
    {

        var closeSheep = 0;

        var sheep = FindObjectsOfType(typeof(SheepAI)) as SheepAI[];
        //Debug.Log(sheep.Length);

        foreach (var s in sheep)
        {
            var distance = (transform.position - s.transform.position).magnitude;
            if (distance < goalRadius)
            {
                closeSheep++;
            }
        }

        //Debug.Log($"{closeSheep}/{sheep.Length}");

        if (closeSheep == sheep.Length)
        {
            Debug.Log($"All Sheep Herded");
        }
    }
}
