using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player; // nhân vật
    public float start, end;  // Điểm bắt đầu và điểm kết thúc của màn chơi
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Vị trí của người chơi (nhân vật)
        var playerX = player.transform.position.x;

        // Lấy vị trí của camera
        var camX = transform.position.x;
        var camY = transform.position.y;
        var camZ = transform.position.z;

        if (playerX > start && playerX < end)
        {
            camX = playerX;
        }   
        else
        {
            if (playerX < start)
            {
                camX = start;
            }

            //if (playerX < start) { camX = playerX; }
            if (playerX > end)
            {
                camX = end;
            }
        }
        // Set lại vị trí cho camera
        transform.position = new Vector3 (camX, camY, camZ);
    }
}
