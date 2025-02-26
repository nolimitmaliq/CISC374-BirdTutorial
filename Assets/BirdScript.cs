using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;

    public bool birdIsALive = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //once once when you start the game
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic1").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) == true && birdIsALive){
             myRigidbody.linearVelocity = Vector2.up * flapStrength;
        }
       

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        birdIsALive = false;
    }
}
