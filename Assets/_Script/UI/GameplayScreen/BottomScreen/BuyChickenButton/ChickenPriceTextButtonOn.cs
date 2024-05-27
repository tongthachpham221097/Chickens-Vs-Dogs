using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenPriceTextButtonOn : BaseText
{

    [SerializeField] private int _chickenPrice = 1;
    
    public int ChickenPrice => _chickenPrice;
    
    protected void Start()
    {
        this.ShowChickenPriceText();
    }

    public void ShowChickenPriceText()
    {
        this.text.text = this._chickenPrice.ToString();
    }

}
