<script language="JavaScript">
<!--
  javascript:window.history.forward(1);
//-->
</script>

<?php

echo "to";
echo $to;
echo "email";
echo $email;


// subject
$subject = 'Add to Classic Sinai List.';

// message


// To send HTML mail, the Content-type header must be set

// Additional headers



// Mail it
mail($to, $subject, $message, $headers);
echo "mailed"
?>
<form name="redirectform" onsubmit="javascript:location.replace(this.href); event.returnValue=false;" action="http://www.classicsinai.com/index.php" METHOD ="POST">
<INPUT type=hidden value="Israel Program Participants" name=company>.

</form>
<script type="text/javascript">
// this doesn't seem to work in Firefox
document.redirectform.submit();
</script>


