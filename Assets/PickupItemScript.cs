using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemScript : MonoBehaviour
{
    public PickupableItems thisItem;
    public LogicScript logic;

    public SpriteRenderer thisSprite;

    // Start is called before the first frame update
    public void Start()
    {
        thisSprite.sprite = thisItem.inventorySprite;
        logic = GameObject.Find("Logic Manager").GetComponent<LogicScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            logic.pickUpItem(thisItem);
            Destroy(gameObject);
        }
    }
}
