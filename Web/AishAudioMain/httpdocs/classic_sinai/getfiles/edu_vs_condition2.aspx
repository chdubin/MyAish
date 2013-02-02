<%
Dim Filepath  as string
Response.ContentType = "Application/octet-stream"
string FilePath = MapPath("https://aishaudio.com/classic_sinai/files/CS_BA680-Education.mp3")
Response.WriteFile(FilePath)
Response.End()
%>
