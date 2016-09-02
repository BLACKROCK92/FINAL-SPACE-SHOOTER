using UnityEngine;
using System.Collections;

public class EnemyController3 : MonoBehaviour
{
    public GameObject shotPrefab;
    public Transform shotSpawn;
    public float[] fireRate = { 1f, 1.5f, 2f };
    public float delay;

    public AudioSource audioS;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate[Random.Range(0,fireRate.Length)]);
    }

    void Fire()
    {
        Instantiate(shotPrefab, shotSpawn.position, shotSpawn.rotation);
        audioS.Play();
    }
}
