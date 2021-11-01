using Amlos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logo : MonoBehaviour
{
    public float time;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time < 1)
        {
            image.color = new Color(0, 0, 0, 1 - time / 1);
        }
        if (time > 2)
        {
            image.color = new Color(0, 0, 0, (time - 2) / 1);
        }
        if (time > 3)
        {
            SceneControl.GotoSceneImmediate(MainScene.mainHall);
        }
    }
}
