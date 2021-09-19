using UnityEngine;

public class PacMovement : MonoBehaviour
{

    [SerializeField]
    private GameObject Obj;
    private Tween activeTween;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        addTween();

        if (activeTween != null) {

            //calculating lerp variables using linear interpolation since pacman has linear movement
            float timeFraction = (Time.time-activeTween.StartTime) / activeTween.Duration;
            float distance = Vector3.Distance(activeTween.Target.position, activeTween.EndPos);

            if (distance > 0.1f) {
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, timeFraction);
            }

            if (distance <= 0.1f) {
                activeTween.Target.position = activeTween.EndPos;
                activeTween = null;
            }
        }
    }

    public void addTween() {
        if (activeTween == null) {

            //if pacman is in top left corner, move right
            if ( (Obj.transform.position.x == -12.5f) && (Obj.transform.position.y == 13.5f) ) {
                activeTween = new Tween (Obj.transform, Obj.transform.position, new Vector3 (-1.5f, 13.5f, 0.0f), Time.time, 3.0f);
            }

            //if pacman is in top right corner, move down
            if ( (Obj.transform.position.x == -1.5f) && (Obj.transform.position.y == 13.5f) ) {
                activeTween = new Tween (Obj.transform, Obj.transform.position, new Vector3 (-1.5f, 9.5f, 0.0f), Time.time, 1.5f);
            }

            //if pacman is in bottom right corner, move left
            if ( (Obj.transform.position.x == -1.5f) && (Obj.transform.position.y == 9.5f) ) {
                activeTween = new Tween (Obj.transform, Obj.transform.position, new Vector3 (-12.5f, 9.5f, 0.0f), Time.time, 3.0f);
            }

            //if pacman is in bottom left corner, move up
            if ( (Obj.transform.position.x == -12.5f) && (Obj.transform.position.y == 9.5f) ) {
                activeTween = new Tween (Obj.transform, Obj.transform.position, new Vector3 (-12.5f, 13.5f, 0.0f), Time.time, 1.5f);
            }
        }
    }//end addTween()
}
