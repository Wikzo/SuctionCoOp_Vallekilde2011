using UnityEngine;
using System.Collections;

public class GizmoDraw : MonoBehaviour
{
	void OnDrawGizmos()
	{
        Gizmos.DrawIcon(transform.position, "Killzone_Gizmo.tiff");
    }
}
