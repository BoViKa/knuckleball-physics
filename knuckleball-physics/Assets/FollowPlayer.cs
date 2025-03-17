using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform player;
    public int offset_x = 0;
    public int offset_y = 1;
    public int offset_z = -5;

    // Update is called once per frame
    void Update () {
        transform.position = player.transform.position + new Vector3(offset_x, offset_y, offset_z);
    }
}