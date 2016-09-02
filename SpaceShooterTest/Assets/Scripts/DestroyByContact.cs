using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    private GameController gameController;
    public int scoreValue;

    void Start()
    {
        GameObject gameControllerGO = GameObject.FindWithTag("GameController");
        if (gameControllerGO != null)
        {
            gameController = gameControllerGO.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" || other.tag == "Shot")
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
            Destroy(gameObject, 0.01f);
            Destroy(other.gameObject, 0.01f);
            Instantiate(explosion, transform.position, transform.rotation);

            if (other.tag == "Player")
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.TakeLife(-1);
                if (gameController.getLives() <= 0)
                {
                    gameController.StartCoroutine(gameController.EndGame());
                }

                if (gameController.getLives() > 0)
                {
                    gameController.GameOver();
                }
            }

            if (other.tag == "Shot")
            {
                gameController.AddScore(scoreValue);
            }
        }

    }
}
