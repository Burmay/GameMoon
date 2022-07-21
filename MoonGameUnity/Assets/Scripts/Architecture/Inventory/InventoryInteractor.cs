using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInteractor : Interactor
{
    private InventoryRepository inventoryRepository;

    //Связь интерактора и репозитория. Не в конструкторе для гарантии выполнения последовательности создание-вызов
    public override void OnCreate()
    {
        base.OnCreate();
        this.inventoryRepository = Tester.repositoriesBase.GetRepository<InventoryRepository>();
    }

    //public override void Initialize()
    //{
    //    Inventory.Initialize(this);
    //}

    public int bulb => this.inventoryRepository.bulb;
    
    public bool IsEnoughtBulbs(int value)
    {
        if(bulb - value >= 0) { return true; }
        else { return false; }
    }

    public void AddBulbs(object sender, int value)
    {
        this.inventoryRepository.bulb += value;
        this.inventoryRepository.Save();
    }

    public void SpendBulbs(object sender, int value)
    {
        if (IsEnoughtBulbs(value))
        {
            this.inventoryRepository.bulb -= value;
            this.inventoryRepository.Save();
        }
        else { Debug.Log("There is no light bulb in inventory!"); }
    }
}
