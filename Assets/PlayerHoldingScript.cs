using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerHoldingScript : MonoBehaviour
{
    public LogicScript logic;
    public SpriteRenderer onHandSprite;
    public OnHandParentScript parent;
    public Animator animator;

    public PickupableItems lantern;
    public PickupableItems apple;

    public float delay = 0.3f;
    private bool attackBlocked;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.Find("Logic Manager").GetComponent<LogicScript>();
        animator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.anyKey);
        logic.changeItem();
        onHandSprite.sprite = logic.itemOnHand.itemSprite;
        if (logic.itemOnHand != null)
        {
            if (logic.itemOnHand == lantern)
            {
                animator.enabled = true;
            }

            else
            {
                animator.enabled = false;
            }
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
         
            Attack();
        }

        else if (Input.GetKeyDown(KeyCode.X))
        {
            Eat();
        }


    }

    public AudioSource hitSFX;
     public void Attack()
    {
        
        if (attackBlocked)
            return;

        attackBlocked = true;
        StartCoroutine(DelayAttack());


        if (logic.itemOnHand == lantern) //lantern to attack
        {
            parent.detectCollidersAttack();
            animator.SetTrigger("Attack");
            hitSFX.Play();
        }

        else //other items to use
        {
            parent.detectCollidersUse();
        }
     }    

    public void Eat()
    {
        if (logic.itemOnHand == apple)
        {
            logic.statIncrease();
            logic.usedItem();
            logic.resetHealthBar();
        }
    }    


    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }



}
