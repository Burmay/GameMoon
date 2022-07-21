using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RepositoriesBase
{
    // �������� ������ ���� ������������ ��� �������������� � ����
    private Dictionary<Type, Repository> repositoriesRoll;

    public RepositoriesBase()
    {
        this.repositoriesRoll = new Dictionary<Type, Repository>();
    }

    // ����� ������������� ��� ���������� ������� ��� �������������. ���������� ������
    public void CreateAllRepositories()
    {
        this.CreateRepository<InventoryRepository>();
    }

    // �������� �� ���������������� ��������� ������� ���������� ������
    public void CreateRepository<T>() where T : Repository, new()
    {
        var repository = new T();
        var type = typeof(T);
        this.repositoriesRoll[type] = repository;
    }

    //�������������� ������ �� ����������� �������. ���������� ������
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

    // ���������� ������ �� �������� �����������, �������� ��� ���
    public T GetRepository<T>() where T : Repository
    {
        var type = typeof(T);
        return (T)this.repositoriesRoll[type];
    }
}
