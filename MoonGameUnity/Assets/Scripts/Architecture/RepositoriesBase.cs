using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RepositoriesBase
{
    // —оздание списка всех интеракторов дл€ взаимодействи€ с ними
    private Dictionary<Type, Repository> repositoriesRoll;

    public RepositoriesBase()
    {
        this.repositoriesRoll = new Dictionary<Type, Repository>();
    }

    // «десь прописываютс€ все экзмепл€ры классов дл€ инициализации. ¬ызываетс€ первым
    public void CreateAllRepositories()
    {
        this.CreateRepository<InventoryRepository>();
    }

    // ќтвечает за последовательную генерацию каждого экземпл€ра класса
    public void CreateRepository<T>() where T : Repository, new()
    {
        var repository = new T();
        var type = typeof(T);
        this.repositoriesRoll[type] = repository;
    }

    //»нициализирует каждый из объ€вленных классов. ¬ызываетс€ вторым
    public void SendOnCreateToAllRepository()
    {
        var allRepository = this.repositoriesRoll.Values;
        foreach (var repository in allRepository)
        {
            repository.OnCreate();
        }
    }

    public void InitializeAllRepository()
    {
        var allRepository = this.repositoriesRoll.Values;
        foreach (var repository in allRepository)
        {
            repository.Initialize();
        }
    }

    public void SendOnStartAllRepository()
    {
        var allRepository = this.repositoriesRoll.Values;
        foreach (var repository in allRepository)
        {
            repository.OnStart();
        }
    }

    // возвращает ссылку на экземл€р интерактора, принима€ его тип
    public T GetRepository<T>() where T : Repository
    {
        var type = typeof(T);
        return (T)this.repositoriesRoll[type];
    }
}
