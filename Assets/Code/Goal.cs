using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private int herdedSheep = 0;
    private static int totalSheep = 8;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn Sheep!
        var rand = new System.Random();
        var sheepSpawns = GameObject.FindGameObjectsWithTag("SheepSpawn").OrderBy(a => rand.Next()).ToList<GameObject>();

        var sheepToSpawn = totalSheep;

        foreach (var sheepSpawn in sheepSpawns)
        {
            if (sheepToSpawn > 0)
            {
                var sheepObject = Resources.Load("Sheep/Sheep") as GameObject;
                Debug.Log($"Loaded {sheepObject}");
                var singleSheep = Instantiate(sheepObject);
                singleSheep.transform.position = sheepSpawn.transform.position;
                sheepToSpawn--;
            }
            Object.Destroy(sheepSpawn);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log($"Start Collider: {collider}");
        Debug.Log($"Start Collider: {collider.gameObject}");
        Debug.Log($"Start Collider: {collider.gameObject.tag}");

        if (collider.gameObject.tag.Equals("Sheep"))
        {
            herdedSheep++;
        }

        Debug.Log($"Herded: {herdedSheep}/{totalSheep}");

        if (herdedSheep == totalSheep)
        {
            Debug.Log($"All Sheep Herded");
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log($"End Collider: {collider}");
        Debug.Log($"End Collider: {collider.gameObject}");
        Debug.Log($"End Collider: {collider.gameObject.tag}");

        if (collider.gameObject.tag.Equals("sheep"))
        {
            herdedSheep--;
        }
    }
}
