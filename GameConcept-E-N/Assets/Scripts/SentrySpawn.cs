using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentrySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject Sentry;
    private GameObject s1;
    private Quaternion rot1;

    private GameObject s2;
    private Quaternion rot2;

    [SerializeField]
    private Transform playerCam;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos1 = new Vector3(-356.4f, 65.5f, 249.3f);
        rot1.eulerAngles = new Vector3(36.6f, 165.5f, 4.6f);
        s1 = Instantiate(Sentry, pos1, rot1) as GameObject;
        Sentry sentry1 = s1.GetComponent<Sentry>();
        sentry1.xAngle = 36.6f;
        sentry1.yAngle = 165.5f;
        sentry1.zAngle = 4.6f;
        sentry1.scanAngle = 160.0f;
        Target t1 = s1.GetComponent<Target>();
        t1.setCamera(this.playerCam);
        sentry1.setCamera(this.playerCam);

        Vector3 pos2 = new Vector3(-269f, 65.5f, 249.4f);
        rot2.eulerAngles = new Vector3(36.6f, 174.9f, 4.6f);
        s2 = Instantiate(Sentry, pos2, rot2) as GameObject;
        Sentry sentry2 = s2.GetComponent<Sentry>();
        sentry2.xAngle = 36.6f;
        sentry2.yAngle = 174.9f;
        sentry2.zAngle = 4.6f;
        sentry2.scanAngle = 160.0f;
        Target t2 = s2.GetComponent<Target>();
        t2.setCamera(this.playerCam);
        sentry2.setCamera(this.playerCam);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
