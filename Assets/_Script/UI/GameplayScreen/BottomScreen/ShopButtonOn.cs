using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonOn : BaseButton
{

    protected override void OnClick()
    {
        base.OnClick();
        this.NotifyEventEnableShoppingMenu();
    }

    private void NotifyEventEnableShoppingMenu()
    {
        ObserverManager.Instance.NotifyEvent(EventType.EnableShoppingMenu, null);
    }

}
