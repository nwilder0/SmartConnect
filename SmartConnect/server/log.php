<?php
	ini_set('display_errors', 1); 
	error_reporting(E_ALL);

	// add user and pwd here
	$user = "";
	$pwd = "";
	$db_name = "smart_connect";
	$host = "localhost";
	
	$dsn = "mysql:dbname=" . $db_name . ";host=" . $host;
	
	$type = "";
	if(isset($_GET["type"]) && isset($_GET["json"]))
	{
		$type = $_GET["type"];
		$json = $_GET["json"];
	}
	elseif(isset($_POST["type"])&&isset($_POST["json"]))
	{
		$type = $_POST["type"];
		$json = $_POST["json"];
	}
	
	if($type != "") 
	{
		$sql = "(";
		$values = ") VALUES (";
		if(isset($_POST["mac"]))
		{
			$value = $_POST["mac"];
			$sql = $sql . "mac,";
			$values = $values . "'" . $value . "',";
		}
		if(isset($_POST["ip"]))
		{
			$value = $_POST["ip"];
			$sql = $sql . "ip,";
			$values = $values . "'" . $value . "',";				
		}
		if(isset($_POST["os"]))
		{
			$value = $_POST["os"];
			$sql = $sql . "os,";
			$values = $values . "'" . $value . "',";				
		}
		if(isset($_POST["connected_ssid"]))
		{
			$value = $_POST["connected_ssid"];
			$sql = $sql . "connected_ssid,";
			$values = $values . "'" . $value . "',";				
		}
		if(isset($_POST["connected_ap"]))
		{
			$value = $_POST["connected_ap"];
			$sql = $sql . "connected_ap,";
			$values = $values . "'" . $value . "',";				
		}
		if(isset($_POST["connected_time"]))
		{
			$value = $_POST["connected_time"];
			$sql = $sql . "connected_time,";
			$values = $values . "'" . $value . "',";				
		}
		
		try {
			$db = new PDO($dsn,$user,$pwd);
			$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_WARNING);
		} catch (PDOException $err) {
			echo 'Connection failed: ' . $err->getMessage();
		}
		if($type=="error")
		{
			$messages = json_decode($json);
			
			$cmd = "";
			$total = 0;
			
			foreach($messages as $mesg)
			{
				$cmd = $cmd . "INSERT INTO error " . $sql . "message" . $values . "'" . cleanSQL($mesg) . "'); ";
				if($cmd != "")
				{
					try {
						$count = $db->exec($cmd);
						if(!$count) {
							$count = 0;
						}
						$total = $total + $count;
					} catch (PDOException $err) {
						echo 'Connection failed: ' . $err->getMessage();
					}
				}
			}
			print $total;

			
		}
		elseif ($type=="data")
		{
			$data = json_decode($json);
			$last_id = -1;
			$total = 0;
			
			if($values != ") VALUES (") 
			{
				$cmd = "INSERT INTO net_session " . substr($sql,0,-1) . substr($values,0,-1) . "); " ;
				try {
					$count = $db->exec($cmd);
					if(!$count) {
						$count = 0;
					}
					$last_id = $db->lastInsertId();
				} catch (PDOException $err) {
					echo 'Connection failed: ' . $err->getMessage();
				}
				
				if ($last_id>=0)
				{
					$cmd2 = "";
					$sql2 = "INSERT INTO net_data (session,ap,signal) VALUES (" . $last_id . ",";
					foreach($data as $datum)
					{
						$cmd2 = $cmd2 . $sql2 . "'" . cleanSQL($datum->ap) . "','" . cleanSQL($datum->signal) . "');";
						if($cmd2 != "")
						{
							try {
								$count2 = $db->exec($cmd2);
								if(!$count2) {
									$count2 = 0;
								}
								$total = $total + $count2;
							} catch (PDOException $err) {
								echo 'Connection failed: ' . $err->getMessage();
							}
						}
					}
				}
			}
			print $total;
		}
	}
	
	function cleanSQL($sql) {
		$sql = str_replace("\'","",$sql);
		$sql = str_replace("\"","",$sql);
		return $sql;
	}
?>