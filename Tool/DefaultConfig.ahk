#Requires AutoHotkey v2.0
#Include "../Lib/Json.ahk"
class DefaultConfig {
    static Ensure(filePath) {
        if !FileExist(filePath)
            FileAppend(DefaultConfig.DefaultJSON(), filePath, "UTF-8")
    }

    static Load(filePath) {
        return Json.parse(FileRead(filePath, "UTF-8"))
    }
    static LoadObject(filePath) {
        return Json.parse(FileRead(filePath, "UTF-8"),false,false)
    }

    static DefaultJSON() {
        return '
        (
            {
                "buttons": [
                    {
                        "label": "W",
                        "key": "w",
                        "x": 80,
                        "y": 0,
                        "size": 70,
                        "btncolor": {
                            "bgcolor":"#3F51B5",
                            "fontcolor":"#FFFFCC",
                            "bordercolor": "#FFFFCC",
                            "fontsize": 15
                        }
                    },
                    {
                        "label": "A",
                        "key": "a",
                        "x": 0,
                        "y": 80,
                        "size": 70,
                        "btncolor": {
                            "bgcolor":"#3F51B5",
                            "fontcolor":"#FFFFCC",
                            "bordercolor": "#FFFFCC",
                            "fontsize": 15
                        }
                    },
                    {
                        "label": "S",
                        "key": "s",
                        "x": 80,
                        "y": 80,
                        "size": 70,
                    "btncolor": {
                            "bgcolor":"#3F51B5",
                            "fontcolor":"#FFFFCC",
                            "bordercolor": "#FFFFCC",
                            "fontsize": 15
                        }
                    },
                    {
                        "label": "D",
                        "key": "d",
                        "x": 160,
                        "y": 80,
                        "size": 70,
                        "btncolor": {
                            "bgcolor":"#3F51B5",
                            "fontcolor":"#FFFFCC",
                            "bordercolor": "#FFFFCC",
                            "fontsize": 10
                        }
                    }
                ],
                "window": {
                    "position": {
                        "x": 500,
                        "y": 500
                    },
                    "size": {
                        "width": 240,
                        "height": 160
                    },
                    "background": "000000"
                }
            }
        )'
    }
}
