using System.Collections;
using UnityEngine;

public class childTest : extendsTest
{
    void Start()
    {
        // print(test1);

        if (gameObject.name == "Test1")
        {
            print("Test1 before: " + test2);
            test2 = 10;
            print("Test1 after: " + test2);
        }
            
        else if (gameObject.name == "Test2")
        {
            print("Test2 before: " + test2);
            test2 = 30;
            print("Test2 after: " + test2);
        }
            

        test();
    }

    public override void methodTest()
    {
        print("상솟ㄱ");
    }
}