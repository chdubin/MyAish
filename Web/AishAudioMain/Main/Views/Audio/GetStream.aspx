<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GetStream</title>
    <style type="text/css">
        body
        {
            background-color: Black;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            color: #c4e5f8;
            margin: 0 0 0 0;
        }
        
        .linktxt
        {
            font-size: 9pt;
            color: #c4e5f8;
        }
        
        td
        {
            font-size: 10pt;
        }
    </style>
</head>
<body style="margin-top: 0px; background-image: url(<%= Url.Image("new_background.jpg") %>);
    background-repeat: no-repeat;">
    <table cellpadding="0" cellspacing="0" border="0" width="250">
        <tr>
            <td align="center">
                <img src='<%= Url.Image("spacer.gif") %>' width="250" height="150" border="0" usemap="#Pop" />
            </td>
        </tr>
        <map name="Pop" id="Pop">
            <area shape="rect" coords="25,122,226,147" href="#" target="TMmain" />
        </map>
        <tr>
            <td align="center" height="45">
                <object id="MediaPlayer1" width="225" height="45" classid="CLSID:22D6F312-B0F6-11D0-94AB-0080C74C7E95"
                    codebase="http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,0,02,902"
                    standby="Loading Microsoft Windows Media Player components..." type="application/x-oleobject">
                    <param name="FileName" value="<%= ViewData["src"] %>">
                    <param name="AutoStart" value="true">
                    <embed type="application/x-mplayer2" pluginspage="http://www.microsoft.com/Windows/MediaPlayer/"
                        src="<%= ViewData["src"] %>" name="MediaPlayer1" width="225" height="45" autostart="true">
                 </embed>
                </object>
            </td>
        </tr>
        <tr>
            <td height="30" align="center" valign="middle">
                <img src='<%= Url.Image("spacer.gif") %>' width="250" height="40" border="0" usemap="#PopMac" />
                <map name="PopMac" id="PopMac">
                    <area shape="rect" coords="170,1,236,22" href="#" />
                </map>
            </td>
        </tr>
    </table>
</body>
</html>
