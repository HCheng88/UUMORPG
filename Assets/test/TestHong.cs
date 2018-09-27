using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHong : MonoBehaviour {

	
	void Start () {

#if DEBUG_MODEL
        Debug.Log("测试宏");
#endif
#if DEBUG_LOG
        Debug.Log("测试宏");
#endif
#if STAT_TD
        Debug.Log("测试宏");
#endif

    }


}
