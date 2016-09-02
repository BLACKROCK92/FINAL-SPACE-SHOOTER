using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Boundary bounds;
    public float tilt;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.5f;
    public float nextFire = 0.01f;

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 pos = new Vector3(moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().velocity = pos * speed;

        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, bounds.xMin, bounds.xMax),
            0,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, bounds.zMin, bounds.zMax)
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 0, GetComponent<Rigidbody>().velocity.x * -tilt);
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }
    }

}

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
