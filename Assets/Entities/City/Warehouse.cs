using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Warehouse : MonoBehaviour
{
    public UnityEvent suppliesEmpty;
    [SerializeField] private int _supplies = 100;
    [SerializeField] private int _maxSupplies = 100;
    private WarehouseUI ui;

    void Start()
    {
        ui = GetComponent<WarehouseUI>();
        ui.SetSupplies(_supplies);
    }

    public int supplies
    {
        get { return _supplies; }
        private set { _supplies = value; }
    }
    [SerializeField] private int _supplyConsumption = 10;

    /** Consume the standard amount from the class' config- useful for consuming overtime on a game tick. */
    public void ConsumeSupplies() { ConsumeSupplies(_supplyConsumption); }
    
    /** Consume a specific amount - useful for triggering on a specific event */
    public void ConsumeSupplies(int amount) {
        _supplies = System.Math.Clamp(_supplies -amount, 0, _maxSupplies);
        if (_supplies <= 0) suppliesEmpty.Invoke();
        ui.SetSupplies(_supplies);
    }

    public void DeliverSupplies(int amount)
    {
        _supplies = System.Math.Clamp(_supplies + amount, 0, _maxSupplies);
        ui.SetSupplies(_supplies);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Robot"))
        {
            Debug.Log("Robot delivered supplies");
            DeliverSupplies(80);
            Destroy(other.gameObject);
        }
    }
}
