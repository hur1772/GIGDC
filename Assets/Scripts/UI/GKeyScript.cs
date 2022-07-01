using UnityEngine;

public class GKeyScript : MonoBehaviour
{
    public GameObject player;

    Vector3 GKeyVec = Vector3.zero;

    private void Start() => StartFunc();

    private void StartFunc()
    {
         
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if(player.transform.localScale.x < 0.0f)
        {
            GKeyVec = new Vector3(0, 180.0f, 0);
            this.transform.eulerAngles = GKeyVec;
        }
        else
        {
            GKeyVec = new Vector3(0, 0.0f, 0);
            this.transform.eulerAngles = GKeyVec;
        }
    }
}