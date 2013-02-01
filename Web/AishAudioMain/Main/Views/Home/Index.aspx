<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="body" runat="server">

<% string menuActionName = MainBL.CookieBL.ShowDetailed(Request.Cookies, Response.Cookies) ? "resultsdetail" : "results"; %>

    <div id="blue_field">
        <div style="float: left; width: 448px;">
            <a href="<%=Url.Action("Free","Home") %>"><img height="226" border="0" width="446" src="<%= Url.Image("welcome.gif") %>" alt="Excitement"></a>
            <a href="/torah-portions"><img height="33" border="0" width="449" style="margin-top: 9px;" src="<%= Url.Image("Torah-Po.png")%>"
                    alt="Torah Portion" /></a>
            <a href="<%= Request.IsAuthenticated ? "/" : "/search/free" %>"><img height="70" border="0" width="220" src="<%= Url.Image("350_free.jpg")%>" alt="350"></a>
            <a href="/free-downloads"><img height="70" border="0" width="220" src="<%= Url.Image("2_free_d.jpg")%>" alt="2" style="margin-left: 2px;">
            </a>
        </div>
        <div style="float: right; width: 145px;">
            <a href="<%=Url.Action("Free","Home") %>"><img height="109" border="0" width="145" src="<%= Url.Image("ON_952_D_Tree_Teacher_Good_-1.jpg") %>" alt="I'm a Good Person - Why Be Religious?">
            </a><a href="<%=Url.Action("Free","Home") %>"><img height="109" border="0" width="145" style="margin-top: 9px;" src="<%= Url.Image("GL_805_jewish_eco_system_14.jpg") %>"
                    alt="Why I Wouldn't Make Aliyah">
            </a>
            <div style="width: 145px; height: 101px; margin-top: 9px;">                
                <a href="/top-lectures">
                    <img style="vertical-align: top;" border="0" align="top" width="145" src="<%= Url.Image("top_favo.jpg") %>" alt="top" usemap="#Maped" />
                </a>
                <a href="/new-lectures">
                    <img style="vertical-align: top;" border="0" align="top" width="145" src="<%= Url.Image("new_addi.jpg") %>" alt="middle" usemap="#Map5" />
                </a>
                <a href="/search/<%= menuActionName %>/code/017""><img style="vertical-align: top;" align="top" width="145" src="<%= Url.Image("editor_p.jpg") %>"
                        alt="bottom" usemap="#Map8" />
                </a>
            </div>
        </div>
    </div>
    <div class="midArticlesBg">
        <div style="width: 300px; float: left;">
            <div>
                <!-- the right column -->
                <h1>
                    <a href="/search/<%= menuActionName %>/cat/Essentials">JEWISH ESSENTIALS </a>
                </h1>
                <ul>
                    <li>
                        <img border="0" alt="bullet" src="<%= Url.Image("square-b.gif") %>">
                        <a href="/search/<%= menuActionName %>/class/31260">Crash Course in Torah </a></li>
                    <li>
                        <img border="0" alt="bullet" src="<%= Url.Image("square-b.gif") %>">
                        <a href="/search/<%= menuActionName %>/class/32608">How to Run a Traditional Shabbos Dinner </a></li>
                    <li>
                        <img border="0" alt="bullet" src="<%= Url.Image("square-b.gif") %>">
                        <a href="/search/<%= menuActionName %>/class/33048">Judaism in a Nutshell </a></li>
                </ul>
            </div>
            <div>
                <h1>
                    <a href="/search/<%= menuActionName %>/cat/Jewish-Literacy">JEWISH LITERACY </a>
                </h1>
                <ul>
                    <li>
                        <img border="0" alt="Bullet" src="<%= Url.Image("square-b.gif") %>">
                        <a href="/search/<%= menuActionName %>/class/34770">Prophetic Codes Embedded in the Bible 1 </a></li>
                    <li>
                        <img border="0" alt="Bullet" src="<%= Url.Image("square-b.gif") %>">
                        <a href="/search/<%= menuActionName %>/class/31353">Derech Hashem #1 </a></li>
                    <li>
                        <img border="0" alt="Bullet" src="<%= Url.Image("square-b.gif") %>">
                        <a href="/search/<%= menuActionName %>/class/38011">What Happens After You Die </a></li>
                </ul>
            </div>
            <div>
                <h1>
                    <a href="/search/<%= menuActionName %>/cat/dating-home">DATING AND FAMILY </a>
                </h1>
                <ul>
                    <li>
                        <img border="0" alt="Bullet" src="<%= Url.Image("square-b.gif") %>">
                        <a href="/search/<%= menuActionName %>/class/36585">The Happy Wife </a></li>
                    <li>
                        <img border="0" alt="Bullet" src="<%= Url.Image("square-b.gif") %>">
                        <a href="/search/<%= menuActionName %>/class/29876">A Woman of Valor: Creating a Loving Home </a></li>
                    <li>
                        <img border="0" alt="Bullet" src="<%= Url.Image("square-b.gif") %>">
                        <a href="/search/<%= menuActionName %>/class/29801">A Mystical Look at a Wedding Ceremony </a></li>
                </ul>
            </div>
        </div>
        <div style="width: 295px; float: right;">
            <div>
                <!-- the right column -->
                <h1>
                    <a href="/search/<%= menuActionName %>/cat/spirituality-home">SPIRITUALITY </a>
                </h1>
                <ul>
                    <li>
                        <img border="0" alt="bullet" src="<%= Url.Image("square-b.gif") %>">
                        <a href="/search/<%= menuActionName %>/class/38388">Working on Ourselves: Getting Things Done </a></li>
                    <li>
                        <img border="0" src="<%= Url.Image("square-b.gif") %>">
                        <a href="/search/<%= menuActionName %>/class/31306">David Hamelech: Our Actualizing Force </a></li>
                    <li>
                        <img border="0" alt="bullet" src="<%= Url.Image("square-b.gif") %>">
                        <a href="/search/<%= menuActionName %>/class/40763">Why I Donated My Kidney To A Stranger </a></li>
                </ul>
            </div>
            <div>
                <h1>
                    <a href="/search/<%= menuActionName %>/cat/israel-home">ISRAEL </a>
                </h1>
                <ul>
                    <li>
                        <img border="0" alt="Bullet" src="<%= Url.Image("square-b.gif") %>">
                        <a href="/search/<%= menuActionName %>/class/39396">Judaism, Paganism and Multiculturalism </a></li>
                    <li>
                        <img border="0" alt="Bullet" src="<%= Url.Image("square-b.gif") %>">
                        <a href="/search/<%= menuActionName %>/class/37076">The Tabernacle: The Universe in Miniature </a></li>
                    <li>
                        <img border="0" alt="Bullet" src="<%= Url.Image("square-b.gif") %>">
                        <a href="/search/<%= menuActionName %>/class/32787">Israel </a></li>
                </ul>
            </div>
            <div>
                <h1>
                    <a href="/static/TorahPortions">TORAH PORTION </a>
                </h1>
                <ul>
                    <li>
                        <img border="0" alt="Bullet" src="<%= Url.Image("square-b.gif") %>">
                        <a href="/search/<%= menuActionName %>/code/017">Yisro </a></li>
                    <li>
                        <img border="0" alt="Bullet" src="<%= Url.Image("square-b.gif") %>">
                        <a href="/search/<%= menuActionName %>/code/018">Mishpatim</a></li>
                    <li>
                        <img border="0" alt="Bullet" src="<%= Url.Image("square-b.gif") %>">
                        <a href="/search/<%= menuActionName %>/code/019">Terumah</a></li>
                </ul>
            </div>
        </div>
    </div>
    <div style="margin-top: 25px; margin-bottom: 15px; margin-left: 10px;">
        <a href="#">
            <img src="<%= Url.Image("listen_s.jpg") %>" alt="" />
        </a>
    </div>
    <div style="margin-left: 40px;">
        <a href="javascript:openpopup('http://www.classicsinai.com/7_wonders_stream_s.htm')"><img style="margin-right: 20px;" border="0" alt="1" src="<%= Url.Image("7_wonder.jpg") %>" /></a>
        <a href="javascript:openpopup('http://www.classicsinai.com/happiness_streams.htm')"><img style="margin-right: 20px;" border="0" alt="3" src="<%= Url.Image("happines.jpg") %>" /></a>
        <a href="javascript:openpopup('http://www.classicsinai.com/bigbang_stream_s.htm')"><img style="margin-right: 20px;" border="0" alt="2" src="<%= Url.Image("genesis0.jpg") %>" /></a>
        <a href="javascript:openpopup('http://www.classicsinai.com/torahs.htm')"><img style="margin-right: 20px;" border="0" alt="5" src="<%= Url.Image("torah000.jpg") %>" /></a>
        <a href="javascript:openpopup('http://www.classicsinai.com/bodiess.htm')"><img border="0" alt="4" src="<%= Url.Image("our_bodi.jpg") %>" /></a>
    </div>

<script type="text/javascript">
    function openpopup(url) {
        winpops = window.open(url, "", "width=150,height=100,")
    }
</script>

</asp:Content>
