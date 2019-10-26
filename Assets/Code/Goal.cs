using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private int herdedSheep = 0;
    private static int totalSheep = 8;
    private GameObject[] sheep;
    private GameObject[] dogs;

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
        sheep = GameObject.FindGameObjectsWithTag("Sheep");
    }

    // Update is called once per frame
    void Update()
    {
        /*dogs = GameObject.FindGameObjectsWithTag("Player");

        for (var i = 0; i < totalSheep; i++)
        {
            var s = sheep[i];
            for (var j = 0; j < totalSheep; j++)
            {
                // Can't assert a force from yourself on yourself
                if (i == j)
                {
                    continue;
                }
            }
            foreach (var d in dogs)
            {
                var srb = s.GetComponent<Rigidbody>();
                var heading = s.transform.position - d.transform.position;
                var distance = heading.magnitude;
                Debug.Log($"{heading}: {distance}");

                var angle = Mathf.Atan2(heading.x, heading.z) * Mathf.Rad2Deg;
                Debug.Log($"angle: {angle}");

                var forceMagnitude = 1/Mathf.Pow(distance, 2);
                Debug.Log($"forceMagnitude: {forceMagnitude}");
                var forceDirection = angle;
                Debug.Log($"forceDirection: {forceDirection}");

                srb.AddForce(Quaternion.Euler(0, angle, 0) * new Vector3(40000 * forceMagnitude* Time.deltaTime, 0, 0));
            }
        }*/
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
