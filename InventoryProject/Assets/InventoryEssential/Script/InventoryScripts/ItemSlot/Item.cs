using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Asset/Inventoy Item")]
public class Item : ScriptableObject {

    [SerializeField]
    private string _name = "";
    [SerializeField]
    private Sprite _icon = null;
    [SerializeField]
    private string _textInfo = "item info";
    [SerializeField]
    private int _price = 1;
    [SerializeField]
    private int _maxStack = 5;

    public new string name => _name;
    public Sprite icon => _icon;
    public string textInfo => _textInfo;
    public int price => _price;
    public int maxStack => _maxStack;

    public string GetItemInfo()
    {
        return _name + "\n" 
            + _textInfo + "\n" 
            + "price : " + price.ToString();
    }

}
