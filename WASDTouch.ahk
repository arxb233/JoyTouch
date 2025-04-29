#Requires AutoHotkey v2.0
#SingleInstance Force
Persistent

if !A_IsAdmin {
    Run '*RunAs "' A_ScriptFullPath '"', , , &PID
    ExitApp
}

WM_LBUTTONDOWN := 0x201
WM_LBUTTONUP := 0x202
picSize := 70
gap := 10
rows := 2
cols := 3

for img in ["letter-w.png", "letter-a.png", "letter-s.png", "letter-d.png"]
{
    if !FileExist(img)
    {
        MsgBox "错误：" img " 未找到！"
        ExitApp
    }
}

myGui := Gui("+AlwaysOnTop +ToolWindow -Caption +E0x08000000")
myGui.BackColor := "000000"

; 保存控件与按键的映射
keyMap := Map()

addKeyImage(x, y, img, key) {
    global myGui, keyMap, picSize
    myGui.Add("Picture", Format("x{} y{} w{} h{} +BackgroundTrans", x, y, picSize, picSize), img)
    btn := myGui.Add("Button", Format("x{} y{} w{} h{} +BackgroundTrans +E0x200", x, y, picSize, picSize), "")
    keyMap[btn.Hwnd] := key
}

addKeyImage(picSize + gap, 0, "letter-w.png", "w")
addKeyImage(0, picSize + gap, "letter-a.png", "a")
addKeyImage(picSize + gap, picSize + gap, "letter-s.png", "s")
addKeyImage(2*(picSize + gap), picSize + gap, "letter-d.png", "d")

guiWidth := cols*picSize + (cols-1)*gap
guiHeight := rows*picSize + (rows-1)*gap
gridWidth := A_ScreenWidth // 3
gridHeight := A_ScreenHeight // 3
targetX := gridWidth - guiWidth
targetY := gridHeight * 2 - guiHeight
offsetX := -10
offsetY := -10
screenX := targetX + offsetX
screenY := targetY + offsetY

myGui.Show("x" screenX " y" screenY " w" guiWidth " h" guiHeight)
WinSetTransColor("0x000000", "ahk_id " myGui.Hwnd)

OnMessage(WM_LBUTTONDOWN, MsgLButtonDown)
OnMessage(WM_LBUTTONUP, MsgLButtonUp)

downKeys := Map()

MsgLButtonDown(wParam, lParam, msg, hwnd) {
    global keyMap, downKeys
    if keyMap.Has(hwnd) {
        key := keyMap[hwnd]
        Send("{" key " down}")
        downKeys[hwnd] := key
    }
}

MsgLButtonUp(wParam, lParam, msg, hwnd) {
    global downKeys
    if downKeys.Has(hwnd) {
        key := downKeys[hwnd]
        Send("{" key " up}")
        downKeys.Delete(hwnd)
    }
}

Esc::ExitApp
