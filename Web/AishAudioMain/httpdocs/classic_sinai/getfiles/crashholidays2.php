<? 
echo $clink;

/* Havi Goodman 11/22/05
This page forces a file download or streams an audio file in the default player.
Variables:
$type: 'd' or 's'
$filepath: relative filepath from this folder
Examples: 
	http://docs.torahmedia.com/sites/aish/getfile.php?type=s&filepath=files/aishtest1.mp3
	http://docs.torahmedia.com/sites/aish/getfile.php?type=d&filepath=files/aishtest2.wma
Simply use these paths as the links in the anchor tag on a web page to trigger the download or stream.
*/

$mimetype = array( 
     'doc'=>'application/msword', 
     'htm'=>'text/html', 
     'html'=>'text/html', 
     'jpg'=>'image/jpeg', 
     'pdf'=>'application/pdf', 
     'txt'=>'text/plain', 
     'xls'=>'application/vnd.ms-excel',
     'ram'=>'audio/x-pn-realaudio',
     'rm'=>'audio/x-pn-realaudio',
     'ra'=>'audio/x-realaudio',
	'mp3'=>'audio/x-mpegurl',
	'm3u'=>'audio/x-mpegurl',
	'pls'=>'audio/x-mpegurl',
	'wma'=>'audio/x-ms-wma',
	'wmv'=>'video/x-ms-wmv',
	'asx'=>'video/x-ms-asx',
	'asf'=>'video/x-ms-asf',
	'wav'=>'audio/wav'
     ); 

if (!($type=='d' || $type=='s')) { // default to stream
	$type='s';
}
if ($filepath=='') { // error
	echo "Error: no file found.";
	exit;
}

$fullpath="http://docs.torahmedia.com/sites/aish/$filepath";
// get filename
$fnArr = explode("/",$filepath);
$last_position = count($fnArr) - 1 ; 
$filename = $fnArr[$last_position] ;
//echo "<br>filename=$filename";
// get extension
$ext_array = explode(".",$filename);
$last_position2 = count($ext_array) - 1 ; 
if ($last_position2>0) $extension = strtolower($ext_array[$last_position2]);
else $extension = '';
//echo "<br>extension=$extension";

if ($type=='d') {
	//send headers -- force download dialog 
     header("Content-type: application/octet-stream\n"); 
     header("Content-transfer-encoding: binary\n"); 
	$filesize=filesize($filepath);
     if ($filesize>0) header("Content-length: $filesize\n");
	header("Content-disposition: attachment; filename=\"$filename\"\n"); 
     header("Cache-control: private"); // another fix for IE
     //send file contents 
	//echo "fullpath=$fullpath";
	@readfile($fullpath); 
     exit;
} else { // streaming
     if ($filetype=="wma" || $filetype=="wmv") { // Windows Media File
		$fullpath.="&mswmext=.asx";
		header("Content-Type: video/x-ms-asf");
		// the $str variable must start and end as the first character of the line
$str= <<<ASX
<asx version = "3.0">
  <title>AishAudio.com Stream</title>
  <entry>
    <title>$filename</title>
    <AUTHOR></AUTHOR>
    <ref href = "$fullpath" />
  </entry>
</asx>
ASX;
          echo $str;
		
	} else { // mp3 or something else -- just open file
		if (in_array($extension, $mimetype)) { 
          	header("Content-Type: ".$mimetype["$extension"]);
               echo $fullpath;	
               exit;
		} else header("location: $fullpath\r\n");	
	}
} // end streaming


?>
