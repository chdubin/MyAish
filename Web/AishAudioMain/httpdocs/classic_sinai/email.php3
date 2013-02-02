<?php 
if (($email == "") OR ($email == "your email address")){
	$error1 = "<font size='3' class='s4' color='red' face='Arial, Helvetica'><STRONG>No email address was entered.  Please click the BACK tab on your browser to try again.</STRONG></font><br>";
	echo $error1;}	
else {
	mail("cdubin@aish.com", "Sign me up for Classic Sinai email!","$email");
        mail("voices4@aish.com", "Sign me up for Classic Sinai email!","$email");

}

?>
<form name="redirectform" onsubmit="javascript:location.replace(this.href); event.returnValue=false;" action="http://www.classicsinai.com/index.php" METHOD ="POST">
<INPUT type=hidden value="Israel Program Participants" name=company>.

</form>
<script type="text/javascript">
// this doesn't seem to work in Firefox
document.redirectform.submit();
</script>


