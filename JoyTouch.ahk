#Requires AutoHotkey v2.0
#SingleInstance Force
Persistent

global CurrentScriptPID := 0

; 检查管理员权限
if !A_IsAdmin {
    try {
        Run '*RunAs "' A_ScriptFullPath '"'
        ExitApp
    } catch {
        MsgBox "此程序需要管理员权限才能运行。", "错误", 16
        ExitApp
    }
}

; 检查重复进程
if ProcessExist("JoyTouch.exe") && (ProcessExist() != DllCall("GetCurrentProcessId")) {
    MsgBox "JoyTouch 已在运行！", "错误", 16
    ExitApp
}

DirCreate(A_ScriptDir "\JoyTouch")
FileInstall "Tool\JoyTouch.ico", A_ScriptDir "\JoyTouch\JoyTouch.ico", 1
FileInstall "Tool\JoyTouchKey.ahk", A_ScriptDir "\JoyTouch\JoyTouchKey.ahk", 1
FileInstall "Tool\JoyTouchKeySetGUI.ahk", A_ScriptDir "\JoyTouch\JoyTouchKeySetGUI.ahk", 1
FileInstall "Tool\DefaultConfig.ahk", A_ScriptDir "\JoyTouch\DefaultConfig.ahk", 1
FileInstall "Tool\TouchButton.ahk", A_ScriptDir "\JoyTouch\TouchButton.ahk", 1
FileInstall "Tool\JSON.ahk", A_ScriptDir "\JoyTouch\JSON.ahk", 1
FileInstall "Tool\ColorButton.ahk", A_ScriptDir "\JoyTouch\ColorButton.ahk", 1
FileInstall "Tool\AutoHotkey64.exe", A_ScriptDir "\JoyTouch\AutoHotkey64.exe", 1


try {
    TraySetIcon(A_ScriptDir "\JoyTouch\JoyTouch.ico")
} catch as e {
    MsgBox "无法加载图标 JoyTouch.ico: " . e.Message
}
A_IconTip := "JoyTouch 控制器"
A_TrayMenu.Delete()
A_TrayMenu.Add("按键模式", (*) => RunKeySetGUI(A_ScriptDir "\JoyTouch\JoyTouchKey.ahk"))
A_TrayMenu.Add("按键设计", (*) => RunKeySetGUI(A_ScriptDir "\JoyTouch\JoyTouchKeySetGUI.ahk"))
A_TrayMenu.Add()  ; 分隔线
A_TrayMenu.Add("退出程序", (*) => ExitWithCleanup())

RunKeySetGUI(A_ScriptDir "\JoyTouch\JoyTouchKey.ahk")

RunKeySetGUI(scriptPath) {
    global CurrentScriptPID
    AhkExe := A_IsCompiled ? A_ScriptDir "\JoyTouch\AutoHotkey64.exe" : A_AhkPath
    try {
        if (CurrentScriptPID != 0) {
            ProcessClose(CurrentScriptPID)
            ProcessWaitClose(CurrentScriptPID, 2)
            CurrentScriptPID := 0
        }
        fullCmd := Format('"{}" "{}"', AhkExe, scriptPath)
        Run fullCmd, , , &NewPID
        CurrentScriptPID := NewPID
    } catch as e {
        MsgBox "无法运行 " . scriptPath . ": " . e.Message
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