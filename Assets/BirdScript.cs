using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //once once when you start the game
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) == true){
             myRigidbody.linearVelocity = Vector2.up * flapStrength;
        }
       

    }
}
