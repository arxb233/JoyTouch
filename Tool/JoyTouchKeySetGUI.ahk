#SingleInstance Force
#Include "Json.ahk"
#Include "DefaultConfig.ahk"
#Include "TouchButton.ahk"
#NoTrayIcon

; 初始化 JSON 数据（从文件加载或嵌入）
jsonFile := "JoyTouch.json"
DefaultConfig.Ensure(jsonFile)
jsonObj := DefaultConfig.LoadObject(jsonFile)
TraySetIcon("JoyTouch.ico")
; 创建主 GUI
MainGui := Gui("+Resize", "JoyTouch设计器")
MainGui.SetFont("s10", "Segoe UI")

; 按钮列表和属性
MainGui.Add("ListBox", "x10 y10 w200 h300 vButtonList", GetButtonList(jsonObj)).OnEvent("Change", SelectButton)
MainGui.Add("GroupBox", "x220 y10 w250 h440", "按钮属性")
MainGui.Add("Text", "x230 y30 w100", "标签:")
MainGui.Add("Edit", "x330 y30 w120 vBtnLabel")
MainGui.Add("Text", "x230 y60 w100", "键:")
MainGui.Add("Edit", "x330 y60 w120 vBtnKey")
MainGui.Add("Text", "x230 y90 w100", "X 位置:")
MainGui.Add("Edit", "x330 y90 w120 vBtnX Number")
MainGui.Add("Text", "x230 y120 w100", "Y 位置:")
MainGui.Add("Edit", "x330 y120 w120 vBtnY Number")
MainGui.Add("Text", "x230 y150 w100", "大小:")
MainGui.Add("Edit", "x330 y150 w120 vBtnSize Number")
MainGui.Add("Text", "x230 y180 w100", "背景颜色:")
MainGui.Add("Edit", "x330 y180 w120 vBtnBgColor")
MainGui.Add("Text", "x230 y210 w100", "字体颜色:")
MainGui.Add("Edit", "x330 y210 w120 vBtnFontColor")
MainGui.Add("Text", "x230 y240 w100", "边框颜色:")
MainGui.Add("Edit", "x330 y240 w120 vBtnBorderColor")
MainGui.Add("Text", "x230 y270 w100", "字体大小:")
MainGui.Add("Edit", "x330 y270 w120 vBtnFontSize Number")
MainGui.Add("Button", "x230 y300 w100", "保存按钮").OnEvent("Click", SaveButton)
MainGui.Add("Button", "x340 y300 w100", "添加按钮").OnEvent("Click", AddButton)
MainGui.Add("Button", "x230 y330 w100", "删除按钮").OnEvent("Click", DeleteButton)

; 窗口属性
MainGui.Add("GroupBox", "x10 y320 w210 h350", "窗口属性")
MainGui.Add("Text", "x20 y340 w100", "X 位置:")
MainGui.Add("Edit", "x120 y340 w80 vWinX Number", jsonObj.window.position.x)
MainGui.Add("Text", "x20 y370 w100", "Y 位置:")
MainGui.Add("Edit", "x120 y370 w80 vWinY Number", jsonObj.window.position.y)
MainGui.Add("Text", "x20 y400 w100", "宽度:")
MainGui.Add("Edit", "x120 y400 w80 vWinWidth Number", jsonObj.window.size.width)
MainGui.Add("Text", "x20 y430 w100", "高度:")
MainGui.Add("Edit", "x120 y430 w80 vWinHeight Number", jsonObj.window.size.height)
MainGui.Add("Text", "x20 y460 w100", "背景:")
MainGui.Add("Edit", "x120 y460 w80 vWinBg", jsonObj.window.background)

; 创建预览窗口
PreviewGui := Gui("+ToolWindow", "预览")
PreviewGui.BackColor := jsonObj.window.background
for index, btn in jsonObj.buttons
    TouchButton.addKeyButton(btn,PreviewGui)
PreviewGui.Show("x" jsonObj.window.position.x " y" jsonObj.window.position.y " w" jsonObj.window.size.width " h" jsonObj.window.size.height)

; 主 GUI 按钮
MainGui.Add("Button", "x10 y490 w100", "保存 JSON").OnEvent("Click", SaveJSON)
MainGui.Add("Button", "x120 y490 w100", "更新预览").OnEvent("Click", UpdatePreview)
MainGui.Show("w480 h520")

; 事件处理
SelectButton(Ctrl, Info) {
    selected := MainGui["ButtonList"].Value
    if (selected > 0) {
        btn := jsonObj.buttons[selected]
        MainGui["BtnLabel"].Value := btn.label
        MainGui["BtnKey"].Value := btn.key
        MainGui["BtnX"].Value := btn.x
        MainGui["BtnY"].Value := btn.y
        MainGui["BtnSize"].Value := btn.size
        MainGui["BtnBgColor"].Value := btn.btncolor.bgcolor
        MainGui["BtnFontColor"].Value := btn.btncolor.fontcolor
        MainGui["BtnBorderColor"].Value := btn.btncolor.bordercolor
        MainGui["BtnFontSize"].Value := btn.btncolor.fontsize
    }
}

SaveButton(Ctrl, Info) {
    selected := MainGui["ButtonList"].Value
    if (selected > 0) {
        jsonObj.buttons[selected].label := MainGui["BtnLabel"].Value
        jsonObj.buttons[selected].key := MainGui["BtnKey"].Value
        jsonObj.buttons[selected].x := MainGui["BtnX"].Value
        jsonObj.buttons[selected].y := MainGui["BtnY"].Value
        jsonObj.buttons[selected].size := MainGui["BtnSize"].Value
        jsonObj.buttons[selected].btncolor.bgcolor := MainGui["BtnBgColor"].Value
        jsonObj.buttons[selected].btncolor.fontcolor := MainGui["BtnFontColor"].Value
        jsonObj.buttons[selected].btncolor.bordercolor := MainGui["BtnBorderColor"].Value
        jsonObj.buttons[selected].btncolor.fontsize := MainGui["BtnFontSize"].Value
        MsgBox("按钮属性已保存。")
    }
}

AddButton(Ctrl, Info) {
    newBtn := {label: "New", key: "n", x: 0, y: 0, size: 70, btncolor: {bgcolor: "#3F51B5", fontcolor: "#FFFFCC", bordercolor: "#FFFFCC", fontsize: 15}}
    jsonObj.buttons.Push(newBtn)
    MainGui["ButtonList"].Delete()
    MainGui["ButtonList"].Add(GetButtonList(jsonObj))
    MsgBox("新按钮已添加。")
}

DeleteButton(Ctrl, Info) {
    selected := MainGui["ButtonList"].Value
    if (selected > 0) {
        jsonObj.buttons.RemoveAt(selected)
        MainGui["ButtonList"].Delete()
        MainGui["ButtonList"].Add(GetButtonList(jsonObj))
        MsgBox("按钮已删除。")
    }
}

SaveJSON(Ctrl, Info) {
    jsonObj.window.position.x := MainGui["WinX"].Value
    jsonObj.window.position.y := MainGui["WinY"].Value
    jsonObj.window.size.width := MainGui["WinWidth"].Value
    jsonObj.window.size.height := MainGui["WinHeight"].Value
    jsonObj.window.background := MainGui["WinBg"].Value
    FileDelete(jsonFile)
    FileAppend(JSON.Stringify(jsonObj, true), jsonFile)
    MsgBox("JSON 已保存至 " jsonFile "。")
}

UpdatePreview(Ctrl, Info) {
    global PreviewGui  ; 声明 PreviewGui 为全局变量
    jsonObj.window.position.x := MainGui["WinX"].Value
    jsonObj.window.position.y := MainGui["WinY"].Value
    jsonObj.window.size.width := MainGui["WinWidth"].Value
    jsonObj.window.size.height := MainGui["WinHeight"].Value
    jsonObj.window.background := MainGui["WinBg"].Value
    PreviewGui.Destroy()
    PreviewGui := Gui("+ToolWindow", "预览")
    PreviewGui.BackColor := jsonObj.window.background
    for index, btn in jsonObj.buttons
        TouchButton.addKeyButton(btn,PreviewGui)
    PreviewGui.Show("x" jsonObj.window.position.x " y" jsonObj.window.position.y " w" jsonObj.window.size.width " h" jsonObj.window.size.height)
}

; 辅助函数
GetButtonList(obj) {
    list := []
    for index, btn in obj.buttons
        list.Push("按钮 " index)
    return list
}

SetTimer(UpdatePosition, 100)

UpdatePosition() {
    try{
        WinGetPos(&X, &Y, , , "预览")
        MainGui["WinX"].Value := X
        MainGui["WinY"].Value := Y 
    }catch{
        
    }
}
MainGui["WinX"].OnEvent("Change", MovePreviewWindow)
MainGui["WinY"].OnEvent("Change", MovePreviewWindow)

MovePreviewWindow(GuiCtrl, *) {
    X := MainGui["WinX"].Value
    Y := MainGui["WinY"].Value
    if (X != "" && Y != "") {
        WinMove(X, Y, , , "预览")
    }
}

MainGui.OnEvent("Close", (*) => ExitApp())