using System.Collections;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;

    public void Cube1()
    {
        print("Test Btn 1111");
        cube1.GetComponent<Parent>().Print(); // my name is cube 1
    }
    public void Cube2()
    {
        print("Test Btn 2222");
        cube2.GetComponent<Parent>().Print();
    }
    public void Cube3()
    {
        print("Test Btn 3333, 스크립트 붙이기 테스트");
        cube3.AddComponent<Child3>();
        Destroy(cube3.gameObject);
        myPrint(cube3.GetComponent<Child3>());
    }

    void myPrint(Parent script)
    {
        script.Print();
    }
}