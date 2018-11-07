using UnityEngine;
using System.Collections;

public interface IBinaryStateListener
{
    void OnStateChange(object caller, bool active);
}
