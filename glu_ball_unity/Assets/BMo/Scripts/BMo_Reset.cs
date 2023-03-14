using UnityEngine;
using UnityEngine.SceneManagement;

public class BMo_Reset : MonoBehaviour
{
    //Fine idea to make this a separate script but... why put it on the ball instead of a killbox collider?
    //This is a static killbox, if the level ever goes below it doesn't work.
    //And if the level goes very high and the player falls off... it takes ages for the level to reset.
    public float threshhold = -50f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < threshhold)
        {
            //Reloading the scene i a terrible way of resetting a game. It causes a lot of overhead.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
