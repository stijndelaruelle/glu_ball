using UnityEngine;

//Note: This code is taken directly from the following tutorial: https://www.youtube.com/watch?v=BwPT7huwjn4
//All comments were added by me to indicate where things could be improved
public class BMo_PlayerController : MonoBehaviour
{
    //Public without [SerializeField] & naming isn't distinct from local variables
    //Not initialized with null.
    //Doesn't need to be public as it will be automatically set in the Awake method.
    public Rigidbody rb;

    //Same naming problem as above
    public float moveSpeed = 10f;

    //Naming problems & no default value;
    private float xInput;
    private float zInput;

    //No access modifier on private methods (while declaring private correctly on certain variables = inconsistent)
    void Awake()
    {
        //There's no [RequireComponent(typeof(Rigidbody))] at the top, so this can fail!
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    //Nicely separated functionality in methods.
    private void ProcessInputs()
    {
        //Usage of the old input system.
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
    }

    private void Move()
    {
        //No null check. Rigidbody can be null (check comment in awake)
        rb.AddForce(new Vector3(xInput, 0f, zInput) * moveSpeed);
    }
}
