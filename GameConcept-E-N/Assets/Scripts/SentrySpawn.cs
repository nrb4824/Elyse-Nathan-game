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

    private GameObject s3;
    private Quaternion rot3;

    private GameObject s4;
    private Quaternion rot4;

 /*   private GameObject s5;
    private Quaternion rot5;*/

    [SerializeField]
    private Transform playerCam;

    // Start is called before the first frame update
    void Start()
    {

        Vector3 pos1 = new Vector3(-338f, 79.8f, 245.1f);
        rot1.eulerAngles = new Vector3(41f, 268.8f, 0f);
        s1 = Instantiate(Sentry, pos1, rot1) as GameObject;
        Sentry sentry1 = s1.GetComponent<Sentry>();
        sentry1.xAngle = 41f;
        sentry1.yAngle = 268.8f;
        sentry1.zAngle = 0f;
        sentry1.scanAngle = 70;
        Target t1 = s1.GetComponent<Target>();
        t1.setCamera(this.playerCam);
        sentry1.setCamera(this.playerCam);

        Vector3 pos2 = new Vector3(-288f, 79.8f, 245f);
        rot2.eulerAngles = new Vector3(41f, 89.7f, 0f);
        s2 = Instantiate(Sentry, pos2, rot2) as GameObject;
        Sentry sentry2 = s2.GetComponent<Sentry>();
        sentry2.xAngle = 41f;
        sentry2.yAngle = 89.7f;
        sentry2.zAngle = 0f;
        sentry2.scanAngle = 70f;
        Target t2 = s2.GetComponent<Target>();
        t2.setCamera(this.playerCam);
        sentry2.setCamera(this.playerCam);

        Vector3 pos3 = new Vector3(-338f, 79.8f, 275f);
        rot3.eulerAngles = new Vector3(41f, 268.8f, 0f);
        s3 = Instantiate(Sentry, pos3, rot3) as GameObject;
        Sentry sentry3 = s3.GetComponent<Sentry>();
        sentry3.xAngle = 41f;
        sentry3.yAngle = 268.8f;
        sentry3.zAngle = 0f;
        sentry3.scanAngle = 70f;
        Target t3 = s3.GetComponent<Target>();
        t3.setCamera(this.playerCam);
        sentry3.setCamera(this.playerCam);

        Vector3 pos4 = new Vector3(-288f, 79.8f, 275f);
        rot4.eulerAngles = new Vector3(41f, 89.7f, 0f);
        s4 = Instantiate(Sentry, pos4, rot4) as GameObject;
        Sentry sentry4 = s4.GetComponent<Sentry>();
        sentry4.xAngle = 41f;
        sentry4.yAngle = 89.7f;
        sentry4.zAngle = 0f;
        sentry4.scanAngle = 70f;
        Target t4 = s4.GetComponent<Target>();
        t4.setCamera(this.playerCam);
        sentry4.setCamera(this.playerCam);


/*
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

        Vector3 pos3 = new Vector3(-314.3f, 84.84f, 205.7f);
        rot3.eulerAngles = new Vector3(84.6f, 49.1f, -123.4f);
        s3 = Instantiate(Sentry, pos3, rot3) as GameObject;
        Sentry sentry3 = s3.GetComponent<Sentry>();
        sentry3.xAngle = 84.6f;
        sentry3.yAngle = 49.1f;
        sentry3.zAngle = -123.4f;
        sentry3.scanAngle = 0.0f;
        sentry3.beeNumber = 1;
        Target t3 = s3.GetComponent<Target>();
        t3.setCamera(this.playerCam);
        sentry3.setCamera(this.playerCam);

        Vector3 pos4 = new Vector3(-397.5f, 66.9f, 300.8f);
        rot4.eulerAngles = new Vector3(36.6f, 133.8f, 4.6f);
        s4 = Instantiate(Sentry, pos4, rot4) as GameObject;
        Sentry sentry4 = s4.GetComponent<Sentry>();
        sentry4.xAngle = 36.6f;
        sentry4.yAngle = 133.8f;
        sentry4.zAngle = 4.6f;
        sentry4.scanAngle = 90.0f;
        Target t4 = s4.GetComponent<Target>();
        t4.setCamera(this.playerCam);
        sentry4.setCamera(this.playerCam);*/

/*        Vector3 pos5 = new Vector3(-227.9f, 66.9f, 300.8f);
        rot5.eulerAngles = new Vector3(36.6f, 239.3f, 4.6f);
        s5 = Instantiate(Sentry, pos5, rot5) as GameObject;
        Sentry sentry5 = s5.GetComponent<Sentry>();
        sentry5.xAngle = 36.6f;
        sentry5.yAngle = 239.3f;
        sentry5.zAngle = 4.6f;
        sentry5.scanAngle = 90.0f;
        Target t5 = s5.GetComponent<Target>();
        t5.setCamera(this.playerCam);
        sentry5.setCamera(this.playerCam);*/


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
