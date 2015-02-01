<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>South Indian Society | London | Birmingham | United Kingdom</title>
<meta name="description" content="South Indian Society was started by a few enthusiastic members of the South Indian Tamil Communities in London, Birmingham and other parts of United Kingdom. SIS as an organisation aimed at bringing together the South Indian Tamil speaking families living in London, Birmingham and other parts of United Kingdom." />
<meta name="keywords" content="south indian society, united kingdom indian society, london indian society, tamil families, indian community" />
<meta name="robots" content="index, follow" />
<meta http-equiv="pragma" content="no-cache" />
<link rel="stylesheet" href="/css/style.css" type="text/css"/>
<link href='http://fonts.googleapis.com/css?family=PT+Sans' rel='stylesheet' type='text/css' />
<link href="http://fonts.googleapis.com/css?family=Open+Sans" rel="stylesheet" type="text/css" />
<link href='http://fonts.googleapis.com/css?family=Neucha' rel='stylesheet' type='text/css' />
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/jscript" src="js/jquery-1.9.0.js"></script>
<link href="/css/skitter.styles.css" type="text/css" media="all" rel="stylesheet" />
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>
<script type="text/javascript" src="js/jquery.skitter.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        var options = { thumbs: false };
        $('.box_skitter_large').skitter(options);
    });
</script>
</head>
<body>
<form id="form1" runat="server">
<!--HEADER-->
<div id="sis-header" class="sis-theme">
    <div id="header">
        <div><a href="/"><img src="/images/sis-header.jpg" style="border:0px;" alt="South Indian Society" title="South Indian Society" /></a></div> 
    </div>
    
    <div id="nav-hold">
        <ul class="nav clear" id="site">
	        <li ><a href="/" ><span>Home</span></a></li>
		    <li><a href="/about-sis.aspx">About SIS</a></li>
		    <li><a href="/committee-members.aspx">Committee Members</a></li>
		    <li><a href="/sis-events/">Events</a></li>
		    <li><a href="/photos.aspx">Photos</a></li>
	        <li><a href="/articles/">Articles</a></li>
		    <li><a href="/useful-info/">Useful Info</a></li>
		    <li><a href="/charity.aspx">Charity</a></li>
		    <asp:panel ID="pnlNewletter" runat="server">
                <li><a href="" id="hrefnewsletter" runat="server"><span>Newsletter</span></a></li>
            </asp:panel>
            <li><a href="/register.aspx">Register</a></li>
            <li><a href="/contact-sis.aspx">Contact us</a></li>
        </ul>
    </div>
</div><!--/#HEADER-->

<!--CONTENT-->
<div id="main-content">
    <div class="holder sponsor clear sis-theme">
        <div class="textcontent">
            <div style= "margin: 0 0 10px; position: relative; height: 346px; width: 668px; overflow:visible;">
                <asp:Literal ID="ltBanners" runat="server"></asp:Literal>
            </div>

            <h1>South Indian Society</h1>
            <p>The initiative for the South Indian Society came about more than a quarter century ago. Incorporated as a UK charity in 1995, it has since grown into a prestigious London based, and internationally well regarded, institution.</p>
            
            <%--<div style="margin-left:150px;">
            <ul class="squarebox clear">
                <li class="first">
                <div class="squareboxblock">
                <div class="header" style="text-decoration:underline;">Navaratri - Day 1 Special</div>
                <div class="box-txt"><a href="http://www.sisnambalava.org.uk/articles/religion/navaratri-day-1-special-20121016063907.aspx">Goddess Durga’s nine incarnations are worshipped on the nine days. The mother is worshipped as Sailaputri on the first day. Sailaputri is essentially Goddess Parvathi, the daughter of Himavan. Mother holds Trishul on her right hand and a lotus on the left hand and seated on a Rishabh vahanam.</a><br /><br />
                </div>
                <p><a href="http://www.sisnambalava.org.uk/articles/religion/navaratri-day-1-special-20121016063907.aspx" class="button">More &raquo;</a></p>
                </div>
                </li>
            </ul></div>--%>

            <p>The objectives of the South Indian Society are three fold:</p>
            <ul>
            <li>Provide, for the South Indian, especially Tamil, diaspora in the UK,  regular opportunities to stay connected with their roots, even while helping them integrate much better with the multicultural British society and succeed</li>
            <li>Further education on all aspects of South Indian Vedic culture, including Tamil and Sanskrit languages, scriptures and literature, religion and arts particularly among children and young adults</li>
            <li>Help support relevant causes and charities in the UK and in India.</li>
            </ul>
            <p>We hold several cultural events, spiritual satsangs and educational workshops. We also help disseminate aspects of India’s great ancient heritage, highlighting in particular their contemporary relevance: this we do through a professionally presented website and articles and newsletters signed-for by patrons not just in the UK but India, USA and elsewhere.</p>

            <asp:Literal ID="ltThiruPaavai" runat="server"></asp:Literal>
           
        </div>


        <div class="adsponsor"> 
            <div class="blog-panel sign-up-panel">
                <h2>Newsletter Sign up</h2>
                <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter your email address" MaxLength="200"></asp:TextBox><asp:LinkButton Text="<img style='border:0px' src='/images/go-button.png' />" CssClass="btn" runat="server" ID="lnkSubmit" ValidationGroup="newsletter" CausesValidation="true"></asp:LinkButton><br />
                <asp:RequiredFieldValidator ID="revEmail" runat="server" Font-Size="1.1em" ControlToValidate="txtEmail" ValidationGroup="newsletter" InitialValue="" Text="Email Address is missing"></asp:RequiredFieldValidator>
            </div>
            
            <%--#Advert--%>
            <asp:Literal ID="ltAdverts" runat="server"></asp:Literal>
            
            <div class="advert">
	            <iframe width="252" height="159" src="//www.youtube.com/embed/kx5IMBYKmL0" frameborder="0" allowfullscreen></iframe>
            </div>

        </div>
    </div>
</div><!--/#CONTENT-->

<!--FOOTER-->
<div id="sisfooter" class="sis-theme">
<div class="clear" id="footer">
    <div class="footerimage">
    <img src="/images/sis-logo.png" alt="South Indian Society" title="South Indian Society" id="footer-logo" />
    </div>

    <div class="footerbox">
        <div class="boxheader">Popular Articles</div>
        <asp:Literal ID="ltPopularArticles" runat="server"></asp:Literal>
    </div>

    <div class="footerbox">
        <div class="boxheader">Article Categories</div>
        <ul class="nav">
            <li><a href="/articles/art/">Arts</a></li>
            <li><a href="/articles/culture/">Hindu Culture</a></li>
            <li><a href="/articles/religion/">Hindu Religion</a></li>
            <li><a href="/articles/temples/">Hindu Temples</a></li>
            <li><a href="/articles/others/">Others</a></li>
            <li><a href="/articles/recipes/">Recipes</a></li>
        </ul>
        <%--#Latest Articles--%>
        <asp:Literal ID="ltLatestArticles" runat="server"></asp:Literal>
    </div>

    <div class="footerbox">
        <asp:Literal ID="ltUpcomingEvents" runat="server"></asp:Literal>
        <asp:Panel ID="pnlOtherEvents" runat="server">
            <asp:Literal ID="ltOtherEvents" runat="server"></asp:Literal>
        </asp:Panel>  
        <div class="boxheader">Other Links</div>
        <ul class="nav">
            <li><a href="/useful-info/">Useful Information</a></li>
            <li><a href="/useful-info/uk-hindu-temples.aspx">UK Hindu Temples</a></li>
            <li><a href="/useful-info/uk-hindu-priests.aspx">UK Hindu Priests</a></li>
            <li><a href="/disclaimer.aspx">Disclaimer</a></li>
        </ul>
    </div>

    <div class="footer_txt">Copyright &copy; 2014 South Indian Society. All Rights Reserved.</div>
</div>
</div><!--/#FOOTER-->
<script type="text/javascript">
  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-19943769-1']);
  _gaq.push(['_trackPageview']);

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();
</script> 
</form>
</body>
</html>
