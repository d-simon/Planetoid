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
	
	 echo "Rank" . "\t - \t " .  str_pad("Name", 25, " ") . "\t - \t " . str_pad("Time", "9", " ", STR_PAD_LEFT) . "\t -\t " . "Coins" . "\t - \t " . str_pad("Score",10, " ") . "\n";
    for($i = 0; $i < $num_results; $i++)
    {
         $row = mysql_fetch_array($result);
         echo str_pad($i + 1 . ".)", 3, " ") . "\t - \t " .  str_pad($row['name'], 25, " ") . "\t - \t " . str_pad(str_pad($row['time'] / 100, 5, "0", STR_PAD_LEFT), "10", " ", STR_PAD_LEFT) . "s\t - \t " . str_pad($row['coins'], 3, " ") . "\t - \t " . str_pad($row['score'],10, " ") . "\n";
    }
?>