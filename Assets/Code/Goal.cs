using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private static int goalRadius = 10;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(2 * goalRadius, 1, 2 * goalRadius);

        // Spawn Sheep!
        var rand = new System.Random();
        var sheepToSpawn = 8;
        var sheepSpawns = GameObject.FindGameObjectsWithTag("SheepSpawn").OrderBy(a => rand.Next()).ToList<GameObject>();

        for (var i = 0; i < sheepToSpawn; i++)
        {
            var sheepObject = Resources.Load("Sheep/Sheep") as GameObject;
            Debug.Log($"Loaded {sheepObject}");
            var singleSheep = Instantiate(sheepObject);
            singleSheep.transform.position = sheepSpawns[i].transform.position;
        }
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
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
