using System.Collections;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int pointsValue;

    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        } 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) //Ignores destroying the boundary
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate (explosion, transform.position, transform.rotation); //Creates explosion if their is an explosion game object 
        }

        // Debug.Log(other.name);

        if (other.CompareTag("Player")) // Creates Player explosion with asteroid
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        gameController.AddPoints (pointsValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}