using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSPlayer : MonoBehaviour
{
    public MapPoint currentPoint;

    public float moveSpeed = 10f;

    private bool levelLoading;

    public LSManager theManager;

// new stuff
    public Rigidbody2D rigidBody;
    public float speed;

    // ensure player cannot go outside the map
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    public static LSPlayer singleton;

    public bool dialogOpen = false;

    // Start is called before the first frame update
    void Start()
    {

        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            if (singleton != this)
            {
                Destroy(gameObject);
            }
        }

        dialogOpen = false;
    }

    void Update()
    {


        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

       
        rigidBody.velocity = new Vector2(moveX, moveY) * speed;


        if (dialogOpen)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        if (currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked)
        {
            LSUIController.instance.ShowInfo(currentPoint);

            if (Input.GetButtonDown("Jump"))
            {
                levelLoading = true;

                theManager.LoadLevel();
            }
        }

        //transform.position = new Vector3(
        //Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
        //Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
        //0);

        //anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));

    }

    public void SetBoundaries(Vector3 bottomLeftLimit, Vector3 topRightLimit)
    {
        this.bottomLeftLimit = bottomLeftLimit + new Vector3(.5f, .5f, 0); // ensure that there's some padding...
        this.topRightLimit = topRightLimit + new Vector3(-.5f, -.5f, 0);

    }

    // Update is called once per frame
    void OldUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentPoint.transform.position) < .1f && !levelLoading)
        {

            if (Input.GetAxisRaw("Horizontal") > .5f)
            {
                if (currentPoint.right != null)
                {
                    SetNextPoint(currentPoint.right);
                }
            }

            if (Input.GetAxisRaw("Horizontal") < -.5f)
            {
                if (currentPoint.left != null)
                {
                    SetNextPoint(currentPoint.left);
                }
            }

            if (Input.GetAxisRaw("Vertical") > .5f)
            {
                if (currentPoint.up != null)
                {
                    SetNextPoint(currentPoint.up);
                }
            }

            if (Input.GetAxisRaw("Vertical") < -.5f)
            {
                if (currentPoint.down != null)
                {
                    SetNextPoint(currentPoint.down);
                }
            }

            if(currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked)
            {
                LSUIController.instance.ShowInfo(currentPoint);

                if(Input.GetButtonDown("Jump"))
                {
                    levelLoading = true;

                    theManager.LoadLevel();
                }
            }
        }


    }

    public void SetNextPoint(MapPoint nextPoint)
    {
        currentPoint = nextPoint;
        LSUIController.instance.HideInfo();

        AudioManager.instance.PlaySFX(5);
    }
}
