using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiChuyen : MonoBehaviour
{

    //Script duoc dung boi object HoiThoai
    public Texture Mizu, Lily, Lisa, Luna, Main, Rhino, None, Heart;
    public GameObject hoiThoai, face, talk, ten, luaChon, luaChon1, luaChon2, luaChon3, luaChon4, baCham, boiRoi, bongDen, chamHoi, chamThan, moHoi, notNhac, sleep, traiTim, tucGian;

    void Start()
    {
        hoiThoai = GameObject.FindGameObjectWithTag("HoiThoai");
        face = GameObject.FindGameObjectWithTag("Face");
        talk = GameObject.FindGameObjectWithTag("Talk");
        ten = GameObject.FindGameObjectWithTag("Name");
        luaChon = GameObject.FindGameObjectWithTag("LuaChon");

    }

    #region LuaChon
    public void LuaChon1()
    {

    }

    public void LuaChon2()
    {

    }

    public void LuaChon3()
    {

    }

    public void LuaChon4()
    {

    }
    #endregion
}
