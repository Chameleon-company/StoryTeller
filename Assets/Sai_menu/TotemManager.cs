using UnityEngine;
using UnityEngine.UI;

public class TotemManager : MonoBehaviour
{
    public TotemDatabase totemDB;

    public Image artworkSprite;

    private int selectedOption = 0; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateTotem(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++; 

        if(selectedOption >= totemDB.TotemCount)
        {
            selectedOption = 0; 
        }

        UpdateTotem(selectedOption);

    }

    public void BackOption()
    {
        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = totemDB.TotemCount - 1;
        }

        UpdateTotem(selectedOption);
    }

    private void UpdateTotem(int selectedOption)
    {
        Totem totem = totemDB.GetTotem(selectedOption);
        artworkSprite.sprite = totem.totemSprite;
        // nameText.text = totem.totemName;  
    }

}
