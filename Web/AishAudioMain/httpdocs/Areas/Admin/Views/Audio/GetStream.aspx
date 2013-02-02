<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GetStream</title>
    <style type="text/css">
        html, body
        {
            padding:0;
            margin:0;
        }
    </style>
</head>
<body>
    <div>
        <object id="MediaPlayer1" width="300" height="50" classid="CLSID:22D6F312-B0F6-11D0-94AB-0080C74C7E95"
            codebase="http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,0,02,902"
            standby="Loading Microsoft Windows Media Player components..." type="application/x-oleobject">
            <param name="FileName" value="<%= ViewData["src"] %>">
            <param name="AutoStart" value="true">
            <embed type="application/x-mplayer2" pluginspage="http://www.microsoft.com/Windows/MediaPlayer/"
                src="<%= ViewData["src"] %>" name="MediaPlayer1" width="225" height="45" autostart="true">
                 </embed>
        </object>
    </div>
</body>
</html>
