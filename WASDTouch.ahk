#SingleInstance Force
Persistent

for img in ["letter-w.png", "letter-a.png", "letter-s.png", "letter-d.png"]
{
    if !FileExist(img)
    {
        MsgBox "错误：" img " 未找到！"
        ExitApp
    }
}
picSize := 70 
gap := 10  
rows := 2 
cols := 3 
myGui := Gui("+AlwaysOnTop +ToolWindow -Caption")
myGui.BackColor := "000000"

wPic := myGui.Add("Picture", "x" (picSize+gap) " y0 w" picSize " h" picSize " +BackgroundTrans", "letter-w.png")  ; W
aPic := myGui.Add("Picture", "x0 y" (picSize+gap) " w" picSize " h" picSize " +BackgroundTrans", "letter-a.png")  ; A
sPic := myGui.Add("Picture", "x" (picSize+gap) " y" (picSize+gap) " w" picSize " h" picSize " +BackgroundTrans", "letter-s.png")  ; S
dPic := myGui.Add("Picture", "x" (2*(picSize+gap)) " y" (picSize+gap) " w" picSize " h" picSize " +BackgroundTrans", "letter-d.png")  ; D

wPic.OnEvent("Click", (*) => Send("{w down}"))
aPic.OnEvent("Click", (*) => Send("{a down}"))
sPic.OnEvent("Click", (*) => Send("{s down}"))
dPic.OnEvent("Click", (*) => Send("{d down}"))

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

Esc::
{
    myGui.Destroy()
    ExitApp
}