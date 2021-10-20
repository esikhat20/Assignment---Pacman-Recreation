using UnityEngine;

public class PacStudentController : MonoBehaviour
{

    [SerializeField]
    private GameObject Obj;
    private Tween activeTween;
    private Animator anim;
    private AudioSource pacAudioSource;
    private string lastInput;
    private string currentInput;
    private float distance;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        pacAudioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        checkLastInput();

        // if the raycast does not hit an object (i.e. wall), then continue movement
        if (!RaycastCheck()) {
            currentInput = lastInput;
            anim.speed = 1;
            pacAudioSource.UnPause();
            addTween();
        }
        // else stop animation and walking sound
        else {
            anim.speed = 0;
            pacAudioSource.Pause();
        }

        if (activeTween != null) {

            //calculating lerp variables using linear interpolation since pacman has linear movement
            float timeFraction = (Time.time - activeTween.StartTime) / activeTween.Duration;
            distance = Vector3.Distance(activeTween.Target.position, activeTween.EndPos);

            if (distance > 0.1f) {
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, timeFraction);
            }

            if (distance <= 0.1f) {
                activeTween.Target.position = activeTween.EndPos;
                activeTween = null;
            }
        }        

    }//end Update()

    public void addTween() {
        if (activeTween == null) {
            // move up
            if (currentInput == "W") {
                if (distance < 0.2f) { // delay for lastInput 
                activeTween = new Tween (Obj.transform, Obj.transform.position, new Vector3 (transform.position.x, transform.position.y + 1.0f, 0.0f), Time.time, 0.35f);
                anim.ResetTrigger("walk-left");
                anim.ResetTrigger("walk-down");
                anim.ResetTrigger("walk-right");
                anim.SetTrigger("walk-up");
                }
            }
            // move left
            else if (currentInput == "A") {
                if (distance < 0.2f) {
                    activeTween = new Tween (Obj.transform, Obj.transform.position, new Vector3 (transform.position.x - 1.0f, transform.position.y, 0.0f), Time.time, 0.35f);
                    anim.ResetTrigger("walk-down");
                    anim.ResetTrigger("walk-right");
                    anim.ResetTrigger("walk-up");
                    anim.SetTrigger("walk-left");
                }
            }
            //move down
            else if (currentInput == "S") {
                if (distance < 0.2f) {
                    activeTween = new Tween (Obj.transform, Obj.transform.position, new Vector3 (transform.position.x, transform.position.y - 1.0f, 0.0f), Time.time, 0.35f);
                    anim.ResetTrigger("walk-right");
                    anim.ResetTrigger("walk-up");
                    anim.ResetTrigger("walk-left");
                    anim.SetTrigger("walk-down");
                }
            }
            //move right
            else if (currentInput == "D") {
                if (distance < 0.2f) {
                    activeTween = new Tween (Obj.transform, Obj.transform.position, new Vector3 (transform.position.x + 1.0f, transform.position.y, 0.0f), Time.time, 0.35f);
                    anim.ResetTrigger("walk-up");
                    anim.ResetTrigger("walk-left");
                    anim.ResetTrigger("walk-down");
                    anim.SetTrigger("walk-right");
                }
            }

            //for pacman death sprite, set trigger to play death animation
            // if ( (Obj.transform.position.x == -5.5f) && (Obj.transform.position.y == 0.5f) ) {
            //     anim.SetTrigger("death-state");
            // }
        }
    }//end addTween()

    // Checks the value of lastInput
    public void checkLastInput() {
        
        if (Input.GetKeyDown(KeyCode.W)) {
            lastInput = "W";
        }
        else if (Input.GetKeyDown(KeyCode.A)) {
            lastInput = "A";
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            lastInput = "S";
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            lastInput = "D";
        }
    }

    // Checks if pacman will hit a wall using raycasts
    public bool RaycastCheck() {
        
        // Casts a ray straight up, left, down or right
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 1.0f);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1.0f);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1.0f);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 1.0f);

        bool wasHit = false;
        
        switch (lastInput) {

            case "W":
                if (hitUp.collider != null) {
                    return true;
                }
                break;
            
            case "A":
                if (hitLeft.collider != null) {
                    return true;
                }
                break;
            
            case "S":
                
                if (hitDown.collider != null) {
                    return true;
                }
                break;

            case "D":
                if (hitRight.collider != null) {
                    return true;
                }
                break;
        }//end switch
        return wasHit;
    }//end RaycastCheck()

}//end class
