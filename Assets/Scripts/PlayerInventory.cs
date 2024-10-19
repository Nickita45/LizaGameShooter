using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;
using System;

public class PlayerInventory : MonoBehaviour
{
    // An array to store up to two items
    public string[] items = new string[2];


    public GameObject enterLabel;
    [SerializeField]
    private StringGameObjectPair[] itemsAvaliable;

    // Index to track the currently equipped item
    private int currentItemIndex = 0;
    private StarterAssetsInputs _input;
    public Camera playerCamera;
    public float collectionRange = 5f;

    private GameObject currentCollectible = null;

    void Start()
    {
        _input = transform.GetComponent<StarterAssetsInputs>();
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    void Update()
    {
        CheckForCollectible();
        // Switch items using mouse scroll from Starter Assets inputs
        if (_input.scrollInput.y != 0f)
        {
            SwitchItem(_input.scrollInput.y);
        }
        if (_input.interactive && currentCollectible != null)
        {
            CollectItem(currentCollectible);
        }

    }
    void CheckForCollectible()
    {
        // Cast a ray from the camera forward
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // If the ray hits an object within the range
        if (Physics.Raycast(ray, out hit, collectionRange))
        {
            // Check if the object has the "Collectible" tag
            if (hit.collider.CompareTag("Collectible"))
            {
                currentCollectible = hit.collider.gameObject;

                // Show the collect panel if we are looking at a collectible
                if (enterLabel != null)
                {
                    enterLabel.SetActive(true);
                    enterLabel.GetComponentInChildren<TextMeshProUGUI>().text = "[E] to collect " + hit.collider.gameObject.name;
                }
                return;
            }
        }

        // If no collectible is hit, hide the panel
        currentCollectible = null;
        if (currentCollectible == null)
        {
            enterLabel.SetActive(false);

        }
    }

    void CollectItem(GameObject item)
    {
        item.SetActive(false);
        if (items[0] != "" && items[1] != "")
        {
            DropItem();
            //DROP CURRENT ITEM
        }
        // Find the first empty slot in the inventory
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == "")
            {
                items[i] = item.gameObject.name;  // Store the item in the array
                SetActiveItemInHand(items[i]);
                Debug.Log("Collected: " + item.name);
                break;
            }
        }
    }
    void DropItem()
    {

        for (int i = 0; i < itemsAvaliable.Length; i++)
        {
            if (itemsAvaliable[i].key == items[currentItemIndex])
            {

                GameObject gmj = Instantiate(itemsAvaliable[i].value, _input.gameObject.transform.position,_input.gameObject.transform.rotation);
                gmj.tag = "Collectible";
                if(gmj.GetComponent<GunShoot>() != null)
                    gmj.GetComponent<GunShoot>().enabled=false;
                if(gmj.GetComponent<Collider>() == null)
                    gmj.AddComponent<BoxCollider>();
                gmj.AddComponent<BoxCollider>().enabled = true;
                gmj.name = itemsAvaliable[i].value.name;
                items[currentItemIndex] = "";
            }
        }
    }
    void SetActiveItemInHand(string str)
    {
        for (int i = 0; i < itemsAvaliable.Length; i++)
        {
            if (itemsAvaliable[i].key == str)
            {
                itemsAvaliable[i].value.SetActive(true);
            }
            else
            {
                itemsAvaliable[i].value.SetActive(false);
            }
        }
    }

    void SwitchItem(float scrollValue)
    {
        // Change the current item index based on scroll direction
        currentItemIndex += scrollValue > 0 ? 1 : -1;

        // Wrap around the item index if it exceeds bounds
        if (currentItemIndex >= items.Length) currentItemIndex = 0;
        if (currentItemIndex < 0) currentItemIndex = items.Length - 1;

        SetActiveItemInHand(items[currentItemIndex]);
        //EquipCurrentItem();
    }



}
[System.Serializable]
public class StringGameObjectPair
{
    public string key;
    public GameObject value;

    public StringGameObjectPair(string key, GameObject value)
    {
        this.key = key;
        this.value = value;
    }
}