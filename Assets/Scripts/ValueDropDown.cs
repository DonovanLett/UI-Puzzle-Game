using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueDropDown : MonoBehaviour, IResetScript
{
    [SerializeField]
    private Button _dropDownButton;

    [SerializeField]
    private Image _dropDownImage;



    [SerializeField]
    private List<ValueSlot> allImages = new List<ValueSlot>();

    // Tracks images not yet enabled
    private List<ValueSlot> remainingImages = new List<ValueSlot>();

    // Start is called before the first frame update
    void Start()
    {
        RiseUp();
        ResetImages();
    }

    public void Reset()
    {
        RiseUp();
        ResetImages();
    }

    public void DropDown()
    {
        _dropDownButton.gameObject.SetActive(false);
        _dropDownImage.gameObject.SetActive(true);
    }

    public void RiseUp()
    {
        _dropDownImage.gameObject.SetActive(false);
        _dropDownButton.gameObject.SetActive(true);
    }

    public void EnableRandomImages(int amount)
    {
        // Prevent requesting more than remain
        amount = Mathf.Clamp(amount, 0, remainingImages.Count);

        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(0, remainingImages.Count);

            ValueSlot chosen = remainingImages[randomIndex];

            chosen.Show();

            // Remove from available pool
            remainingImages.RemoveAt(randomIndex);
        }
    }

    public void ResetImages()
    {
        remainingImages.Clear();

        foreach (ValueSlot image in allImages)
        {
            image.Hide();

            remainingImages.Add(image);
        }
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }
}