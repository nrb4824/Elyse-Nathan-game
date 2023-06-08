using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject Bee;
    private GameObject bee;
    [SerializeField]
    private int beeNumber;

    [SerializeField]
    private Transform playerCam;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < beeNumber; i++)
        {
            bee = Instantiate(Bee, this.transform.position, this.transform.rotation) as GameObject;
            Target t = bee.GetComponent<Target>();
            t.setCamera(this.playerCam);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
