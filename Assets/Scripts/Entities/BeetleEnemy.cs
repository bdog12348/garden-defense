using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleEnemy : MonoBehaviour
{
    NavigationScript navigation;

    // Start is called before the first frame update
    void Start()
    {
        navigation = GetComponent<NavigationScript>();
        navigation.GetGardenTarget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
