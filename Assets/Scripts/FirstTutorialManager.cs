using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FirstTutorialManager : MonoBehaviour
{
    [SerializeField] private RobotCleanerController robotCleanerController;
    [SerializeField] private Sprite LemurLogo;

    void Start()
    {
        StartCoroutine(StrartTutorial());
    }

    private IEnumerator StrartTutorial()
    {
        NodificationController.Instance.SandNodification(LemurLogo, "Lemuz", "Hello, my name is Lemuz, i`m your creator");
        yield return new WaitUntil(() => !NodificationController.Instance.NodifyNow);
        NodificationController.Instance.SandNodification(LemurLogo, "Lemuz", "");
        yield return new WaitUntil(() => !NodificationController.Instance.NodifyNow);
        // Text about robot cleaner tusk
        NodificationController.Instance.SandNodification(LemurLogo, "Lemuz", "");
        yield return new WaitUntil(() => !robotCleanerController.TrashTarget);
        // Text when robot find a trash
        NodificationController.Instance.SandNodification(LemurLogo, "Lemuz", "");
    }
}
