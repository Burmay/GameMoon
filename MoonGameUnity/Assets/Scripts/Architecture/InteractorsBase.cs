using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorsBase
{
    // �������� ������ ���� ������������ ��� �������������� � ����
    private Dictionary<Type, Interactor> interactorsRoll;

    public InteractorsBase()
    {
        this.interactorsRoll = new Dictionary<Type, Interactor>();
    }
    
    // ����� ������������� ��� ���������� ������� ��� �������������. ���������� ������
    public void CreateAllInteractors()
    {
        this.CreateInteractor<InventoryInteractor>();
    }

    // �������� �� ���������������� ��������� ������� ���������� ������
    public void CreateInteractor<T>() where T: Interactor, new()
    {
        var intetactor = new T();
        var type = typeof(T);
        this.interactorsRoll[type] = intetactor;
    }

    // ������ OnCreate � ��������� �������� �����
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

    // ���������� ������ �� �������� �����������, �������� ��� ���
    public T GetInteracror <T>() where T : Interactor
    {
        var type = typeof(T);
        Debug.Log(type);
        return (T)this.interactorsRoll[type];
    }
}
