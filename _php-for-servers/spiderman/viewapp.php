<?php

//-------------------------------------------------------------------------
// Function:    GeneratePDFFromReport
// Author:      Derek Driver
// Modified:    06/03/2008
// Description: Function creates pdf file from the crystal rpt and xml file
//-------------------------------------------------------------------------
function GeneratePDFFromReport( $Report, $Xml, $OutputDir )
{
	require_once('C:\\unitedmcgill\\wwwroot\\content\\reports\\rpt2pdf.inc.php');
	require_once('C:\\unitedmcgill\\wwwroot\\content\\reports\\xmlrpc.inc.php');
	require_once('C:\\unitedmcgill\\wwwroot\\content\\reports\\codebase.inc.php');

	// Set path names to dataman where the report2pdf service is running
	$sXMLRPCServer = 'dataman.unitedmcgill.com';
	$sXMLRPCScript = 'http://' . $sXMLRPCServer . '/reports/index.php';
	$sXMLRPCReportsFolder = "d:\\umcdata\\webservers\\dataman\\htdocs\\reports\\rpt\\";
	$sXMLRPCPDFFolder = 'http://' . $sXMLRPCServer . '/reports/temp/';

	// Gets random, unique file name to hold the generated pdf
	$pdfTempFile = file_get_contents($sXMLRPCScript . '?Temp');

	// Function creates and returns the name of a file that contains the xml data
	$xmlTempFile = SendPost($sXMLRPCScript, 'XML=' . file_get_contents($Xml));

	// Array used by GenerateReportFromServer to create the pdf
	// All parameters are local to dataman (server with the report-2-pdf service running)
	$Parameters = array();
	$Parameters['rpt'] = $sXMLRPCReportsFolder . $Report;
	$Parameters['pdf'] = $pdfTempFile;
	$Parameters['readxml'] = $xmlTempFile;
		
	// function defined in rpt2pdf.inc.php that uses xml and the crystal report to make a pdf
	$success = GenerateReportFromServer($sXMLRPCServer, $Parameters, $result);

	//$myfile = fopen("c:\\out.txt", "a");
	//fwrite($myfile, $result."\n");
	//fwrite($myfile,$success."\n");
	//fclose($myfile);

	// the html addres to the temp pdf that was just created
	$generatedPDF = $sXMLRPCPDFFolder . basename(str_replace('\\', '/', $pdfTempFile));

	// Gets random, unique file name to hold the generated pdf on spiderman
	$outputFile = tempnam($OutputDir, "rpt-");

	// if the pdf generation was successful, copy the file to spiderman
	if( $success && IsPDF($generatedPDF) )
		copy($generatedPDF, $outputFile. '.pdf');
	else
		$outputFile = false;

	// deletes the temp xml and pdf files on dataman (server with the report-2-pdf service running)
	file_get_contents($sXMLRPCScript . '?RemoveTemp=' . urlencode(basename(str_replace('\\', '/', $pdfTempFile))) . '&RemoveXML=' . urlencode(basename(str_replace('\\', '/', $xmlTempFile))) );
	@unlink($Xml);
	@unlink($outputFile);

	// returns the path on spiderman to the pdf
	return $outputFile. '.pdf';
}

if(isset($_GET['XML']))
	$XMLFile = $_GET['XML'];

if($XMLFile) 
	$NewFile = GeneratePDFFromReport( "onlineapp.rpt", $XMLFile, "C:\\unitedmcgill\\wwwroot\\pdf" );

if($NewFile)
{
	print $NewFile;
}
?>