#Requires AutoHotkey v2.0
#Include <ColorButton>

class TouchButton
{
    static keyMap := Map()
    static downKeys := Map()
    
    static addKeyButton(btnstyle,myGui) {
        btn := myGui.Add("Button", Format("x{} y{} w{} h{}", btnstyle.x, btnstyle.y, btnstyle.size, btnstyle.size), btnstyle.label)
        TouchButton.btnCustomstyle(btn,btnstyle.btncolor)
        TouchButton.keyMap[btn.Hwnd] := btnstyle.key
    }
    static btnCustomstyle(btn,btncolor) {
        btn.SetColor(
            TouchButton.strToColor(btncolor.bgcolor),
            TouchButton.strToColor(btncolor.fontcolor),,
            TouchButton.strToColor(btncolor.bordercolor),15)
        btn.SetFont("s" btncolor.fontsize) 
    }
    static ColorLoad() {
        SetTimer(RefreshButtons, -50)
        RefreshButtons() {
            for hwnd, key in TouchButton.keyMap {
                PostMessage(0x200, 0, 0, , hwnd)
                DllCall("InvalidateRect", "Ptr", hwnd, "Ptr", 0, "Int", true)
            }
        }       
    }
    static strToColor(color) {
        return SubStr(color, 1, 1) = "#" ? "0x" SubStr(color, 2) : color
    }
    static Load() {
        static WM_LBUTTONDOWN := 0x201
        static WM_LBUTTONUP := 0x202
        static WM_POINTERDOWN := 0x0246
        static WM_POINTERUP := 0x0247

        OnMessage(WM_LBUTTONDOWN, MsgLButtonDown.Bind(, , , , TouchButton.keyMap, TouchButton.downKeys))
        OnMessage(WM_LBUTTONUP, MsgLButtonUp.Bind(, , , , TouchButton.downKeys))
        OnMessage(WM_POINTERDOWN, MsgLButtonDown.Bind(, , , , TouchButton.keyMap, TouchButton.downKeys))
        OnMessage(WM_POINTERUP, MsgLButtonUp.Bind(, , , , TouchButton.downKeys))
        
        MsgLButtonDown(wParam, lParam, msg, hwnd, keyMap, downKeys) {
            if keyMap.Has(hwnd) {
                key := keyMap[hwnd]
                if !downKeys.Has(hwnd) {
                    Send("{" key " down}")
                    downKeys[hwnd] := key
                }
            }
        } 
        MsgLButtonUp(wParam, lParam, msg, hwnd, downKeys) {
            if downKeys.Has(hwnd) {
                key := downKeys[hwnd]
                Send("{" key " up}")
                downKeys.Delete(hwnd)
            }
        }
    }
}