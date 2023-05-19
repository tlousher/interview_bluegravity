using System.Collections;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shelf;
    public GameObject cart;
    public GameObject slotPrefab;

    private Canvas _canvas;

    public void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
    }

    public void Open()
    {
        _canvas.enabled = true;
        MovementController.CanMove.Add("Shop");
        OpenTab("BodyArmor");
    }

    public void Close()
    {
        _canvas.enabled = false;
        MovementController.CanMove.Remove("Shop");
    }
    
    /// <summary>
    /// Remove all the items from the shelf and load the new ones.
    /// </summary>
    /// <param name="items">The items to load.</param>
    private IEnumerator LoadItems(SimpleItem[] items)
    {
        foreach (Transform child in shelf.transform)
        {
            Destroy(child.gameObject);
        }
    
        // Wait for one frame
        yield return new WaitForEndOfFrame();
    
        foreach (var item in items)
        {
            var slot = Instantiate(slotPrefab, shelf.transform);
            // Moves the slot to the right position with an offset of 110 on x and 110 on y every 4 slots instantiated.
            slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                (slot.transform.GetSiblingIndex() % 4) * 110,
                -110 * (slot.transform.GetSiblingIndex() / 4)
            );
            slot.GetComponent<Slot>().item = item.itemData;
        }
    }

    /// <summary>
    /// Search all the items in the folder Resources/Items/folder and load them in the shelf.
    /// </summary>
    /// <param name="folder">The folder where the items are.</param>
    public void OpenTab(string folder)
    {
        var items = Resources.LoadAll<SimpleItem>($"Items/{folder}");
        StartCoroutine(LoadItems(items));
    }
}
