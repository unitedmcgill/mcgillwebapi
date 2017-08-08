<?php
// File: rpt2pdf.inc.php
// Author: Angelo Mandato
// Last Updated: 02/13/2006

// Initialized variables
$RPT2PDF_PATH = "C:\\progra~2\\united~1\\xmlrpt2pdfservice\\rpt2pdf.exe"; 
// $GHOSTSCRIPT_PATH = "C:\\gs\\gs8.11\\bin\\gswin32c.exe"; // AFPL version
// Note: 7.06 works better for merging documents getother.
//$GHOSTSCRIPT_PATH = "C:\\gs\\gs7.06\\bin\\gswin32c.exe"; // GNU version
// Note: 8.13 excels at merging documents.  Previously tested 8.09 had problems that since hace been resolved.
// 8.14 has problems with multipage Pages with the corporate logos.
$GHOSTSCRIPT_PATH = "C:\\progra~2\\united~1\\ePrinter\\gs\\gs8.56\\bin\\gswin32c.exe"; // AFPL version

$ITEXTFRONT_PATH = "C:\\progra~2\\united~1\\ePrinter\\iTextFront.exe"; 
$ITEXT_PATH = str_replace("/", "\\", $_SERVER['DOCUMENT_ROOT']. "\\doccenter2\\itext.jar");

/********************************************************************/
// Method Name:    GetRpt2PdfPath()
//
// Description:    retrieves the default PRT2PDF path.
//
// Parameters: none.
//
// Returns: full path to the rpt2pdf.exe executable.
//
/********************************************************************/
function GetRpt2PdfPath()
{
  global $RPT2PDF_PATH;
  return $RPT2PDF_PATH;
}

/********************************************************************/
// Method Name:    GetiTextFrontPath()
//
// Description:    retrieves the default iTextFront path.
//
// Parameters: none.
//
// Returns: full path to the iTextFront.exe executable.
//
/********************************************************************/
function GetiTextFrontPath()
{
  global $ITEXTFRONT_PATH;
  return $ITEXTFRONT_PATH;
}

/********************************************************************/
// Method Name:    GetReportTempFile( $TempDir )
//
// Description:    Creates a temporary report file.
//
// Parameters: $Report - Path to Temporary directory.
//
// Returns: Full path to new temporary file.
//
/********************************************************************/
function GetReportTempFile( $TempDir )
{
	return tempnam( $TempDir, "rpt" );
}

/********************************************************************/
// Method Name:    PassthruReport($File, $FileName )
//
// Description:    Sends the specifed file to the browser.
//
// Parameters: $File - Path to file to send to browser.
//             $FileName - File name, excluding path.
//
// Returns: Number of characters sent or false if error.
//
/********************************************************************/
function PassthruReport( $File, $FileName = "report.pdf", $ContentType = "pdf" )
{
  // Get the content length
	$FileStats = stat( $File );
		$len = $FileStats[7];

  // Adjust time for slower networks
	$Minutes = 10 + ((integer)($len / (1024 * 1024)));
	set_time_limit($Minutes * 60);

	// Set the headers for a PDF
	header("Content-Type: application/$ContentType");
	header("Content-Disposition: filename=$FileName");
	header("Content-Transfer-Encoding: binary"); 
	header("Content-Length: $len");

  // open and send the specified file
	$fn=fopen($File, "rb"); 
	return fpassthru($fn); // Closes document when done sending
}

/********************************************************************/
// Method Name:    GetUsageFromServer( $Server )
//
// Parameters: $Server - Server where xmlrpc rpt2pdf service is running.  Typically 'localhost'.
//
// Returns: usage as a string if successful, false otherwise.
//
/********************************************************************/
function GetUsageFromServer($Server)
{
	require_once('xmlrpc.inc.php');
	list($success, $Returned) = XMLRPC_request("$Server:5678", '/generatepdf.rem', 'getUsage', array()	);
	if( $success )
		return $Returned;
	return false;
}

/********************************************************************/
// Method Name:    GenerateReportFromServer( $Server, $Parameters, &$Returned )
//
// Description:    Creates a PDF report from a crystal report.  Options below
//
// Parameters: $Server - Server where xmlrpc rpt2pdf service is running.  Typically 'localhost'.
//             $Parameters - Parameters to pass to the xmlrpc rpt2pdf service.
//             $Returned - Returned by value string returned from xmlrpc rpt2pdf service.
//
// Returns: true if successful, false otherwise.  Refer to $Returned for error message upon false
//
// $Parameters explained:  The $Parameters array is an associative array of the parameters to
//						pass to the xmlrpc rpt2pdf service.  See function below for list of the parameters.
//
/********************************************************************/
function GenerateReportFromServer( $Server, $Parameters, &$Returned, $Port = 5678 )
{
	require_once('xmlrpc.inc.php');
	$DefaultParameters = array(
		'rpt'=>'', // Valid path to rpt file
		'pdf'=>'', // Destination where to write PDF file
		'db'=>'', // Database name
		'odbc'=>'', // ODBC Datasource for report data
		'password'=>'', // ODBC datasource password
		'user'=>'', // ODBC datasource username
		'sql'=>'', // ODBC/DB SQL query
		'dataset'=>'', // Dataset name
		'readxml'=>'', // Source XML for report data
		'writexml'=>'', // Destination XML, of report data
		'nosign'=>'',
		'useprinter'=>'' ); // Nosign, currently not implemented.
		
	$Parameters = rpt2pdf_array_intersect_key($Parameters, $DefaultParameters); // Make sure only valid parameters are passed here
	$Parameters = array_merge($DefaultParameters, $Parameters); // Makes sure only the paramters specified are passed
		
	$ReturnedArray = XMLRPC_request("$Server:$Port", '/generatepdf.rem', 'getPDF', 
				array(
					XMLRPC_prepare($Parameters['rpt']), // strRPT
					XMLRPC_prepare($Parameters['pdf']), // strPDF
					XMLRPC_prepare($Parameters['db']), // strDB
					XMLRPC_prepare($Parameters['odbc']), // strODBC
					XMLRPC_prepare($Parameters['password']), // strPassword
					XMLRPC_prepare($Parameters['user']), // strUser
					XMLRPC_prepare($Parameters['sql']), // strSQL
					XMLRPC_prepare($Parameters['dataset']), // strDataSet
					XMLRPC_prepare($Parameters['writexml']), // strWriteXML
					XMLRPC_prepare($Parameters['readxml']), // strReadXML
					XMLRPC_prepare($Parameters['nosign']), // strNoSign
					XMLRPC_prepare($Parameters['useprinter']) // strUsePrinter
			)	);
	
	list($success, $value) = $ReturnedArray;
	$Returned = $value;
	return $success;
}

/********************************************************************/
// Method Name:    IsPDF( $File )
//
// Description:    Verifies if the specified file is a PDF file
//
// Parameters: $File - Path to PDF file to check.
//
// Returns: true if successful, false otherwise.
//
/********************************************************************/
function IsPDF($File)
{
	$IsPDF = false;
	$fp = @fopen( $File, 'rb' );
	if( fread($fp, 4) == '%PDF' )
		$IsPDF = true;
	fclose($fp);
	return $IsPDF;
}

/********************************************************************/
// Method Name:    MergeReports($Destination, $Append, $bUseGhostscript = true )
//
// Description:    Merges 1 or more PDF documents together.
//
// Parameters:	$Destination - Path to destination PDF file.
//             			$Append - Text to use for watermark. Cannot contain double quotes
//								$bUseGhostscript - If true, will use ghostscript to merge pdfs, otherwise use iText
//
// Returns: Array of comamnd line output.
//
// NOTE: Both Ghostscript and iText are used depending on which works better.
//
/********************************************************************/
function MergeReports( $Destination, $Append, $bUseGhostscript = true )
{
	$Server = "localhost:82";
	if( function_exists('IsBeta') && IsBeta() )
		$Server = "localhost:80";
	if( function_exists('IsAlpha') && IsAlpha() )
		$Server = "localhost:80";

	//MergePDFsFromServer( $Server, $Destination, $Append, $bUseGhostscript, $Result );
	//return $Result;

	//$bUseGhostscript = true;
  global $GHOSTSCRIPT_PATH, $ITEXT_PATH;

  if( !is_array($Append) )
    $Append[0] = $Append;

	if( $bUseGhostscript )
		$cmd = sprintf( "%s -dCompatibilityLevel#1.4 -dSAFER -dNOPAUSE -dBATCH -sDEVICE#pdfwrite -sOutputFile#\"%s\" -q -c .setpdfwrite -f 2>NUL",
			$GHOSTSCRIPT_PATH, $Destination );
	else
		$cmd = "java -cp \"$ITEXT_PATH\" com.lowagie.tools.concat_pdf ";

  while( list(,$value) = each($Append) )
    $cmd .= sprintf(" \"%s\"", $value );

	if( $bUseGhostscript == false )
		$cmd .= " \"$Destination\"";

	$ToReturn = array();
  @exec($cmd, $ToReturn );
	return $ToReturn;
}


function MergePDFsFromServer($Server, $Destination, $Documents, $UseGhostscript = true, &$Returned = '')
{
	require_once('xmlrpc.inc.php');
	$method = 'MergePDF.iText';
	if( $UseGhostscript )
		$method = 'MergePDF.Ghostscript';
	
	$Params = array();
	$Params[] = XMLRPC_prepare($Destination);
	while( list(,$path) = each($Documents) )
		$Params[] = XMLRPC_prepare($path);
		
	list($success, $value) = XMLRPC_request($Server, '/pdfmerge/', $method, $Params);
	
	$Returned = $value;
	return $success;
}

/********************************************************************/
// Method Name:    PDFAddWatermark($File, $Text, $Diagonal = true )
//
// Description:    Sends the specifed file to the browser.
//
// Parameters:	$File - Path to PDF file.
//             			$Text - Text to use for watermark. Cannot contain double quotes
//								$Diagonal - Display text diagonally.  Horizontal otherwise
//
// Returns: true if successful, false if error.
//
/********************************************************************/
function PDFAddWatermark($File, $Text, $Diagonal = true )
{
	$Text = str_replace("\"", "'", $Text); // Remove double quotes if exists

	$TempFrom = dirname($File) . "\\" . md5( uniqid( (rand()))) . ".pdf";
	while( is_file($TempFrom) )
		$TempFrom = dirname($File) . "\\" . md5( uniqid( (rand()))) . ".pdf";
		
	$TempTo = dirname($File) . "\\" . md5( uniqid( (rand()))) . ".pdf";
	while( is_file($TempTo) || $TempTo == $TempFrom )
		$TempTo = dirname($File) . "\\" . md5( uniqid( (rand()))) . ".pdf";
	
	rename( $File, $TempFrom );
	
	$cmd = GetiTextFrontPath();
	$cmd .= " /watermark-text \"$Text\" ";
	if( $Diagonal )
		$cmd .= "diagonal";
	else
		$cmd .= "horizontal";
	$cmd .= " \"Helvetica\" \"lightgray\" /merge \"$TempFrom\" \"$TempTo\" /silent";
	
	@exec($cmd);

	if( file_exists($TempTo) )
	{
		rename($TempTo, $File);
		unlink($TempFrom);
		return true;
	}
	
	rename($TempFrom, $File);
	return false;
}

/********************************************************************/
// Method Name:    rpt2pdf_array_intersect_key($isec, $arr2)
//
// Description:    Returns an array where keys are found in both arrays.
//
// Parameters: $isec - Array to intersect with.
//             $arr2 - Array to check.
//
// Returns: array of associative values from $isec where the keys are in both passed arrays
//
/********************************************************************/
function rpt2pdf_array_intersect_key($isec, $arr2)
{
	if( function_exists('array_intersect_key') )
		return array_intersect_key($isec,$arr2);
	
	$argc = func_num_args();

	for ($i = 1; !empty($isec) && $i < $argc; $i++)
	{
		$arr = func_get_arg($i);
		foreach ($isec as $k =>& $v)
			if (!isset($arr[$k]))
				unset($isec[$k]);
	}
	
	return $isec;
}

?>