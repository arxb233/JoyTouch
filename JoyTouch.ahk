#Requires AutoHotkey v2.0
#SingleInstance Force
Persistent
#Include <ColorButton>
#Include "./Tool/TouchButton.ahk"
#Include "./Tool/DefaultConfig.ahk"
#Include <JSON>

DefaultConfig.Ensure("JoyTouch.json")
config := DefaultConfig.Load("JoyTouch.json")
myGui := Gui("+AlwaysOnTop +ToolWindow -Caption +E0x08000000")
myGui.BackColor := config["window"]["background"]

for btn in config["buttons"] {
    TouchButton.addKeyButton(btn, myGui)
}
myGui.Show(
    "x" config["window"]["position"]["x"] 
    "y" config["window"]["position"]["y"] 
    "w" config["window"]["size"]["width"] 
    "h" config["window"]["size"]["height"]
)

WinSetTransColor("0x" config["window"]["background"], "ahk_id " myGui.Hwnd)

TouchButton.ColorLoad()
TouchButton.Load()

; 托盘设置
A_IconTip := "JoyTouch 控制器"
TraySetIcon("JoyTouch.png")

A_TrayMenu.Delete()
A_TrayMenu.Add("按键设计", (*) => Run(A_AhkPath " ./Tool/JoyTouchSetGUI.ahk"))
A_TrayMenu.Add("显示窗口", ShowGui)
A_TrayMenu.Add("隐藏窗口", HideGui)
A_TrayMenu.Add("退出程序", (*) => ExitApp())

ShowGui(*) {
    myGui.Show()
}

HideGui(*) {
    myGui.Hide()
}
