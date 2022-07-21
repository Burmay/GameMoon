/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inventory //Фасад
{
    public static bool isInitialized { get; private set; } = false;

    public static event Action OnInventoryInitializedEvent;

    public static int bulb
    {
        get { CheckClass();
            return inventoryInteractor.bulb;
        }
    }

    private static InventoryInteractor inventoryInteractor;

    public static void Initialize(InventoryInteractor interactor)
    {
        inventoryInteractor = interactor;
        isInitialized = true;
        OnInventoryInitializedEvent.Invoke();
    }

    public static bool IsEnoughtBulbs(int value)
    {
        CheckClass();
        return inventoryInteractor.IsEnoughtBulbs(value);
    }

    public static void AddBulbs(object sender, int value)
    {
        CheckClass();
        inventoryInteractor.AddBulbs(sender, value);
    }

    public static void SpendBulbs(object sender, int value)
    {
        CheckClass();
        inventoryInteractor.SpendBulbs(sender, value);
    }

    private static void CheckClass()
    {
        if (!isInitialized)
        {
            throw new Exception("Inventary is not Init");
        }
    }
}
*/