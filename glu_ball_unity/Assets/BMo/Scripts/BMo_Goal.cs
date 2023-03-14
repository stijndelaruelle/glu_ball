using UnityEngine;
using UnityEngine.SceneManagement;

public class BMo_Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Name "component"? Whynot name it what it is.. a playerContoller
        BMo_PlayerController component = other.gameObject.GetComponent<BMo_PlayerController>();
        if (component != null)
        {
            //No overflow checks or anything. Why not have a list of scenes or something more flexible..
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
