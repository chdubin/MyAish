<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MainEntity.Models.File.FileEntity[]>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lybrary items list</title>
</head>
<body>
    <table cellspacing="2" cellpadding="0" border="0" width="100%">
        <tbody>
            <tr>
                <td>Code</td>
                <td>Title</td>
                <td>Speaker</td>
            </tr>
            <tr>
                <td height="8" colspan="6">
                </td>
            </tr>
            <% foreach (var item in Model)
                { %>
            <tr>
                <td nowrap="" valign="top" class="td2">
                    <%= item.EntityItem.FileClassEntity.CatalogItemExtend.code %>
                </td>
                <td valign="top" class="td3">
                    <%= item.EntityItem.FileClassEntity.ClassEntityItem.title %>
                </td>
                <td valign="top" class="td4">
                    <%= item.EntityItem.FileClassEntity.SpeakerEntityItem.title %>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
</body>
</html>
