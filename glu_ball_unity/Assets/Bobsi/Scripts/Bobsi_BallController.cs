using UnityEngine;

public class Bobsi_BallController : MonoBehaviour
{
    //Naming.. nice that he uses f to make it very clear it's a float
    public float speed = 5f;
    private Rigidbody rigid;

    private void Start()
    {
        //No [RequireComponent] tag on top, this can fail.
        rigid = gameObject.GetComponent<Rigidbody>();    
    }

    private void Update()
    {
        //Adding force in Update instead of FixedUpdate!
        //Why not cache the GetAxis result in a local variable (improves readability)
        //No f here to compare.
        if (Input.GetAxis("Horizontal") > 0)
        {
            rigid.AddForce(Vector3.right * speed);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            rigid.AddForce(-Vector3.right * speed);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            rigid.AddForce(Vector3.forward * speed);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            rigid.AddForce(-Vector3.forward * speed);
        }
    }
}
