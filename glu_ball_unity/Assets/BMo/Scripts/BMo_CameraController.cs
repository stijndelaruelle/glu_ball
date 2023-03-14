using UnityEngine;

//Note: This code is taken directly from the following tutorial: https://www.youtube.com/watch?v=BwPT7huwjn4
//All comments were added by me to indicate where things could be improved
public class BMo_CameraController : MonoBehaviour
{
    //SerializeField, init value, naming convention.
    public GameObject target;
    public float xOffset, yOffset, zOffset;

    void Update()
    {
        //Cache our transform in start if you're using it every frame
        //If you're only ever intending to use the transform of the target, why not reference that instead. (this calls GetComponent behind the scenes)
        //Why have 3 floats as a variable when you're combining them in a Vector3 every frame? Just have a Vector3 as a variable (even has a nicer default editor layout)
        transform.position = target.transform.position + new Vector3(xOffset, yOffset, zOffset);

        //Same as above. Cache both transforms please.
        transform.LookAt(target.transform.position);
    }
}
