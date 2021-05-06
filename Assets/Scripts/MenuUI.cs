using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public GameObject menuOffset;

    [SerializeField]
    private Button button;
    [SerializeField]
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        //button.onClick.AddListener(() =>
        //{
        //    text.text = "버튼 클릭!";
        //});
    }

    // Update is called once per frame
    void Update()
    {
        if (menuOffset != null)
        {
            this.transform.position = menuOffset.transform.position;
            this.transform.rotation = menuOffset.transform.rotation;
        }
    }
}
