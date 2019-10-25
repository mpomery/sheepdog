using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepAI : MonoBehaviour
{
    private GameObject[] dogs;
    private Rigidbody rb;
    private float angle;
    private static int startleDistance = 10;
    private static int moveSpeed = 800;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        dogs = GameObject.FindGameObjectsWithTag("Player");
        if (dogs.Length > 0)
        {
            var closestDog = dogs[0];
            var closestDogDistance = (transform.position - closestDog.transform.position).magnitude;

            foreach (var dog in dogs)
            {
                var myDistance = (transform.position - dog.transform.position).magnitude;
                if (myDistance < closestDogDistance)
                {
                    closestDog = dog;
                    closestDogDistance = myDistance;
                }
            }

            var heading = transform.position - closestDog.transform.position;
            var distance = heading.magnitude;

            if (distance < startleDistance)
            {
                var angle = Mathf.Atan2(heading.x, heading.z) * Mathf.Rad2Deg;
                //Debug.Log($"angle: {angle}");

                var step = 100 * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(90, angle, 0), step);

                var speed = (startleDistance - distance) / startleDistance;

                rb.velocity = Quaternion.Euler(0, angle - 90, 0) * new Vector3(speed * moveSpeed * Time.deltaTime, 0, 0);
                Debug.Log(rb.velocity);
            }
        }
    }
}
