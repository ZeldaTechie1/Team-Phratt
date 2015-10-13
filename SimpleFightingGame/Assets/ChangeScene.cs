using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour
{


    // Update is called once per frame
    public void ChangeToScene(string SceneChange)
    {
        Application.LoadLevel(SceneChange);
    }
}
