using UnityEngine;

public class UseChest : MonoBehaviour
{
    private GameObject OB;
    public GameObject handUI;
    public GameObject objToActivate;

    public float TheDistance;
    public float interactDistance = 2.5f;
    private bool isOpened = false;

    void Start()
    {
        OB = this.gameObject;
        handUI.SetActive(false);
        objToActivate.SetActive(false);
    }

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    void OnMouseOver()
    {
        if (isOpened) return;  // Do nothing if chest already opened

        if (TheDistance <= interactDistance)
        {
            handUI.SetActive(true);

            if (Input.GetButtonDown("Action"))
            {
                handUI.SetActive(false);
                objToActivate.SetActive(true);
                OB.GetComponent<Animator>().SetBool("open", true);
                OB.GetComponent<BoxCollider>().enabled = false;
                isOpened = true;
            }
        }
        else
        {
            handUI.SetActive(false);
        }
    }

    void OnMouseExit()
    {
        handUI.SetActive(false);
    }
}
