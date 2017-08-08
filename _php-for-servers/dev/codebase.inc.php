<?php
	function DisableMagicQuotes()
	{
		if (get_magic_quotes_gpc() || ini_get('magic_quotes_sybase'))
		{
			$_GET = StripSlashesRecursive($_GET);
			$_POST = StripSlashesRecursive($_POST);
			$_COOKIE = StripSlashesRecursive($_COOKIE);
			$_FILES = StripSlashesRecursive($_FILES);
			$_ENV = StripSlashesRecursive($_ENV);
			$_SERVER = StripSlashesRecursive($_SERVER);

			$_REQUEST = array_merge($_GET, $_POST, $_COOKIE);
		}

		set_magic_quotes_runtime(0);

		return true;
	}

	function StripSlashesRecursive($aData)
	{
		return is_array($aData) ? array_map('StripSlashesRecursive', $aData) : stripslashes($aData);
	}

	function RegisterGlobals()
	{
		foreach ($_COOKIE as $sKey => $objValue)
		{
			global ${$sKey};
			${$sKey} = $objValue;
		}

		foreach ($_GET as $sKey => $objValue)
		{
			global ${$sKey};
			${$sKey} = $objValue;
		}

		foreach ($_POST as $sKey => $objValue)
		{
			global ${$sKey};
			${$sKey} = $objValue;
		}

		return true;
	}

	function GetRequestedURI()
	{
		return preg_replace('/[#?].*$/', '', $_SERVER['REQUEST_URI']);
	}

	function GetFormValue($sFormName, $sKey)
	{
		if (!(isset($_SESSION['Form'])
		&& isset($_SESSION['Form'][$sFormName])
		&& isset($_SESSION['Form'][$sFormName][$sKey])))
		{
			return false;
		}

		return $_SESSION['Form'][$sFormName][$sKey];
	}

	function SetFormValue($sFormName, $sKey, $sValue)
	{
		if (!(isset($_SESSION['Form'])
		&& isset($_SESSION['Form'][$sFormName])))
		{
			return false;
		}

		$_SESSION['Form'][$sFormName][$sKey] = $sValue;
	}

	function UnsetFormValue($sFormName, $sKey)
	{
		if (!(isset($_SESSION['Form'])
		&& isset($_SESSION['Form'][$sFormName])
		&& isset($_SESSION['Form'][$sFormName][$sKey])))
		{
			return false;
		}

		unset($_SESSION['Form'][$sFormName][$sKey]);

		return true;
	}

	function InitSessionForm($sFormName, $aInitData = array())
	{
		if (!isset($_SESSION['Form']))
		{
			$_SESSION['Form'] = array();
		}

		if (!isset($_SESSION['Form'][$sFormName]))
		{
			$_SESSION['Form'][$sFormName] = array();
		}

		while (list($sKey, $sValue) = each($aInitData))
		{
			if (!isset($_SESSION['Form'][$sFormName][$sKey]))
			{
				$_SESSION['Form'][$sFormName][$sKey] = $sValue;
			}
		}

		reset($_POST);
		while (list($sKey, $sValue) = each($_POST))
		{
			$_SESSION['Form'][$sFormName][$sKey] = $sValue;
		}

		return true;
	}

	function CleanSessionForm($aAllowedForms = false)
	{
		if (!isset($_SESSION['Form']))
		{
			$_SESSION['Form'] = array();
		}

		if (!is_array($aAllowedForms))
		{
			$aAllowedForms = array();
		}

		$aRemoveForms = array_values(array_diff(array_keys($_SESSION['Form']), $aAllowedForms));
		for ($ctr = 0; $ctr < sizeof($aRemoveForms); ++$ctr)
		{
			if (isset($_SESSION['Form'][$aRemoveForms[$ctr]]))
			{
				unset($_SESSION['Form'][$aRemoveForms[$ctr]]);
			}
		}

		return true;
	}

	function StreamText($sText, $sFile, $sContentType = 'application/pdf')
	{
		$sFile = str_replace(' ', '_', $sFile);

		header('Pragma: ');
		header('Expires: Fri, 19 Apr 1996 11:00:00 GMT');
		header('Cache-Control: public');
		header('Content-Description: File Transfer');
		header('Content-Type: ' . $sContentType);
		header('Content-Disposition: filename=' . basename($sFile) . ';');
		header('Content-Transfer-Encoding: binary');
		header('Content-Length: ' . strlen($sText));

		print $sText;

		return true;
	}

	function StreamFile($sAbsFileName, $sContentType = 'application/pdf')
	{
		if (!file_exists($sAbsFileName))
		{
			return false;
		}

		$nSize = filesize($sAbsFileName);
		if ($objStream = fopen($sAbsFileName, 'rb'))
		{
			fclose($objStream);
			StreamText(file_get_contents($sAbsFileName), $sAbsFileName, $sContentType);
		}

		return true;
	}

	function SelectDirectory($sDirectory, $sRegExp = false)
	{
		$aFiles = array();
		if ($objHandle = opendir($sDirectory))
		{
			while (($sFile = readdir($objHandle)) !== false)
			{
				if (($sFile != '.') && ($sFile != '..') && (($sRegExp === false) || preg_match($sRegExp, $sFile)))
				{
					$aFiles[] = trim($sFile);
				}
			}
			closedir($objHandle);
		}

		return $aFiles;
	}

	function IsValidCCNumber($sCreditCardType, &$nCreditCard)
	{
		$sCreditCardType = preg_replace('[\s]', '', strtolower($sCreditCardType));
		$nCreditCard = preg_replace('([^\d]|\s)', '', $nCreditCard);

		switch ($sCreditCardType)
		{
			case 'mastercard':
				return preg_match('/^5[1-5]\d{14}$/', $nCreditCard) && LuhnTest($nCreditCard);
			case 'visa':
				return preg_match('/^4((\d{12})|(\d{15}))$/', $nCreditCard) && LuhnTest($nCreditCard);
			case 'americanexpress':
				return preg_match('/^3[47]\d{13}$/', $nCreditCard) && LuhnTest($nCreditCard);
			case 'discover':
				return preg_match('/^6011\d{12}$/', $nCreditCard) && LuhnTest($nCreditCard);
		}

		return false;
	}

	function LuhnTest($nCreditCard)
	{
		$nCheckSum = 0;
		$nRevCreditCard = strrev($nCreditCard);
		for ($ctr = 0; $ctr < strlen($nRevCreditCard); ++$ctr)
		{
			$nLuhnNum = $nRevCreditCard[$ctr] * (($ctr % 2) + 1);
			$nCheckSum += $nLuhnNum < 10 ? $nLuhnNum : $nLuhnNum - 9;
		}

		return !($nCheckSum % 10);
	}

	function GetDebug()
	{
		$aParameters = func_get_args();

		$sDebug  = '<pre>' . "\n";
		if (empty($aParameters))
		{
			$sDebug .= 'ENV' . "\n" . print_r($_ENV, true) . "\n";
			$sDebug .= 'GET' . "\n" . print_r($_GET, true) . "\n";
			$sDebug .= 'POST' . "\n" . print_r($_POST, true) . "\n";
			$sDebug .= 'COOKIE' . "\n" . print_r($_COOKIE, true) . "\n";
			$sDebug .= 'SESSION' . "\n" . print_r($_SESSION, true) . "\n";
			$sDebug .= 'FILES' . "\n" . print_r($_FILES, true) . "\n";
		}

		foreach ($aParameters as $sKey => $objParam)
		{
			$sDebug .= 'Parameter ' . $sKey . "\n";
			$sDebug .= is_scalar($objParam) ? $objParam . "\n\n" : print_r($objParam, true) . "\n\n";
		}

		$sDebug .= '</pre>' . "\n";

		return $sDebug;
	}

	function AddToFile($sFileName, $sText)
	{
		return WriteToFile($sFileName, $sText, true);
	}

	function WriteToFile($sFileName, $sText, $bAppend = false)
	{
		$bReturn = false;
		$sLockFile = defined('LOCKFOLDER') ? LOCKFOLDER . md5($sFileName) . '.lock' : false;

		$nTimeCount = 0;
		while ($sLockFile && !@mkdir($sLockFile))
		{
			usleep(40);
			if ($nTimeCount++ > 50) return $bReturn;
		}

		$sMode = ($bAppend) ? 'ab' : 'wb';
		if ($objFile = @fopen($sFileName, $sMode))
		{
			fwrite($objFile, $sText, strlen($sText));
			fclose($objFile);
			$bReturn = true;
		}

		if ($sLockFile) @rmdir($sLockFile);

		return $bReturn;
	}

	function ReadFromFile($sFileName)
	{
		$sLockFile = defined('LOCKFOLDER') ? LOCKFOLDER . md5($sFileName) . '.lock' : false;

		$nTimeCount = 0;
		while ($sLockFile && file_exists($sLockFile))
		{
			usleep(40);
			if ($nTimeCount++ > 50) return false;
		}

		if (!is_readable($sFileName)) return false;

		return file_get_contents($sFileName);
	}

	function SubstituteQueryString($aQueryString, $aSubstitutes, $bHTML = true)
	{
		if (is_string($aQueryString)) parse_str($aQueryString, $aQueryString);
		if (is_string($aSubstitutes)) parse_str($aSubstitutes, $aSubstitutes);

		return ArrayToQueryString(array_merge($aQueryString, $aSubstitutes), $bHTML);
	}

	function ArrayToQueryString($aQuery, $bHTML = true)
	{
		if (function_exists('http_build_query'))
		{
			return $bHTML ? http_build_query($aQuery, '', '&amp;') : http_build_query($aQuery);
		}

		$sQueryString = '';
		foreach ($aQuery as $sKey => $sValue)
		{
			$sQueryString .= $sKey . '=' . $sValue . '&';
			if ($bHTML) $sQueryString .= 'amp;';
		}

		return $bHTML ? substr($sQueryString, 0, -5) : substr($sQueryString, 0, -1);
	}

	function URLEncodeArray($aToEncode)
	{
		$sURLEncoded = '';
		foreach ($aToEncode as $sKey => $sValue)
		{
			$sURLEncoded .= $sKey . '=' . urlencode($sValue) . '&';
		}

		return substr($sURLEncoded, 0, -1);
	}

	function LinkifyText($sText, $aAttributes = array('title' => 'Linkified Text', 'rel' => 'nofollow'), $aProtocols = array('http:\/\/', 'https:\/\/', 'ftp:\/\/', 'news:\/\/', 'nntp:\/\/', 'telnet:\/\/', 'irc:\/\/', 'mms:\/\/', 'ed2k:\/\/', 'xmpp:', 'mailto:'), $aSubdomains = array('www'=>'http://', 'ftp'=>'ftp://', 'irc'=>'irc://', 'jabber'=>'xmpp:'))
	{
		$sRELinks = '/(?:(' . implode('|', $aProtocols) . ')[^\^\[\]{}|\\"\'<>`\s]*[^!@\^()\[\]{}|\\:;"\',.?<>`\s])|(?:(?:(?:(?:[^@:<>(){}`\'"\/\[\]\s]+:)?[^@:<>(){}`\'"\/\[\]\s]+@)?(' . implode('|', array_keys($aSubdomains)) . ')\.(?:[^`~!@#$%^&*()_=+\[{\]}\\|;:\'",<.>\/?\s]+\.)+[a-z]{2,6}(?:[\/#?](?:[^\^\[\]{}|\\"\'<>`\s]*[^!@\^()\[\]{}|\\:;"\',.?<>`\s])?)?)|(?:(?:[^@:<>(){}`\'"\/\[\]\s]+@)?((?:(?:(?:(?:[0-1]?[0-9]?[0-9])|(?:2[0-4][0-9])|(?:25[0-5]))(?:\.(?:(?:[0-1]?[0-9]?[0-9])|(?:2[0-4][0-9])|(?:25[0-5]))){3})|(?:[A-Fa-f0-9:]{16,39}))|(?:(?:[^`~!@#$%^&*()_=+\[{\]}\\|;:\'",<.>\/?\s]+\.)+[a-z]{2,6}))\/(?:[^\^\[\]{}|\\"\'<>`\s]*[^!@\^()\[\]{}|\\:;"\',.?<>`\s](?:[#?](?:[^\^\[\]{}|\\"\'<>`\s]*[^!@\^()\[\]{}|\\:;"\',.?<>`\s])?)?)?)|(?:[^@:<>(){}`\'"\/\[\]\s]+:[^@:<>(){}`\'"\/\[\]\s]+@((?:(?:(?:(?:[0-1]?[0-9]?[0-9])|(?:2[0-4][0-9])|(?:25[0-5]))(?:\.(?:(?:[0-1]?[0-9]?[0-9])|(?:2[0-4][0-9])|(?:25[0-5]))){3})|(?:[A-Fa-f0-9:]{16,39}))|(?:(?:[^`~!@#$%^&*()_=+\[{\]}\\|;:\'",<.>\/?\s]+\.)+[a-z]{2,6}))(?:\/(?:(?:[^\^\[\]{}|\\"\'<>`\s]*[^!@\^()\[\]{}|\\:;"\',.?<>`\s])?)?)?(?:[#?](?:[^\^\[\]{}|\\"\'<>`\s]*[^!@\^()\[\]{}|\\:;"\',.?<>`\s])?)?))|([^@:<>(){}`\'"\/\[\]\s]+@(?:(?:(?:[^`~!@#$%^&*()_=+\[{\]}\\|;:\'",<.>\/?\s]+\.)+[a-z]{2,6})|(?:(?:(?:(?:(?:[0-1]?[0-9]?[0-9])|(?:2[0-4][0-9])|(?:25[0-5]))(?:\.(?:(?:[0-1]?[0-9]?[0-9])|(?:2[0-4][0-9])|(?:25[0-5]))){3})|(?:[A-Fa-f0-9:]{16,39}))))(?:[^\^*\[\]{}|\\"<>\/`\s]+[^!@\^()\[\]{}|\\:;"\',.?<>`\s])?)/i';

		$sAttributes = '';
		while (list($sKey, $sValue) = each($aAttributes))
		{
			$sAttributes .= ' ' . $sKey . '="' . $sValue . '"';
		}

		$sNewText = '';
		while (preg_match($sRELinks, $sText, $aMatches))
		{
			$nMatchType = sizeof($aMatches) - 1;
			$sMatchText = $aMatches[$nMatchType];

			$sNewText .= substr($sText, 0, strpos($sText, $aMatches[0]));
			$sText = substr($sText, strpos($sText, $aMatches[0]) + strlen($aMatches[0]));

			if ($nMatchType == 1)
			{
				$sNewText .= '<a href="' . $aMatches[0] . '"' . $sAttributes . '>' . $aMatches[0] . '</a>';
			}
			elseif ($nMatchType == 2)
			{
				$sNewText .= '<a href="' . $aSubdomains[$sMatchText] . $aMatches[0] . '"' . $sAttributes . '>' . $aMatches[0] . '</a>';
			}
			elseif (($nMatchType == 3) || ($nMatchType == 4))
			{
				$sNewText .= '<a href="http://' . $aMatches[0] . '"' . $sAttributes . '>' . $aMatches[0] . '</a>';
			}
			else
			{
				$sNewText .= '<a href="mailto:' . $aMatches[0] . '"' . $sAttributes . '>' . $aMatches[0] . '</a>';
			}
		}

		return $sNewText . $sText;
	}

	function RegenerateSession($bDeleteOld = true)
	{
		if (!$bDeleteOld)
		{
			session_regenerate_id();
			return true;
		}

		if (version_compare('5.1.0', phpversion()) <= 0)
		{
			session_regenerate_id(true);
			return true;
		}

		$nOldID = session_id();
		session_regenerate_id();
		$nNewID = session_id();

		session_id($nOldID);
		session_destroy();

		$aOldSession = $_SESSION;
		session_id($nNewID);
		session_start();
		$_SESSION = $aOldSession;

		return true;
	}

	function UnicodeChar($nDecimal)
	{
		if ($nDecimal < 128)
		{
			return chr($nDecimal);
		}
		else if ($nDecimal < 2048)
		{
			return chr(192 + (($nDecimal - ($nDecimal % 64)) / 64)) . chr(128 + ($nDecimal % 64));
		}

		return chr(224 + (($nDecimal - ($nDecimal % 4096)) / 4096)) . chr(128 + ((($nDecimal % 4096) - ($nDecimal % 64)) / 64)) . chr(128 + ($nDecimal % 64));
	}

	function EncryptString($sKey, $sString)
	{
		$nIVSize = mcrypt_get_iv_size(MCRYPT_XTEA, MCRYPT_MODE_ECB);
		$nIV = mcrypt_create_iv($nIVSize, MCRYPT_RAND);

		return bin2hex(mcrypt_encrypt(MCRYPT_XTEA, $sKey, $sString, MCRYPT_MODE_ECB, $nIV));
	}

	function DecryptString($sKey, $sString)
	{
		$nIVSize = mcrypt_get_iv_size(MCRYPT_XTEA, MCRYPT_MODE_ECB);
		$nIV = mcrypt_create_iv($nIVSize, MCRYPT_RAND);

		return rtrim(mcrypt_decrypt(MCRYPT_XTEA, $sKey, pack('H*', $sString), MCRYPT_MODE_ECB, $nIV), "\0");
	}

	function IsSpamClient()
	{
		$sIPAddress = $_SERVER['REMOTE_ADDR'];
		$sIPReverse = implode('.', array_reverse(explode('.', $sIPAddress)));

		$sLookup = $sIPReverse . '.l1.spews.dnsbl.sorbs.net.';
		if ($sLookup != gethostbyname($sLookup)) return 'http://www.spews.org/ask.cgi?x=' . $sIPAddress;

		$sLookup = $sIPReverse . '.sbl-xbl.spamhaus.org.';
		if ($sLookup != gethostbyname($sLookup)) return 'http://www.spamhaus.org/query/bl?ip=' . $sIPAddress;

		$sLookup = $sIPReverse . '.list.dsbl.org.';
		if ($sLookup != gethostbyname($sLookup)) return 'http://dsbl.org/listing?' . $sIPAddress;

		return false;
	}

	function IsSpamText($sComment, $bIsURL = false)
	{
		$sREURL = '/@?([a-z0-9\-]+(\.[a-z0-9\-]+)+)/i';

		$aMatches = array();
		preg_match_all($sREURL, $sComment, $aMatches);
		$aMatches = $aMatches[1];

		for ($nMatch = 0; $nMatch < sizeof($aMatches); ++$nMatch)
		{
			$sHost = str_replace('www.', '', $aMatches[$nMatch]);
			if (strlen($sHost) < 4) continue;

			$sHost .= '.multi.surbl.org.';
			if (gethostbyname($sHost) != $sHost) return 'http://' . $aMatches[$nMatch];
			if ($bIsURL) return false;
		}

		return false;
	}

	function SendPost($sURL, $aPost, $aHeaders = array())
	{
		$bContent = false;
		$aHeaderList = array();
		foreach ($aHeaders as $sName => $sValue)
		{
			if (strtolower($sName) == 'content-type') $bContent = true;
			$aHeaderList[] = $sName . ': ' . $sValue;
		}
		if (!$bContent) $aHeaderList[] .= 'Content-Type: application/x-www-form-urlencoded';

		$sPostEncoded = is_array($aPost) ? URLEncodeArray($aPost) : $aPost;

		if (function_exists('curl_init'))
		{
			$objCURL = curl_init();
			curl_setopt($objCURL, CURLOPT_URL, $sURL);
			curl_setopt($objCURL, CURLOPT_HEADER, false);
			curl_setopt($objCURL, CURLOPT_RETURNTRANSFER, true);
			curl_setopt($objCURL, CURLOPT_POST, true);
			curl_setopt($objCURL, CURLOPT_USERAGENT, 'Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; (R1 1.5); .NET CLR 1.1.4322)');
			curl_setopt($objCURL, CURLOPT_HTTPHEADER, $aHeaderList);
			curl_setopt($objCURL, CURLOPT_POSTFIELDS, $sPostEncoded);

			return curl_exec($objCURL);
		}

		$objURL = parse_url($sURL);
		if (!empty($objURL['query'])) $objURL['query'] = '?' . $objURL['query'];

		$sHeader  = 'POST ' . $objURL['path'] . $objURL['query'] . ' HTTP/1.1' . "\n";
		$sHeader .= 'Host: ' . $objURL['host'] . "\n";
		$sHeader .= 'User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; (R1 1.5); .NET CLR 1.1.4322)' . "\n";
		$sHeader .= 'Content-Length: ' . strlen($sPostEncoded) . "\n";
		$sHeader .= implode("\n", $aHeaderList) . "\n";

		$sResult = false;
		if ($objSocket = fsockopen($sHost, 80))
		{
			fputs($objSocket, $sHeader . $sPostEncoded);
			$sResult = '';
			while (!feof($objSocket))
			{
				$sResult .= fgets($objSocket, 1024);
			}
			fclose($objSocket);
		}

		return $sResult;
	}

	function SortDataset($aArray, $sField, $bDescending = false)
	{
		$aKeys = array_keys($aArray);
		$bSequential = ($aKeys === array_flip($aKeys));
		$nSize = sizeof($aArray);

		for ($nIndex = 0; $nIndex < $nSize - 1; $nIndex++)
		{
			$nMinIndex = $nIndex;
			$objMinValue = $aArray[$aKeys[$nIndex]][$sField];
			$sKey = $aKeys[$nIndex];

			for ($nSortIndex = $nIndex + 1; $nSortIndex < $nSize; ++$nSortIndex)
			{
				if ($aArray[$aKeys[$nSortIndex]][$sField] < $objMinValue)
				{
					$nMinIndex = $nSortIndex;
					$sKey = $aKeys[$nSortIndex];
					$objMinValue = $aArray[$aKeys[$nSortIndex]][$sField];
				}
			}

			$aKeys[$nMinIndex] = $aKeys[$nIndex];
			$aKeys[$nIndex] = $sKey;
		}

		$aReturn = array();
		for ($nSortIndex = 0; $nSortIndex < $nSize; ++$nSortIndex)
		{
			$nIndex = $bDescending ? $nSize - $nSortIndex - 1: $nSortIndex;
			$aReturn[$aKeys[$nIndex]] = $aArray[$aKeys[$nIndex]];
		}

		return $bSequential ? array_values($aReturn) : $aReturn;
	}
?>