using UnityEngine;

[CreateAssetMenu(fileName = "New Rarity", menuName = "Items/Rarity")]
public class Rarity : ScriptableObject
{
    //Name of the rarity
    [SerializeField] private new string name = "New Rarity Name";
    //Text color of the rarity
    [SerializeField] private Color textColour = new Color(1f, 1f, 1f, 1f);

    public string Name => name;             //Public reference to the name
    public Color TextColour => textColour;  //Public reference to the TextColour
}
