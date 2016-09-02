using UnityEngine;
using System.Collections;

public class EnemyController2 : MonoBehaviour
{
    public GameObject shotPrefab;
    public Transform shotSpawn;
    public Transform shotSpawn2;
    public float[] fireRate = {1f,1.5f,2f};
    public float delay;

    public AudioSource audioS;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate[Random.Range(0, fireRate.Length)]);
    }

    void Fire()
    {
        Instantiate(shotPrefab, shotSpawn.position, shotSpawn.rotation);
        Instantiate(shotPrefab, shotSpawn2.position, shotSpawn2.rotation);
        audioS.Play();
    }
}
