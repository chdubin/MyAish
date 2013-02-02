<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="search_indent your_search">
        Your search of:
    </div>
    <div class="search_big_blue">
        Spirituality <span class="search_has_found">has found 4 results</span>
    </div>
    <div class="search_indent">
        <div class="your_search">
            This page displays results 1 to 4 of 4.
        </div>
    </div>
    <div class="search_indent" style="margin-top: 10px;">
        <form name="resultsortform" method="GET" action="#">
        </form>
        <div class="results_per_page">
            Results per page:
        </div>
        <select id="resultsperpagedropdown" style="font-size: 10px;" name="dnum">
            <option value="10">10</option>
            <option value="20">20</option>
            <option value="50">50</option>
            <option value="100">100</option>
            <option value="150">150</option>
            <option selected="" value="200">200</option>
            <option value="500">500</option>
            <option value="1000">1000</option>
        </select>
        <input id="resultsubmit" type="image" value="Submit" name="submit" src='<%= Url.Image("submit.jpg") %>' />
        <div id="full_description">
            <a href="#">
                <img style="float: right;" alt="Change to Catalog View" src='<%= Url.Image("View_ful.jpg") %>' />
            </a>
        </div>
    </div>
    <img vspace="5" src='<%= Url.Image("linea_az.jpg") %>' alt="" />

    <div class="mp3_item">
        <div class="music_icons">
            <img height="25" width="26" border="0" src='<%= Url.Image("listening_icon.jpg") %>' title="Listening Not Available" />
            <a href="#">
                <img height="25" width="25" border="0" src='<%= Url.Image("arrow_icon-over.jpg") %>' title="Buy MP3 Download" />
            </a>
            <a href="#">
                <img height="25" width="26" border="0" src='<%= Url.Image("tape_icon_over.jpg") %>' title="Order Tape" />
            </a>
            <img height="25" width="26" border="0" src='<%= Url.Image("compactDisc_icon.jpg") %>' title="CD Not Available" />
        </div>
        <div class="rightpart_mp3">
            <div class="rightpart_mp3">
                <div class="title">
                    <a href="#" class="search_title">Jewish Philosophy 101: #19 Develop a Bias for Truth
                    </a><span align="right" class="search_author">&nbsp;&nbsp; <a href="#">Berkovits, Rabbi
                        Yitzchak </a></span>
                </div>
                <div class="search_id">
                    BY 807 S
                </div>
            </div>
            <div class="rightpart_mp3">
                <div class="price_and_details">
                    <a href="#" class="nolinkformat"><span class="price_prompt">mp3: </span><span class="price_value">
                        $3.50 </span></a><a href="#" class="nolinkformat">&nbsp; <span class="price_prompt">
                            Members </span><span class="price_value">$1.00 </span></a>&nbsp;&nbsp; <a href="#"
                                class="nolinkformat"><span class="price_prompt">Tape:&nbsp; </span><span class="price_value">
                                    $7.00 </span></a>&nbsp;&nbsp; <span class="price_prompt">CD:&nbsp;
                    </span><span class="price_value">N/A </span>
                </div>
                <div class="search_details">
                    <a href="#">DETAILS </a>
                </div>
            </div>
        </div>
    </div>
    <div class="mp3_item">
        <div class="music_icons">
            <img height="25" width="26" border="0" src='<%= Url.Image("listening_icon.jpg") %>' title="Listening Not Available" />
            <a href="#">
                <img height="25" width="25" border="0" src='<%= Url.Image("arrow_icon-over.jpg") %>' title="Buy MP3 Download" />
            </a>
            <img height="25" width="26" border="0" src='<%= Url.Image("tape_icon.jpg") %>' title="Order Tape" />
            <img height="25" width="26" border="0" src='<%= Url.Image("compactDisc_icon.jpg") %>' title="CD Not Available" />
        </div>
        <div class="rightpart_mp3">
            <div class="rightpart_mp3">
                <div class="title">
                    <a href="#" class="search_title">David Hamelech: Our Actualizing Force </a><span
                        align="right" class="search_author">&nbsp;&nbsp; <a href="#">Juravel, Mrs. Chana
                        </a></span>
                </div>
                <div class="search_id">
                    JO 079 A
                </div>
            </div>
            <div class="rightpart_mp3">
                <div class="price_and_details">
                    <a href="#" class="nolinkformat"><span class="price_prompt">mp3: </span><span class="price_value">
                        $3.50 </span></a><a href="#" class="nolinkformat">&nbsp; <span class="price_prompt">
                            Members </span><span class="price_value">$1.00 </span></a>&nbsp;&nbsp; <a href="#"
                                class="nolinkformat"><span class="price_prompt">Tape:&nbsp; </span><span class="price_value">
                                    N/A </span></a>&nbsp;&nbsp; <span class="price_prompt">CD:&nbsp;
                    </span><span class="price_value">N/A </span>
                </div>
                <div class="search_details">
                    <a href="#">DETAILS </a>
                </div>
            </div>
        </div>
    </div>
    <div class="mp3_item">
        <div class="music_icons">
            <a href="#">
                <img height="25" border="0" width="26" title="FREE Listening" src='<%= Url.Image("listening_icon-over.jpg") %>'>
            </a><a href="#">
                <img height="25" width="25" border="0" src='<%= Url.Image("arrow_icon-over.jpg") %>' title="Buy MP3 Download">
            </a>
            <img height="25" width="26" border="0" src='<%= Url.Image("tape_icon.jpg") %>' title="Order Tape">
            <img height="25" width="26" border="0" src='<%= Url.Image("compactDisc_icon.jpg") %>' title="CD Not Available">
        </div>
        <div class="rightpart_mp3">
            <div class="rightpart_mp3">
                <div class="title">
                    <a href="#" class="search_title">Discovering the Real You </a><span align="right"
                        class="search_author">&nbsp;&nbsp; <a href="#">Palatnik, Mrs. Lori </a></span>
                </div>
                <div class="search_id">
                    LO 952
                </div>
            </div>
            <div class="rightpart_mp3">
                <div class="price_and_details">
                    <a href="#" class="nolinkformat"><span class="price_prompt">mp3: </span><span class="price_value">
                        $3.50 </span></a><a href="#" class="nolinkformat">&nbsp; <span class="price_prompt">
                            Members </span><span class="price_value">$2.00 </span></a>&nbsp;&nbsp; <a href="#"
                                class="nolinkformat"><span class="price_prompt">Tape:&nbsp; </span><span class="price_value">
                                    N/A </span></a>&nbsp;&nbsp; <span class="price_prompt">CD:&nbsp;
                    </span><span class="price_value">N/A </span>
                </div>
                <div class="search_details">
                    <a href="#">DETAILS </a>
                </div>
            </div>
        </div>
    </div>
    <div class="mp3_item">
        <div class="music_icons">
            <a href="#">
                <img height="25" border="0" width="26" title="FREE Listening" src='<%= Url.Image("listening_icon-over.jpg") %>'>
            </a><a href="#">
                <img height="25" width="25" border="0" src='<%= Url.Image("arrow_icon-over.jpg") %>' title="Buy MP3 Download">
            </a>
            <img height="25" width="26" border="0" src='<%= Url.Image("tape_icon.jpg") %>' title="Order Tape">
            <img height="25" width="26" border="0" src='<%= Url.Image("compactDisc_icon.jpg") %>' title="CD Not Available">
        </div>
        <div class="rightpart_mp3">
            <div class="rightpart_mp3">
                <div class="title">
                    <a href="#" class="search_title">A Butterfly Needs Proper Wings to Fly </a><span
                        align="right" class="search_author">&nbsp;&nbsp; <a href="#">Leibman, Shoshana Lane
                        </a></span>
                </div>
                <div class="search_id">
                    LS 912
                </div>
            </div>
            <div class="rightpart_mp3">
                <div class="price_and_details">
                    <a href="#" class="nolinkformat"><span class="price_prompt">mp3: </span><span class="price_value">
                        $3.50 </span></a><a href="#" class="nolinkformat">&nbsp; <span class="price_prompt">
                            Members </span><span class="price_value">$1.00 </span></a>&nbsp;&nbsp; <a href="#"
                                class="nolinkformat"><span class="price_prompt">Tape:&nbsp; </span><span class="price_value">
                                    N/A </span></a>&nbsp;&nbsp; <span class="price_prompt">CD:&nbsp;
                    </span><span class="price_value">N/A </span>
                </div>
                <div class="search_details">
                    <a href="#">DETAILS </a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
    Search
</asp:Content>
