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
            <option selected="" value="20">20</option>
            <option value="50">50</option>
            <option value="100">100</option>
            <option value="150">150</option>
            <option value="200">200</option>
        </select>
        <input id="resultsubmit" type="image" value="Submit" name="submit" src='<%= Url.Image("submit.jpg") %>'>
        <div id="full_description">
            <a href="#">
                <img style="float: right;" alt="Change to Catalog View" src='<%= Url.Image("View_ful.jpg") %>'>
            </a>
        </div>
    </div>
    <img vspace="5" src='<%= Url.Image("linea_az.jpg") %>' alt="">

    <div class="big_item">
        <img alt="" src='<%= Url.Image("search_grey_line.jpg") %>' style="display: block; float: left;
            margin-bottom: 18px;">
        <div>
            <div class="left_img">
                <span class="search_introduction">Intro </span><span class="search_downloadunits">1
                    Cart Unit </span>
                <div class="search_big_img">
                    <a href="#">
                        <img border="0" width="120" alt="Jewish Philosophy 101: #19 Develop a Bias for Truth"
                            src='<%= Url.Image("BY_807_S_jewish_philosophy_.gif") %>'>
                    </a>
                </div>
                <div class="listen_button">
                    <img height="25" border="0" width="60" title="Listening Not Available" src='<%= Url.Image("listen_off.jpg") %>'>
                </div>
                <div class="listen_button">
                    <a href="#">
                        <img height="25" border="0" width="60" title="Add MP3 Download to Cart" src='<%= Url.Image("add_to.jpg") %>'>
                    </a>
                </div>
            </div>
            <div class="right_info">
                <div class="title_info">
                    <span class="search_title"><a class="search_title" href="#">Jewish Philosophy 101: #19
                        Develop a Bias for Truth </a></span><span class="search_author full_author">by <a
                            href="#">Berkovits, Rabbi Yitzchak </a></span>
                </div>
                <div class="search_id full_id">
                    # BY 807 S
                </div>
                <div class="search_synopsis">
                    Before the destruction of the First Temple, there were no disputes as to the correct
                    transmission of the Oral Law. We have to have a love of truth to correctly interpret
                    the Torah and recognize the boundaries of interpretation and maintain its integrity.
                    Listen to this eye-opening talk on truth.
                </div>
                <div style="height: 47px; width: 413px; background-image: url(<%= Url.Image("new_background_free.jpg") %>);
                    background-repeat: no-repeat;">
                    <div style="height: 47px; width: 145px; *width: 140px;">
                        <div style="width: 117px; *width: 125px">
                            <div class="price_prompt" style="margin-top: 12px; margin-left: 8px; *margin-left: 4px;">
                                MP3 Price:&nbsp;
                            </div>
                            <div class="price_value" style="margin-top: 12px;">
                                $3.50
                            </div>
                        </div>
                        <div style="width: 117px; *width: 125px; margin-top: 3px;">
                            <div class="price_prompt" style="margin-left: 8px; *margin-left: 4px;">
                                Member Price:
                            </div>
                            <div class="price_value">
                                $1.00
                            </div>
                        </div>
                    </div>
                    <div style="height: 47px; width: 97px;">
                        <div style="width: 93px; *width: 100px">
                            <div class="price_prompt" style="margin-top: 12px; margin-left: 8px; *margin-left: 6px;">
                                Buy Tape:
                            </div>
                            <div class="price_value" style="margin-top: 12px;">
                                $7.00
                            </div>
                        </div>
                        <div style="width: 93px; *width: 100px; margin-top: 3px;">
                            <div class="price_prompt" style="margin-left: 8px; *margin-left: 6px;">
                                Buy CD:
                            </div>
                            <div class="price_value">
                                N/A
                            </div>
                        </div>
                    </div>
                    <a href="#">
                        <img title="SPETIAL OFFER Get this for FREE" src='<%= Url.Image("learn_more.jpg") %>'>
                    </a>
                </div>
                <span class="search_synopsis" style="margin-top: 3px; margin-bottom: 0px;">Ships from
                    Israel </span>
            </div>
        </div>
    </div>
    <div class="big_item">
        <img alt="" src='<%= Url.Image("search_grey_line.jpg") %>' style="display: block; float: left;
            margin-bottom: 18px;">
        <div>
            <div class="left_img">
                <span class="search_introduction">Adv </span><span class="search_downloadunits">1 Cart
                    Unit </span>
                <div class="search_big_img">
                    <a href="#">
                        <img border="0" width="120" alt="Jewish Philosophy 101: #19 Develop a Bias for Truth"
                            src='<%= Url.Image("tehilim_jo_intro_120x90.gif") %>'>
                    </a>
                </div>
                <div class="listen_button">
                    <img height="25" border="0" width="60" title="Listening Not Available" src='<%= Url.Image("listen_off.jpg") %>'>
                </div>
                <div class="listen_button">
                    <a href="#">
                        <img height="25" border="0" width="60" title="Add MP3 Download to Cart" src='<%= Url.Image("add_to.jpg") %>'>
                    </a>
                </div>
            </div>
            <div class="right_info">
                <div class="title_info">
                    <span class="search_title"><a class="search_title" href="#">David Hamelech: Our Actualizing
                        Force </a></span><span class="search_author full_author">by <a href="#">Juravel, Mrs.
                            Chana </a></span>
                </div>
                <div class="search_id full_id">
                    # JO 079 A
                </div>
                <div class="search_synopsis">
                    In order to understand Tehillim, we need to delve into the personality behind the
                    Psalms. David struggled with an extremely difficult life, making Herculean efforts
                    to connect to G-d despite all that was drawing him away. He became living proof
                    that there is no experience in human existence that can't be used to relate to Hashem,
                    linking heaven and earth in a way that no one succeeded in doing before him.
                </div>
                <div style="height: 47px; width: 413px; background-image: url(<%= Url.Image("new_background_free.jpg") %>);
                    background-repeat: no-repeat;">
                    <div style="height: 47px; width: 145px; *width: 140px;">
                        <div style="width: 117px; *width: 125px">
                            <div class="price_prompt" style="margin-top: 12px; margin-left: 8px; *margin-left: 4px;">
                                MP3 Price:&nbsp;
                            </div>
                            <div class="price_value" style="margin-top: 12px;">
                                $3.50
                            </div>
                        </div>
                        <div style="width: 117px; *width: 125px; margin-top: 3px;">
                            <div class="price_prompt" style="margin-left: 8px; *margin-left: 4px;">
                                Member Price:
                            </div>
                            <div class="price_value">
                                $1.00
                            </div>
                        </div>
                    </div>
                    <div style="height: 47px; width: 97px;">
                        <div style="width: 93px; *width: 100px">
                            <div class="price_prompt" style="margin-top: 12px; margin-left: 8px; *margin-left: 6px;">
                                Buy Tape:
                            </div>
                            <div class="price_value" style="margin-top: 12px;">
                                N/A
                            </div>
                        </div>
                        <div style="width: 93px; *width: 100px; margin-top: 3px;">
                            <div class="price_prompt" style="margin-left: 8px; *margin-left: 6px;">
                                Buy CD:
                            </div>
                            <div class="price_value">
                                N/A
                            </div>
                        </div>
                    </div>
                    <a href="#">
                        <img title="SPETIAL OFFER Get this for FREE" src='<%= Url.Image("learn_more.jpg") %>'>
                    </a>
                </div>
            </div>
        </div>
    </div>
    <div class="big_item">
        <img alt="" src='<%= Url.Image("search_grey_line.jpg") %>' style="display: block; float: left;
            margin-bottom: 18px;">
        <div>
            <div class="left_img">
                <span class="search_introduction">Intro </span><span class="search_downloadunits">2
                    Cart Unit </span>
                <div class="search_big_img">
                    <a href="#">
                        <img border="0" width="120" alt="Jewish Philosophy 101: #19 Develop a Bias for Truth"
                            src='<%= Url.Image("A_butterfly_120x90.gif") %>'>
                    </a>
                </div>
                <div class="listen_button">
                    <img height="25" border="0" width="60" title="Listening Not Available" src='<%= Url.Image("listen_on.jpg") %>'>
                </div>
                <div class="listen_button">
                    <a href="#">
                        <img height="25" border="0" width="60" title="Add MP3 Download to Cart" src='<%= Url.Image("add_to.jpg") %>'>
                    </a>
                </div>
            </div>
            <div class="right_info">
                <div class="title_info">
                    <span class="search_title"><a class="search_title" href="#">A Butterfly Needs Proper
                        Wings to Fly </a></span><span class="search_author full_author">by <a href="#">Leibman,
                            Shoshana Lane </a></span>
                </div>
                <div class="search_id full_id">
                    # LS 912
                </div>
                <div class="search_synopsis">
                    This heartfelt talk gives a compelling analogy of the 'beautiful you' rising above
                    all your worries and suffering. Shoshana Lane Leibman speaks with understanding
                    and compassion and shares amazing stories that illustrate her innovative break-out
                    approach. Don't miss this opportunity to flutter above the pain of your mundane
                    existence.
                </div>
                <div style="height: 47px; width: 413px; background-image: url(<%= Url.Image("new_background_free.jpg") %>);
                    background-repeat: no-repeat;">
                    <div style="height: 47px; width: 145px; *width: 140px;">
                        <div style="width: 117px; *width: 125px">
                            <div class="price_prompt" style="margin-top: 12px; margin-left: 8px; *margin-left: 4px;">
                                MP3 Price:&nbsp;
                            </div>
                            <div class="price_value" style="margin-top: 12px;">
                                $3.50
                            </div>
                        </div>
                        <div style="width: 117px; *width: 125px; margin-top: 3px;">
                            <div class="price_prompt" style="margin-left: 8px; *margin-left: 4px;">
                                Member Price:
                            </div>
                            <div class="price_value">
                                $1.00
                            </div>
                        </div>
                    </div>
                    <div style="height: 47px; width: 97px;">
                        <div style="width: 93px; *width: 100px">
                            <div class="price_prompt" style="margin-top: 12px; margin-left: 8px; *margin-left: 6px;">
                                Buy Tape:
                            </div>
                            <div class="price_value" style="margin-top: 12px;">
                                N/A
                            </div>
                        </div>
                        <div style="width: 93px; *width: 100px; margin-top: 3px;">
                            <div class="price_prompt" style="margin-left: 8px; *margin-left: 6px;">
                                Buy CD:
                            </div>
                            <div class="price_value">
                                N/A
                            </div>
                        </div>
                    </div>
                    <a href="#">
                        <img title="SPETIAL OFFER Get this for FREE" src='<%= Url.Image("learn_more.jpg") %>'>
                    </a>
                </div>
            </div>
        </div>
    </div>
    <div class="big_item">
        <img alt="" src='<%= Url.Image("search_grey_line.jpg") %>' style="display: block; float: left;
            margin-bottom: 18px;">
        <div>
            <div class="left_img">
                <span class="search_introduction">Adv </span><span class="search_downloadunits">1 Cart
                    Unit </span>
                <div class="search_big_img">
                    <a href="#">
                        <img border="0" width="120" alt="Jewish Philosophy 101: #19 Develop a Bias for Truth"
                            src='<%= Url.Image("tehilim_jo_intro_120x90.gif") %>'>
                    </a>
                </div>
                <div class="listen_button">
                    <img height="25" border="0" width="60" title="Listening Not Available" src='<%= Url.Image("listen_on.jpg") %>'>
                </div>
                <div class="listen_button">
                    <a href="#">
                        <img height="25" border="0" width="60" title="Add MP3 Download to Cart" src='<%= Url.Image("add_to.jpg") %>'>
                    </a>
                </div>
            </div>
            <div class="right_info">
                <div class="title_info">
                    <span class="search_title"><a class="search_title" href="#">David Hamelech: Our Actualizing
                        Force </a></span><span class="search_author full_author">by <a href="#">Juravel, Mrs.
                            Chana </a></span>
                </div>
                <div class="search_id full_id">
                    # JO 079 A
                </div>
                <div class="search_synopsis">
                    In order to understand Tehillim, we need to delve into the personality behind the
                    Psalms. David struggled with an extremely difficult life, making Herculean efforts
                    to connect to G-d despite all that was drawing him away. He became living proof
                    that there is no experience in human existence that can't be used to relate to Hashem,
                    linking heaven and earth in a way that no one succeeded in doing before him.
                </div>
                <div style="height: 47px; width: 413px; background-image: url(<%= Url.Image("new_background_free.jpg") %>);
                    background-repeat: no-repeat;">
                    <div style="height: 47px; width: 145px; *width: 140px;">
                        <div style="width: 117px; *width: 125px">
                            <div class="price_prompt" style="margin-top: 12px; margin-left: 8px; *margin-left: 4px;">
                                MP3 Price:&nbsp;
                            </div>
                            <div class="price_value" style="margin-top: 12px;">
                                $3.50
                            </div>
                        </div>
                        <div style="width: 117px; *width: 125px; margin-top: 3px;">
                            <div class="price_prompt" style="margin-left: 8px; *margin-left: 4px;">
                                Member Price:
                            </div>
                            <div class="price_value">
                                $1.00
                            </div>
                        </div>
                    </div>
                    <div style="height: 47px; width: 97px;">
                        <div style="width: 93px; *width: 100px">
                            <div class="price_prompt" style="margin-top: 12px; margin-left: 8px; *margin-left: 6px;">
                                Buy Tape:
                            </div>
                            <div class="price_value" style="margin-top: 12px;">
                                N/A
                            </div>
                        </div>
                        <div style="width: 93px; *width: 100px; margin-top: 3px;">
                            <div class="price_prompt" style="margin-left: 8px; *margin-left: 6px;">
                                Buy CD:
                            </div>
                            <div class="price_value">
                                N/A
                            </div>
                        </div>
                    </div>
                    <a href="#">
                        <img title="SPETIAL OFFER Get this for FREE" src='<%= Url.Image("learn_more.jpg") %>'>
                    </a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
    Search big
</asp:Content>
