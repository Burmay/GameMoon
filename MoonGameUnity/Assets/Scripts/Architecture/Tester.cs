using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public static InteractorsBase interactorsBase;
    public static RepositoriesBase repositoriesBase;

    // test
    InventoryInteractor inventoryInteractor;
    InventoryRepository inventoryRepository;

    

    void Start()
    {
        this.StartCoroutine(this.StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        interactorsBase = new InteractorsBase();
        repositoriesBase = new RepositoriesBase();

        interactorsBase.CreateAllInteractors();
        repositoriesBase.CreateAllRepositories();
        yield return null;

        interactorsBase.SendOnCreateToAllInteractors();
        repositoriesBase.SendOnCreateToAllRepository();
        yield return null;

        interactorsBase.InitializeAllInteractors();
        repositoriesBase.InitializeAllRepository();
        yield return null;

        interactorsBase.SendOnStartToAllInteractors();
        repositoriesBase.SendOnStartAllRepository();

        Debug.Log($"All Create, <OnCreate> Start, <Init> Start, <OnStart> Start");

        //test


        inventoryInteractor = interactorsBase.GetInteracror<InventoryInteractor>();
        inventoryRepository = repositoriesBase.GetRepository<InventoryRepository>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            inventoryInteractor.SpendBulbs(sender: this, value: 5);
            Debug.Log($"Bulbs spent (5) {inventoryInteractor.bulb}");
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            inventoryInteractor.AddBulbs(sender: this, value: 10);
            Debug.Log($"Bulbs added (10) {inventoryInteractor.bulb}");
        }
    }
}
