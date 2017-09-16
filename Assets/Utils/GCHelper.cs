using UnityEngine;

public class GCHelper : MonoBehaviour
{
	void Start ()
    {
        var tmp = new System.Object[1024];

        // make allocations in smaller blocks to avoid them to be treated in a special way, which is designed for large blocks
        for (int i = 0; i < 1024; i++)
            tmp[i] = new byte[1024];

        // release reference
        tmp = null;
    }
}
