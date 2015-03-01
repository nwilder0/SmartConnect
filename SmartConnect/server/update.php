<?php
	$update = "";
	
	if(isset($_GET["file"]))
	{
		$update = $_GET["file"];
	}
	elseif(isset($_POST["file"]))
	{
		$update = $_POST["file"];
	}
	$filename = "";
	
	if($update != "")
	{
		switch($update)
		{
			case "config":
				$filename = "template.json";
				break;
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
		
		if($filename != "")
		{
			$file_json = file_get_contents($filename);
		}
		print $file_json;
	}
	
	
?>