using config;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlotCore : MonoBehaviour
{
    private readonly ConfigXML configxml = new ConfigXML();
    public AudioClip ClickAudio;
    public AudioSource audioSource;

    public struct Struct_PlotMessage
    {
        public string txt;
        public string character;
        public string skin_back;
        public string skin_character;
        public bool event_white;
        public bool next;//�Ƿ�����һ��
        public List<string> choice;
    }

    /// <summary>
    /// ר�����������һ��֧������
    /// </summary>
    public struct TemporaryBranchInfo
    {
        public bool IsTemporaryBranch;
        public int Branch_2_From;
        public int Branch_2_End;
    }
    public struct PlotHistory
    {
        public string _Character;
        public string _Text;
        public void SetNewPlot (string _Character, string _Text)
        {
            this._Character = _Character;
            this._Text = _Text;
        }
    }
    public static List<PlotHistory> _PlotHistory = new List<PlotHistory>();

    public TemporaryBranchInfo _TemporaryBranchInfo = new TemporaryBranchInfo();
    public List<Struct_PlotMessage> PlotMessage = new List<Struct_PlotMessage>();
    public int CoreIndex = 0;

    public Image F_Skin_Back;
    public Image F_Skin_Character;
    public Text F_text;
    public bool Core_statenext;
    public GameObject F_Choice1;
    public GameObject F_Choice2;
    public GameObject F_dialog_white;

    public GameObject F_Left_dialog_name;
    public GameObject F_right_dialog_name;
    public AudioSource F_AudioSource;

    /// <summary>
    /// ÿ��һ�ж�15Ԫ�ص㡣
    /// 130��ʱ����ʾ11����
    /// </summary>
    private const int Value_Default_width_choie = 130;

    public void Awake ()
    {
        InitializePlot();
    }

    // ����Э�̺���
    public void InitializePlot ()
    {
        try
        {
            // ��ʼ������
            if (File.Exists(Application.persistentDataPath + "/config_PlotProgress.xml"))
            {
                if (configxml.��������("plot" + ListPlotButton.Plotindex.ToString(), "config_PlotProgress.xml") != "None")
                {
                    int.TryParse(configxml.��������("plot" + ListPlotButton.Plotindex.ToString(), "config_PlotProgress.xml"), out CoreIndex);
                }
            }

            Core_statenext = true; // ����״̬��־
            int count = 0; // ������
            XmlDocument xmlDoc = new XmlDocument(); // ���� XML �ĵ�����
            var _ = Resources.Load<TextAsset>($"TextAsset/Plot/MainPlotTXT/plotall{ListPlotButton.Plotindex}");
            string _temp = _.text; // �� StreamingAssets �ļ����ж�ȡ XML �ļ�
            Debug.Log(_temp);
            xmlDoc.LoadXml(_temp); // ���� XML ����
            XmlNodeList node = xmlDoc.SelectSingleNode("root").ChildNodes; // ��ȡ root �ڵ�������ӽڵ�
            foreach (XmlElement x1 in node) // �����ӽڵ�
            {
                foreach (XmlElement data1 in x1.ChildNodes) // �����ӽڵ���ӽڵ�
                {
                    //<t1>Q|���ϣ��㿴���������ӵ��·�����һ����ǰ���ġ��ر�ͼ��Ŷ��|background/kitchen_10am|illustration/014 #157711|0|12</t1>
                    string[] messageArray = data1.InnerText.Split('|'); // �ָ�����
                    List<string> _temp_message = new List<string>(messageArray);

                    // �����ṹ����󲢸�ֵ
                    var _Struct_PlotMessage = new Struct_PlotMessage();
                    _Struct_PlotMessage.character = _temp_message[0];
                    _Struct_PlotMessage.txt = _temp_message[1];
                    _Struct_PlotMessage.skin_back = _temp_message[2];
                    _Struct_PlotMessage.skin_character = _temp_message[3];

#pragma warning disable CS0164
                    choiceskin:
#pragma warning restore CS0164
                    {
                        // ����ѡ���֧����
                        _Struct_PlotMessage.choice = new List<string>();//��ʼ��

                        List<string> __temp_message = new List<string>(_temp_message[4].Split('-'));

                        foreach (string s in __temp_message)
                        {
                            _Struct_PlotMessage.choice.Add(s);
                        }
                    }

                    if (_temp_message[4] == "3") { _Struct_PlotMessage.event_white = true; }


                    _Struct_PlotMessage.next = true;
                    _Struct_PlotMessage.next = true;

                    PlotMessage.Add(_Struct_PlotMessage); // ���ṹ�������ӵ��б���
                    count++;
                }
            }

        }
        catch (Exception ex)
        {
            new PopNewMessage(ex.Message);
        }
        var lastPlotMessage = PlotMessage[PlotMessage.Count - 1];
        lastPlotMessage.event_white = true;
        lastPlotMessage.next = false;
        PlotMessage[PlotMessage.Count - 1] = lastPlotMessage;
        Debug.Log($"����:{PlotMessage.Count}");
        NextPlotScene(); // ������һ������
    }
    /*
    public void Update ()
    {
        Debug.Log(CoreIndex);
    }
    */
    public void NextPlotScene ()
    {
        audioSource.PlayOneShot(ClickAudio);

        Debug.Log($"��ǰ��ţ�{CoreIndex}");
        if (!Core_statenext) return;
        if (PlotMessage[CoreIndex].next)
        {

            if (_TemporaryBranchInfo.IsTemporaryBranch)//���ڵ�һ��֧��Ҫ�����ڶ���֧��ȫ���ı�
            {
                Debug.Log(CoreIndex + " " + _TemporaryBranchInfo.Branch_2_From);
                if (CoreIndex + 1 >= _TemporaryBranchInfo.Branch_2_From)
                {
                    Debug.Log("��һѡ����ı�����");
                    _TemporaryBranchInfo.IsTemporaryBranch = false;
                    GotoPlotIndex(_TemporaryBranchInfo.Branch_2_End + 1, true);
                    return;
                }
            }

            if (PlotMessage[CoreIndex].event_white)
            {
                ShowWhiteWait();
                Debug.Log("�����ȴ�");
                return;
            }
            if (Core_statenext)
            {
                F_Choice1.SetActive(false); F_Choice2.SetActive(false);
                Core_statenext = false;
                ChangeImage_back(PlotMessage[CoreIndex].skin_back);
                ChangeImage_character(PlotMessage[CoreIndex].skin_character);
                ChangeTxt(PlotMessage[CoreIndex].txt);
                ChangeImage_character_Txt(PlotMessage[CoreIndex].character);
                ChangeCharacterVoice(CoreIndex + 1);
            }
            CoreIndex++;
        }
        else
        {
            Debug.Log("����");

            configxml.����������("plot" + ListPlotButton.Plotindex.ToString(), "0", "config_PlotProgress.xml");
            //дһ��Ҫ������ƪ��
            string _nextplot_index = configxml.��������_StreamingAssets("plot" + ListPlotButton.Plotindex.ToString(), "config/plot/plotdetail.xml").Split('|')[1];
            if (configxml.��������($"plot{_nextplot_index}", "plotlock.xml") == "None")
            {
                configxml.����������($"plot{_nextplot_index}", "True", "plotlock.xml");
            }
            else if (configxml.��������($"plot{_nextplot_index}", "plotlock.xml") == "False" || configxml.��������($"plot{_nextplot_index}", "plotlock.xml") == "True")
            {
                configxml.����������($"plot{_nextplot_index}", "True", "plotlock.xml");
            }
            else
            {
                new PopNewMessage("Error:The plot is not allowed");
            }

            ShowWhiteWait();
            AsyncOperation operation;
            IEnumerator start_map ()
            {
                yield return new WaitForSeconds(1);
                operation = SceneManager.LoadSceneAsync(2);
                yield return operation;
            }
            StartCoroutine(start_map());
        }
    }

    public void ChangeCharacterVoice (int _index)
    {
        var _audio = Resources.Load<AudioClip>($"Audio/Character/Plot{ListPlotButton.Plotindex}/t{_index}");

        if (_audio == null)
        {
            Debug.LogWarning("û���ҵ���Ƶ��Դ");
            return;
        }
        else
        {
            F_AudioSource.clip = _audio;
            F_AudioSource.Play();
            return;
        }
    }

    public void ChangeImage_character_Txt (string name)
    {
        Debug.Log(name);
        void setnone ()
        {
            F_Left_dialog_name.SetActive(false);
            F_right_dialog_name.SetActive(false);
        }
        switch (name)
        {
            case "Q":
                setnone();
                F_Left_dialog_name.SetActive(true);
                F_Left_dialog_name.GetComponentInChildren<Text>().text = "��������";
                break;

            case "M":
                setnone();
                F_right_dialog_name.SetActive(true);
                F_right_dialog_name.GetComponentInChildren<Text>().text = "��";
                break;
        }
    }

    public void ChangeImage_back (string path)
    {
        var photo = Resources.Load<Sprite>(path);
        if (photo != null)
        {
            F_Skin_Back.gameObject.GetComponent<Image>().sprite = photo;
        }
        else
        {
            photo = Resources.Load<Sprite>("background/" + path);
            if (photo != null)
            {
                F_Skin_Back.gameObject.GetComponent<Image>().sprite = photo;
            }
            else
            {
                HL.IO.HL_Log.Log("����ľ�����Դ", "���ش���");
                new PopNewMessage("����ľ�����Դ");
            }
        }

    }

    public void ChangeImage_character (string path)
    {
        var photo = Resources.Load<Sprite>(path);
        if (photo != null)
        {
            F_Skin_Character.gameObject.GetComponent<Image>().sprite = photo;
        }
        else
        {
            photo = Resources.Load<Sprite>("illustration/" + path);
            if (photo != null)
            {
                F_Skin_Character.gameObject.GetComponent<Image>().sprite = photo;
            }
            else
            {
                HL.IO.HL_Log.Log("����ľ�����Դ", "���ش���");
                new PopNewMessage("����ľ�����Դ");
            }
        }

    }

    public void ChangeTxt (string _txt)
    {
        F_text.text = "";
        F_text.DOText(_txt, _txt.Length * 0.07f).OnComplete(_overtxt);
    }

    private IEnumerator UpdateProgress ()
    {
        yield return null; // �ȴ�һ֡��ȷ��Э������һ֡��ʼִ��

        if (File.Exists(Application.persistentDataPath + "/config_PlotProgress.xml"))
        {
            if (configxml.��������("plot" + ListPlotButton.Plotindex.ToString(), "config_PlotProgress.xml") != "None")
            {
                configxml.����������("plot" + ListPlotButton.Plotindex.ToString(), CoreIndex.ToString(), "config_PlotProgress.xml");
            }
            else
            {
                configxml.����������("plot" + ListPlotButton.Plotindex.ToString(), CoreIndex.ToString(), "config_PlotProgress.xml");
            }
        }
        else
        {
            configxml.���������ļ�("plot" + ListPlotButton.Plotindex.ToString(), CoreIndex.ToString(), "config_PlotProgress.xml");
        }
    }

    /// <summary>
    /// ��һ�仰������ִ�еģ������н����ֹ״̬���ж��Ƿ���ѡ�
    /// </summary>
    public void _overtxt ()
    {
        if (PlotMessage[CoreIndex].choice.Count == 1)
        {
            Core_statenext = true;

            StartCoroutine(UpdateProgress());
        }
        else
        {
            /*
            for (int i = 0; i < PlotMessage[index].choice.Count; i++)
            {
                Debug.Log(PlotMessage[index].choice[i]);
            }
            */
            //����PlotMessage[index]�ĸ�ʽ
            //���1-��һ��ѡ���ı�-�ڶ���ѡ���ı�-���2
            F_Choice1.SetActive(true);

            F_Choice1.GetComponent<RectTransform>().sizeDelta = new Vector2(Value_Default_width_choie + (PlotMessage[CoreIndex].choice[1].Length - 11) / 2 * 15f, 48.5244f);

            F_Choice1.GetComponentInChildren<Text>().text = PlotMessage[CoreIndex].choice[1];
            if (PlotMessage[CoreIndex].choice.Count >= 3)
            {
                F_Choice2.SetActive(true);
                F_Choice2.GetComponent<RectTransform>().sizeDelta = new Vector2(Value_Default_width_choie + (PlotMessage[CoreIndex].choice[2].Length - 11) / 2 * 15f, 48.5244f);
                F_Choice2.GetComponentInChildren<Text>().text = PlotMessage[CoreIndex].choice[2];
            }
        }
    }

    /// <summary>
    /// ��ת��ָ���ľ;�������
    /// </summary>
    private void GotoPlotIndex (int _Goto_index, bool _Immediately = false)
    {
        CoreIndex = _Goto_index - 1;
        if (_Immediately)
        {
            NextPlotScene();
        }
    }

    public void ButtonCoreChoice (int choiceindex)
    {
        audioSource.PlayOneShot(ClickAudio);
        switch (choiceindex)
        {
            case 1:
                if (PlotMessage[CoreIndex].choice.Count > 3)
                {
                    _TemporaryBranchInfo.IsTemporaryBranch = true;
                    _TemporaryBranchInfo.Branch_2_From = Convert.ToInt32(PlotMessage[CoreIndex].choice[0]);
                    _TemporaryBranchInfo.Branch_2_End = Convert.ToInt32(PlotMessage[CoreIndex].choice[3]);
                    Debug.Log(_TemporaryBranchInfo.Branch_2_From + "   " + _TemporaryBranchInfo.Branch_2_End);
                }
                else
                {
                    _TemporaryBranchInfo.IsTemporaryBranch = false;
                }

                Core_statenext = true;
                CoreIndex++;
                break;

            case 2:
                if ((PlotMessage[CoreIndex].choice.Count > 3))
                {
                    _TemporaryBranchInfo.IsTemporaryBranch = false;
                    Core_statenext = true;
                    GotoPlotIndex(Convert.ToInt32(PlotMessage[CoreIndex].choice[3]) + 1);
                    return;
                }
                else
                {
                    CoreIndex++;
                }
                Core_statenext = true;
                break;
            default:

                Debug.LogError("Error!");
                return;
        }
        NextPlotScene();
    }

    public void ShowWhiteWait ()
    {
        if (PlotMessage[CoreIndex].event_white)
        {
            F_dialog_white.SetActive(true);
            Core_statenext = false;
            F_dialog_white.GetComponent<Image>().DOColor(new Color32(255, 255, 255, 255), 1).OnComplete(() =>
            {
                F_dialog_white.GetComponent<Image>().DOColor(new Color32(255, 255, 255, 0), 1).OnComplete(() =>
                {
                    F_dialog_white.SetActive(false);
                });

                Core_statenext = true;
                CoreIndex++;
                NextPlotScene();
            });
        }
    }
}