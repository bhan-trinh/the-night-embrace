using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class HealthScript : MonoBehaviour
{
    public LogicScript logic;
    //public PickupableItems itemToHit;
    public PickupableItems itemToUse;

    [SerializeField]
    public int currentHealth, maxHealth;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    [SerializeField]
    public bool isDead = false;

    private void Start()
    {
        logic = GameObject.Find("Logic Manager").GetComponent<LogicScript>();
        InitializeHealth(maxHealth);
    }

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }

    public AudioSource hurtSFX;

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;

        IEnumerator HurtAnim()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 163, 172, 255);
            yield return new WaitForSeconds(0.2f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }

        if (logic.itemOnHand == logic.lantern || sender.layer == 7)
        {
            currentHealth -= amount;
            StartCoroutine(HurtAnim());
            if (hurtSFX != null)
            {
                hurtSFX.Play();
            }

            if (gameObject.layer == 6)
            {
                Debug.Log("reset bar");
                logic.resetHealthBar();
            }
        }

        

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }

        else
        {
            
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            
            if (gameObject.layer == 6)
            {
                Debug.Log("player dedd");
                logic.GameOver();
            }

            if (gameObject.layer == 9)
            {
                logic.monsterKilled++;
            }

            StartCoroutine(DelayDestroy());
        }

    }


    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    public bool usedItemOn;
    public void GetUsedOn()
    {
        if (itemToUse != null)
        {
            if (logic.itemOnHand == itemToUse)
            {
                logic.usedItem();
                //logic.M1_receiveApple(gameObject); //Monster response
                usedItemOn = true;
            }
        }

    }

}
