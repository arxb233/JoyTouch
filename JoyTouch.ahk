#Requires AutoHotkey v2.0
#SingleInstance Force
Persistent

global CurrentScriptPID := 0
try {
    TraySetIcon("JoyTouch.ico")
} catch as e {
    MsgBox "无法加载图标 JoyTouch.ico: " . e.Message
}
A_IconTip := "JoyTouch 控制器"
A_TrayMenu.Delete()
A_TrayMenu.Add("按键模式", (*) => RunKeySetGUI("JoyTouchKey.ahk"))
A_TrayMenu.Add("按键设计", (*) => RunKeySetGUI("JoyTouchKeySetGUI.ahk"))
A_TrayMenu.Add()  ; 分隔线
A_TrayMenu.Add("退出程序", (*) => ExitWithCleanup())

RunKeySetGUI("JoyTouchKey.ahk")

RunKeySetGUI(scriptName) {
    global CurrentScriptPID
    try {
        if (CurrentScriptPID != 0) {
            ProcessClose(CurrentScriptPID)
            ProcessWaitClose(CurrentScriptPID, 2)
            CurrentScriptPID := 0
        }
        Run A_AhkPath . " " . scriptName, "", "", &NewPID
        CurrentScriptPID := NewPID
    } catch as e {
        MsgBox "无法运行 " . scriptName . ": " . e.Message
    }
}

ExitWithCleanup() {
    global CurrentScriptPID
    if (CurrentScriptPID != 0) {
        ProcessClose(CurrentScriptPID)
        ProcessWaitClose(CurrentScriptPID, 2)
        CurrentScriptPID := 0
    }
    ExitApp()
}