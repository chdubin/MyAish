<%@ Page Title="" Language="C#" MasterPageFile="../Shared/oneColumnEmpty.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href='<%= Url.Css("main_styles2.css") %>' rel="Stylesheet" />
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

This links on this page are temporarily disabled. To put through an order, please call 973-767-3700. Thank you.
    <table cellspacing="4" cellpadding="0" border="0" width="100%">
        <tbody>
            <tr>
                <td>
            
                    <table border="0" style="margin:0 auto;" width="733">
                        <tbody>
                            <tr>
                                <td colspan="5">
                                    <img width="542" height="102" src='<%= Url.Image("download_plans_page_i_new.jpg") %>' />
                                </td>
                                <td width="172">&nbsp;
                                    
                                </td>
                            </tr>
                            <tr>
                                <td width="173" valign="top">
                                    <table border="0" width="173">
                                        <tbody>
                                            <tr>
                                                <td valign="top" height="78" colspan="2">
                                                    <div class="benefit_regular">
                                                        Get a free iPod /iPhone<br>
                                                        with the purchase of an<br>
                                                        Introductory or Advanced<br>
                                                        Jewish mp3 Library</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="9" valign="top" height="47">
                                                    <!-- Join Form Here -->
                                                    <p>                                                        
                                                        <img width="12" height="13" alt="bullet" src='<%= Url.Image("bullet.jpg") %>' />
                                                    </p>
                                                </td>
                                                <td width="154" valign="top" height="47">
                                                    <div class="benefit_regular">
                                                        Includes 3 free months of AishAudio to-go</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" height="68" colspan="2">
                                                    <div align="center">
                                                        <span class="benefit_regular">
                                                            <a href="#">
                                                                <img width="62" height="16" alt="details" src='<%= Url.Image("details.jpg") %>'>
                                                            </a>
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" height="37" colspan="2">
                                                    <div align="left">
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td width="4" valign="top">
                                    <img width="2" height="539" src='<%= Url.Image("smallline.gif") %>' />
                                </td>
                                <td width="183" valign="top">
                                    <table border="0" width="173">
                                        <tbody>
                                            <tr>
                                                <td width="7" height="13">
                                                    <img width="12" height="13" src='<%= Url.Image("bullet.jpg") %>' />
                                                </td>
                                                <td width="156" height="13">
                                                    <div>
                                                        <div align="top">
                                                            <div>
                                                                <div align="left">
                                                                    <span class="benefit_regularredbigger">Only $59</span> <span class="benefit_regularred">
                                                                        - a $148 value</span></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="benefit_regular">
                                                        <div align="left">
                                                        </div>
                                                    </div>
                                                    <div class="benefit_regular">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" colspan="2">
                                                    <form method="post" action="/shopping/AddPackage?package_id=74">
                                                    <div align="center">
                                                        <input type="submit" value="Get it Now" name="Submit" />
                                                    </div>
                                                    </form>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="13">
                                                    <img width="12" height="13" src='<%= Url.Image("bullet.jpg") %>' />
                                                </td>
                                                <td height="13">
                                                    <div class="benefit_regular">
                                                        <div align="left">
                                                            Free 1 GB Sansa Clip</div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="13">&nbsp;
                                                    
                                                </td>
                                                <td height="13" class="benefit_regular">
                                                    mp3 Player (worth $55)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                    
                                                </td>
                                                <td>
                                                    <span class="benefit_smaller"><a class="blueboldtextsmall" href="#beamer">Specs below</a></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                    
                                                </td>
                                                <td class="benefit_regular">&nbsp;
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <img width="12" height="13" src='<%= Url.Image("bullet.jpg") %>' />
                                                </td>
                                                <td>
                                                    <span class="benefit_regular">mp3 player loaded with</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div align="center">
                                                    </div>
                                                </td>
                                                <td>
                                                    <span class="benefit_regularred">Jewish History</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                    
                                                </td>
                                                <td>
                                                    <span class="benefit_regularred">Crash Course</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td rowspan="2">&nbsp;
                                                    
                                                </td>
                                                <td>
                                                    <span class="benefit_regular">33 classes (worth $63)</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span class="benefit_regular"><a class="blueboldtextsmall" href="#beamer">Write-up below</a></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                    
                                                </td>
                                                <td>
                                                    <span class="benefit_regular"><strong><strong><strong><strong></strong></strong></strong>
                                                    </strong></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <img width="12" height="13" src='<%= Url.Image("bullet.jpg") %>' />
                                                </td>
                                                <td>
                                                    <span class="benefit_regular">3 Months of pre-paid</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">&nbsp;
                                                    
                                                </td>
                                                <td valign="top">
                                                    <span class="benefit_regular">downloads - 10 units/mo.</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">&nbsp;
                                                    
                                                </td>
                                                <td valign="top">
                                                    <span class="benefit_regular">(worth $30)</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="21" colspan="2">
                                                    <div align="center">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="74" colspan="2">
                                                    <div align="center">
                                                        <table border="0" align="center" width="150" height="56">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="background-image:url(<%= Url.Image("bground.gif") %>);" height="52" />
                                                                        <div align="center" class="benefit_larger_banner1">
                                                                            <strong>
                                                                                <br>
                                                                                Upgrade to 2 GB</strong></div>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <img width="12" height="13" src='<%= Url.Image("bullet.jpg") %>' />
                                                </td>
                                                <td>
                                                    <span class="benefit_regular">2 GB mp3 player loaded </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div align="center">
                                                    </div>
                                                </td>
                                                <td>
                                                    <span class="benefit_regular">with </span><span class="benefit_regularred">Jewish History</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                    
                                                </td>
                                                <td>
                                                    <span class="benefit_regularred">Crash Course</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                    
                                                </td>
                                                <td>&nbsp;
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <img width="12" height="13" src='<%= Url.Image("bullet.jpg") %>' />
                                                </td>
                                                <td>
                                                    <span class="benefit_regular">3 Months of pre-paid</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">&nbsp;
                                                    
                                                </td>
                                                <td valign="top">
                                                    <span class="benefit_regular">downloads - 10 units/mo.</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" colspan="2">
                                                    <span class="benefit_regularred">
                                                        <div align="center">
                                                            Add $25
                                                        </div>
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td height="38" colspan="2">
                                                    <div align="center">
                                                        <form method="post" action="#">
                                                            <input type="submit" value="Get it Now" name="Submit" />
                                                        </form>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td width="2" valign="top">
                                    <img width="2" height="533" src='<%= Url.Image("smallline.gif") %>' />
                                </td>
                                <td width="173" valign="top">
                                    <table border="0" width="173">
                                        <tbody>
                                            <tr>
                                                <td width="7" height="13">
                                                    <img width="12" height="13" src='<%= Url.Image("bullet.jpg") %>' />
                                                </td>
                                                <td width="156" height="13">
                                                    <span class="benefit_regular">Beam all your mp3 </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="13">&nbsp;
                                                    
                                                </td>
                                                <td height="13">
                                                    <span class="benefit_regular">players to your car radio</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="13">&nbsp;
                                                    
                                                </td>
                                                <td height="13">
                                                    <span class="benefit_regular">through the headphone</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="13">&nbsp;
                                                    
                                                </td>
                                                <td height="13">
                                                    <span class="benefit_regular">jack. <a class="blueboldtextsmall" href="#beamer">Specs
                                                        below</a></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                    
                                                </td>
                                                <td>&nbsp;
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <img width="12" height="13" src='<%= Url.Image("bullet.jpg") %>' />
                                                </td>
                                                <td class="benefit_regular">
                                                    Add $29.95 to 3 Month
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                    
                                                </td>
                                                <td class="benefit_regular">
                                                    1 GB player Super Deal
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <span class="benefit_regularred">
                                                        <div align="center">
                                                            $88.95
                                                        </div>
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td height="32" colspan="2">
                                                    <div align="center">
                                                        <strong class="benefit_regularred">
                                                            <form method="post" action="#">
                                                                <input type="submit" value="Get it Now" name="Submit" />
                                                            </form>
                                                        </strong><strong class="benefit_regularred"></strong>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle" height="78" colspan="2">
                                                    <table border="0" style="background-image:url(<%= Url.Image("bground.gif") %>);background-repeat:no-repeat;"
                                                        align="center" width="148" height="78">
                                                        <tbody>
                                                            <tr>
                                                                <td width="130" height="65">
                                                                    <div align="center" class="benefit_larger_banner2">
                                                                        <strong>
                                                                            <br>
                                                                            Upgrade to 2 GB
                                                                            <br>
                                                                            plus Beamer </strong>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <img width="12" height="13" src='<%= Url.Image("bullet.jpg") %>'>
                                                </td>
                                                <td class="benefit_regular">
                                                    FM Beamer
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                    
                                                </td>
                                                <td class="benefit_regular">&nbsp;
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <img width="12" height="13" src='<%= Url.Image("bullet.jpg") %>'>
                                                </td>
                                                <td class="benefit_regular">
                                                    Add $29.95 to 3 Month
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                    
                                                </td>
                                                <td class="benefit_regular">
                                                    2 GB player Upgrade
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <span class="benefit_regularred">
                                                        <div align="center">
                                                            $113.95
                                                        </div>
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td height="37" colspan="2">
                                                    <div align="center">
                                                        <form method="post" action="#">
                                                            <input type="submit" value="Get it Now" name="Submit" />
                                                        </form>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td valign="top">
                                    <img width="181" height="278" src='<%= Url.Image("download_plans_include.jpg") %>' />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>&nbsp;
                        </p>
                    <table cellspacing="0" cellpadding="0" align="center" width="752">
                        <tbody>
                            <tr>
                                <td width="257" valign="top" style="width: 257px;">
                                    <img src='<%= Url.Image("sansa_pict.jpg") %>' alt="" /><br />
                                    <div class="benefit_header">
                                        <span class="bodytext">SANDISK 1GB Sansa Clip mp3 Player<br>
                                            <em>"The tiny player with the big sound."</em></span></div>
                                    <ul class="benefit_description">
                                        <li>1 GB memory plays over 16 hours of MP3 (32 hours of WMA) music (over 240 MP3/ 480
                                            WMA songs)</li>
                                        <li><span class="bodytext">Includes MP3 player, Clip accessory, USB 2.0 transfer cable,
                                            Earphones, Quick start guide, Installation CD with user guide.</span></li>
                                        <li><span class="bodytext">Up to 15 hours continuous playback using internal <strong>
                                            rechargeable battery</strong></span></li>
                                        <li><span class="bodytext">Supports MP3, WMA, secure WMA, and Audible audio file formats</span></li>
                                        <li><span class="bodytext">Hi-speed USB 2.0 port for fast and easy transfer of files</span></li>
                                        <li><span class="bodytext">Includes digital FM tuner with 20 preset stations voice recorder
                                            with built-in microphone.</span></li>
                                        <li><span class="bodytext">Mfr. Warranty: 2 YEARS</span></li>
                                        <li>Dimensions: Just 2.17"x1.35"x.65" </li>
                                    </ul>
                                </td>
                                <td width="7" style="width: 7px;">&nbsp;
                                    
                                </td>
                                <td width="1" valign="top" style="width: 1px; font-size: 1px; height: 100%; background-color: rgb(233, 233, 233);">
                                    <div style="height: 69px; background-color: white;">
                                        &nbsp;</div>
                                </td>
                                <td width="7" style="width: 7px;">&nbsp;
                                    
                                </td>
                                <td width="271" valign="top" style="width: 270px;">
                                    <a name="beamer"></a>
                                    <img src='<%= Url.Image("beamer_pict.jpg") %>' alt="" /><br />
                                    <div class="benefit_header">
                                        Wireless Audio FM Beamer
                                    </div>
                                    <div class="benefit_subheader">
                                        Beams audio from mp3 player to your car radio &ndash; like magic.</div>
                                    <ul class="benefit_description">
                                        <li>Car power adapter</li>
                                        <li>Up to 18 hours with AAA battery Auto on-off</li>
                                        <li>Just set your radio FM station to the same number on your beaming device and &ndash;
                                            like magic &ndash; your favorite audio will play loud and clear over your radio,
                                            without wires or static.</li>
                                    </ul>
                                </td>
                                <td width="7" style="width: 7px;">&nbsp;
                                    
                                </td>
                                <td width="1" valign="top" style="width: 1px; font-size: 1px; height: 100%; background-color: rgb(233, 233, 233);">
                                    <div style="height: 69px; background-color: white;">
                                        &nbsp;</div>
                                </td>
                                <td width="7" style="width: 7px;">&nbsp;
                                    
                                </td>
                                <td width="192" valign="top">
                                    <p>
                                        <img src='<%= Url.Image("crash_course_pict.jpg") %>' alt="" /><br />
                                    </p>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <br />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
