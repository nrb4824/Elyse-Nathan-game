using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Sentry))]
public class FOVEditorSentry : Editor
{
    private void OnSceneGUI()
    {
        Sentry sentry = (Sentry)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(sentry.transform.position, Vector3.up, Vector3.forward, 360, SentryEnemySettings.AggroRadius);
        Handles.DrawWireArc(sentry.transform.position, Vector3.forward, Vector3.up, 360, SentryEnemySettings.AggroRadius);
        Handles.DrawWireArc(sentry.transform.position, Vector3.right, Vector3.forward, 360, SentryEnemySettings.AggroRadius);

        Vector3 viewAngle01 = DirectionFromAngle(sentry.transform.eulerAngles.y, -SentryEnemySettings.Angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(sentry.transform.eulerAngles.y, SentryEnemySettings.Angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(sentry.transform.position, sentry.transform.position + viewAngle01 * SentryEnemySettings.AggroRadius);
        Handles.DrawLine(sentry.transform.position, sentry.transform.position + viewAngle02 * SentryEnemySettings.AggroRadius);


       /* Vector3 viewAngle03 = DirectionFromAngleY(sentry.transform.eulerAngles.z, -SentryEnemySettings.Angle / 2);
        Vector3 viewAngle04 = DirectionFromAngleY(sentry.transform.eulerAngles.z, SentryEnemySettings.Angle / 2);

        Handles.DrawLine(sentry.transform.position, sentry.transform.position + viewAngle03 * SentryEnemySettings.AggroRadius);
        Handles.DrawLine(sentry.transform.position, sentry.transform.position + viewAngle04 * SentryEnemySettings.AggroRadius);*/
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private Vector3 DirectionFromAngleY(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(0, Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}