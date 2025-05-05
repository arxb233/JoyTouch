#SingleInstance Force
SetWorkingDir A_ScriptDir

; 定义圆形区域（根据你的屏幕分辨率调整）
centerX := 50  ; 圆心 X 坐标（1920x1080 屏幕中心）
centerY := 54  ; 圆心 Y 坐标
radius := 20  ; 圆形区域半径（像素）
sensitivity := 0.5 ; 陀螺仪灵敏度

; 全局变量
global lastX := 0
global lastY := 0
global isTracking := false

; 创建 GUI 显示圆形区域（矩形边界框模拟圆）
myGui := Gui("-Caption +AlwaysOnTop +ToolWindow")
myGui.BackColor := "000000" ; 背景色（黑色）
myGui.Opt("+E0x080000") ; WS_EX_LAYERED for transparency
left := centerX - radius
top := centerY - radius
width := radius * 2
height := radius * 2
myGui.Show("x" left " y" top " w" width " h" height " NA")
; 设置 GUI 半透明
DllCall("UpdateLayeredWindow", "Ptr", myGui.hwnd, "Ptr", 0, "Ptr", 0, "Int64*", 0, "Int64*", 0, "Int*", 0, "Int*", 0xFF000000 | (100 << 16), "Int", 2)

; 显示脚本启动提示
MsgBox("脚本已启动！圆形区域已显示（中心: " . centerX . "," . centerY . ", 半径: " . radius . "）。`n请在圆内点击并拖动。`n按 Ctrl+Esc 退出。")

; 热键：左键按下开始检测
~$LButton::
{
    global lastX, lastY, isTracking
    startX := 0
    startY := 0
    MouseGetPos(&startX, &startY) ; 获取鼠标/触控坐标
    ToolTip("点击坐标: " . startX . "," . startY)
    FileAppend("点击: " . startX . "," . startY . "`n", A_ScriptDir . "\debug.log") ; 调试日志
    if IsInCircle(startX, startY, centerX, centerY, radius) {
        isTracking := true
        lastX := startX
        lastY := startY
        SetTimer(TrackTouch, 10) ; 每 10ms 检查移动
        ToolTip("开始跟踪: " . startX . "," . startY)
    } else {
        ToolTip("点击不在圆内！坐标: " . startX . "," . startY)
        SetTimer(() => ToolTip(""), 2000) ; 2秒后清除提示
    }
    return
}

; 热键：左键松开停止检测
~$LButton Up::
{
    global isTracking
    isTracking := false
    SetTimer(TrackTouch, 0) ; 关闭定时器
    ToolTip("") ; 清除提示
    return
}

; 跟踪触控移动并模拟鼠标
TrackTouch()
{
    global lastX, lastY, isTracking
    if (!isTracking)
        return
    currentX := 0
    currentY := 0
    MouseGetPos(&currentX, &currentY) ; 修复：使用正确引用
    FileAppend("移动: " . currentX . "," . currentY . "`n", A_ScriptDir . "\debug.log") ; 调试日志
    if (IsInCircle(currentX, currentY, centerX, centerY, radius)) {
        deltaX := (currentX - lastX) * sensitivity
        deltaY := (currentY - lastY) * sensitivity
        MouseMove(deltaX, deltaY, 0, "R") ; 相对移动
        lastX := currentX
        lastY := currentY
        ToolTip("移动中: " . currentX . "," . currentY)
    } else {
        isTracking := false
        SetTimer(TrackTouch, 0)
        ToolTip("移出圆形区域！")
        SetTimer(() => ToolTip(""), 2000) ; 2秒后清除提示
    }
}

; 函数：判断点是否在圆内
IsInCircle(x, y, centerX, centerY, radius)
{
    distance := Sqrt((x - centerX)**2 + (y - centerY)**2)
    return distance <= radius
}

; 退出脚本并销毁 GUI
^Esc::
{
    myGui.Destroy()
    ExitApp
}