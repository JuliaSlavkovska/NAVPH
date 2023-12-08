using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPickUps : MonoBehaviour
{
    [SerializeField] private List<PickUpField> PickUps = new List<PickUpField>();
    // Start is called before the first frame update
    
    private int pickup_index;
    private int old_pickup_index;
    void Awake()
    {
        old_pickup_index=pickup_index = Random.Range(0, PickUps.Count-1);
        PickUps[pickup_index].gameObject.SetActive(true);
        
    }
    
    public void NextPickup()
    {
        PickUps[pickup_index].gameObject.SetActive(false);
        pickup_index = Random.Range(0, PickUps.Count-1);
        while (old_pickup_index==pickup_index)
        {
            pickup_index = Random.Range(0, PickUps.Count-1);
        }
        
        
        PickUps[pickup_index].gameObject.SetActive(true);
    }

    public PickUpField GetActuallPickUpField()
    {
        return PickUps[pickup_index]; 
    }
}
