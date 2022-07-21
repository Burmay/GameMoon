using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorsBase
{
    // —оздание списка всех интеракторов дл€ взаимодействи€ с ними
    private Dictionary<Type, Interactor> interactorsRoll;

    public InteractorsBase()
    {
        this.interactorsRoll = new Dictionary<Type, Interactor>();
    }
    
    // «десь прописываютс€ все экзмепл€ры классов дл€ инициализации. ¬ызываетс€ первым
    public void CreateAllInteractors()
    {
        this.CreateInteractor<InventoryInteractor>();
    }

    // ќтвечает за последовательную генерацию каждого экземпл€ра класса
    public void CreateInteractor<T>() where T: Interactor, new()
    {
        var intetactor = new T();
        var type = typeof(T);
        this.interactorsRoll[type] = intetactor;
    }

    // запуск OnCreate с задержкой карутины после
    public void SendOnCreateToAllInteractors()
    {
        var allInteractors = this.interactorsRoll.Values;
        foreach(var interactor in allInteractors)
        {
            interactor.OnCreate();
        }
    }

    public void InitializeAllInteractors()
    {
        var allInteractors = this.interactorsRoll.Values;
        foreach (var interactor in allInteractors)
        {
            interactor.Initialize();
        }
    }

    public void SendOnStartToAllInteractors()
    {
        var allInteractors = this.interactorsRoll.Values;
        foreach (var interactor in allInteractors)
        {
            interactor.OnStart();
        }
    }

    // возвращает ссылку на экземл€р интерактора, принима€ его тип
    public T GetInteracror <T>() where T : Interactor
    {
        var type = typeof(T);
        Debug.Log(type);
        return (T)this.interactorsRoll[type];
    }
}
