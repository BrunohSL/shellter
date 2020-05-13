using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public PlayerController player;

    public bool isFollowing;

    public float xOffset;
    public float yOffset;
    public float cameraXMin;
    public float cameraXMax;
    public float cameraYMin;
    public float cameraYMax;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();

        isFollowing = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (isFollowing) { //então, aqui ele atualiza a posição da camera a cada quadro e limita o movimento com a função Clamp
            transform.position = new Vector3(Mathf.Clamp(player.transform.position.x + xOffset, cameraXMin, cameraXMax),  // no caso do X, por exemplo, ele poe o valor do X no espaço e depois diz
                                             Mathf.Clamp(player.transform.position.y + yOffset, cameraYMin, cameraYMax),  // quais são os limites com Min e Max (o mesmo pro Y e pro Z não precisa luls)
                                             transform.position.z);
        }
	}
}
