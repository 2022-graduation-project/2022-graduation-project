using System.Collections;
using UnityEngine;


public class extendsTest : MonoBehaviour
{
    private int test1 = 0;
    static protected int test2 = 1;
    static public int test3 = 2;

    public void test()
    {
        print("test1111");
    }

    public virtual void methodTest()
    {
        print("method TEst");
    }
}