using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryRepository : Repository
{
    public int bulb { get; set; }
    private const string KEY = "INVENTORY_KEY";

    public override void Initialize()
    {
        this.bulb = PlayerPrefs.GetInt(KEY, defaultValue:0);
    }

    public override void Save()
    {
        PlayerPrefs.SetInt(KEY, this.bulb);
    }

    public override void OnCreate()
    {

    }

    public override void OnStart()
    {

    }
}
