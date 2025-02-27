using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;
    private Camera _mainCamera;

    public bool birdIsALive = true;
     public AudioSource flapSFX; // Assign in Inspector
    public AudioSource collisionSFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //once once when you start the game
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic1").GetComponent<LogicScript>();
        _mainCamera = Camera.main;
        myRigidbody.bodyType = RigidbodyType2D.Kinematic; // Disable physics initially
        myRigidbody.linearVelocity= Vector2.zero;
    }

    // Update is called once per frame
     void Update()
    {
        // Enable physics when the game starts
        if (logic.gameIsActive && myRigidbody.bodyType != RigidbodyType2D.Dynamic)
        {
            myRigidbody.bodyType = RigidbodyType2D.Static;
            myRigidbody.linearVelocity = Vector2.zero;
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Space) && birdIsALive && logic.gameIsActive)
        {
            myRigidbody.linearVelocity = Vector2.up * flapStrength;
            flapSFX.PlayOneShot(flapSFX.clip); 
        }

        if (birdIsALive && logic.gameIsActive)
        {
            Vector2 screenMin = _mainCamera.ViewportToWorldPoint(Vector2.zero);
            Vector2 screenMax = _mainCamera.ViewportToWorldPoint(Vector2.one);

            if (transform.position.y < screenMin.y || transform.position.y > screenMax.y)
            {
                TriggerGameOver();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    { 
        TriggerGameOver();
    }

    private void TriggerGameOver()
    {
        if (birdIsALive)
        {
            collisionSFX.PlayOneShot(collisionSFX.clip);
            logic.gameOver();
            birdIsALive = false;
        }
    }
}
