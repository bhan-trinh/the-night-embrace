using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LogicScript : MonoBehaviour
{
    public Canvas inventoryUI;
    public PickupableItems lantern;
    public GameObject wall;
    public GameObject player;
    public SceneFaderScript SFScript;
    public GameObject portal;
    public GameObject mainCamera;
    public AudioSource mainTheme;


    //Handle items in Inventory
    public List<PickupableItems> invBeforeMap;
    public List<PickupableItems> inventory;
    public AudioSource pickUpSFX;

    public int monsterKilledB4;

    private void Start()
    {
        //inventory.Add(lantern);
        //itemOnHand = lantern;
        //resetInventory();
    
        //DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad (inventoryUI);
        
        if (GameObject.Find("Tilemap_Wall") != null)
        {
            wall = GameObject.Find("Tilemap_Wall");
            wall.SetActive(false);
        }
        
    
        if (GameObject.Find("Portal2") != null)
        {
            portal = GameObject.Find("Portal2");
            portal.SetActive(false);
        }
        
    
        player = GameObject.Find("Player");
    
    }

    private void OnLevelWasLoaded()
    {
        if (SceneManager.GetActiveScene().name == "Title Screen")
        {
            Debug.Log("Hi");
            Destroy(GameObject.Find("SF"));
            Destroy(GameObject.Find("Main Canvas"));
            Destroy(mainCamera);
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad (inventoryUI);
        DontDestroyOnLoad(SFScript.gameObject.transform.parent.gameObject);
        DontDestroyOnLoad(mainCamera);
        DontDestroyOnLoad(GameObject.Find("EventSystem"));

        if (GameObject.Find("Tilemap_Wall") != null)
        {
            wall = GameObject.Find("Tilemap_Wall");
            wall.SetActive(false);
        }

        if (GameObject.Find("Portal2") != null)
        {
            portal = GameObject.Find("Portal2");
            portal.SetActive(false);
        }
   
        player = GameObject.Find("Player");
        resetHealthBar();
        Debug.Log("reset");

        if (SceneManager.GetActiveScene().name == "Layer 1" || SceneManager.GetActiveScene().name == "Outer Path" || SceneManager.GetActiveScene().name == "Map Phake")
        {
            inventory.Clear();
            inventory.Add(lantern);
        }
        itemOnHand = lantern;
        //Add UI change here
        resetInventory();


    }

    public void pickUpItem(PickupableItems itemOnFloor)
    {
        if (inventory.Count < 5)
        {
            pickUpSFX.Play();
            inventory.Add(itemOnFloor);
            resetInventory();
        }
    }


    //Handle changing player's holding item
    public List<KeyCode> itemKeys;
    public PickupableItems itemOnHand;
    public Text InventoryIndicator;
    public void changeItem()
    {
        
        foreach (KeyCode key in itemKeys)
        {
            
            if (Input.GetKeyDown(key))
            {
                itemOnHand = inventory[itemKeys.IndexOf(key)];
                InventoryIndicator.text = itemOnHand.name;

                Debug.Log(itemKeys.IndexOf(key));
                Debug.Log(itemOnHand);
                
            }
        }
    }

    public void usedItem()
    {
        inventory.Remove(itemOnHand);
        Debug.Log("Used");
        resetInventory();
        itemOnHand = inventory[0];
        InventoryIndicator.text = itemOnHand.name;
    }

    public List<GameObject> inventorySlots;
    public void resetInventory()
    {
        foreach (GameObject slot in inventorySlots)
        {
            slot.GetComponent<Image>().sprite = null;
        }
        for (int i=0; i<inventory.Count;i++)
        {
            
            inventorySlots[i].GetComponent<Image>().sprite = inventory[i].inventorySprite;
        }
    }

    public int attackPower=1;
    public int currentHealth, maxHealth;
    public void statIncrease()
    {
        attackPower += 1;
        if (player.GetComponent<HealthScript>().currentHealth < player.GetComponent<HealthScript>().maxHealth)
        {
            player.GetComponent<HealthScript>().currentHealth += 1;
        }
    }

    public void activatePortal()
    {
        portal.SetActive(true);
    }    

    public void deactivateWall()
    {
        wall.SetActive(false);
    }

    public void activateWall()
    {
        Debug.Log("Wall locked");
        
        wall.SetActive(true);
    }

    public int health;
    public List<GameObject> heartpoint;
    public void resetHealthBar()
    {
        if (player != null)
        {

            health = player.GetComponent<HealthScript>().currentHealth;
            Debug.Log(health);

            foreach (GameObject heart in heartpoint)
            {
                heart.SetActive(false);
            }

            int healthlmao;
            healthlmao = health - 1;
            for (int i = 0; i < health; i++)
            {
                Debug.Log("set");
                heartpoint[i].SetActive(true);
            }
        }
    }


    public void teleportPortal(string MapName)
    {
        monsterKilledB4 = 0;
        monsterKilledB4 += monsterKilled;
        Debug.Log("changed inventoryB4 map to inventory");
        invBeforeMap.Clear();
        foreach (PickupableItems i in inventory)
        {
            invBeforeMap.Add(i);
        }
        StartCoroutine(delayFade(MapName));
       
    }    

    private IEnumerator delayFade (string MapName)
    {
        SFScript.transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 10;
        StartCoroutine(SFScript.Fade(SceneFaderScript.FadeDirection.In));
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(MapName);
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(SFScript.Fade(SceneFaderScript.FadeDirection.Out));
        SFScript.transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = -10;
    }

    public int monsterKilled;
    public GameObject happyEndDestination;
    public List<GameObject> EndingText;
    public AudioSource HEbgm;
    public MusicFadeScript MFScript;
    public IEnumerator Ending()
    {
        inventoryUI.GetComponent<FadeCanvasScript>().fadeUIout = true;
        Destroy(GameObject.Find("Reset"));
        player.GetComponent<PlayerMovementScript>().enabled = false;
        GameObject.Find("OnHand").GetComponent<PlayerHoldingScript>().enabled = false;
        player.GetComponent<Animator>().SetBool("Moving", true);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.up*3;
        yield return new WaitForSeconds(2);

        StartCoroutine(SFScript.Fade(SceneFaderScript.FadeDirection.In));
        yield return new WaitForSeconds(3);

        



        if (monsterKilled == 0)
        {
            mainTheme.Stop();
            HEbgm.Play();
            for (int i = 0; i < GameObject.Find("HappyEndText").transform.childCount; i++)
            {
                EndingText.Add(GameObject.Find("HappyEndText").transform.GetChild(i).gameObject);
            }
            happyEndDestination = GameObject.Find("HEDestination");
            player.transform.position = happyEndDestination.transform.position;
            StartCoroutine(SFScript.Fade(SceneFaderScript.FadeDirection.Out));
            yield return new WaitForSeconds(3.5f);
            player.GetComponent<Animator>().SetBool("Moving", false);
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            yield return new WaitForSeconds(2);


            StartCoroutine(SFScript.Fade(SceneFaderScript.FadeDirection.In));
            yield return new WaitForSeconds(2);

            foreach (GameObject HEtext in EndingText)
            {
                StartCoroutine(HEtext.GetComponent<TextTriggerScript>().FadeTextToFullAlpha(0.2f, HEtext.GetComponent<Text>()));
                yield return new WaitForSeconds(3);
                StartCoroutine(HEtext.GetComponent<TextTriggerScript>().FadeTextToZeroAlpha(0.2f, HEtext.GetComponent<Text>()));
                yield return new WaitForSeconds(1);
            }

            StartCoroutine(MFScript.FadeOut(HEbgm, 0.5f));

        }

        else //BE
        {
            for (int i = 0; i < GameObject.Find("BadEndText").transform.childCount; i++)
            {
                EndingText.Add(GameObject.Find("BadEndText").transform.GetChild(i).gameObject);
            }

            foreach (GameObject BEtext in EndingText)
            {
                StartCoroutine(BEtext.GetComponent<TextTriggerScript>().FadeTextToFullAlpha(0.2f, BEtext.GetComponent<Text>()));
                yield return new WaitForSeconds(3);
                StartCoroutine(BEtext.GetComponent<TextTriggerScript>().FadeTextToZeroAlpha(0.2f, BEtext.GetComponent<Text>()));
                yield return new WaitForSeconds(1);
            }
        }

        yield return new WaitForSeconds(1);
        StartCoroutine(SFScript.Fade(SceneFaderScript.FadeDirection.Out));
        SceneManager.LoadScene("Title Screen");

        yield return new WaitForSeconds(0.5f);

    }

    public GameObject GameOverScreen;
    public void GameOver()
    {
        SFScript.transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 10;
        StartCoroutine(SFScript.Fade(SceneFaderScript.FadeDirection.In));
        inventoryUI.GetComponent<FadeCanvasScript>().fadeUIout = true;

      
        GameOverScreen.SetActive(true);


    }

    public void resetLevel()
    {
        monsterKilled = 0;
        monsterKilled += monsterKilledB4;
        inventory.Clear();
        foreach (PickupableItems i in invBeforeMap)
        {
            inventory.Add(i);
        }
        Debug.Log("change inventory to inventory before map");
        StartCoroutine(SFScript.Fade(SceneFaderScript.FadeDirection.Out));
        GameOverScreen.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Outer Path")
        {
            SceneManager.LoadScene("Map Phake");
        }

        else 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        SFScript.transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = -10;
        inventoryUI.GetComponent<FadeCanvasScript>().fadeUIin = true;

    }
}
