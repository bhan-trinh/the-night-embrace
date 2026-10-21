using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TooltipScript : MonoBehaviour
{
    public LogicScript logic;
    public GameObject itemTipBox;
    public Text itemName;
    public Text itemDesc;

    private void Start()
    {
        logic = GameObject.Find("Logic Manager").GetComponent<LogicScript>();
    }

    private void  OnPointerEnter(PointerEventData eventData)
    {
        itemTipBox.SetActive(true);
        itemName.text = logic.inventory[logic.inventorySlots.IndexOf(gameObject)].itemName;
        itemDesc.text = logic.inventory[logic.inventorySlots.IndexOf(gameObject)].itemDesc;
    }

    private void OnPointerExit(PointerEventData eventData)
    {
        itemTipBox.SetActive(false);
    }
}
