using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ZomSight : MonoBehaviour
{
    ZomManager zomManager;
    Animator zomAnimator;
    SpriteRenderer zomRenderer;
    
    public bool isAlert;
    public Transform target;
    public float minDistance;
    public float maxDistance;




    private void Awake()
    {
        zomRenderer = GetComponentInParent<SpriteRenderer>();
        zomManager = GetComponentInParent<ZomManager>();
        zomAnimator = GetComponentInParent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isAlert = false;
        minDistance = 0.7f;
        maxDistance = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("mainCharacter") && Vector2.Distance(zomManager.transform.position, target.position) > minDistance)
        {
            isAlert = true;
            zomAnimator.SetBool("IsAlert", true);
            zomManager.walkSpeed = 3f;
            zomManager.transform.position = Vector2.MoveTowards(zomManager.transform.position, target.position, zomManager.walkSpeed * Time.fixedDeltaTime);

            if (zomManager.transform.localScale.x > target.position.x)
            {
                zomRenderer.flipX = true;
                
            }else if (transform.localScale.x < target.position.x)
            {
                zomRenderer.flipX = false;
            }

        }
        else if (Vector2.Distance(zomManager.transform.position, target.position) > maxDistance)
        {
            isAlert = false;
            zomAnimator.SetBool("IsAlert", false);
            zomManager.walkSpeed = 2f;


        }
    }
}
