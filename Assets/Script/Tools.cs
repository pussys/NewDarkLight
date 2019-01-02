using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Tools
{
    public static void ResetTransformation(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }
    public static void SceneManagers(string str)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        GameCtrl.Instance.nextSceneName = str;
    }
}
