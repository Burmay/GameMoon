using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactor
{
    public virtual void OnCreate(){ } // �������� �� ��������� ����� ����������� ����� �����

    public virtual void Initialize() { }

    public virtual void OnStart() { }
}
