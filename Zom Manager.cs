using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class ZomManager : MonoBehaviour
{

    ZomSight zomSight;

    public Transform[] patrolPoints;
    int currentPointIndex;
    bool wander;

    public Animator zomAnimator;
    SpriteRenderer zomRenderer;
    public Transform target;

    public Image healthBarFill; 

    public float walkDistance = 15f;
    public float walkSpeed = 2f;
    public float walkInterval = 5f;

    public float maximumHealth=50f;
    public float currentHealth;

    public GameObject lootBag;


    void Start()
    {
        zomRenderer = GetComponent<SpriteRenderer>();
        zomSight = GetComponentInChildren<ZomSight>();
        zomAnimator = GetComponent<Animator>();      
        currentHealth = maximumHealth;
        healthBarFill.fillAmount = 1f; 

    }

    void Update()
    {
        if (transform.position != patrolPoints[currentPointIndex].position && zomSight.isAlert == false)
        {
            zomAnimator.SetBool("IsWalking", true);
            zomRenderer.flipX = true;
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, walkSpeed * Time.deltaTime);
        }
        else
        {
            if(wander==false && zomSight.isAlert == false)
            {
                zomRenderer.flipX = false;
                wander = true;
                zomAnimator.SetBool("IsWalking", false);
                StartCoroutine(wanderWait());
                

            }
            
        }

    }
    IEnumerator wanderWait()
    {
        yield return new WaitForSeconds(walkInterval);
        if(currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }
        wander = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; 
        
        float fillAmount = (float)currentHealth / maximumHealth;
        healthBarFill.fillAmount = fillAmount; 

        if (currentHealth <= 0)
        {
            StartCoroutine("Death");
            
        }
        

    }
    private void Die()
    {
        
        Destroy(gameObject);
    }
    IEnumerator Death()
    {
        
        zomAnimator.SetBool("DEAD", true);
        Instantiate(lootBag, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.91f);
        Die();
        
        
    }
}
