#Requires AutoHotkey v2.0
#SingleInstance Force
Persistent

; 确保以管理员权限运行
if !A_IsAdmin {
    Run '*RunAs "' A_ScriptFullPath '"', , , &PID
    ExitApp
}

; 定义 Windows 消息
WM_LBUTTONDOWN := 0x201
WM_LBUTTONUP := 0x202

; GUI 参数
picSize := 70
gap := 10
rows := 2
cols := 3

; 检查图片文件是否存在
for img in ["letter-w.png", "letter-a.png", "letter-s.png", "letter-d.png"] {
    if !FileExist(img) {
        MsgBox "错误：" img " 未找到！"
        ExitApp
    }
}

; 创建 GUI
myGui := Gui("+AlwaysOnTop +ToolWindow -Caption +E0x08000000")
myGui.BackColor := "000000"

; 保存控件与按键的映射
keyMap := Map()
downKeys := Map()

; 添加按键图像和按钮的函数
addKeyImage(x, y, img, key) {
    global myGui, keyMap, picSize
    pic := myGui.Add("Picture", Format("x{} y{} w{} h{} +BackgroundTrans", x, y, picSize, picSize), img)
    btn := myGui.Add("Button", Format("x{} y{} w{} h{} +BackgroundTrans +E0x200", x, y, picSize, picSize), "")
    keyMap[btn.Hwnd] := key
}

; 添加 W、A、S、D 按键
addKeyImage(picSize + gap, 0, "letter-w.png", "w")
addKeyImage(0, picSize + gap, "letter-a.png", "a")
addKeyImage(picSize + gap, picSize + gap, "letter-s.png", "s")
addKeyImage(2*(picSize + gap), picSize + gap, "letter-d.png", "d")

; 计算 GUI 尺寸和位置
guiWidth := cols*picSize + (cols-1)*gap
guiHeight := rows*picSize + (rows-1)*gap
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

; 注册鼠标消息处理
OnMessage(WM_LBUTTONDOWN, MsgLButtonDown)
OnMessage(WM_LBUTTONUP, MsgLButtonUp)

; 按下处理
MsgLButtonDown(wParam, lParam, msg, hwnd) {
    global keyMap, downKeys
    if keyMap.Has(hwnd) {
        key := keyMap[hwnd]
        if !downKeys.Has(hwnd) {  ; 避免重复按下
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

; 定时器检查长按状态（可选，增强稳定性）
SetTimer(CheckKeyStates, 50)

CheckKeyStates() {
    global keyMap, downKeys
    for hwnd, key in downKeys {
        if !GetKeyState("LButton", "P") {  ; 如果鼠标左键已释放
            Send("{" key " up}")
            downKeys.Delete(hwnd)
        }
    }
}

; 退出脚本
Esc::ExitApp