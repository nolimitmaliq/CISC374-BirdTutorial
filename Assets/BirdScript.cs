using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;
    private Camera _mainCamera;

    public bool birdIsALive = true;
     public AudioSource flapSFX; 
    public AudioSource collisionSFX;
    [Header("Squish & Stretch")]
    public float flapSquishAmount = 0.7f; 
    public float stretchAmount = 1.3f;    
    public float animationSpeed = 5f;
    private Vector3 _originalScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //once once when you start the game
    void Start()
    {
        _originalScale = transform.localScale;
        logic = GameObject.FindGameObjectWithTag("Logic1").GetComponent<LogicScript>();
        _mainCamera = Camera.main;
        myRigidbody.bodyType = RigidbodyType2D.Kinematic; // Disable physics initially
        myRigidbody.linearVelocity= Vector2.zero;
    }

    // Update is called once per frame
     void Update()
    {
         if (logic.gameIsActive && birdIsALive)
        {
            float velocityY = myRigidbody.linearVelocity.y;
            Vector3 targetScale = _originalScale;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Squish on flap
                targetScale = new Vector3(_originalScale.x * 1.2f, _originalScale.y * flapSquishAmount, 1);
            }
            else if (velocityY < -2f)
            {
                // Stretch when falling fast
                targetScale = new Vector3(_originalScale.x * stretchAmount, _originalScale.y * 0.8f, 1);
            }

            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * animationSpeed);
        }
        // Enable physics when the game starts
        if (logic.gameIsActive && myRigidbody.bodyType != RigidbodyType2D.Dynamic)
        {
            myRigidbody.bodyType = RigidbodyType2D.Static;
            myRigidbody.linearVelocity = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.Space) && birdIsALive && logic.gameIsActive)
        {
            myRigidbody.linearVelocity = Vector2.up * flapStrength;
            flapSFX.PlayOneShot(flapSFX.clip); 
        }

        if (birdIsALive)
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
            transform.localScale = new Vector3(_originalScale.x * 0.8f, _originalScale.y * 1.5f, 1);
            collisionSFX.PlayOneShot(collisionSFX.clip);
            logic.gameOver();
            birdIsALive = false;
        }
    }
}
