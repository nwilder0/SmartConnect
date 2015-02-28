<?php
	$update = $_GET["file"];
	
	$filename = "template.json";
	
	switch($update)
	{
		case "ap":
			$filename = "APs.json";
			break;
		case "ssid":
			$filename = "SSIDs.json";
			break;
		case "link":
			$filename = "links.json";
			break;
	}
	
	$file_json = file_get_contents($filename);
	
	print $file_json;
	
	
?>