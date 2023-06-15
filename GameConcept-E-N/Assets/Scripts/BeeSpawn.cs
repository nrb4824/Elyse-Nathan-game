using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentrySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject Sentry;
    private GameObject s;
    private Quaternion rot1;

    [SerializeField]
    private Transform playerCam;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos1 = new Vector3(-356.3f, 65.5f, 249.3f);
        rot1.eulerAngles = new Vector3(37.2f, 165.4f, 4.5f);
        s = Instantiate(Sentry, pos1, rot1) as GameObject;
        Sentry sentry = s.GetComponent<Sentry>();
        sentry.xAngle = 37.2f;
        sentry.yAngle = 165.4f;
        sentry.zAngle = 4.5f;
        Target t = s.GetComponent<Target>();
        t.setCamera(this.playerCam);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
