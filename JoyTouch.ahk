#Requires AutoHotkey v2.0
#SingleInstance Force
Persistent
#Include <ColorButton>
#Include <TouchButton>

if !A_IsAdmin {
    Run '*RunAs "' A_ScriptFullPath '"', , , &PID
    ExitApp
}

btnSize := 70
gap := 10
rows := 2
cols := 3

myGui := Gui("+AlwaysOnTop +ToolWindow -Caption +E0x08000000")
myGui.BackColor := "000000"
myGui.SetFont("s18 cWhite", "Segoe UI")

TouchButton.addKeyButton(btnSize + gap, 0, "W", "w",myGui,btnSize)
TouchButton.addKeyButton(0, btnSize + gap, "A", "a",myGui,btnSize)
TouchButton.addKeyButton(btnSize + gap, btnSize + gap, "S", "s",myGui,btnSize)
TouchButton.addKeyButton(2*(btnSize + gap), btnSize + gap, "D", "d",myGui,btnSize)

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


myGui.Show("x" screenX " y" screenY " w" guiWidth " h" guiHeight)
WinSetTransColor("0x000000", "ahk_id " myGui.Hwnd)

TouchButton.ColorLoad()
TouchButton.Load()

F10::ExitApp

;"F:\Program Files\AutoHotKey\Compiler\Ahk2Exe.exe" /in "F:\Github\github\WASDTouch\WASDTouch.ahk" /out "F:\Github\github\WASDTouch\build\WASDTouch.exe" /base "F:\Program Files\AutoHotKey\v2\AutoHotkey64.exe"