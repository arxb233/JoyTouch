#Requires AutoHotkey v2.0
#SingleInstance Force
Persistent
#Include <ColorButton>
#Include "./Tool/TouchButton.ahk"
#Include <JSON>

config := Json.parse(FileRead("JoyTouch.json"))
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

F10::ExitApp