using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodificationController : MonoBehaviour
{
    public static NodificationController Instance { get; private set; }

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _context;
    [SerializeField] private GameObject _nodifyPanel;

    public Sprite TestIcon;

    public bool NodifyNow;
    private bool _skip;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _nodifyPanel.transform.position = new Vector3(-1000, 0, 0);
        _context.text = "";
    }

    public void Update()
    {
        if (NodifyNow && Input.GetKeyDown(KeyCode.Space))
            _skip = true;
    }

    public void SandNodification(Sprite icon, string title, string context)
    {
        NodifyNow = true;
        StartCoroutine(SandNodificationCoroutine(icon, title, context, () => Input.GetKeyDown(KeyCode.Space)));
    }

    public void SandNodification(Sprite icon, string title, string context, System.Func<bool> cancelWhen)
    {
        NodifyNow = true;
        StartCoroutine(SandNodificationCoroutine(icon, title, context, cancelWhen));
    }

    public IEnumerator SandNodificationCoroutine(Sprite icon, string title, string context, System.Func<bool> cancelWhen, float speed = 0.08f)
    {
        _skip = false;
        _iconImage.sprite = icon;
        _titleText.text = title;
        _nodifyPanel.transform.DOMove(new Vector3(0, 0, 0), 0.7f);

        yield return new WaitForSeconds(1f);

        foreach (char c in context)
        {
            if (_skip)
                break;
            _context.text += c;
            yield return new WaitForSeconds(speed);
        }
        _context.text = context;

        yield return new WaitForSeconds(0.25f);

        yield return new WaitUntil(cancelWhen);

        _nodifyPanel.transform.DOMove(new Vector3(-1000, 0, 0), 0.5f);
        _context.text = "";

        yield return new WaitForSeconds(0.75f);

        NodifyNow = false;
    }
}
