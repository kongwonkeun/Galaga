using UnityEngine;
using System.IO;
using System.IO.Pipes;
using System.Collections;

public class Score : MonoBehaviour {

    public int score;

    //---- kong ----
    //public const string PIPE_SCORE = "bar";
    public const string PIPE_SCORE = "RDTLauncherPipe";
    public NamedPipeClientStream m_score_pipe;
    public StreamWriter m_writer;
    public string m_str;
    public bool m_x = false;

    void Start()
    {
        using (m_score_pipe = new NamedPipeClientStream(".", PIPE_SCORE, PipeDirection.Out))
        {
            m_score_pipe.Connect();
            m_writer = new StreamWriter(m_score_pipe);
            m_x = true;
        }
    }
    //----

    public void OnHitEnemy() {
        score += 10;
        m_str = "S" + score.ToString();
        if (m_x)
        {
            m_writer.Write(m_str);
            m_writer.Flush();
        }
    }

    void Update() {
        TextMesh mesh = GetComponent<TextMesh>();
        mesh.text = "" + score;
        //m_str = "S" + score.ToString();
        //m_writer.Write(m_str);
    }

}
