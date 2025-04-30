#Requires AutoHotkey v2.0

class TouchButton
{
    static keyMap := Map()
    static downKeys := Map()
    
    static addKeyButton(x, y, label, key,myGui,btnSize,color := "3F51B5") {
        btn := myGui.Add("Button", Format("x{} y{} w{} h{}", x, y, btnSize, btnSize), label)
        btn.SetColor("3F51B5", "FFFFCC",, "fff5cc", 15)
        TouchButton.keyMap[btn.Hwnd] := key
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