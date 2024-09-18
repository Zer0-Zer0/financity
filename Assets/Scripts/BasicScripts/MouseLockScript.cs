using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mouse
{
    public static void Show()
    {
        Cursor.visible = true;
    }

    public static void Hide()
    {
        Cursor.visible = false;
    }
}

public class MouseLockScript : MonoBehaviour
{
    public static MouseLockScript Instance { get; private set; }

    private bool tabbedIn = false;
    public static bool tabbedInX = false;
    public static bool tabbedInTab = false;

    [SerializeField] bool StartVisible = false;
    [SerializeField] bool InputLogicEnabled = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("WARNING: Two MouseLockScripts in this scene");
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        MLSDebug();
        StartingVisibilityChecker();
    }

    private void StartingVisibilityChecker()
    {
        if (StartVisible)
            ShowMouse();
        else
            HideMouse();
    }

    private void MLSDebug()
    {
        string objectName = gameObject.name;
        Debug.Log($"The {objectName} has MouseLockScript");
    }

    private void Update()
    {
        if (InputLogicEnabled)
            InputLogic();
    }

    private void InputLogic()
    {
        bool _hasClicked = Input.GetKeyUp(KeyCode.Mouse0);
        bool _hasPressedTab = Input.GetKeyUp(KeyCode.Tab);
        bool _hasPressedX = Input.GetKeyUp(KeyCode.X);
        bool _hasOpenedStore = _hasPressedX && inventoryToggler.canStoreAppear;

        if (_hasClicked && !tabbedIn)
            Mouse.Hide();
        else if (_hasOpenedStore && !tabbedInTab)
        {
            tabbedInX = !tabbedInX;
            TabLogic();
        }
        else if (_hasPressedTab && !tabbedInX)
        {
            tabbedInTab = !tabbedInTab;
            TabLogic();
        }
    }

    private void TabLogic()
    {
        tabbedIn = !tabbedIn;
        if (tabbedIn)
            Mouse.Show();
        else
            Mouse.Hide();
    }

    public void ShowMouse() => Mouse.Show();
    public void ShowMouseTabbed()
    {
        Mouse.Show();
        tabbedIn = true;
    }

    public void HideMouse()
    {
        Mouse.Hide();
        tabbedIn = false;
    }
}
