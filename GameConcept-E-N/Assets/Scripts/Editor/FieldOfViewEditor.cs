using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Bee))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        Bee bee = (Bee)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(bee.transform.position, Vector3.up, Vector3.forward, 360, BeeEnemySettings.AggroRadius);
        Handles.DrawWireArc(bee.transform.position, Vector3.forward, Vector3.up, 360, BeeEnemySettings.AggroRadius);

        Vector3 viewAngle01 = DirectionFromAngle(bee.transform.eulerAngles.y, -BeeEnemySettings.Angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(bee.transform.eulerAngles.y, BeeEnemySettings.Angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(bee.transform.position, bee.transform.position + viewAngle01 * BeeEnemySettings.AggroRadius);
        Handles.DrawLine(bee.transform.position, bee.transform.position + viewAngle02 * BeeEnemySettings.AggroRadius);


        Vector3 viewAngle03 = DirectionFromAngleY(bee.transform.eulerAngles.y, -BeeEnemySettings.Angle / 2);
        Vector3 viewAngle04 = DirectionFromAngleY(bee.transform.eulerAngles.y, BeeEnemySettings.Angle / 2);

        Handles.DrawLine(bee.transform.position, bee.transform.position + viewAngle03 * BeeEnemySettings.AggroRadius);
        Handles.DrawLine(bee.transform.position, bee.transform.position + viewAngle04 * BeeEnemySettings.AggroRadius);
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