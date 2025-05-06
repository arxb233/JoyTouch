#Requires AutoHotkey v2.0
#SingleInstance Force
SetWorkingDir A_ScriptDir

; 设置圆心和半径（根据需要调整）
centerX := A_ScreenWidth // 2
centerY := A_ScreenHeight // 2
radius := 80
sensitivity := 0.5

; 窗口位置和大小
left := centerX - radius
top := centerY - radius
width := radius * 2
height := radius * 2

; 全局状态变量
global isTracking := false
global lastX := 0
global lastY := 0

; 创建可视 GUI 区域
myGui := Gui("-Caption +AlwaysOnTop +ToolWindow +E0x20") ; 鼠标穿透
myGui.BackColor := "200a0a"
myGui.Show(Format("x{} y{} w{} h{} NA", left, top, width, height))
WinSetTransparent(180, myGui.Hwnd)

; 鼠标按下时判断是否在圆内，启动跟踪
~LButton::
{
    MouseGetPos(&x, &y)
    if (IsInCircle(x, y, centerX, centerY, radius)) {
        isTracking := true
        lastX := x
        lastY := y
        SetTimer(TrackTouch, 10)
    }
    return
}

; 鼠标松开时停止跟踪
~LButton Up::
{
    isTracking := false
    SetTimer(TrackTouch, 0)
    return
}

; 定时函数：模拟相对鼠标移动
TrackTouch() {
    global isTracking, lastX, lastY, sensitivity
    if (!isTracking)
        return

    MouseGetPos(&x, &y)
    if (IsInCircle(x, y, centerX, centerY, radius)) {
        dx := (x - lastX) * sensitivity
        dy := (y - lastY) * sensitivity
        MouseMove(dx, dy, 0, "R")
        lastX := x
        lastY := y
    } else {
        isTracking := false
        SetTimer(TrackTouch, 0)
        ToolTip("移出控制区域")
        SetTimer(() => ToolTip(""), 1000)
    }
}

; 判断点是否在圆内
IsInCircle(x, y, centerX, centerY, radius) {
    return Sqrt((x - centerX)**2 + (y - centerY)**2) <= radius
}

; 快捷键：Ctrl+Esc 退出
^Esc::
{
    myGui.Destroy()
    ExitApp
}
