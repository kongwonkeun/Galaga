using System;
using System.IO.Ports;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public int m_direction = 0;
    public int m_speed = 0;
    public bool m_run = true;
    public SerialPort m_stream;

    long m_lastReadTime = 0;
    long m_duration = 0;

    void Start()
    {
        String[] args = Environment.GetCommandLineArgs();
        m_stream = new SerialPort(args[1], 115200, Parity.None, 8, StopBits.One);
        // m_stream = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);
        m_run = true;
        try {
            m_stream.Open();
            m_stream.ReadTimeout = 100;
        } catch (Exception e) {
            return;
        }
        new Thread(ReadThread).Start();
    }

    void Update()
    {
        //
    }

    private void OnApplicationQuit() {
        m_run = false;
        m_stream.Close();
    }

    long GetTickTime() {
        return (DateTime.Now.Ticks) / 10000; // 1ms
    }

    void ReadThread() {
        while (m_run) {
            try {
                int b = m_stream.ReadByte();
                ReadStateMachine(b);
                // Debug.Log("" + b);
            } catch (Exception e) {
                //
            }
        }
    }

    int m_s;
    int m_v;
    int m_d;
    int m_d_last;

    void ReadStateMachine(int b) {
        if (b == 86) { m_s = 1; m_v = 0; }
        else if (b == 68) { m_s = 7; m_d = 0; }
        else {
            switch (m_s) {
                case 1: m_s = 2;  m_v = (b - 48); break;
                case 2: m_s = 3;  m_v = (m_v * 10) + (b - 48); break;
                case 3: m_s = 4;  m_v = (m_v * 10) + (b - 48); break;
                case 4: m_s = 5;  m_v = (m_v * 10) + (b - 48); break;
                case 5: m_s = 6;  m_v = (m_v * 10) + (b - 48); break;
                case 7: m_s = 8;  m_d = (b - 48); break;
                case 8: m_s = 9;  m_d = (m_d * 10) + (b - 48); break;
                case 9: m_s = 10; m_d = (m_d * 10) + (b - 48); break;
                default: break;
            }
        }
        if (m_s ==  6) {
            // Debug.Log("" + m_v);
            m_speed = m_v;
            return;
        }
        if (m_s == 10) {
            // Debug.Log("S=" + m_d);
            if (m_d == m_d_last) {
                if (m_d > 17) { m_direction = -1; }
                else if (m_d < 15) { m_direction = 1; }
                else { m_direction = 0; }
                return;
            }
            m_d_last = m_d;
        }
    }

}
