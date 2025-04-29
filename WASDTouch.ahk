#Requires AutoHotkey v2.0
#SingleInstance Force
Persistent
#Include <ColorButton>

; 确保以管理员权限运行
if !A_IsAdmin {
    Run '*RunAs "' A_ScriptFullPath '"', , , &PID
    ExitApp
}

; 定义 Windows 消息
WM_LBUTTONDOWN := 0x201
WM_LBUTTONUP := 0x202
WM_POINTERDOWN := 0x0246
WM_POINTERUP := 0x0247

; GUI 参数
btnSize := 70
gap := 10
rows := 2
cols := 3

; 创建 GUI
myGui := Gui("+AlwaysOnTop +ToolWindow -Caption +E0x08000000")
myGui.BackColor := "000000"

; 注册触控窗口（可选，增强兼容性）
DllCall("RegisterTouchWindow", "Ptr", myGui.Hwnd, "UInt", 0)

; 保存控件与按键的映射
keyMap := Map()
downKeys := Map()

addKeyButton(x, y, label, key, color := "3F51B5") {
    global myGui, keyMap, btnSize
    myGui.SetFont("s18 cWhite", "Segoe UI")
    btn := myGui.Add("Button", Format("x{} y{} w{} h{}", x, y, btnSize, btnSize), label)
    btn.SetColor("3F51B5", "FFFFCC",, "fff5cc", 15)
    keyMap[btn.Hwnd] := key
}

; 添加 W、A、S、D 按键
addKeyButton(btnSize + gap, 0, "W", "w")
addKeyButton(0, btnSize + gap, "A", "a")
addKeyButton(btnSize + gap, btnSize + gap, "S", "s")
addKeyButton(2*(btnSize + gap), btnSize + gap, "D", "d")

; 计算 GUI 尺寸和位置
guiWidth := cols*btnSize + (cols-1)*gap
guiHeight := rows*btnSize + (rows-1)*gap
gridWidth := A_ScreenWidth // 3
gridHeight := A_ScreenHeight // 3
targetX := gridWidth - guiWidth
targetY := gridHeight * 2 - guiHeight
offsetX := -150
offsetY := 100
screenX := targetX + offsetX
screenY := targetY + offsetY

; 显示 GUI
myGui.Show("x" screenX " y" screenY " w" guiWidth " h" guiHeight)
WinSetTransColor("0x000000", "ahk_id " myGui.Hwnd)

SetTimer(RefreshButtons, -50)  ; -50 表示只执行一次

RefreshButtons() {
    global keyMap
    for hwnd, key in keyMap {
        PostMessage(0x200, 0, 0, , hwnd)
        DllCall("InvalidateRect", "Ptr", hwnd, "Ptr", 0, "Int", true)
    }
}

; 注册鼠标与触控消息处理
OnMessage(WM_LBUTTONDOWN, MsgLButtonDown)
OnMessage(WM_LBUTTONUP, MsgLButtonUp)
OnMessage(WM_POINTERDOWN, MsgLButtonDown)
OnMessage(WM_POINTERUP, MsgLButtonUp)

; 按下处理
MsgLButtonDown(wParam, lParam, msg, hwnd) {
    global keyMap, downKeys
    if keyMap.Has(hwnd) {
        key := keyMap[hwnd]
        if !downKeys.Has(hwnd) {
            Send("{" key " down}")
            downKeys[hwnd] := key
        }
    }
}

; 释放处理
MsgLButtonUp(wParam, lParam, msg, hwnd) {
    global downKeys
    if downKeys.Has(hwnd) {
        key := downKeys[hwnd]
        Send("{" key " up}")
        downKeys.Delete(hwnd)
    }
}

; 按 F10 退出脚本
F10::ExitApp

;"F:\Program Files\AutoHotKey\Compiler\Ahk2Exe.exe" /in "F:\Github\github\WASDTouch\WASDTouch.ahk" /out "F:\Github\github\WASDTouch\build\WASDTouch.exe" /base "F:\Program Files\AutoHotKey\v2\AutoHotkey64.exe"