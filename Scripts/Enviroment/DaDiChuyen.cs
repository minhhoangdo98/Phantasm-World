using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaDiChuyen : MonoBehaviour {

    //Script duoc su dung boi prefeb DaDiChuyen
    public GameObject DiemDiChuyen1, DiemDiChuyen2;
    public float tocDo = 3;
    Vector3 DiemDen;
	
	void Start () {
        transform.position = DiemDiChuyen1.transform.position;//bat dau vi tri da voi toa do cua diem di chuyen 1
        DiemDen = DiemDiChuyen2.transform.position;//diem den la toa do cua diem di chuyen 2
	}
	
	
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, DiemDen, tocDo * Time.deltaTime);//di tu diem di chuyen 1 den 2
        if(Vector2.Distance(transform.position,DiemDen)<= 0.5f)//Neu den noi
        {
            if (DiemDen == DiemDiChuyen1.transform.position)//Neu diem den la diem di chuyen 1
            {
                DiemDen = DiemDiChuyen2.transform.position;//thay doi diem den la diem di chuyen 2
            }
            else//nguoc lai neu diem den la diem di chuyen 2
            {
                DiemDen = DiemDiChuyen1.transform.position;//thay doi diem den la diem di chuyen 1
            }
        }
        //Nhu vay da se di chuyen qua lai giua 2 diem
    }
}
