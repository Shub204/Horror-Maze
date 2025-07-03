using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Door : MonoBehaviour
{
    public GameObject handUI;    // UI for hand icon
    public GameObject UIText;    // UI text for messages
    public GameObject invKey;    // Key object in inventory
    public GameObject fadeFX;    // Fade effect object
    public string nextSceneName; // Name of the next scene

    private GameObject OB;
    public float TheDistance;
    public float interactDistance = 2.5f;

    private bool doorOpened = false;

    void Start()
    {
        OB = this.gameObject;
        handUI.SetActive(false);
        UIText.SetActive(false);
        fadeFX.SetActive(false);
    }

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    void OnMouseOver()
    {
        if (doorOpened) return; // Don't allow interaction if already opened

        if (TheDistance <= interactDistance)
        {
            handUI.SetActive(true);

            if (Input.GetButtonDown("Action"))
            {
                if (invKey.activeInHierarchy)
                {
                    // Player has the key, open the door
                    handUI.SetActive(false);
                    UIText.SetActive(false);
                    fadeFX.SetActive(true);
                    doorOpened = true;
                    StartCoroutine(ending());
                }
                else
                {
                    // Player doesn't have the key, show message
                    UIText.SetActive(true);
                }
            }
        }
        else
        {
            handUI.SetActive(false);
            UIText.SetActive(false);
        }
    }

    void OnMouseExit()
    {
        handUI.SetActive(false);
        UIText.SetActive(false);
    }

    IEnumerator ending()
    {
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(nextSceneName);
    }
}
