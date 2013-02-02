<%@ Page Title="" Language="C#" %>
Environment Information
<br>
[CLR] System.Environment.Version: <%= System.Environment.Version %>
<br>
[FRAMEWORK] Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5").GetValue("Version"): <%= Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5").GetValue("Version") %>
<br>
[FRAMEWORK] Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5").GetValue("SP"): <%= Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5").GetValue("SP") %>
<br>
System.Reflection.Assembly.GetExecutingAssembly().GetName().Version: <%= System.Reflection.Assembly.GetExecutingAssembly().GetName().Version%>
<br>
<br>
Details from SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall:<br>
<%
foreach (string s in Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall").GetSubKeyNames() )
{ Response.Write(s + "<br>"); }
 %>