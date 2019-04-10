using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPoint : MonoBehaviour
{
    public GameObject noiChuyen, gameController;
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        noiChuyen = GameObject.FindGameObjectWithTag("HoiThoai");
    }

    
    void Update()
    {
        
    }
}
