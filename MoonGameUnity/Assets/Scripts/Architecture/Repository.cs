using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Repository
{
    public abstract void Initialize(); // ��������� ������ �� Db
    public abstract void Save(); // �������� ������ � Unity
    public abstract void OnCreate(); 
    public abstract void OnStart();
}
