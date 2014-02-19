
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="refresh" content="60" > 
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<title>Planetoid HighScore</title>

<script type="text/javascript" src="//use.typekit.net/dze4trh.js"></script>
<script type="text/javascript">try{Typekit.load();}catch(e){}</script>
<style>

body { 
	height:100%;
	width:100%;
	padding:0;
	margin:0;
	background:#000;
	color:#eee; 
	font-family:"nimbus-sans", Helvetica, sans-serif;
}

#container {
	width:100%;
	height:100%;
	min-width:1000px;
	min-height:1050px;
	position:relative;
}

#highscore {
	width:600px; margin:0 auto;
	line-height:2em;
}
#highscore thead {
	text-align:left;
	z-index:10;
	line-height:50px;
}
#header {
	margin:0 auto;
	background: center url(header.png);
	width:1000px;
	height:350px;
	z-index:11;
}
#right_red {
	position:absolute;
	z-index:-10;
	right:0;
	top:0;
	background:url(right_red.png);
	width:339px;
	height:1050px;
}
#bottom_left{
	position:absolute;
	z-index:-10;
	left:-200px;
	bottom:0;
	background:url(bottom_left.png);
	width:800px;
	height:635px;
}
</style>


</head>

<body>
<div id="container">

<div id="right_red"></div>

<div id="bottom_left"></div>
<div id="header"></div>
<?php
include("common.php");
	$link=dbConnect();

	if (isset($_POST['limit'])) {
	$limit = safe($_POST['limit']);
	} else {
	$limit = 30;
	}
	
	$query = "SELECT * FROM $dbName . `scores` ORDER by `score` DESC LIMIT $limit";

    $result = mysql_query($query);    
    $my_err = mysql_error();
    if($result === false || $my_err != '')
    {
        echo "
        <pre>
            $my_err <br />
            $query <br />
        </pre>";
        die();
    }

    $num_results = mysql_num_rows($result);
	
	


	//echo "<table border=\"1px solid #000\" borderspacing=\"0\" cellspacing=\"0\">"; 
	echo "<table id=\"highscore\">"; 
	
	
	echo ("
		<thead>
			<tr>
				<th>Name</th><th>Zeit</th><th>Münzen</th><th>Punkte</th>
			</tr>
		</thead>
	");
	
    for($i = 0; $i < $num_results; $i++)
    {
         $row = mysql_fetch_array($result);
         echo "<tr><td>" . $row['name'] . "</td><td>" . $row['time'] / 100 . "</td><td>" . $row['coins'] . "</td><td>" . $row['score'] . "</td></tr>\n";
    }
	echo "</table>";
?>
</div>
</body>
</html>