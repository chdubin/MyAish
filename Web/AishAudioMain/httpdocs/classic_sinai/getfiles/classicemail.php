<script language="JavaScript">
<!--
  javascript:window.history.forward(1);
//-->
</script>

<?php


IF ($emailcode == "edu")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/edu_stream.htm";
$classname = "Education vs. Conditioning";
}


IF ($emailcode == "jewishidentity")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/identity_stream.htm";
$classname = "How to Awaken Your Jewish Identity";
}

IF ($emailcode == "goodfight")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/marriage_stream.htm";
$classname = "Marriage: How to Have a Good Fight";
}

IF ($emailcode == "worldperfect")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/worldperfect_stream.htm";
$classname = "Worldperfect";
}

IF ($emailcode == "misconceptions")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/misconceptions_stream.htm";
$classname = "4 Misconceptions About Judaism";
}

IF ($emailcode == "pleasure")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/pleasure_stream.htm";
$classname = "Pleasure: The Ultimate Energizer";
}

IF ($emailcode == "science")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/science_stream.htm";
$classname = "The Science of Miracles";
}

IF ($emailcode == "dating")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/dating.htm";
$classname = "The Dating Process for Older Singles";
}

IF ($emailcode == "nourish")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/nourish.htm";
$classname = "How Does Torah Nourish Your Soul?";
}

IF ($emailcode == "bodies")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/bodies_stream";
$classname = "Our Bodies; Our Souls";
}

IF ($emailcode == "bridges")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/bridges_stream";
$classname = "Building Bridges With Those You Love";
}

IF ($emailcode == "eco-system")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/eco_stream.htm";
$classname = "The Jewish Eco-system";
}


IF ($emailcode == "crashcourse")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/crash_stream.htm";
$classname = "A Crashcourse in Jewish History";
}



IF ($emailcode == "wonders")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/hebrew_stream.htm";
$classname = "The Wonders of the Jewish Language";
}


IF ($emailcode == "overview")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/5books_stream.htm";
$classname = "Overview of the 5 Book of Moses";
}


IF ($emailcode == "obsession")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/holocaust_stream_htm";
$classname = "The Jewish Obsession with the Holocaust";
}


IF ($emailcode == "intimacy")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/intimacy_stream.htm";
$classname = "Intimacy";
}

IF ($emailcode == "values")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/values_stream.htm";
$classname = "Real Values";
}

IF ($emailcode == "matrix")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/matrix_stream.htm";
$classname = "The Matrix and Jewish Reality";
}

IF ($emailcode == "morality")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/morality_stream.htm";
$classname = "Morality in the Public Eye";
}

IF ($emailcode == "bigbang")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/bigbang_stream.htm";
$classname = "Genesis and the Big Bang";
}

IF ($emailcode == "joseph")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/joseph_stream.mp3";
$classname = "Joseph: 7 Lessons in Diplomacy";
}


IF ($emailcode == "bodysoul2")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/bodies_stream.htm";
$classname = "Our Bodies, Our Souls 2";
}

IF ($emailcode == "happiness")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/happiness_stream.htm";
$classname = "Happiness";
}

IF ($emailcode == "relationships")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/relationships_stream.htm";
$classname = "Body and Soul";
}

IF ($emailcode == "holidays")
{$link = "http://aish.teamgenesis.com/ssi/aish/classic_sinai/holidays_stream.htm";
$classname = "Crash Course in Jewish Holidays";
}


IF ($sendToSender == "Y"){
$to  = '$wi_recipmail' . ', '; // note the comma
$to .= '$wi_sendermail';
}
ELSE {
$to  = '$wi_recipmail'
}

// subject
$subject = 'Thought you might like this.';

// message
$message = '
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title>Audio Email</title>
</head>

<body>
<p><a href="http://www.classicsinai.com"><img src="http://www.aishaudio.com/ssi/aish/classicsinai/images/classic_sinai_476x65.gif" width="476" height="65" border="0"></a></p>
<p>Here's a link to a free audio class that I think you'll like.
    
</p>
<p>It's called $classname, and <a href="$link">here's the link</a>.</p>
<p> Enjoy!</p>$wi_sendmessage
</body>
</html>
';

// To send HTML mail, the Content-type header must be set
$headers  = 'MIME-Version: 1.0' . "\r\n";
$headers .= 'Content-type: text/html; charset=iso-8859-1' . "\r\n";

// Additional headers
$headers .= 'To: $wi_recipmail' . "\r\n";
$headers .= 'From: $wi_sendermail' . "\r\n";
$headers .= 'Cc:' . "\r\n";
$headers .= 'Bcc: ' . "\r\n";

// Mail it
mail($to, $subject, $message, $headers);

?>

<form name="redirectform" onsubmit="javascript:location.replace(this.href); event.returnValue=false;" action="http://www.classicsinai.com/index.php" METHOD ="POST">
<INPUT type=hidden value="Israel Program Participants" name=company>.

</form>
<script type="text/javascript">
<!--
// this doesn't seem to work in Firefox
document.redirectform.submit();

// -->
</script>
