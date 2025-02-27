using UnityEngine;

public class PipeSpawnerScript1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject pipe;
    public float spawnRate = 2;
    private float timer = 0;

    public float heightOffset = 10;
    private LogicScript logic;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic1").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!logic.gameIsActive) return;
        if(timer < spawnRate){
            timer = timer + Time.deltaTime;
        }
        else{
            spawnPipe();
            timer = 0;
        }
        
    }

    void spawnPipe(){

        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}
