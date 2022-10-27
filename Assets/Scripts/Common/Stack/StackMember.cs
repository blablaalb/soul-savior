using UnityEngine;

public class StackMember : MonoBehaviour
{
    private Vector3 _stackPosition;
    public Vector3 StackPosition
    {
        get { return _stackPosition; }
        set
        {
            _stackPosition = value;
        }
    }

    public void ReturnToStack()
    {

    }


}