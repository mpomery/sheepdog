using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepAI : MonoBehaviour
{
    private GameObject[] dogs;
    private Rigidbody rb;
    private float angle;
    private static int startleDistance = 10;
    private static int moveSpeed = 400;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        dogs = GameObject.FindGameObjectsWithTag("Player");

        var startX = Random.Range(-80, 80);
        var startZ = Random.Range(-80, 80);

        transform.position = new Vector3(startX, 1, startZ);
    }

    // Update is called once per frame
    void Update()
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
        //Debug.Log($"heading: {heading}");
        //Debug.Log($"distance: {distance}");

        if (distance < startleDistance)
        {
            var theta = Mathf.Atan2(heading.x, heading.z) * Mathf.Rad2Deg;
            angle = theta + 180;
            //Debug.Log($"angle: {angle}");

            var step = 1000 * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(90, angle, 0), step);

            var speed = (startleDistance - distance) / startleDistance;

            rb.velocity = Quaternion.Euler(0, angle + 90, 0) * new Vector3(speed * 10, 0, 0);
        }
    }
}
