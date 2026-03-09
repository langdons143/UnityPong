using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NetworkedObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public abstract void Initialize();

   
    public abstract void GetNetworkId();

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
