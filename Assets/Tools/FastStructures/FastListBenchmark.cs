using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Tools.FastStructures
{
    public class FastListBenchmark : MonoBehaviour
    {
        void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                var arrayTime = 0f;
                var listTime = 0f;
                var fastListTime = 0f;

                for (var j = 0; j < 100; j++)
                {
                    var timer = new Stopwatch();
                    timer.Start();
                    var array = new int[1000000];
                    for (var i = 0; i < 1000000; i++) array[i] = i;
                    for (var i = 0; i < 999999; i++) array[i] = array[i + 1];
                    timer.Stop();
                    arrayTime += timer.ElapsedMilliseconds;

                    timer.Reset();

                    timer.Start();
                    var list = new List<int>(1000000);
                    for (var i = 0; i < 1000000; i++) list.Add(i);
                    for (var i = 0; i < 999999; i++) list[i] = list[i + 1];
                    timer.Stop();
                    listTime += timer.ElapsedMilliseconds;

                    timer.Reset();

                    timer.Start();
                    var fastList = new FastArray<int>(1000000);
                    for (var i = 0; i < 1000000; i++) fastList[i] = i;
                    fastList.count = 1000000;
                    for (var i = 0; i < 999999; i++) fastList[i] = fastList[i + 1];
                    timer.Stop();
                    fastListTime += timer.ElapsedMilliseconds;

                    timer.Reset();
                }

                arrayTime /= 100f;
                listTime /= 100f;
                fastListTime /= 100f;

                Debug.Log($"T[] - {arrayTime}ms, List<T> - {listTime}ms, FastList<T> - {fastListTime}ms");
            }
        }
    }
}