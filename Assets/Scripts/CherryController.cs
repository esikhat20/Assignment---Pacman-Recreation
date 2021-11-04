using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public GameObject bonusCherry;
    private GameObject cherryClone;
    private Tween cherryTween;
    private Vector3 spawnPos0;
    private Vector3 spawnPos1;
    private Vector3 spawnPos2;
    private Vector3 spawnPos3;
    private List<Vector3> spawnList = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        // Repeats the spawnCherry method every 10 seconds
        InvokeRepeating("spawnCherry", 10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (cherryClone != null) {
            // Calculating lerp variable t for lerping
            float timeFraction = (Time.time - cherryTween.StartTime) / cherryTween.Duration;
            cherryClone.transform.position = Vector3.Lerp(cherryTween.StartPos, cherryTween.EndPos, timeFraction);

            // If the distance between the cherry and it's end position is less than 0.1, destroy it
            if ( Vector3.Distance(new Vector3(cherryClone.transform.position.x, cherryClone.transform.position.y, 0), cherryTween.EndPos) < 0.1f) {
                Destroy(cherryClone.gameObject);
            }
        }
    }

    private void spawnCherry() {
        // Camera x and y boundaries
        float xPosLeft = (-Camera.main.orthographicSize * Camera.main.aspect) - 0.5f;
        float xPosRight = (Camera.main.orthographicSize * Camera.main.aspect) + 0.5f;
        float yPosUp = Camera.main.orthographicSize + 0.5f;
        float yPosDown = -Camera.main.orthographicSize - 0.5f;

        // Randomly generating a value between the boundaries
        float randomXPos = Random.Range(xPosLeft, xPosRight);
        float randomYPos = Random.Range(yPosDown, yPosUp);

        // All the different spawn point combinations that take place outside of the camera view
        spawnPos0 = new Vector3(randomXPos, yPosUp, 0); // random x, above the camera view
        spawnPos1 = new Vector3(randomXPos, yPosDown, 0); // random x, below the camera view
        spawnPos2 = new Vector3(xPosLeft, randomYPos, 0); // left of the camera view, random y
        spawnPos3 = new Vector3(xPosRight, randomYPos, 0); // right of the camera view, random y

        // Add the spawn positions to the list
        spawnList.Add(spawnPos0);
        spawnList.Add(spawnPos1);
        spawnList.Add(spawnPos2);
        spawnList.Add(spawnPos3); 

        // Generate a random list index from the list's size boundary 
        // (this will increase every time the method is called but I can't think of a cleaner way to do this)
        int spawnIndex = Random.Range(0, spawnList.Count);

        // Starting position of the tween is the index of the list (which is a random spawn position)
        // Ending position of the tween is the negative alternate of the starting position
        // (this ensures that the cherry will pass through (0,0) -> basically a linear function like y = x, with startingPos
        // and endingPos coordinates which lie on it)
        Vector3 startingPos = spawnList[spawnIndex];
        Vector3 endingPos = -startingPos;

        // Create a clone of the bonus cherry at the starting position
        cherryClone = Instantiate(bonusCherry, startingPos, Quaternion.identity);

        // The clone's tween : Transform, Start Position, End Position, Start Time, Duration
        cherryTween = new Tween(cherryClone.transform, startingPos, endingPos, Time.time, 10.0f);        
    }
}
