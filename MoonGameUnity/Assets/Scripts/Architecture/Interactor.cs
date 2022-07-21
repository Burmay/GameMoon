using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactor
{
    public virtual void OnCreate(){ } // ќтвечает за локальную св€зь экземпл€ров между собой

    public virtual void Initialize() { }

    public virtual void OnStart() { }
}
