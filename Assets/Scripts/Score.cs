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
    public bool m_x = true;

    void Start()
    {
        try {
            m_score_pipe = new NamedPipeClientStream(".", PIPE_SCORE, PipeDirection.Out);
            m_score_pipe.Connect();
            m_writer = new StreamWriter(m_score_pipe);
            m_x = true;
        }
        catch (IOException e) {
            m_x = false;
        }
    }
    //----

    public void OnHitEnemy() {
        score += 10;
        if (!m_x) return;
        m_str = "S" + score.ToString();
        m_writer.Write(m_str);
        m_writer.Flush();
    }

    void Update() {
        TextMesh mesh = GetComponent<TextMesh>();
        mesh.text = "" + score;
    }

}
